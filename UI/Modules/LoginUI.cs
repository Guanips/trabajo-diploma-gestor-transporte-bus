using BE;
using BLL;
using servicios;

namespace UI.Login
{
    public partial class LoginUI : FormBaseObserver
    {
        // Mensajes del formulario (MessageBox y textos armados por código) en el idioma por defecto
        private static readonly Dictionary<string, string> MensajesFormulario = new()
        {
            ["msg_InicioSesionExito"] = "Inicio de sesión exitoso."
        };

        protected override IReadOnlyDictionary<string, string> Mensajes => MensajesFormulario;


        private GestorLogin gestorLogin;
        public event EventHandler? SesionIniciada;

        public LoginUI()
        {
            InitializeComponent();
            gestorLogin = new GestorLogin();
            this.AcceptButton = loginUIButtonIniciarSesion;

        }

        private void loginUIButtonIniciarSesion_Click(object sender, EventArgs e)
        {
            try
            {
                string nUsername = textBoxUsername.Text;
                string nPassword = textBoxContrasena.Text;

                ValidationResult usernameValidationResult = FormFieldValidationService.ValidateUsername(nUsername);
                bool isPasswordEmpty = string.IsNullOrWhiteSpace(nPassword);

                if (!usernameValidationResult.IsValid)
                {
                    string msgUser = T(usernameValidationResult);
                    throw new Exception(msgUser);
                }

                if (isPasswordEmpty)
                {
                    string msgPass = T("err_PassVacia");
                    throw new Exception(msgPass);
                }

                gestorLogin.LogIn(nUsername, nPassword);

                string mensaje = T("msg_InicioSesionExito");
                string titulo = T("msg_TituloExito");

                MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                SesionIniciada?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                string tituloError = T("msg_TituloError");
                MessageBox.Show(ex.Message, tituloError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
