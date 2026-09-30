using BE.interno_entities;
using Microsoft.Data.SqlClient;
using servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.interno_components
{
    public class GestorInterno
    {
        public GestorInterno () { }

        public static List<Interno> ObtenerInternos ()
        {
            try
            {
                string connectionString = EnvGetterService.GetConnectionString();
                List<Interno> internos = new List<Interno>();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("usp_Interno_GetAll", conn);
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
                            DateTime fechaIncorporacionDateTime = Convert.ToDateTime(reader["fechaIncorporacion"]);
                            DateOnly fechaIncorporacion = DateOnly.FromDateTime(fechaIncorporacionDateTime);

                            Interno interno = new Interno(
                                Convert.ToInt32(reader["num_interno"]),
                                reader["patente"].ToString(),
                                reader["modelo"].ToString(),
                                fechaIncorporacion,
                                Convert.ToBoolean(reader["disponible"])
                            );
                            internos.Add(interno);
                        }
                    }
                }

                return internos;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }  

        public static void InsertarInterno(Interno nInterno)
        {
            try
            {
                string connectionString = EnvGetterService.GetConnectionString();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("usp_Interno_Insert", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@patente", nInterno.patente);
                    cmd.Parameters.AddWithValue("@modelo", nInterno.modelo);
                    cmd.Parameters.AddWithValue("@fechaIncorporacion", nInterno.fechaIncorporacion.ToDateTime(new TimeOnly(0, 0)));
                    cmd.Parameters.AddWithValue("@disponible", nInterno.disponible);
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

        public static void ModificarInterno(Interno nInterno)
        {
            try
            {
                string connectionString = EnvGetterService.GetConnectionString();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("usp_Interno_Update", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@num_interno", nInterno.num_interno);
                    cmd.Parameters.AddWithValue("@patente", nInterno.patente);
                    cmd.Parameters.AddWithValue("@modelo", nInterno.modelo);
                    cmd.Parameters.AddWithValue("@fechaIncorporacion", nInterno.fechaIncorporacion.ToDateTime(new TimeOnly(0, 0)));
                    cmd.Parameters.AddWithValue("@disponible", nInterno.disponible);
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

        public static void EliminarInterno(int num_interno)
        {
            try
            {
                string connectionString = EnvGetterService.GetConnectionString();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("usp_Interno_Delete", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@num_interno", num_interno);
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
