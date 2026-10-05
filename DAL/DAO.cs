using Microsoft.Data.SqlClient;
using servicios;
using System;
using System.Data;
using System.Linq;

namespace DAL
{
    public class DAO
    {
        private static DAO? Instance;
        private static readonly object _lock = new object();

        private DataSet mainDataSet;

        private SqlDataAdapter daUsers, daBitacora, daPermiso, daPermisoRelacion, daPerfilUsuario, daHistorialUsuario, daDVV;
        private SqlCommandBuilder cbUsers, cbBitacora, cbPermiso, cbPermisoRelacion, cbPerfilUsuario, cbHistorialUsuario, cbDVV;

        private DAO()
        {
            string connectionString = EnvGetterService.GetConnectionString();

            daUsers = new SqlDataAdapter("Select * From Usuario", connectionString);
            daBitacora = new SqlDataAdapter("Select * From Bitacora", connectionString);
            daPermiso = new SqlDataAdapter("Select * From Permiso", connectionString);
            daPermisoRelacion = new SqlDataAdapter("Select * From PermisoRelacion", connectionString);
            daPerfilUsuario = new SqlDataAdapter("Select * From PerfilUsuario", connectionString);
            daHistorialUsuario = new SqlDataAdapter("Select * From HistorialUsuario", connectionString);
            daDVV = new SqlDataAdapter("Select * From DVV", connectionString);

            daUsers.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            daBitacora.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            daPermiso.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            daPermisoRelacion.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            daPerfilUsuario.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            daHistorialUsuario.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            daDVV.MissingSchemaAction = MissingSchemaAction.AddWithKey;

            cbUsers = new SqlCommandBuilder(daUsers);
            cbBitacora = new SqlCommandBuilder(daBitacora);
            cbPermiso = new SqlCommandBuilder(daPermiso);
            cbPermisoRelacion = new SqlCommandBuilder(daPermisoRelacion);
            cbPerfilUsuario = new SqlCommandBuilder(daPerfilUsuario);

            mainDataSet = new DataSet("Users");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                if (conn.State != ConnectionState.Open) throw new Exception("Conexión a base de datos fallida");

                CargarTablaConEsquema(daUsers, "Usuario", conn);
                CargarTablaConEsquema(daBitacora, "Bitacora", conn);
                CargarTablaConEsquema(daPermiso, "Permiso", conn);
                CargarTablaConEsquema(daPermisoRelacion, "PermisoRelacion", conn);
                CargarTablaConEsquema(daPerfilUsuario, "PerfilUsuario", conn);
                CargarTablaConEsquema(daHistorialUsuario, "HistorialUsuario", conn);
                CargarTablaConEsquema(daDVV, "DVV", conn);
            }

            cbUsers = new SqlCommandBuilder(daUsers);
            cbBitacora = new SqlCommandBuilder(daBitacora);
            cbPermiso = new SqlCommandBuilder(daPermiso);
            cbPermisoRelacion = new SqlCommandBuilder(daPermisoRelacion);
            cbPerfilUsuario = new SqlCommandBuilder(daPerfilUsuario);
            cbHistorialUsuario = new SqlCommandBuilder(daHistorialUsuario);
            cbDVV = new SqlCommandBuilder(daDVV);

            ConfigurarAutoincrementoGeneral();
            ArmarRelaciones();
        }

        private void CargarTablaConEsquema(SqlDataAdapter adapter, string tableName, SqlConnection connection)
        {
            if (adapter.SelectCommand == null)
            {
                adapter.SelectCommand = new SqlCommand();
            }

            adapter.SelectCommand.CommandText = $"Select * From {tableName}";
            adapter.SelectCommand.Connection = connection;

            adapter.FillSchema(mainDataSet, SchemaType.Source, tableName);
            adapter.Fill(mainDataSet, tableName);

            if (mainDataSet.Tables[tableName]!.PrimaryKey.Length == 0)
            {
                if (tableName == "PermisoRelacion")
                {
                    mainDataSet.Tables[tableName]!.PrimaryKey = new DataColumn[] {
                        mainDataSet.Tables[tableName]!.Columns["ID_Padre"]!,
                        mainDataSet.Tables[tableName]!.Columns["ID_Hijo"]!
                    };
                }
                else if (tableName == "PerfilUsuario")
                {
                    mainDataSet.Tables[tableName]!.PrimaryKey = new DataColumn[] {
                        mainDataSet.Tables[tableName]!.Columns["ID_Usuario"]!,
                        mainDataSet.Tables[tableName]!.Columns["ID_Perfil"]!
                    };
                }
                else if (tableName == "DVV")
                {
                    mainDataSet.Tables[tableName]!.PrimaryKey = new DataColumn[] { mainDataSet.Tables[tableName]!.Columns["NombreTabla"]! };
                }
                else
                {
                    mainDataSet.Tables[tableName]!.PrimaryKey = new DataColumn[] { mainDataSet.Tables[tableName]!.Columns["ID"]! };
                }
            }
        }

