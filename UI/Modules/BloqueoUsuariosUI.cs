using BE;
using BLL;
using System;
using System.Windows.Forms;

namespace UI.Modules
{
    public partial class BloqueoUsuariosUI : FormBaseObserver
    {
        // Mensajes del formulario (MessageBox y textos armados por código) en el idioma por defecto
        private static readonly Dictionary<string, string> MensajesFormulario = new()
        {
            ["err_NoUserDesbloquear"] = "No se ha seleccionado ningún usuario para desbloquear.",
            ["msg_DesbloqueoExito"] = "Usuario desbloqueado correctamente."
        };

        protected override IReadOnlyDictionary<string, string> Mensajes => MensajesFormulario;

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
                    string msgValidacion = T("err_NoUserDesbloquear");
                    throw new Exception(msgValidacion);
                }

                Usuario seleccionado = (Usuario)dataGridViewBloqueados.SelectedRows[0].DataBoundItem;

                gestorBloqueos.DesbloquearUsuario(seleccionado.Username);

                string msgExito = T("msg_DesbloqueoExito");
                string tituloExito = T("msg_TituloExito");
                MessageBox.Show(msgExito, tituloExito, MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarGridBloqueados();
            }
            catch (Exception ex)
            {
                string tituloError = T("msg_TituloError");
                MessageBox.Show(ex.Message, tituloError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
