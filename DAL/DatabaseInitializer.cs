using Microsoft.Data.SqlClient;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace DAL
{
    public class DatabaseInitializationException : Exception
    {
        public string Script { get; }
        public int BatchNumber { get; }
        public int Line { get; }

        public DatabaseInitializationException(string script, int batchNumber, int line, string message, Exception? inner)
            : base($"[{script}] batch #{batchNumber}, línea {line}: {message}", inner)
        {
            Script = script;
            BatchNumber = batchNumber;
            Line = line;
        }
    }

    /// <summary>
    /// Ejecuta los scripts SQL embebidos (esquema, stored procedures y datos iniciales).
    /// Todos los scripts son idempotentes, por lo que Initialize puede invocarse tantas veces
    /// como haga falta (tambien despues de una ejecucion parcial) hasta que termine sin errores.
    /// </summary>
    public static class DatabaseInitializer
    {
        private const string DatabaseName = "GestorTransporteCG";
        private const string LockResource = "GestorTransporteCG.DatabaseInitialization";
        private const int CommandTimeoutSeconds = 300;

        // Orden obligatorio: esquema -> stored procedures -> datos iniciales
        private static readonly string[] Scripts =
        {
            "InitDatabase.sql",
            "InitParadaStoredProcedures.sql",
            "InitRutaStoredProcedures.sql",
            "InitChoferStoredProcedures.sql",
            "InitInternoStoredProcedures.sql",
            "InitCronogramaStoredProcedures.sql",
            "InitSalidaStoredProcedures.sql",
            "InitCargaCombustibleStoredProcedures.sql",
            "InitRevisionTallerStoredProcedures.sql",
            "InitAuditoriaCronogramasStoredProcedures.sql",
            "InitStartingData.sql"
        };

        // "GO" solo en su linea (sqlcmd/SSMS lo aceptan con espacios y comentario final)
        private static readonly Regex GoSeparator =
            new(@"^[ \t]*GO[ \t]*(?:--.*)?\r?$", RegexOptions.Multiline | RegexOptions.IgnoreCase | RegexOptions.Compiled);

        /// <param name="connectionString">Si es null se usa la de appsettings.json.</param>
        /// <param name="log">Recibe el avance (script y batch actual).</param>
        public static void Initialize(string? connectionString = null, Action<string>? log = null)
        {
            connectionString ??= servicios.EnvGetterService.GetConnectionString();

            var builder = new SqlConnectionStringBuilder(connectionString);
            if (!string.IsNullOrEmpty(builder.InitialCatalog) &&
                !builder.InitialCatalog.Equals(DatabaseName, StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(
                    $"Los scripts crean la base '{DatabaseName}', pero la cadena de conexión apunta a '{builder.InitialCatalog}'.");
            }

            // Se conecta a master porque la base puede no existir todavia (los scripts hacen USE).
            // Sin pooling para no dejar conexiones abiertas contra la base recien creada (LocalDB).
            builder.InitialCatalog = "master";
            builder.Pooling = false;
            if (builder.ConnectTimeout < 60) builder.ConnectTimeout = 60; // primer arranque de LocalDB
            string masterConnectionString = builder.ConnectionString;

            // Evita dos inicializaciones simultaneas (instalador + aplicacion, doble clic, etc.)
            using SqlConnection lockConnection = new(masterConnectionString);
            lockConnection.Open();
            AcquireLock(lockConnection);

            foreach (string script in Scripts)
            {
                log?.Invoke($"Ejecutando {script}...");
                RunScript(masterConnectionString, script, log);
            }

            log?.Invoke("Base de datos inicializada.");
            // El applock de sesion se libera al cerrar lockConnection
        }

        /// <summary>
        /// Verifica de forma barata (sin ejecutar scripts) que la base tenga todas las tablas y
        /// stored procedures que crean los scripts embebidos y que el seed haya sido confirmado.
        /// Devuelve false ante cualquier problema (servidor inaccesible, base inexistente, etc.).
        /// </summary>
        public static bool IsReady(string? connectionString = null)
        {
            try
            {
                connectionString ??= servicios.EnvGetterService.GetConnectionString();

                // Los objetos esperados se leen de los propios scripts, asi el chequeo no queda desactualizado
                var expected = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                foreach (string script in Scripts)
                {
                    string sql = ReadEmbeddedScript(script);
                    foreach (Match m in Regex.Matches(sql, @"CREATE\s+(?:OR\s+ALTER\s+PROCEDURE|TABLE)\s+(?:dbo\.)?(\w+)", RegexOptions.IgnoreCase))
                        expected.Add(m.Groups[1].Value);
                }

                var builder = new SqlConnectionStringBuilder(connectionString) { Pooling = false };
                using SqlConnection connection = new(builder.ConnectionString);
                connection.Open();

                var names = expected.ToList();
                string inList = string.Join(",", names.Select((_, i) => $"@p{i}"));
                using (SqlCommand cmd = new($"SELECT COUNT(*) FROM sys.objects WHERE schema_id = SCHEMA_ID(N'dbo') AND type IN (N'U', N'P') AND name IN ({inList})", connection))
                {
                    for (int i = 0; i < names.Count; i++)
                        cmd.Parameters.AddWithValue($"@p{i}", names[i]);
                    if ((int)cmd.ExecuteScalar()! != names.Count) return false;
                }

                using SqlCommand seed = new("SELECT COUNT(*) FROM dbo.Idioma WHERE Codigo = N'ES'", connection);
                if ((int)seed.ExecuteScalar()! == 0) return false;

                // Permisos agregados despues del seed inicial: si faltan, Initialize los completa (es idempotente)
                using SqlCommand permisos = new("SELECT COUNT(*) FROM dbo.Permiso WHERE Nombre = N'PERM-ELIMINAR-SANCION'", connection);
                return (int)permisos.ExecuteScalar()! > 0;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Elimina la base (y sus archivos .mdf/.ldf). Pensado para la desinstalacion: evita que queden
        /// archivos huerfanos que luego hagan fallar CREATE DATABASE. No falla si la base no existe.
        /// </summary>
        public static void DropDatabase(string? connectionString = null)
        {
            connectionString ??= servicios.EnvGetterService.GetConnectionString();
            var builder = new SqlConnectionStringBuilder(connectionString) { InitialCatalog = "master", Pooling = false };

            using SqlConnection connection = new(builder.ConnectionString);
            connection.Open();
            using SqlCommand cmd = new(
                $"IF DB_ID(N'{DatabaseName}') IS NOT NULL BEGIN " +
                $"ALTER DATABASE [{DatabaseName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; " +
                $"DROP DATABASE [{DatabaseName}]; END", connection)
            { CommandTimeout = CommandTimeoutSeconds };
            cmd.ExecuteNonQuery();
        }

        private static void AcquireLock(SqlConnection connection)
        {
            using SqlCommand cmd = new("sp_getapplock", connection) { CommandType = System.Data.CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@Resource", LockResource);
            cmd.Parameters.AddWithValue("@LockMode", "Exclusive");
            cmd.Parameters.AddWithValue("@LockOwner", "Session");
            cmd.Parameters.AddWithValue("@LockTimeout", 120000);
            SqlParameter result = cmd.Parameters.Add("@result", System.Data.SqlDbType.Int);
            result.Direction = System.Data.ParameterDirection.ReturnValue;
            cmd.ExecuteNonQuery();

            if ((int)result.Value < 0)
                throw new TimeoutException("Otra inicialización de la base de datos está en curso. Intente nuevamente.");
        }

        private static void RunScript(string connectionString, string scriptName, Action<string>? log)
        {
            string script = ReadEmbeddedScript(scriptName);

            // Conexion nueva por script: no se arrastra estado de sesion (USE, SET XACT_ABORT, etc.)
            using SqlConnection connection = new(connectionString);
            connection.Open();

            int batchNumber = 0;
            foreach ((string sql, int startLine) in SplitBatches(script))
            {
                batchNumber++;
                try
                {
                    using SqlCommand cmd = new(sql, connection) { CommandTimeout = CommandTimeoutSeconds };
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    // ex.LineNumber es relativo al inicio del batch; se traduce a linea del archivo
                    int line = startLine + Math.Max(ex.LineNumber, 1) - 1;
                    log?.Invoke($"ERROR en {scriptName} (línea {line}): {ex.Message}");
                    throw new DatabaseInitializationException(scriptName, batchNumber, line, ex.Message, ex);
                }
            }
        }

        private static IEnumerable<(string Sql, int StartLine)> SplitBatches(string script)
        {
            int start = 0;
            foreach (Match go in GoSeparator.Matches(script))
            {
                string batch = script.Substring(start, go.Index - start);
                if (!string.IsNullOrWhiteSpace(batch))
                    yield return (batch, CountLinesBefore(script, start));
                start = go.Index + go.Length;
            }

            string last = script.Substring(start);
            if (!string.IsNullOrWhiteSpace(last))
                yield return (last, CountLinesBefore(script, start));
        }

        private static int CountLinesBefore(string text, int index)
        {
            int lines = 1;
            for (int i = 0; i < index; i++)
                if (text[i] == '\n') lines++;
            return lines;
        }

        private static string ReadEmbeddedScript(string scriptName)
        {
            Assembly assembly = typeof(DatabaseInitializer).Assembly;
            using Stream? stream = assembly.GetManifestResourceStream($"Sql.{scriptName}");
            if (stream == null)
                throw new FileNotFoundException($"Script SQL embebido no encontrado: {scriptName}");

            using StreamReader reader = new(stream, Encoding.UTF8, detectEncodingFromByteOrderMarks: true);
            return reader.ReadToEnd();
        }
    }
}