        private void ArmarRelaciones()
        {
            DataTable? dtPermiso = mainDataSet.Tables["Permiso"];
            DataTable? dtPermisoRelacion = mainDataSet.Tables["PermisoRelacion"];
            DataTable? dtPerfilUsuario = mainDataSet.Tables["PerfilUsuario"];
            DataTable? dtUsuario = mainDataSet.Tables["Usuario"];

            if (dtPermiso == null || dtPermisoRelacion == null || dtPerfilUsuario == null || dtUsuario == null) throw new Exception("Error en el armado de relaciones DAO");

            DataRelation drPermisoPadre = new DataRelation(
                "FK_PermisoRelacion_Padre",
                dtPermiso.Columns["ID"]!,
                dtPermisoRelacion.Columns["ID_Padre"]!
            );

            DataRelation drPermisoHijo = new DataRelation(
                "FK_PermisoRelacion_Hijo",
                dtPermiso.Columns["ID"]!,
                dtPermisoRelacion.Columns["ID_Hijo"]!
            );

            DataRelation drPerfilUsuarioUsuario = new DataRelation(
                "FK_PerfilUsuarioUsuario",
                dtUsuario.Columns["ID"]!,
                dtPerfilUsuario.Columns["ID_Usuario"]!
            );

            DataRelation drPerfilUsuarioPerfil = new DataRelation(
                "FK_PerfilUsuarioPerfil",
                dtPermiso.Columns["ID"]!,
                dtPerfilUsuario.Columns["ID_Perfil"]!
            );

            mainDataSet.Relations.Add(drPermisoPadre);
            mainDataSet.Relations.Add(drPermisoHijo);

            mainDataSet.Relations.Add(drPerfilUsuarioUsuario);
            mainDataSet.Relations.Add(drPerfilUsuarioPerfil);
        }

        private void ConfigurarAutoincrementoGeneral()
        {
            if (mainDataSet.Tables.Contains("Bitacora") && mainDataSet.Tables["Bitacora"]!.Columns.Contains("ID"))
            {
                DataTable dtBitacora = mainDataSet.Tables["Bitacora"]!;
                DataColumn columnaIdRegistroBitacora = dtBitacora.Columns["ID"]!;
                int maxId = dtBitacora.Rows.Count > 0 ? dtBitacora.AsEnumerable().Max(r => r["ID"] == DBNull.Value ? 0 : Convert.ToInt32(r["ID"])) : 0;
                columnaIdRegistroBitacora.AutoIncrement = true;
                columnaIdRegistroBitacora.AutoIncrementSeed = maxId + 1;
                columnaIdRegistroBitacora.AutoIncrementStep = 1;
            }

            if (mainDataSet.Tables.Contains("Permiso") && mainDataSet.Tables["Permiso"]!.Columns.Contains("ID"))
            {
                DataTable dtPermiso = mainDataSet.Tables["Permiso"]!;
                DataColumn columnaIdRegistroPermiso = dtPermiso.Columns["ID"]!;
                int maxIdPermiso = dtPermiso.Rows.Count > 0 ? dtPermiso.AsEnumerable().Max(r => r["ID"] == DBNull.Value ? 0 : Convert.ToInt32(r["ID"])) : 0;
                columnaIdRegistroPermiso.AutoIncrement = true;
                columnaIdRegistroPermiso.AutoIncrementSeed = maxIdPermiso + 1;
                columnaIdRegistroPermiso.AutoIncrementStep = 1;
            }

            if (mainDataSet.Tables.Contains("HistorialUsuario") && mainDataSet.Tables["HistorialUsuario"]!.Columns.Contains("ID"))
            {
                DataTable dtHistorial = mainDataSet.Tables["HistorialUsuario"]!;
                DataColumn colIdHistorial = dtHistorial.Columns["ID"]!;
                int maxIdHistorial = dtHistorial.Rows.Count > 0 ? dtHistorial.AsEnumerable().Max(r => r["ID"] == DBNull.Value ? 0 : Convert.ToInt32(r["ID"])) : 0;
                colIdHistorial.AutoIncrement = true;
                colIdHistorial.AutoIncrementSeed = maxIdHistorial + 1;
                colIdHistorial.AutoIncrementStep = 1;
            }
        }

