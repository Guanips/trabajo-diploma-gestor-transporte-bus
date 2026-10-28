using Microsoft.Data.SqlClient;
using System.ComponentModel;
using System.Diagnostics;
using System.Security.Principal;
using System.ServiceProcess;

namespace UI
{
    internal enum ResultadoServicio
    {
        Ok,
        NoInstalado,
        RequiereAdministrador,
        SinConexion,
        Error
    }

    /// <summary>
    /// Verifica y levanta el servicio de Windows de la instancia de SQL Server a la que apunta la cadena de conexion.
    /// Si el servidor es LocalDB o remoto no hay servicio que administrar (ObtenerNombreServicio devuelve null).
    /// </summary>
    internal static class SqlServerServiceManager
    {
        private const int ErrorAccessDenied = 5;
        private const int ErrorCancelledByUser = 1223;

        /// <summary>Acepta ".", "(local)", "localhost" o el nombre del equipo, con o sin "\instancia". Null si no es un servicio local.</summary>
        public static string? ObtenerNombreServicio(string connectionString)
        {
            string origen = new SqlConnectionStringBuilder(connectionString).DataSource.Trim();
            if (origen.StartsWith("(localdb)", StringComparison.OrdinalIgnoreCase)) return null;

            if (origen.StartsWith("np:", StringComparison.OrdinalIgnoreCase) || origen.StartsWith("tcp:", StringComparison.OrdinalIgnoreCase))
                origen = origen[(origen.IndexOf(':') + 1)..];
            origen = origen.Split(',')[0];

            string[] partes = origen.Split('\\', 2);
            if (!EsEquipoLocal(partes[0].Trim())) return null;

            string instancia = partes.Length > 1 ? partes[1].Trim() : "MSSQLSERVER";
            return instancia.Equals("MSSQLSERVER", StringComparison.OrdinalIgnoreCase) ? "MSSQLSERVER" : "MSSQL$" + instancia;
        }

        public static bool EstaEnEjecucion(string servicio)
        {
            try
            {
                using ServiceController sc = new(servicio);
                return sc.Status == ServiceControllerStatus.Running;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }

        /// <summary>
        /// Deja el servicio en ejecucion y espera a que acepte conexiones.
        /// <paramref name="elevado"/>: el proceso es administrador y puede rehabilitar un servicio deshabilitado.
        /// </summary>
        public static ResultadoServicio Asegurar(string servicio, string connectionString, bool elevado, Action<string> log)
        {
            try
            {
                using ServiceController sc = new(servicio);
                try
                {
                    _ = sc.Status;
                }
                catch (InvalidOperationException)
                {
                    log($"El servicio {servicio} no existe.");
                    return ResultadoServicio.NoInstalado;
                }

                if (sc.Status != ServiceControllerStatus.Running)
                {
                    if (sc.StartType == ServiceStartMode.Disabled)
                    {
                        if (!elevado) return ResultadoServicio.RequiereAdministrador;
                        log($"El servicio {servicio} esta deshabilitado: se pasa a inicio automatico.");
                        if (!ConfigurarInicioAutomatico(servicio)) return ResultadoServicio.Error;
                        sc.Refresh();
                    }

                    if (sc.Status == ServiceControllerStatus.StopPending)
                        sc.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(60));

                    if (sc.Status == ServiceControllerStatus.Stopped)
                    {
                        log($"Iniciando el servicio {servicio}...");
                        try
                        {
                            sc.Start();
                        }
                        catch (InvalidOperationException ex) when (ex.InnerException is Win32Exception { NativeErrorCode: ErrorAccessDenied })
                        {
                            return ResultadoServicio.RequiereAdministrador;
                        }
                    }

                    sc.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(90));
                }

                return EsperarConexion(connectionString, TimeSpan.FromSeconds(60))
                    ? ResultadoServicio.Ok
                    : ResultadoServicio.SinConexion;
            }
            catch (Exception ex)
            {
                log($"ERROR al iniciar el servicio {servicio}: {ex}");
                return ResultadoServicio.Error;
            }
        }

        /// <summary>
        /// Que el servicio figure "Running" no implica que ya acepte conexiones (puede estar recuperando las bases).
        /// Un login rechazado (18456) tambien prueba que el servidor responde: eso lo resuelve la inicializacion.
        /// </summary>
        public static bool EsperarConexion(string connectionString, TimeSpan timeout)
        {
            var builder = new SqlConnectionStringBuilder(connectionString)
            {
                InitialCatalog = "master",
                Pooling = false,
                ConnectTimeout = 5
            };

            DateTime limite = DateTime.UtcNow + timeout;
            while (true)
            {
                try
                {
                    using SqlConnection connection = new(builder.ConnectionString);
                    connection.Open();
                    return true;
                }
                catch (SqlException ex) when (ex.Number == 18456)
                {
                    return true;
                }
                catch (Exception)
                {
                    if (DateTime.UtcNow >= limite) return false;
                    Thread.Sleep(1000);
                }
            }
        }

        /// <summary>Relanza este ejecutable con UAC. False si el usuario cancela o el proceso termina con error.</summary>
        public static bool EjecutarElevado(string argumento)
        {
            try
            {
                using Process? proceso = Process.Start(new ProcessStartInfo(Environment.ProcessPath!, argumento)
                {
                    UseShellExecute = true,
                    Verb = "runas",
                    WorkingDirectory = AppContext.BaseDirectory
                });
                if (proceso == null) return false;
                proceso.WaitForExit();
                return proceso.ExitCode == 0;
            }
            catch (Win32Exception ex) when (ex.NativeErrorCode == ErrorCancelledByUser)
            {
                return false;
            }
        }

        public static bool EsAdministrador()
        {
            using WindowsIdentity identidad = WindowsIdentity.GetCurrent();
            return new WindowsPrincipal(identidad).IsInRole(WindowsBuiltInRole.Administrator);
        }

        private static bool ConfigurarInicioAutomatico(string servicio)
        {
            using Process? proceso = Process.Start(new ProcessStartInfo("sc.exe", $"config \"{servicio}\" start= auto")
            {
                CreateNoWindow = true,
                UseShellExecute = false
            });
            proceso?.WaitForExit();
            return proceso?.ExitCode == 0;
        }

        private static bool EsEquipoLocal(string host) =>
            host is "." or "" ||
            host.Equals("(local)", StringComparison.OrdinalIgnoreCase) ||
            host.Equals("localhost", StringComparison.OrdinalIgnoreCase) ||
            host == "127.0.0.1" || host == "::1" ||
            host.Equals(Environment.MachineName, StringComparison.OrdinalIgnoreCase);
    }
}
