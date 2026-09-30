namespace UI.Modules.gestion_choferes
{
    public partial class GestionChoferAgregarModificarUI : Form
    {
        public int? dniChofer { get; private set; }
        public string? nombreCompletoChofer { get; private set; }
        public bool? activoChofer { get; private set; }

        public GestionChoferAgregarModificarUI(int? numChofer, int? dni, string? nombreCompleto, bool? activo)
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;

            this.AcceptButton = buttonChoferAltaModificarConfirmar;
            textBoxChoferNum.Enabled = false;

            if (numChofer != null)
            {
                textBoxChoferNum.Text = numChofer.ToString();
            }

            if (dni != null)
            {
                textBoxChoferDni.Text = dni.ToString();
            }

            if (nombreCompleto != null)
            {
                textBoxChoferNombreCompleto.Text = nombreCompleto;
            }

            if (activo != null)
            {
                checkBoxChoferAltaModificarActivo.Checked = activo.Value;
            }
        }

        private void buttonRutaAltaModificarConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                string unparsedDniChofer = textBoxChoferDni.Text.Trim();
                int parsedDniChofer = int.TryParse(unparsedDniChofer, out int dniChofer) ? dniChofer : throw new Exception("DNI de chofer inválido");

                if (string.IsNullOrWhiteSpace(textBoxChoferNombreCompleto.Text))
                {
                    throw new Exception("El nombre completo del chofer no puede estar vacío");
                }

                this.nombreCompletoChofer = textBoxChoferNombreCompleto.Text.Trim();
                this.dniChofer = parsedDniChofer;
                this.activoChofer = checkBoxChoferAltaModificarActivo.Checked;


                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
