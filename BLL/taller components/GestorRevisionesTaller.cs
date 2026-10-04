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
    public static class GestorRevisionesTaller
    {
        // Retorna las revisiones con toda su cadena armada: cada revisión con sus órdenes de reparación,
        // y cada orden con sus detalles.
        public static List<RevisionTaller> ObtenerRevisiones ()
        {
            try
            {
                string connectionString = EnvGetterService.GetConnectionString();
                List<RevisionTaller> revisiones = new List<RevisionTaller>();

                // Se indexan por id para poder encadenar las tres consultas en memoria
                Dictionary<int, RevisionTaller> revisionesPorId = new Dictionary<int, RevisionTaller>();
                Dictionary<int, OrdenReparacion> ordenesPorId = new Dictionary<int, OrdenReparacion>();

                // Los internos se cargan una sola vez y luego se asocian por número
                Dictionary<int, Interno> internos = GestorInterno.ObtenerInternos().ToDictionary(interno => interno.num_interno);

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    if (conn.State != System.Data.ConnectionState.Open)
                    {
                        throw new Exception("No se pudo abrir la conexión a la base de datos.");
                    }

                    // 1. Revisiones (raíz de la cadena)
                    SqlCommand cmdRevisiones = new SqlCommand("usp_RevisionTaller_GetAll", conn);
                    cmdRevisiones.CommandType = System.Data.CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmdRevisiones.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int numInterno = Convert.ToInt32(reader["num_interno"]);
                            if (!internos.TryGetValue(numInterno, out Interno? interno) || interno is null)
                            {
                                throw new Exception($"No se encontró el interno '{numInterno}' asociado a la revisión.");
                            }

                            RevisionTaller revision = new RevisionTaller(
                                Convert.ToInt32(reader["id"]),
                                interno,
                                DateOnly.FromDateTime(Convert.ToDateTime(reader["fecha"])),
                                reader["descripcion"].ToString() ?? string.Empty,
                                Convert.ToBoolean(reader["reparacionRequerida"])
                            );

                            revisiones.Add(revision);
                            revisionesPorId[revision.id] = revision;
                        }
                    }

                    // 2. Órdenes de reparación, enganchadas a su revisión por id_revision
                    SqlCommand cmdOrdenes = new SqlCommand("usp_OrdenReparacion_GetAll", conn);
                    cmdOrdenes.CommandType = System.Data.CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmdOrdenes.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int idRevision = Convert.ToInt32(reader["id_revision"]);
                            if (!revisionesPorId.TryGetValue(idRevision, out RevisionTaller? revision) || revision is null)
                            {
                                throw new Exception($"No se encontró la revisión '{idRevision}' asociada a la orden de reparación.");
                            }

                            OrdenReparacion orden = new OrdenReparacion(
                                Convert.ToInt32(reader["id"]),
                                reader["motivoReparacion"].ToString() ?? string.Empty,
                                new List<DetalleOrdenReparacion>()
                            );

                            revision.AgregarOrdenReparacion(orden);
                            ordenesPorId[orden.id] = orden;
                        }
                    }

                    // 3. Detalles, enganchados a su orden por id_ordenReparacion
                    SqlCommand cmdDetalles = new SqlCommand("usp_DetalleOrdenReparacion_GetAll", conn);
                    cmdDetalles.CommandType = System.Data.CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmdDetalles.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int idOrdenReparacion = Convert.ToInt32(reader["id_ordenReparacion"]);
                            if (!ordenesPorId.TryGetValue(idOrdenReparacion, out OrdenReparacion? orden) || orden is null)
                            {
                                throw new Exception($"No se encontró la orden de reparación '{idOrdenReparacion}' asociada al detalle.");
                            }

                            DetalleOrdenReparacion detalle = new DetalleOrdenReparacion(
                                Convert.ToInt32(reader["id"]),
                                reader["insumo"].ToString() ?? string.Empty,
                                Convert.ToInt32(reader["cantidad"]),
                                LeerCostoOpcional(reader["costoUnitario"])
                            );

                            orden.AgregarDetalle(detalle);
                        }
                    }
                }

                return revisiones;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Persiste la revisión y toda su cadena (órdenes y detalles) en una sola transacción,
        // devolviendo en cada objeto el id generado por la base de datos.
        public static void RegistrarRevision (RevisionTaller revision)
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

                    // Toda la cadena se inserta en una transacción: si falla un paso no quedan revisiones incompletas
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            int idRevision = InsertarRevision(conn, transaction, revision);
                            revision.SetId(idRevision);

                            foreach (OrdenReparacion orden in revision.ordenesReparacion)
                            {
                                int idOrden = InsertarOrdenReparacion(conn, transaction, idRevision, orden);
                                orden.SetId(idOrden);

                                foreach (DetalleOrdenReparacion detalle in orden.detalles)
                                {
                                    int idDetalle = InsertarDetalleOrdenReparacion(conn, transaction, idOrden, detalle);
                                    detalle.SetId(idDetalle);
                                }
                            }

                            transaction.Commit();
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Actualiza el costo del detalle en la base de datos y lo refleja en el objeto recibido
        public static void ActualizarCostoUnitarioDetalle (DetalleOrdenReparacion detalle, int costoUnitario)
        {
            try
            {
                string connectionString = EnvGetterService.GetConnectionString();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("usp_DetalleOrdenReparacion_UpdateCostoUnitario", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", detalle.id);
                    cmd.Parameters.AddWithValue("@costoUnitario", costoUnitario);
                    conn.Open();
                    if (conn.State != System.Data.ConnectionState.Open)
                    {
                        throw new Exception("No se pudo abrir la conexión a la base de datos.");
                    }
                    cmd.ExecuteNonQuery();
                }

                detalle.ActualizarCostoUnitario(costoUnitario);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static int InsertarRevision (SqlConnection conn, SqlTransaction transaction, RevisionTaller revision)
        {
            SqlCommand cmd = new SqlCommand("usp_RevisionTaller_Insert", conn, transaction);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@num_interno", revision.interno.num_interno);
            cmd.Parameters.AddWithValue("@fecha", revision.fecha.ToDateTime(TimeOnly.MinValue));
            cmd.Parameters.AddWithValue("@descripcion", revision.descripcion);
            cmd.Parameters.AddWithValue("@reparacionRequerida", revision.reparacionRequerida);

            // El SP devuelve el id insertado; si el interno no existe o los datos son inválidos no devuelve ningún resultado
            object idGenerado = cmd.ExecuteScalar();
            if (idGenerado == null || idGenerado == DBNull.Value)
            {
                throw new Exception("No se pudo registrar la revisión: el interno indicado no existe o los datos son inválidos.");
            }

            return Convert.ToInt32(idGenerado);
        }

        private static int InsertarOrdenReparacion (SqlConnection conn, SqlTransaction transaction, int idRevision, OrdenReparacion orden)
        {
            SqlCommand cmd = new SqlCommand("usp_OrdenReparacion_Insert", conn, transaction);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id_revision", idRevision);
            cmd.Parameters.AddWithValue("@motivoReparacion", orden.motivoReparacion);

            object idGenerado = cmd.ExecuteScalar();
            if (idGenerado == null || idGenerado == DBNull.Value)
            {
                throw new Exception("No se pudo registrar la orden de reparación: la revisión indicada no existe o el motivo es inválido.");
            }

            return Convert.ToInt32(idGenerado);
        }

        private static int InsertarDetalleOrdenReparacion (SqlConnection conn, SqlTransaction transaction, int idOrdenReparacion, DetalleOrdenReparacion detalle)
        {
            SqlCommand cmd = new SqlCommand("usp_DetalleOrdenReparacion_Insert", conn, transaction);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id_ordenReparacion", idOrdenReparacion);
            cmd.Parameters.AddWithValue("@insumo", detalle.insumo);
            cmd.Parameters.AddWithValue("@cantidad", detalle.cantidad);
            cmd.Parameters.AddWithValue("@costoUnitario", EscribirCostoOpcional(detalle.costoUnitario));

            object idGenerado = cmd.ExecuteScalar();
            if (idGenerado == null || idGenerado == DBNull.Value)
            {
                throw new Exception("No se pudo registrar el detalle: la orden de reparación indicada no existe o los datos son inválidos.");
            }

            return Convert.ToInt32(idGenerado);
        }

        // La columna costoUnitario admite NULL (costo todavía no asignado). El modelo usa int,
        // por lo que NULL se representa como 0 al leer y 0 se persiste como NULL al escribir.
        private static int LeerCostoOpcional (object valor)
        {
            return valor == DBNull.Value ? 0 : Convert.ToInt32(valor);
        }

        private static object EscribirCostoOpcional (int costoUnitario)
        {
            return costoUnitario == 0 ? DBNull.Value : costoUnitario;
        }
    }
}
