using BE;
using BLL;
using servicios;

namespace UI.Modules
{
    public partial class GestionUsuariosUI : FormBaseObserver
    {
        // Mensajes del formulario (MessageBox y textos armados por código) en el idioma por defecto
        private static readonly Dictionary<string, string> MensajesFormulario = new()
        {
            ["err_NoUserEliminar"] = "No se ha seleccionado ningún usuario para eliminar.",
            ["err_NoUserModificar"] = "No se ha seleccionado ningún usuario para modificar.",
            ["err_PassNoCoincide"] = "Las contraseñas no coinciden."
        };

        protected override IReadOnlyDictionary<string, string> Mensajes => MensajesFormulario;

        private GestorUsuarios gestorUsuarios;

        public GestionUsuariosUI()
        {
            InitializeComponent();
            gestorUsuarios = new GestorUsuarios();
        }
        private void CargarGridUsuarios()
        {
            dataGridViewListadoUsuarios.DataSource = null;
            List<Usuario> usuarios = gestorUsuarios.ListarUsuarios();

            var usuariosFiltrados = usuarios
                .Where(u => !u.Username.Equals("admin", StringComparison.OrdinalIgnoreCase))
                .ToList();

            dataGridViewListadoUsuarios.DataSource = usuariosFiltrados;

            if (dataGridViewListadoUsuarios.Columns["PasswordHash"] != null)
            {
                dataGridViewListadoUsuarios.Columns["PasswordHash"].Visible = false;
            }
        }

        private void GestionUsuariosUI_Load(object sender, EventArgs e)
        {
            CargarGridUsuarios();
        }

        private void dataGridViewListadoUsuarios_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewListadoUsuarios.SelectedRows.Count < 1) return;
            DataGridViewRow? selectedRow = dataGridViewListadoUsuarios.SelectedRows[0];
            if (selectedRow != null && selectedRow.DataBoundItem is Usuario selectedUsuario)
            {
                textBoxModificacionEmail.Text = selectedUsuario.Email;
                textBoxModificacionNumTelefono.Text = selectedUsuario.NumTelefono;
            }
            else
            {
                textBoxModificacionEmail.Text = "";
                textBoxModificacionNumTelefono.Text = "";
            }

        }

        private void gestionUsuariosUIButtonConfirmarRegistrarUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                string nUsername = textBoxRegistroUsername.Text;
                string nPassword = textBoxRegistroContrasena.Text;
                string nPasswordConfirmacion = textBoxRegistroRepetirConstrasena.Text;
                string nEmail = textBoxRegistroEmail.Text;
                string nNumTelefono = textBoxRegistroNumTelefono.Text;

                ValidationResult usernameValidationResult = FormFieldValidationService.ValidateUsername(nUsername);
                ValidationResult emailVerificationResult = FormFieldValidationService.ValidateEmail(nEmail);
                ValidationResult phoneVerificationResult = FormFieldValidationService.ValidatePhone(nNumTelefono);

                if (!usernameValidationResult.IsValid)
                    throw new Exception(T(usernameValidationResult));

                if (!emailVerificationResult.IsValid)
                    throw new Exception(T(emailVerificationResult));

                if (!phoneVerificationResult.IsValid)
                    throw new Exception(T(phoneVerificationResult));

                if (string.IsNullOrEmpty(nPassword) || string.IsNullOrEmpty(nPasswordConfirmacion))
                    throw new Exception(T("err_PassVacia"));

                if (nPassword != nPasswordConfirmacion)
                    throw new Exception(T("err_PassNoCoincide"));

                gestorUsuarios.RegistrarUsuario(nUsername, nPassword, nEmail, nNumTelefono, GestorIdioma.GetInstance.IdiomaDefault);

                CargarGridUsuarios();
            }
            catch (Exception ex)
            {
                string tituloError = T("msg_TituloError");
                MessageBox.Show(ex.Message, tituloError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gestionUsuariosUIModificacionButtonConfirmarModificarUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewListadoUsuarios.SelectedRows.Count < 1)
                    throw new Exception(T("err_NoUserModificar"));

                DataGridViewRow selectedRow = dataGridViewListadoUsuarios.SelectedRows[0];

                string nEmail = textBoxModificacionEmail.Text;
                string nNumTelefono = textBoxModificacionNumTelefono.Text;

                ValidationResult emailValidation = FormFieldValidationService.ValidateEmail(nEmail);
                ValidationResult phoneValidation = FormFieldValidationService.ValidatePhone(nNumTelefono);

                string tituloError = T("msg_TituloError");

                if (!emailValidation.IsValid)
                {
                    string msgEmail = T(emailValidation);
                    MessageBox.Show(msgEmail, tituloError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!phoneValidation.IsValid)
                {
                    string msgPhone = T(phoneValidation);
                    MessageBox.Show(msgPhone, tituloError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Usuario selectedUsuario = (Usuario)selectedRow.DataBoundItem;
                gestorUsuarios.ModificarUsuario(selectedUsuario.Username, nEmail, nNumTelefono);
                CargarGridUsuarios();
            }
            catch (Exception ex)
            {
                string tituloError = T("msg_TituloError");
                MessageBox.Show(ex.Message, tituloError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gestionUsuariosUIButtonConfirmarEliminarUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewListadoUsuarios.SelectedRows.Count < 1)
                    throw new Exception(T("err_NoUserEliminar"));

                DataGridViewRow selectedRow = dataGridViewListadoUsuarios.SelectedRows[0];
                Usuario selectedUsuario = (Usuario)selectedRow.DataBoundItem;
                gestorUsuarios.EliminarUsuario(selectedUsuario.Username);
                CargarGridUsuarios();
            }
            catch (Exception ex)
            {
                string tituloError = T("msg_TituloError");
                MessageBox.Show(ex.Message, tituloError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
