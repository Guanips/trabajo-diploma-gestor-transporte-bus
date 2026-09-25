using BE.recorrido_entities;
using Microsoft.Data.SqlClient;
using servicios;

namespace BLL.recorrido_components
{
    public static class GestorRuta
    {
        public static List<Ruta> ObtenerRutas()
        {
            try
            {
                string connectionString = EnvGetterService.GetConnectionString();
                List<Ruta> rutas = new List<Ruta>();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("usp_Ruta_GetAll", conn);
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
                            Ruta ruta = new Ruta(
                                reader["id"].ToString() ?? throw new Exception("El campo 'id' es nulo."),
                                reader["descripcion"].ToString() ?? throw new Exception("El campo 'descripcion' es nulo."),
                                reader["sentido"].ToString() ?? throw new Exception("El campo 'sentido' es nulo."),
                                Convert.ToInt32(reader["distanciaTotalKM"]),
                                Convert.ToInt32(reader["tiempoEstimadoMin"])
                            );

                            // Poblar recorrido de la ruta desde la BD
                            List<Parada> recorrido = ObtenerRecorridoDeRuta(ruta.id);
                            foreach (var parada in recorrido)
                            {
                                // Agregar en el orden recibido
                                ruta.AgregarParada(parada);
                            }

                            rutas.Add(ruta);
                        }
                    }
                }
                return rutas;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void InsertarRuta(Ruta nRuta)
        {
            try
            {
                string connectionString = EnvGetterService.GetConnectionString();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("usp_Ruta_Insert", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", nRuta.id);
                    cmd.Parameters.AddWithValue("@descripcion", nRuta.descripcion);
                    cmd.Parameters.AddWithValue("@sentido", nRuta.sentido);
                    cmd.Parameters.AddWithValue("@distanciaTotalKM", nRuta.distanciaTotalKM);
                    cmd.Parameters.AddWithValue("@tiempoEstimadoMin", nRuta.tiempoEstimadoMin);
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

        public static void EliminarRuta(string id)
        {
            try
            {
                string connectionString = EnvGetterService.GetConnectionString();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("usp_Ruta_Delete", conn);
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

        public static void ActualizarRuta(Ruta nRuta)
        {
            try
            {
                string connectionString = EnvGetterService.GetConnectionString();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("usp_Ruta_Update", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", nRuta.id);
                    cmd.Parameters.AddWithValue("@descripcion", nRuta.descripcion);
                    cmd.Parameters.AddWithValue("@sentido", nRuta.sentido);
                    cmd.Parameters.AddWithValue("@distanciaTotalKM", nRuta.distanciaTotalKM);
                    cmd.Parameters.AddWithValue("@tiempoEstimadoMin", nRuta.tiempoEstimadoMin);
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

        public static void AgregarParadaEnRuta(Parada parada, Ruta ruta, int orden)
        {
            try
            {
                string connectionString = EnvGetterService.GetConnectionString();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("usp_Recorrido_AddParada", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_ruta", ruta.id);
                    cmd.Parameters.AddWithValue("@id_parada", parada.id);
                    cmd.Parameters.AddWithValue("@orden", orden);
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

        public static void EliminarParadaDeRuta(Parada parada, Ruta ruta)
        {
            try
            {
                string connectionString = EnvGetterService.GetConnectionString();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("usp_Recorrido_RemoveParada", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_ruta", ruta.id);
                    cmd.Parameters.AddWithValue("@id_parada", parada.id);
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

        public static void ModificarOrdenParadaEnRuta(Parada parada, Ruta ruta, int nuevoOrden)
        {
            try
            {
                string connectionString = EnvGetterService.GetConnectionString();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("usp_Recorrido_EditParadaOrder", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_ruta", ruta.id);
                    cmd.Parameters.AddWithValue("@id_parada", parada.id);
                    cmd.Parameters.AddWithValue("@orden", nuevoOrden);
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

        // Nuevo: obtiene el recorrido (lista de Parada) para una ruta dada
        public static List<Parada> ObtenerRecorridoDeRuta(string idRuta)
        {
            try
            {
                string connectionString = EnvGetterService.GetConnectionString();
                List<Parada> recorrido = new List<Parada>();

                // Obtener todas las paradas disponibles para mapear por id
                List<Parada> todasParadas = GestorParada.ObtenerParadas();
                Dictionary<string, Parada> mapaParadas = todasParadas.ToDictionary(p => p.id);

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("usp_Recorrido_GetForRuta", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_ruta", idRuta);

                    conn.Open();
                    if (conn.State != System.Data.ConnectionState.Open)
                    {
                        throw new Exception("No se pudo abrir la conexión a la base de datos.");
                    }

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Asumimos que el SP devuelve al menos la columna 'id_parada' y que está ordenado por 'orden'
                            string idParada = reader["id_parada"].ToString();
                            if (mapaParadas.TryGetValue(idParada, out Parada parada))
                            {
                                recorrido.Add(parada);
                            }
                            else
                            {
                                // Si no se encuentra la parada en el maestro, crear una instancia mínima
                                Parada p = new Parada(idParada, reader["descripcion"].ToString(), reader["localidad"].ToString(), reader["direccion"].ToString(), Convert.ToBoolean(reader["habilitada"]));
                                recorrido.Add(p);
                            }
                        }
                    }
                }

                return recorrido;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}