        public static DAO GetInstance
        {
            get
            {
                if (Instance == null)
                {
                    lock (_lock)
                    {
                        if (Instance == null)
                        {
                            Instance = new DAO();
                        }
                    }
                }
                return Instance;
            }
        }

        public DataSet ObtenerDataSet()
        {
            return mainDataSet;
        }

        public void SubirCambiosBD()
        {
            string connectionString = EnvGetterService.GetConnectionString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                daUsers.SelectCommand.Connection = conn;
                daBitacora.SelectCommand.Connection = conn;
                daPermiso.SelectCommand.Connection = conn;
                daPermisoRelacion.SelectCommand.Connection = conn;
                daPerfilUsuario.SelectCommand.Connection = conn;
                daHistorialUsuario.SelectCommand.Connection = conn;
                daDVV.SelectCommand.Connection = conn;

                daUsers.InsertCommand = cbUsers.GetInsertCommand();
                daUsers.UpdateCommand = cbUsers.GetUpdateCommand();
                daUsers.DeleteCommand = cbUsers.GetDeleteCommand();
                daUsers.InsertCommand.Connection = conn;
                daUsers.UpdateCommand.Connection = conn;
                daUsers.DeleteCommand.Connection = conn;

                daBitacora.InsertCommand = cbBitacora.GetInsertCommand();
                daBitacora.UpdateCommand = cbBitacora.GetUpdateCommand();
                daBitacora.DeleteCommand = cbBitacora.GetDeleteCommand();
                daBitacora.InsertCommand.Connection = conn;
                daBitacora.UpdateCommand.Connection = conn;
                daBitacora.DeleteCommand.Connection = conn;

                daPermiso.InsertCommand = cbPermiso.GetInsertCommand();
                daPermiso.UpdateCommand = cbPermiso.GetUpdateCommand();
                daPermiso.DeleteCommand = cbPermiso.GetDeleteCommand();
                daPermiso.InsertCommand.Connection = conn;
                daPermiso.UpdateCommand.Connection = conn;
                daPermiso.DeleteCommand.Connection = conn;

                daPermisoRelacion.InsertCommand = cbPermisoRelacion.GetInsertCommand();
                daPermisoRelacion.UpdateCommand = cbPermisoRelacion.GetUpdateCommand();
                daPermisoRelacion.DeleteCommand = cbPermisoRelacion.GetDeleteCommand();
                daPermisoRelacion.InsertCommand.Connection = conn;
                daPermisoRelacion.UpdateCommand.Connection = conn;
                daPermisoRelacion.DeleteCommand.Connection = conn;



                daPerfilUsuario.InsertCommand = cbPerfilUsuario.GetInsertCommand();
                daPerfilUsuario.UpdateCommand = cbPerfilUsuario.GetUpdateCommand();
                daPerfilUsuario.DeleteCommand = cbPerfilUsuario.GetDeleteCommand();
                daPerfilUsuario.InsertCommand.Connection = conn;
                daPerfilUsuario.UpdateCommand.Connection = conn;
                daPerfilUsuario.DeleteCommand.Connection = conn;

                daHistorialUsuario.InsertCommand = cbHistorialUsuario.GetInsertCommand();
                daHistorialUsuario.UpdateCommand = cbHistorialUsuario.GetUpdateCommand();
                daHistorialUsuario.DeleteCommand = cbHistorialUsuario.GetDeleteCommand();
                daHistorialUsuario.InsertCommand.Connection = conn;
                daHistorialUsuario.UpdateCommand.Connection = conn;
                daHistorialUsuario.DeleteCommand.Connection = conn;

                daDVV.InsertCommand = cbDVV.GetInsertCommand();
                daDVV.UpdateCommand = cbDVV.GetUpdateCommand();
                daDVV.DeleteCommand = cbDVV.GetDeleteCommand();
                daDVV.InsertCommand.Connection = conn;
                daDVV.UpdateCommand.Connection = conn;
                daDVV.DeleteCommand.Connection = conn;

                daUsers.Update(mainDataSet, "Usuario");
                daBitacora.Update(mainDataSet, "Bitacora");
                daPermiso.Update(mainDataSet, "Permiso");
                daPermisoRelacion.Update(mainDataSet, "PermisoRelacion");
                daPerfilUsuario.Update(mainDataSet, "PerfilUsuario");
                daHistorialUsuario.Update(mainDataSet, "HistorialUsuario");
                daDVV.Update(mainDataSet, "DVV");

                mainDataSet.AcceptChanges();
            }
        }
    }
}
