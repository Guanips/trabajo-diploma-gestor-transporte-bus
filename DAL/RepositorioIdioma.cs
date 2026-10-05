using BE;
using Microsoft.Data.SqlClient;
using servicios;
using System.Data;

namespace DAL
{
    /// <summary>
    /// Acceso a idiomas, etiquetas y traducciones mediante stored procedures (InitIdiomaStoredProcedures.sql).
    /// </summary>
    public class RepositorioIdioma
    {
        public List<Idioma> ObtenerTodosLosIdiomas()
        {
            List<Idioma> idiomas = new List<Idioma>();

            using SqlConnection conn = AbrirConexion();
            using SqlCommand cmd = CrearComando("usp_Idioma_GetAll", conn);
            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                idiomas.Add(new Idioma(reader["Codigo"].ToString()!, reader["Nombre"].ToString()!, Convert.ToBoolean(reader["EsDefault"]))
                {
                    TotalEtiquetas = Convert.ToInt32(reader["TotalEtiquetas"]),
                    EtiquetasTraducidas = Convert.ToInt32(reader["EtiquetasTraducidas"])
                });
            }

            return idiomas;
        }

        public void InsertarIdioma(Idioma idioma)
        {
            using SqlConnection conn = AbrirConexion();
            using SqlCommand cmd = CrearComando("usp_Idioma_Insert", conn);
            cmd.Parameters.Add("@Codigo", SqlDbType.VarChar, 5).Value = idioma.Codigo;
            cmd.Parameters.Add("@Nombre", SqlDbType.NVarChar, 50).Value = idioma.Nombre;
            cmd.ExecuteNonQuery();
        }

        public void ModificarIdioma(Idioma idioma)
        {
            using SqlConnection conn = AbrirConexion();
            using SqlCommand cmd = CrearComando("usp_Idioma_Update", conn);
            cmd.Parameters.Add("@Codigo", SqlDbType.VarChar, 5).Value = idioma.Codigo;
            cmd.Parameters.Add("@Nombre", SqlDbType.NVarChar, 50).Value = idioma.Nombre;
            cmd.ExecuteNonQuery();
        }

        public HashSet<string> ObtenerClavesEtiquetas()
        {
            HashSet<string> claves = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            using SqlConnection conn = AbrirConexion();
            using SqlCommand cmd = CrearComando("usp_Etiqueta_GetClaves", conn);
            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                claves.Add(reader["Clave"].ToString()!);
            }

            return claves;
        }

        /// <summary>
        /// Registra las etiquetas que no existan, con su texto como traducción del idioma por defecto.
        /// Las que ya existen se ignoran. Devuelve cuántas se registraron.
        /// </summary>
        public int RegistrarEtiquetasFaltantes(IEnumerable<(string Clave, string? Formulario, string Texto)> etiquetas)
        {
            DataTable tabla = new DataTable();
            tabla.Columns.Add("Clave", typeof(string));
            tabla.Columns.Add("Formulario", typeof(string));
            tabla.Columns.Add("Texto", typeof(string));

            foreach (var etiqueta in etiquetas)
            {
                tabla.Rows.Add(etiqueta.Clave, (object?)etiqueta.Formulario ?? DBNull.Value, etiqueta.Texto);
            }

            if (tabla.Rows.Count == 0) return 0;

            using SqlConnection conn = AbrirConexion();
            using SqlCommand cmd = CrearComando("usp_Etiqueta_RegistrarFaltantes", conn);
            SqlParameter param = cmd.Parameters.AddWithValue("@Etiquetas", tabla);
            param.SqlDbType = SqlDbType.Structured;
            param.TypeName = "dbo.EtiquetaTablaTipo";

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        /// <summary>
        /// Textos de todas las etiquetas en el idioma indicado (con el idioma por defecto como respaldo).
        /// </summary>
        public Dictionary<string, string> ObtenerTraducciones(string codigoIdioma)
        {
            Dictionary<string, string> traducciones = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            using SqlConnection conn = AbrirConexion();
            using SqlCommand cmd = CrearComando("usp_Traduccion_GetByIdioma", conn);
            cmd.Parameters.Add("@CodigoIdioma", SqlDbType.VarChar, 5).Value = codigoIdioma;
            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                traducciones[reader["Clave"].ToString()!] = reader["Texto"].ToString()!;
            }

            return traducciones;
        }

        public List<EtiquetaTraduccion> ObtenerMatrizTraduccion(string codigoIdioma)
        {
            List<EtiquetaTraduccion> filas = new List<EtiquetaTraduccion>();

            using SqlConnection conn = AbrirConexion();
            using SqlCommand cmd = CrearComando("usp_Traduccion_GetMatriz", conn);
            cmd.Parameters.Add("@CodigoIdioma", SqlDbType.VarChar, 5).Value = codigoIdioma;
            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                filas.Add(new EtiquetaTraduccion(
                    reader["Clave"].ToString()!,
                    reader["Formulario"] == DBNull.Value ? null : reader["Formulario"].ToString(),
                    reader["TextoReferencia"] == DBNull.Value ? string.Empty : reader["TextoReferencia"].ToString()!,
                    reader["TextoTraducido"] == DBNull.Value ? null : reader["TextoTraducido"].ToString()
                ));
            }

            return filas;
        }

        /// <summary>
        /// Guarda un lote de traducciones. Un texto vacío elimina la traducción (salvo en el idioma por defecto).
        /// </summary>
        public void GuardarTraducciones(string codigoIdioma, Dictionary<string, string?> traducciones)
        {
            DataTable tabla = new DataTable();
            tabla.Columns.Add("Clave", typeof(string));
            tabla.Columns.Add("Texto", typeof(string));

            foreach (KeyValuePair<string, string?> kvp in traducciones)
            {
                tabla.Rows.Add(kvp.Key, (object?)kvp.Value ?? DBNull.Value);
            }

            if (tabla.Rows.Count == 0) return;

            using SqlConnection conn = AbrirConexion();
            using SqlCommand cmd = CrearComando("usp_Traduccion_GuardarLote", conn);
            cmd.Parameters.Add("@CodigoIdioma", SqlDbType.VarChar, 5).Value = codigoIdioma;
            SqlParameter param = cmd.Parameters.AddWithValue("@Traducciones", tabla);
            param.SqlDbType = SqlDbType.Structured;
            param.TypeName = "dbo.TraduccionTablaTipo";
            cmd.ExecuteNonQuery();
        }

        private static SqlConnection AbrirConexion()
        {
            SqlConnection conn = new SqlConnection(EnvGetterService.GetConnectionString());
            conn.Open();
            return conn;
        }

        private static SqlCommand CrearComando(string storedProcedure, SqlConnection conn)
        {
            return new SqlCommand(storedProcedure, conn) { CommandType = CommandType.StoredProcedure };
        }
    }
}
