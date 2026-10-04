using BE.cronograma_entities;
using BE.interno_entities;
using BLL.interno_components;
using Microsoft.Data.SqlClient;
using servicios;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.cronograma_components
{
    public static class AuditorCronogramasBLL
    {
        public static List<Sancion> ObtenerSanciones ()
        {
            try
            {
                string connectionString = EnvGetterService.GetConnectionString();
                List<Sancion> sanciones = new List<Sancion>();

                // Los choferes se cargan una sola vez y luego se asocian por número
                Dictionary<int, Chofer> choferes = GestorChofer.ObtenerChoferes().ToDictionary(chofer => chofer.num_chofer);

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("usp_Sancion_GetAll", conn);
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
                            sanciones.Add(LeerSancion(reader, choferes));
                        }
                    }
                }

                return sanciones;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<Sancion> ObtenerSancionesPorChofer (int numChofer)
        {
            try
            {
                string connectionString = EnvGetterService.GetConnectionString();
                List<Sancion> sanciones = new List<Sancion>();

                // Los choferes se cargan una sola vez y luego se asocian por número
                Dictionary<int, Chofer> choferes = GestorChofer.ObtenerChoferes().ToDictionary(chofer => chofer.num_chofer);

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("usp_Sancion_GetByChofer", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@num_chofer", numChofer);

                    conn.Open();

                    if (conn.State != System.Data.ConnectionState.Open)
                    {
                        throw new Exception("No se pudo abrir la conexión a la base de datos.");
                    }

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            sanciones.Add(LeerSancion(reader, choferes));
                        }
                    }
                }

                return sanciones;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Devuelve el id generado por la base de datos para la nueva sanción
        public static int InsertarSancion (Sancion nSancion)
        {
            try
            {
                string connectionString = EnvGetterService.GetConnectionString();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("usp_Sancion_Insert", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@num_chofer", nSancion.choferSancionado.num_chofer);
                    cmd.Parameters.AddWithValue("@fecha", nSancion.fecha.ToDateTime(new TimeOnly(0, 0)));
                    cmd.Parameters.AddWithValue("@motivo", nSancion.motivo);
                    conn.Open();
                    if (conn.State != System.Data.ConnectionState.Open)
                    {
                        throw new Exception("No se pudo abrir la conexión a la base de datos.");
                    }

                    // El SP devuelve el id insertado; si el chofer no existe o el motivo es vacío no devuelve ningún resultado
                    object idGenerado = cmd.ExecuteScalar();
                    if (idGenerado == null || idGenerado == DBNull.Value)
                    {
                        throw new Exception("No se pudo insertar la sanción: el chofer indicado no existe o el motivo está vacío.");
                    }

                    return Convert.ToInt32(idGenerado);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void EliminarSancion (int id)
        {
            try
            {
                string connectionString = EnvGetterService.GetConnectionString();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("usp_Sancion_Delete", conn);
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

        public static void ActualizarHoraLlegadaReal (int id, TimeOnly horaLlegadaReal)
        {
            try
            {
                string connectionString = EnvGetterService.GetConnectionString();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("usp_Salida_UpdateHoraLlegadaReal", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@horaLlegadaReal", horaLlegadaReal.ToTimeSpan());
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

        private static Sancion LeerSancion (SqlDataReader reader, Dictionary<int, Chofer> choferes)
        {
            int numChofer = Convert.ToInt32(reader["num_chofer"]);
            if (!choferes.TryGetValue(numChofer, out Chofer? chofer) || chofer is null)
            {
                throw new Exception($"No se encontró el chofer '{numChofer}' asociado a la sanción.");
            }

            return new Sancion(
                Convert.ToInt32(reader["id"]),
                reader["motivo"].ToString() ?? throw new Exception("El campo 'motivo' es nulo."),
                DateOnly.FromDateTime(Convert.ToDateTime(reader["fecha"])),
                chofer
            );
        }
    }
}
