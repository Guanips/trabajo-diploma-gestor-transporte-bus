using DAL;

namespace UI
{
    internal static class Program
    {
        // Modos sin interfaz, pensados para el instalador (Inno Setup). Devuelven 0 si salio bien, 1 si fallo.
        private const string InitDbArg = "--init-db";
        private const string DropDbArg = "--drop-db";

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
                return EjecutarModoInstalador(() => DatabaseInitializer.Initialize(log: Log), "inicializar la base de datos");

            if (args.Contains(DropDbArg, StringComparer.OrdinalIgnoreCase))
                return EjecutarModoInstalador(() => DatabaseInitializer.DropDatabase(), "eliminar la base de datos");

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            // Red de seguridad: si el instalador se corto o la base quedo a medias, se completa aca.
            if (!DatabaseInitializer.IsReady())
            {
                try
                {
                    DatabaseInitializer.Initialize(log: Log);
                }
                catch (Exception ex)
                {
                    Log($"ERROR: {ex}");
                    MessageBox.Show(
                        $"No se pudo inicializar la base de datos.\n\n{ex.Message}\n\nDetalle en: {LogPath}",
                        "Error de inicialización", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 1;
                }
            }

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
