using BE.interno_entities;
using BE.recorrido_entities;
using Microsoft.Data.SqlClient;
using servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.interno_components
{
    public class GestorChofer
    {
        public GestorChofer () { }

        public static List<Chofer> ObtenerChoferes ()
        {
            try
            {
                string connectionString = EnvGetterService.GetConnectionString();
                List<Chofer> choferes = new List<Chofer>();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("usp_Chofer_GetAll", conn);
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
                            Chofer chofer = new Chofer(
                                Convert.ToInt32(reader["num_chofer"]),
                                Convert.ToInt32(reader["dni"]),
                                reader["nombreCompleto"].ToString(),
                                Convert.ToBoolean(reader["activo"])
                            );
                            choferes.Add(chofer);
                        }
                    }
                }

                return choferes;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public static void InsertarChofer(Chofer nChofer)
        {
            try
            {
                string connectionString = EnvGetterService.GetConnectionString();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("usp_Chofer_Insert", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@dni", nChofer.dni);
                    cmd.Parameters.AddWithValue("@nombreCompleto", nChofer.nombreCompleto);
                    cmd.Parameters.AddWithValue("@activo", nChofer.activo);
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

        public static void ModificarChofer(Chofer nChofer)
        {
            try
            {
                string connectionString = EnvGetterService.GetConnectionString();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("usp_Chofer_Update", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@num_chofer", nChofer.num_chofer);
                    cmd.Parameters.AddWithValue("@dni", nChofer.dni);
                    cmd.Parameters.AddWithValue("@nombreCompleto", nChofer.nombreCompleto);
                    cmd.Parameters.AddWithValue("@activo", nChofer.activo);
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

        public static void EliminarChofer(int num_chofer)
        {
            try
            {
                string connectionString = EnvGetterService.GetConnectionString();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("usp_Chofer_Delete", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@num_chofer", num_chofer);
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
