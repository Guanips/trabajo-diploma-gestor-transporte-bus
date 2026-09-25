namespace UI.Modules.gestion_rutas
{
    public partial class GestionRutaAgregarModificarUI : Form
    {
        public string rutaID => textBoxRutaID.Text;
        public string rutaDescripcion => textBoxRutaDescripcion.Text;
        public string rutaSentido { get; private set; } = "";
        public int rutaDistanciaTotalKM { get; private set; } = 0;
        public int rutaTiempoEstimadoMin { get; private set; } = 0;

        public GestionRutaAgregarModificarUI(string? id, string? descripcion, string? sentido, int? distanciaTotalKM, int? tiempoEstimadoMin)
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;

            this.AcceptButton = buttonRutaAltaModificarConfirmar;

            if (id != null)
            {
                textBoxRutaID.Text = id;
                textBoxRutaID.Enabled = false; // Disable editing of the ID field
            }

            if (descripcion != null)
            {
                textBoxRutaDescripcion.Text = descripcion;
            }

            if (sentido != null)
            {
                if (sentido == "Ida")
                {
                    radioButtonRutaAltaModificarSentidoIda.Checked = true;
                }
                else if (sentido == "Vuelta")
                {
                    radioButtonRutaAltaModificarSentidoVuelta.Checked = true;
                }
            }

            if (distanciaTotalKM != null)
            {
                numericUpDownRutaAltaModificarDistanciaTotalKM.Value = distanciaTotalKM.Value;
            }

            if (tiempoEstimadoMin != null)
            {
                numericUpDownRutaAltaModificarTiempoEstimado.Value = tiempoEstimadoMin.Value;
            }
        }

        public GestionRutaAgregarModificarUI() : this(null, null, null, null, null)
        {
        }

        private void buttonRutaAltaModificarConfirmar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBoxRutaID.Text) &&
                !string.IsNullOrWhiteSpace(textBoxRutaDescripcion.Text) &&
                (radioButtonRutaAltaModificarSentidoIda.Checked || radioButtonRutaAltaModificarSentidoVuelta.Checked))
            {
                this.rutaSentido = radioButtonRutaAltaModificarSentidoIda.Checked ? "Ida" : "Vuelta";
                this.rutaDistanciaTotalKM = (int)numericUpDownRutaAltaModificarDistanciaTotalKM.Value;
                this.rutaTiempoEstimadoMin = (int)numericUpDownRutaAltaModificarTiempoEstimado.Value;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Por favor, complete todos los campos obligatorios.", "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
