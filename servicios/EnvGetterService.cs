using System.Text.Json;

namespace servicios
{
    public static class EnvGetterService
    {
        public static string GetConnectionString()
        {
            // Opcional: permite sobrescribir con una variable de entorno
            string? cs = Environment.GetEnvironmentVariable("SQL_SERVER_CONNECTION_STRING");
            if (!string.IsNullOrWhiteSpace(cs)) return cs;

            string ruta = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
            if (!File.Exists(ruta))
                throw new FileNotFoundException("No se encontró appsettings.json", ruta);

            using JsonDocument doc = JsonDocument.Parse(File.ReadAllText(ruta));
            if (doc.RootElement.TryGetProperty("ConnectionStrings", out JsonElement cadenas) &&
                cadenas.TryGetProperty("Default", out JsonElement def) &&
                def.ValueKind == JsonValueKind.String)
            {
                return def.GetString()!;
            }

            throw new Exception("Configuración para conexión no encontrada en appsettings.json.");
        }
    }
}
