using DAL;
using Microsoft.Data.SqlClient;
using servicios;

namespace UI
{
    internal static class Program
    {
        // Modos sin interfaz, pensados para el instalador (Inno Setup). Devuelven 0 si salio bien, 1 si fallo.
        private const string InitDbArg = "--init-db";
        private const string DropDbArg = "--drop-db";
        // Levanta el servicio de SQL Server; la app se relanza con UAC con este modo cuando el usuario no puede iniciarlo
        private const string StartSqlArg = "--start-sql";

        private static readonly string LogDirectory =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "GestorTransporte");
        private static readonly string LogPath = Path.Combine(LogDirectory, "init-db.log");

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static int Main(string[] args)
        {
            if (args.Contains(InitDbArg, StringComparer.OrdinalIgnoreCase))
                return EjecutarModoInstalador(() =>
                {
                    ServidorParaModoInstalador();
                    DatabaseInitializer.Initialize(log: Log);
                }, "inicializar la base de datos");

            if (args.Contains(DropDbArg, StringComparer.OrdinalIgnoreCase))
                return EjecutarModoInstalador(() =>
                {
                    ServidorParaModoInstalador();
                    DatabaseInitializer.DropDatabase();
                }, "eliminar la base de datos");

            if (args.Contains(StartSqlArg, StringComparer.OrdinalIgnoreCase))
                return EjecutarModoInstalador(ServidorParaModoInstalador, "iniciar SQL Server");

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            if (!AsegurarServidorSql() || !AsegurarBaseDeDatos())
                return 1;

            // Autodetección: registra las etiquetas de todos los formularios que aún no estén en la base
            try
            {
                RegistroInicialEtiquetas.Ejecutar(Log);
            }
            catch (Exception ex)
            {
                Log($"No se pudieron registrar las etiquetas de los formularios: {ex}");
            }

            Application.Run(new MainUI());
            return 0;
        }

