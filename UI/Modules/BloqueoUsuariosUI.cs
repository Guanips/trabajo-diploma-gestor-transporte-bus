using BE;
using BLL;
using System;
using System.Windows.Forms;

namespace UI.Modules
{
    public partial class BloqueoUsuariosUI : FormBaseObserver
    {
        private GestorBloqueoUsuarios gestorBloqueos;

        public BloqueoUsuariosUI()
        {
            InitializeComponent();
            gestorBloqueos = new GestorBloqueoUsuarios();
            this.Load += BloqueoUsuariosUI_Load;
        }

        private void BloqueoUsuariosUI_Load(object? sender, EventArgs e)
        {
            CargarGridBloqueados();
        }

        private void CargarGridBloqueados()
        {
            dataGridViewBloqueados.DataSource = null;
            dataGridViewBloqueados.DataSource = gestorBloqueos.ObtenerUsuariosBloqueados();

            if (dataGridViewBloqueados.Columns["PasswordHash"] != null)
                dataGridViewBloqueados.Columns["PasswordHash"].Visible = false;
        }

        private void btnDesbloquear_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewBloqueados.SelectedRows.Count < 1)
                {
                    string msgValidacion = T("err_NoUserDesbloquear", "No se ha seleccionado ningún usuario para desbloquear.");
                    throw new Exception(msgValidacion);
                }

                Usuario seleccionado = (Usuario)dataGridViewBloqueados.SelectedRows[0].DataBoundItem;

                gestorBloqueos.DesbloquearUsuario(seleccionado.Username);

                string msgExito = T("msg_DesbloqueoExito", "Usuario desbloqueado correctamente.");
                string tituloExito = T("msg_TituloExito", "Éxito");
                MessageBox.Show(msgExito, tituloExito, MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarGridBloqueados();
            }
            catch (Exception ex)
            {
                string tituloError = T("msg_TituloError", "Error");
                MessageBox.Show(ex.Message, tituloError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
