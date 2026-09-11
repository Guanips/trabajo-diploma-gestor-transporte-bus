using BE.recorrido_entities;
using Microsoft.Data.SqlClient;
using servicios;

namespace BLL.recorrido_components
{
    public static class GestorParada
    {
        public static List<Parada> ObtenerParadas()
        {
            try
            {
                string connectionString = EnvGetterService.GetConnectionString();
                List<Parada> paradas = new List<Parada>();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("usp_Parada_GetAll", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    conn.Open();

                    if (conn.State != System.Data.ConnectionState.Open)
                    {
                        throw new Exception("No se pudo abrir la conexión a la base de datos.");
                    }

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Parada parada = new Parada(
                                reader["id"].ToString(),
                                reader["descripcion"].ToString(),
                                reader["localidad"].ToString(),
                                reader["direccion"].ToString(),
                                Convert.ToBoolean(reader["habilitada"])
                            );
                            paradas.Add(parada);
                        }
                    }
                }

                return paradas;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public static void InsertarParada(Parada nParada)
        {
            try
            {
                string connectionString = EnvGetterService.GetConnectionString();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("usp_Parada_Insert", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id", nParada.id);
                    cmd.Parameters.AddWithValue("@descripcion", nParada.descripcion);
                    cmd.Parameters.AddWithValue("@localidad", nParada.localidad);
                    cmd.Parameters.AddWithValue("@direccion", nParada.direccion);
                    cmd.Parameters.AddWithValue("@habilitada", nParada.habilitada);

                    conn.Open();

                    if (conn.State != System.Data.ConnectionState.Open)
                    {
                        throw new Exception("No se pudo abrir la conexión a la base de datos.");
                    }

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public static void EliminarParada(string id)
        {
            try
            {
                string connectionString = EnvGetterService.GetConnectionString();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("usp_Parada_Delete", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id", id);
                    conn.Open();

                    if (conn.State != System.Data.ConnectionState.Open)
                    {
                        throw new Exception("No se pudo abrir la conexión a la base de datos.");
                    }

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public static void ActualizarParada(Parada parada)
        {
            try
            {
                string connectionString = EnvGetterService.GetConnectionString();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("usp_Parada_Update", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", parada.id);
                    cmd.Parameters.AddWithValue("@descripcion", parada.descripcion);
                    cmd.Parameters.AddWithValue("@localidad", parada.localidad);
                    cmd.Parameters.AddWithValue("@direccion", parada.direccion);
                    cmd.Parameters.AddWithValue("@habilitada", parada.habilitada);
                    conn.Open();
                    if (conn.State != System.Data.ConnectionState.Open)
                    {
                        throw new Exception("No se pudo abrir la conexión a la base de datos.");
                    }
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
