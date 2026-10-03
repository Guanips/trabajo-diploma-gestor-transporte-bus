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
    public class GestorSalida
    {
        public GestorSalida () { }

        // Retorna un diccionario por id de cronograma, con una lista de salidas para cada cronograma.
        public static Dictionary<int, List<Salida>> ObtenerSalidas ()
        {
            try
            {
                string connectionString = EnvGetterService.GetConnectionString();
                Dictionary<int, List<Salida>> salidas = new Dictionary<int, List<Salida>>();

                // Los choferes y los internos se cargan una sola vez y luego se asocian por número
                Dictionary<int, Chofer> choferes = GestorChofer.ObtenerChoferes().ToDictionary(chofer => chofer.num_chofer);
                Dictionary<int, Interno> internos = GestorInterno.ObtenerInternos().ToDictionary(interno => interno.num_interno);

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("usp_Salida_GetAll", conn);
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
                            // Las columnas num_chofer y num_interno admiten NULL cuando la salida todavía no fue asignada
                            Chofer? choferAsignado = null;
                            if (reader["num_chofer"] != DBNull.Value)
                            {
                                int numChofer = Convert.ToInt32(reader["num_chofer"]);
                                if (!choferes.TryGetValue(numChofer, out Chofer? chofer) || chofer is null)
                                {
                                    throw new Exception($"No se encontró el chofer '{numChofer}' asignado a la salida.");
                                }
                                choferAsignado = chofer;
                            }

                            Interno? internoAsignado = null;
                            if (reader["num_interno"] != DBNull.Value)
                            {
                                int numInterno = Convert.ToInt32(reader["num_interno"]);
                                if (!internos.TryGetValue(numInterno, out Interno? interno) || interno is null)
                                {
                                    throw new Exception($"No se encontró el interno '{numInterno}' asignado a la salida.");
                                }
                                internoAsignado = interno;
                            }

                            Salida salida = new Salida(
                                Convert.ToInt32(reader["id"]),
                                choferAsignado,
                                internoAsignado,
                                TimeOnly.FromTimeSpan((TimeSpan)reader["horaSalidaTeorica"]),
                                TimeOnly.FromTimeSpan((TimeSpan)reader["horaLlegadaTeorica"]),
                                LeerHoraOpcional(reader["horaLlegadaReal"]),
                                Convert.ToBoolean(reader["estaSuspendida"]),
                                reader["motivoSuspension"] == DBNull.Value ? null : reader["motivoSuspension"].ToString()
                            );

                            int idCronograma = Convert.ToInt32(reader["id_cronograma"]);
                            if (!salidas.TryGetValue(idCronograma, out List<Salida>? cronogramaSalidas))
                            {
                                cronogramaSalidas = new List<Salida>();
                                salidas[idCronograma] = cronogramaSalidas;
                            }

                            cronogramaSalidas.Add(salida);
                        }
                    }
                }

                return salidas;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Devuelve el id generado por la base de datos para la nueva salida
        public static int InsertarSalida (int idCronograma, Salida nSalida)
        {
            try
            {
                string connectionString = EnvGetterService.GetConnectionString();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("usp_Salida_Insert", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_cronograma", idCronograma);
                    cmd.Parameters.AddWithValue("@num_interno", (object?)nSalida.internoAsignado?.num_interno ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@num_chofer", (object?)nSalida.choferAsignado?.num_chofer ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@horaSalidaTeorica", nSalida.horaSalidaTeorica.ToTimeSpan());
                    cmd.Parameters.AddWithValue("@horaLlegadaTeorica", nSalida.horaLlegadaTeorica.ToTimeSpan());
                    cmd.Parameters.AddWithValue("@horaLlegadaReal", EscribirHoraOpcional(nSalida.horaLlegadaReal));
                    cmd.Parameters.AddWithValue("@estaSuspendida", nSalida.estaSuspendida);
                    cmd.Parameters.AddWithValue("@motivoSuspension", (object?)nSalida.motivoSuspension ?? DBNull.Value);
                    conn.Open();
                    if (conn.State != System.Data.ConnectionState.Open)
                    {
                        throw new Exception("No se pudo abrir la conexión a la base de datos.");
                    }

                    // El SP devuelve el id insertado; si el cronograma, el interno o el chofer no existen no devuelve ningún resultado
                    object idGenerado = cmd.ExecuteScalar();
                    if (idGenerado == null || idGenerado == DBNull.Value)
                    {
                        throw new Exception("No se pudo insertar la salida: el cronograma, el interno o el chofer indicados no existen.");
                    }

                    return Convert.ToInt32(idGenerado);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void BatchInsertarSalidas(int idCronograma, List<Salida> salidas)
        {
            try
            {
                string connectionString = EnvGetterService.GetConnectionString();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    if (conn.State != System.Data.ConnectionState.Open)
                    {
                        throw new Exception("No se pudo abrir la conexión a la base de datos.");
                    }

                    SqlCommand cmd = new SqlCommand("usp_Salida_Insert", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    salidas.ForEach(nSalida => {
                        cmd.Parameters.AddWithValue("@id_cronograma", idCronograma);
                        cmd.Parameters.AddWithValue("@num_interno", (object?)nSalida.internoAsignado?.num_interno ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@num_chofer", (object?)nSalida.choferAsignado?.num_chofer ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@horaSalidaTeorica", nSalida.horaSalidaTeorica.ToTimeSpan());
                        cmd.Parameters.AddWithValue("@horaLlegadaTeorica", nSalida.horaLlegadaTeorica.ToTimeSpan());
                        cmd.Parameters.AddWithValue("@horaLlegadaReal", EscribirHoraOpcional(nSalida.horaLlegadaReal));
                        cmd.Parameters.AddWithValue("@estaSuspendida", nSalida.estaSuspendida);
                        cmd.Parameters.AddWithValue("@motivoSuspension", (object?)nSalida.motivoSuspension ?? DBNull.Value);

                        // El SP devuelve el id insertado; si el cronograma, el interno o el chofer no existen no devuelve ningún resultado
                        object idGenerado = cmd.ExecuteScalar();
                        if (idGenerado == null || idGenerado == DBNull.Value)
                        {
                            throw new Exception("No se pudo insertar la salida: el cronograma, el interno o el chofer indicados no existen.");
                        }                        
                        int parsedId = Convert.ToInt32(idGenerado);
                        nSalida.SetId(parsedId);

                        cmd.Parameters.Clear();
                    });

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Enviar null en el interno desasigna el que estuviera asignado
        public static void AsignarInterno (int id, Interno? interno)
        {
            try
            {
                string connectionString = EnvGetterService.GetConnectionString();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("usp_Salida_AssignInterno", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@num_interno", (object?)interno?.num_interno ?? DBNull.Value);
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

        // Enviar null en el chofer desasigna el que estuviera asignado
        public static void AsignarChofer (int id, Chofer? chofer)
        {
            try
            {
                string connectionString = EnvGetterService.GetConnectionString();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("usp_Salida_AssignChofer", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@num_chofer", (object?)chofer?.num_chofer ?? DBNull.Value);
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

        public static void EliminarSalida (int id)
        {
            try
            {
                string connectionString = EnvGetterService.GetConnectionString();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("usp_Salida_Delete", conn);
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

        public static void SuspenderSalida(int id, string motivoSuspension)
        {
            try
            {
                string connectionString = EnvGetterService.GetConnectionString();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("usp_Salida_Suspender", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@motivoSuspension", motivoSuspension);
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

        // La columna horaLlegadaReal admite NULL cuando la llegada todavía no se registró.
        // Al leer, se representa como null; al escribir, null se persiste como NULL en la base.
        private static TimeOnly? LeerHoraOpcional (object valor)
        {
            return valor == DBNull.Value ? null : TimeOnly.FromTimeSpan((TimeSpan)valor);
        }

        private static object EscribirHoraOpcional (TimeOnly? hora)
        {
            if (hora is null || hora == TimeOnly.MinValue)
            {
                return DBNull.Value;
            }

            return hora.Value.ToTimeSpan();
        }
    }
}