        /// <summary>
        /// Si la cadena apunta a una instancia local de SQL Server, verifica que el servicio este activo y lo levanta si no.
        /// Con LocalDB o un servidor remoto no hay nada que hacer. Devuelve false si hay que abortar el arranque.
        /// </summary>
        private static bool AsegurarServidorSql()
        {
            string connectionString;
            string? servicio;
            try
            {
                connectionString = EnvGetterService.GetConnectionString();
                servicio = SqlServerServiceManager.ObtenerNombreServicio(connectionString);
            }
            catch (Exception ex)
            {
                Log($"ERROR: {ex}");
                MessageBox.Show($"No se pudo leer la configuración de la base de datos.\n\n{ex.Message}",
                    "Error de configuración", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (servicio == null) return true;

            // Camino rapido (caso habitual): servicio activo y respondiendo, sin mostrar ventana
            if (SqlServerServiceManager.EstaEnEjecucion(servicio) &&
                SqlServerServiceManager.EsperarConexion(connectionString, TimeSpan.FromSeconds(3)))
                return true;

            ResultadoServicio resultado = FormEspera.Ejecutar("Iniciando SQL Server, por favor espere...",
                () => SqlServerServiceManager.Asegurar(servicio, connectionString, SqlServerServiceManager.EsAdministrador(), Log));

            if (resultado == ResultadoServicio.RequiereAdministrador)
            {
                if (MessageBox.Show(
                        "El servicio de SQL Server está detenido y se necesitan permisos de administrador para iniciarlo.\n\n¿Desea continuar?",
                        "SQL Server detenido", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return false;

                FormEspera.Ejecutar("Iniciando SQL Server, por favor espere...",
                    () => SqlServerServiceManager.EjecutarElevado(StartSqlArg));
                resultado = FormEspera.Ejecutar("Verificando la conexión...",
                    () => SqlServerServiceManager.Asegurar(servicio, connectionString, false, Log));
            }

            string? problema = resultado switch
            {
                ResultadoServicio.Ok => null,
                ResultadoServicio.NoInstalado =>
                    $"No se encontró el servicio {servicio}.\nSQL Server no está instalado: vuelva a ejecutar el instalador de la aplicación.",
                ResultadoServicio.SinConexion =>
                    $"El servicio {servicio} está activo pero no acepta conexiones.",
                ResultadoServicio.RequiereAdministrador =>
                    $"No se pudo iniciar el servicio {servicio} sin permisos de administrador.",
                _ => $"No se pudo iniciar el servicio {servicio}."
            };

            if (problema == null) return true;

            Log($"ERROR: {problema}");
            MessageBox.Show($"{problema}\n\nDetalle en: {LogPath}", "SQL Server no disponible",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        /// <summary>
        /// Red de seguridad: si el instalador se corto o la base quedo a medias, se completa aca.
        /// Crear la base y dar acceso a los usuarios exige permisos de administrador sobre SQL Server:
        /// si el usuario actual no los tiene se reintenta una vez con UAC.
        /// </summary>
        private static bool AsegurarBaseDeDatos()
        {
            if (DatabaseInitializer.IsReady()) return true;

            try
            {
                FormEspera.Ejecutar("Preparando la base de datos, por favor espere...", () =>
                {
                    DatabaseInitializer.Initialize(log: Log);
                    return true;
                });
                return true;
            }
            catch (Exception ex) when (FaltanPermisos(ex) && !SqlServerServiceManager.EsAdministrador())
            {
                Log($"Permisos insuficientes para inicializar la base: {ex.Message}");
                if (MessageBox.Show(
                        "Para preparar la base de datos se necesitan permisos de administrador.\n\n¿Desea continuar?",
                        "Permisos requeridos", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    FormEspera.Ejecutar("Preparando la base de datos, por favor espere...",
                        () => SqlServerServiceManager.EjecutarElevado(InitDbArg));
                    if (DatabaseInitializer.IsReady()) return true;
                }

                MessageBox.Show($"No se pudo preparar la base de datos.\n\nDetalle en: {LogPath}",
                    "Error de inicialización", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                Log($"ERROR: {ex}");
                MessageBox.Show(
                    $"No se pudo inicializar la base de datos.\n\n{ex.Message}\n\nDetalle en: {LogPath}",
                    "Error de inicialización", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // 18456 login fallido, 4060 base inaccesible, 262/229/230/297/15247 permiso denegado
        private static bool FaltanPermisos(Exception ex)
        {
            for (Exception? actual = ex; actual != null; actual = actual.InnerException)
                if (actual is SqlException { Number: 18456 or 4060 or 262 or 229 or 230 or 297 or 15247 })
                    return true;
            return false;
        }

        /// <summary>Los modos del instalador corren elevados: pueden levantar (o rehabilitar) el servicio antes de usar la base.</summary>
        private static void ServidorParaModoInstalador()
        {
            string connectionString = EnvGetterService.GetConnectionString();
            string? servicio = SqlServerServiceManager.ObtenerNombreServicio(connectionString);
            if (servicio == null) return;

            ResultadoServicio resultado = SqlServerServiceManager.Asegurar(servicio, connectionString, true, Log);
            if (resultado != ResultadoServicio.Ok)
                throw new InvalidOperationException($"SQL Server no está disponible ({resultado}, servicio {servicio}).");
        }

        private static int EjecutarModoInstalador(Action accion, string descripcion)
        {
            try
            {
                Log($"Inicio: {descripcion}");
                accion();
                Log($"OK: {descripcion}");
                return 0;
            }
            catch (Exception ex)
            {
                Log($"ERROR al {descripcion}: {ex}");
                return 1;
            }
        }

        private static void Log(string mensaje)
        {
            try
            {
                Directory.CreateDirectory(LogDirectory);
                File.AppendAllText(LogPath, $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} {mensaje}{Environment.NewLine}");
            }
            catch
            {
                // El log nunca debe romper la inicializacion
            }
        }
    }
}
