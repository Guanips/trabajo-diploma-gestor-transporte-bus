using System.Text.Json;

namespace servicios
{
    public static class EnvGetterService
    {
        public static string GetConnectionString()
        {
            string? connectionString = null;
            string directorioActualDAO = AppContext.BaseDirectory;
            DirectoryInfo? directorioRaiz = new DirectoryInfo(directorioActualDAO);

            while (directorioRaiz != null && directorioRaiz.GetFiles("*.sln").Length == 0)
            {
                directorioRaiz = directorioRaiz.Parent;
            }

            if (directorioRaiz == null) throw new Exception("Raiz del proyecto no encontrada para cargar archivo de configuración");

            string rutaArchivoEnv = Path.Combine(directorioRaiz.FullName, ".env");
            if (File.Exists(rutaArchivoEnv))
            {
                try { DotNetEnv.Env.Load(rutaArchivoEnv); } catch { }
            }

            connectionString = Environment.GetEnvironmentVariable("SQL_SERVER_CONNECTION_STRING")
                ?? Environment.GetEnvironmentVariable("ConnectionStrings__Default")
                ?? Environment.GetEnvironmentVariable("ConnectionStrings:Default")
                ?? Environment.GetEnvironmentVariable("DefaultConnection");

            if (connectionString == null)
            {
                string rutaAppSettings = Path.Combine(directorioRaiz.FullName, "appsettings.json");
                if (File.Exists(rutaAppSettings))
                {
                    try
                    {
                        string json = File.ReadAllText(rutaAppSettings);
                        using JsonDocument doc = JsonDocument.Parse(json);
                        JsonElement root = doc.RootElement;

                        if (root.TryGetProperty("ConnectionStrings", out JsonElement cs))
                        {
                            if (cs.TryGetProperty("Default", out JsonElement def) && def.ValueKind == JsonValueKind.String)
                                connectionString = def.GetString();
                            else if (cs.TryGetProperty("SQL_SERVER_CONNECTION_STRING", out JsonElement ss) && ss.ValueKind == JsonValueKind.String)
                                connectionString = ss.GetString();
                        }
                        else if (root.TryGetProperty("SQL_SERVER_CONNECTION_STRING", out JsonElement top) && top.ValueKind == JsonValueKind.String)
                        {
                            connectionString = top.GetString();
                        }
                    }
                    catch { }
                }
            }

            if (connectionString == null) throw new Exception("Configuración para conexión no encontrada.");

            return connectionString;
        }
    }
}
