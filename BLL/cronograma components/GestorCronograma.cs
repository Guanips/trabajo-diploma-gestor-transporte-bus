using BE.cronograma_entities;
using BE.recorrido_entities;
using BLL.recorrido_components;
using Microsoft.Data.SqlClient;
using servicios;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.cronograma_components
{
    public class GestorCronograma
    {
        public GestorCronograma () { }

        public static List<Cronograma> ObtenerCronogramas ()
        {
            try
            {
                string connectionString = EnvGetterService.GetConnectionString();
                List<Cronograma> cronogramas = new List<Cronograma>();

                // Las rutas se cargan una sola vez y luego se asocian por id
                Dictionary<string, Ruta> rutas = GestorRuta.ObtenerRutas().ToDictionary(ruta => ruta.id);
                Dictionary<int, List<Salida>> salidas = GestorSalida.ObtenerSalidas();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("usp_Cronograma_GetAll", conn);
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
                            string idRuta = reader["id_ruta"].ToString() ?? throw new Exception("El campo 'id_ruta' es nulo.");

                            if (!rutas.TryGetValue(idRuta, out Ruta? ruta) || ruta is null)
                            {
                                throw new Exception($"No se encontró la ruta '{idRuta}' asociada al cronograma.");
                            }

                            int idCronograma = Convert.ToInt32(reader["id"]);
                            Cronograma cronograma = new Cronograma(
                                idCronograma,
                                reader["descripcion"].ToString() ?? throw new Exception("El campo 'descripcion' es nulo."),
                                DateOnly.FromDateTime(Convert.ToDateTime(reader["fechaValidez"])),
                                TimeOnly.FromTimeSpan((TimeSpan)reader["horaInicio"]),
                                TimeOnly.FromTimeSpan((TimeSpan)reader["horaFin"]),
                                Convert.ToInt32(reader["frecuenciaMinutos"]),
                                Convert.ToInt32(reader["tiempoDescansoMinutos"]),
                                ruta
                            );
                            cronogramas.Add(cronograma);

                            // Asociar las salidas correspondientes al cronograma
                            if (salidas.TryGetValue(idCronograma, out List<Salida>? salidasCronograma))
                            {
                                cronograma.salidas.AddRange(salidasCronograma);
                            }
                        }
                    }
                }

                return cronogramas;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Devuelve el id generado por la base de datos para el nuevo cronograma
        public static int InsertarCronograma (Cronograma nCronograma)
        {
            try
            {
                string connectionString = EnvGetterService.GetConnectionString();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("usp_Cronograma_Insert", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@descripcion", nCronograma.descripcion);
                    cmd.Parameters.AddWithValue("@fechaValidez", nCronograma.fechaValidez.ToDateTime(new TimeOnly(0, 0)));
                    cmd.Parameters.AddWithValue("@horaInicio", nCronograma.horaInicio.ToTimeSpan());
                    cmd.Parameters.AddWithValue("@horaFin", nCronograma.horaFin.ToTimeSpan());
                    cmd.Parameters.AddWithValue("@frecuenciaMinutos", nCronograma.frecuenciaMinutos);
                    cmd.Parameters.AddWithValue("@tiempoDescansoMinutos", nCronograma.tiempoDescansoMinutos);
                    cmd.Parameters.AddWithValue("@id_ruta", nCronograma.ruta.id);
                    conn.Open();
                    if (conn.State != System.Data.ConnectionState.Open)
                    {
                        throw new Exception("No se pudo abrir la conexión a la base de datos.");
                    }

                    // El SP devuelve el id insertado; si la ruta no existe no devuelve ningún resultado
                    object idGenerado = cmd.ExecuteScalar();
                    if (idGenerado == null || idGenerado == DBNull.Value)
                    {
                        throw new Exception("No se pudo insertar el cronograma: la ruta indicada no existe.");
                    }

                    return Convert.ToInt32(idGenerado);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void ModificarCronograma (Cronograma nCronograma)
        {
            try
            {
                string connectionString = EnvGetterService.GetConnectionString();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("usp_Cronograma_Update", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", nCronograma.id);
                    cmd.Parameters.AddWithValue("@descripcion", nCronograma.descripcion);
                    cmd.Parameters.AddWithValue("@fechaValidez", nCronograma.fechaValidez.ToDateTime(new TimeOnly(0, 0)));
                    cmd.Parameters.AddWithValue("@horaInicio", nCronograma.horaInicio.ToTimeSpan());
                    cmd.Parameters.AddWithValue("@horaFin", nCronograma.horaFin.ToTimeSpan());
                    cmd.Parameters.AddWithValue("@frecuenciaMinutos", nCronograma.frecuenciaMinutos);
                    cmd.Parameters.AddWithValue("@tiempoDescansoMinutos", nCronograma.tiempoDescansoMinutos);
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

        public static void EliminarCronograma (int id)
        {
            try
            {
                string connectionString = EnvGetterService.GetConnectionString();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("usp_Cronograma_Delete", conn);
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
    }
}