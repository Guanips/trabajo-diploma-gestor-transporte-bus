namespace UI.Modules
{
    public partial class GestionParadasModificacionUI : Form
    {
        public string descripcionModificada => textBoxParadaModificarDescripcion.Text;
        public string localidadModificada => textBoxParadaModificarLocalidad.Text;
        public string direccionModificada => textBoxParadaModificarDireccion.Text;

        public GestionParadasModificacionUI(string descripcionOriginal, string localidadOriginal, string direccionOriginal)
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;

            this.AcceptButton = buttonParadaModificarConfirmar;

            textBoxParadaModificarDescripcion.Text = descripcionOriginal;
            textBoxParadaModificarLocalidad.Text = localidadOriginal;
            textBoxParadaModificarDireccion.Text = direccionOriginal;
        }

        private void buttonParadaModificarConfirmar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxParadaModificarDescripcion.Text) ||
                string.IsNullOrWhiteSpace(textBoxParadaModificarLocalidad.Text) ||
                string.IsNullOrWhiteSpace(textBoxParadaModificarDireccion.Text))
            {
                throw new Exception("Todos los campos son obligatorios.");
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
