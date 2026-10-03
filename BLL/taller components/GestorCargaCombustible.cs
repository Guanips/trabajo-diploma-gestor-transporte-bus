using BE.interno_entities;
using BE.taller_entities;
using BLL.interno_components;
using Microsoft.Data.SqlClient;
using servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.taller_components
{
    public static class GestorCargaCombustible
    {
        public static List<CargaCombustible> ObtenerCargas ()
        {
            try
            {
                string connectionString = EnvGetterService.GetConnectionString();
                List<CargaCombustible> cargas = new List<CargaCombustible>();

                // Los internos se cargan una sola vez y luego se asocian por número
                Dictionary<int, Interno> internos = GestorInterno.ObtenerInternos().ToDictionary(interno => interno.num_interno);

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("usp_CargaCombustible_GetAll", conn);
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
                            int numInterno = Convert.ToInt32(reader["num_interno"]);
                            if (!internos.TryGetValue(numInterno, out Interno? interno) || interno is null)
                            {
                                throw new Exception($"No se encontró el interno '{numInterno}' asociado a la carga de combustible.");
                            }

                            CargaCombustible carga = new CargaCombustible(
                                Convert.ToInt32(reader["id"]),
                                interno,
                                Convert.ToDateTime(reader["fechaHora"]),
                                Convert.ToDecimal(reader["litrosCargados"]),
                                Convert.ToDecimal(reader["precioPorLitro"]),
                                Convert.ToInt32(reader["kilometrajeActual"]),
                                Convert.ToBoolean(reader["anulada"]),
                                reader["motivoAnulacion"] == DBNull.Value ? null : reader["motivoAnulacion"].ToString()
                            );

                            cargas.Add(carga);
                        }
                    }
                }

                return cargas;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void RegistrarCarga(CargaCombustible carga)
        {
            try
            {
                string connectionString = EnvGetterService.GetConnectionString();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("usp_CargaCombustible_Insert", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@num_interno", carga.interno.num_interno);
                    cmd.Parameters.AddWithValue("@fechaHora", carga.fechaHora);
                    cmd.Parameters.AddWithValue("@litrosCargados", carga.litrosCargados);
                    cmd.Parameters.AddWithValue("@precioPorLitro", carga.precioPorLitro);
                    cmd.Parameters.AddWithValue("@kilometrajeActual", carga.kilometrajeActual);
                    conn.Open();
                    if (conn.State != System.Data.ConnectionState.Open)
                    {
                        throw new Exception("No se pudo abrir la conexión a la base de datos.");
                    }

                    // El SP devuelve el id insertado; si el interno no existe o los datos son inválidos no devuelve ningún resultado
                    object idGenerado = cmd.ExecuteScalar();
                    if (idGenerado == null || idGenerado == DBNull.Value)
                    {
                        throw new Exception("No se pudo registrar la carga: el interno indicado no existe o los datos son inválidos.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void AnularCarga(int idCarga, string motivo)
        {
            try
            {
                string connectionString = EnvGetterService.GetConnectionString();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("usp_CargaCombustible_Anular", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", idCarga);
                    cmd.Parameters.AddWithValue("@motivoAnulacion", (object?)motivo ?? DBNull.Value);
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
