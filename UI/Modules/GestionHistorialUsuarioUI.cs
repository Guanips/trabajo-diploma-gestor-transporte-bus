using BE;
using BLL;
using DAL;

namespace UI.Modules
{
    public partial class GestionHistorialUsuarioUI : FormBaseObserver
    {
        // Mensajes del formulario (MessageBox y textos armados por código) en el idioma por defecto
        private static readonly Dictionary<string, string> MensajesFormulario = new()
        {
            ["msg_Atencion"] = "Atención",
            ["msg_ElEstadoDelUsuarioSeHaRecuperadoExitosamente"] = "El estado del usuario se ha recuperado exitosamente.",
            ["msg_Exito"] = "Éxito",
            ["msg_OcurrioUnErrorAlIntentarRecuperarElEstado"] = "Ocurrió un error al intentar recuperar el estado: ",
            ["msg_PorFavorSeleccioneUnUsuarioYUnEstadoHistoricoParaRecuperar"] = "Por favor, seleccione un usuario y un estado histórico para recuperar."
        };

        protected override IReadOnlyDictionary<string, string> Mensajes => MensajesFormulario;

        private GestorHistorialUsuario gestorHistorialUsuario;
        private CaretakerMementoUsuario? caretakerMementoUsuario;

        public GestionHistorialUsuarioUI()
        {
            InitializeComponent();
            gestorHistorialUsuario = new GestorHistorialUsuario();
            ActualizarDataGridViewUsuarios();
        }

        private void ActualizarDataGridViewUsuarios()
        {
            dataGridViewUsuarios.DataSource = null;
            List<Usuario> usuarios = gestorHistorialUsuario.ObtenerUsuarios();
            dataGridViewUsuarios.DataSource = usuarios;

            string[] columnasOcultas = { "PasswordHash", "Permisos", "EstaBloqueado", "DVH", "Idioma", "IntentosFallidos", "ID" };
            foreach (string nombreColumna in columnasOcultas)
            {
                if (dataGridViewUsuarios.Columns[nombreColumna] != null)
                {
                    dataGridViewUsuarios.Columns[nombreColumna].Visible = false;
                }
            }
        }

        private void ActualizarDataGridViewHistorial(CaretakerMementoUsuario caretaker)
        {
            dataGridViewHistorial.DataSource = null;
            List<IMementoUsuario> historial = caretaker.ObtenerHistorial();
            dataGridViewHistorial.DataSource = historial;
        }

        private void gestionHistorialUIButtonRecuperarEstado_Click(object sender, EventArgs e)
        {
            if (dataGridViewUsuarios.CurrentRow == null || dataGridViewHistorial.CurrentRow == null)
            {
                MessageBox.Show(T("msg_PorFavorSeleccioneUnUsuarioYUnEstadoHistoricoParaRecuperar"), T("msg_Atencion"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Usuario? usuarioSeleccionado = dataGridViewUsuarios.CurrentRow.DataBoundItem as Usuario;
            IMementoUsuario? mementoSeleccionado = dataGridViewHistorial.CurrentRow.DataBoundItem as IMementoUsuario;

            if (usuarioSeleccionado != null && mementoSeleccionado != null)
            {
                try
                {
                    usuarioSeleccionado.RestaurarEstado(mementoSeleccionado);
                    RepositorioUsuarios.GetInstance.ModificarUsuario(usuarioSeleccionado);

                    MessageBox.Show(T("msg_ElEstadoDelUsuarioSeHaRecuperadoExitosamente"), T("msg_Exito"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ActualizarDataGridViewUsuarios();
                    dataGridViewHistorial.DataSource = null;
                    caretakerMementoUsuario = null;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(T("msg_OcurrioUnErrorAlIntentarRecuperarElEstado") + ex.Message, T("msg_Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dataGridViewUsuarios_SelectionChanged(object sender, EventArgs e)
        {
            Usuario? usuarioSeleccionado = dataGridViewUsuarios.CurrentRow?.DataBoundItem as Usuario;
            if (usuarioSeleccionado != null)
            {
                caretakerMementoUsuario = gestorHistorialUsuario.ObtenerCaretakerUsuario(usuarioSeleccionado);
                ActualizarDataGridViewHistorial(caretakerMementoUsuario);
            }
        }
    }
}