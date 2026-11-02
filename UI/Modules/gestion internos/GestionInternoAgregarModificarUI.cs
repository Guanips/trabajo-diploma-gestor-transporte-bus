using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI.Modules.gestion_internos
{
    public partial class GestionInternoAgregarModificarUI : FormBaseObserver
    {
        // Mensajes del formulario (MessageBox y textos armados por código) en el idioma por defecto
        private static readonly Dictionary<string, string> MensajesFormulario = new()
        {
            ["msg_ElModeloDelInternoNoPuedeEstarVacio"] = "El modelo del interno no puede estar vacío",
            ["msg_LaPatenteDelInternoNoPuedeEstarVacia"] = "La patente del interno no puede estar vacía"
        };

        protected override IReadOnlyDictionary<string, string> Mensajes => MensajesFormulario;

        public string? patenteInterno { get; private set; }
        public string? modeloInterno { get; private set; }
        public DateOnly? fechaIncorporacionInterno { get; private set; }
        public bool? disponibleInterno { get; private set; }

        public GestionInternoAgregarModificarUI(int? numInterno, string? patente, string? modelo, DateOnly? fechaIncorporacion, bool? disponible)
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterParent;

            this.AcceptButton = buttonInternoAltaModificarConfirmar;

            // El num_interno es IDENTITY: nunca se ingresa a mano, ni en el alta ni en la modificación
            textBoxInternoNum.Enabled = false;

            if (numInterno != null)
            {
                textBoxInternoNum.Text = numInterno.ToString();
            }

            if (patente != null)
            {
                textBoxInternoPatente.Text = patente;
            }

            if (modelo != null)
            {
                textBoxInternoModelo.Text = modelo;
            }

            if (fechaIncorporacion != null)
            {
                dateTimePickerInternoFechaIncorporacion.Value = fechaIncorporacion.Value.ToDateTime(TimeOnly.MinValue);
            }

            if (disponible != null)
            {
                checkBoxInternoAltaModificarDisponible.Checked = disponible.Value;
            }
        }

        private void buttonInternoAltaModificarConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBoxInternoPatente.Text))
                {
                    throw new Exception(T("msg_LaPatenteDelInternoNoPuedeEstarVacia"));
                }

                if (string.IsNullOrWhiteSpace(textBoxInternoModelo.Text))
                {
                    throw new Exception(T("msg_ElModeloDelInternoNoPuedeEstarVacio"));
                }

                this.patenteInterno = textBoxInternoPatente.Text.Trim();
                this.modeloInterno = textBoxInternoModelo.Text.Trim();
                this.fechaIncorporacionInterno = DateOnly.FromDateTime(dateTimePickerInternoFechaIncorporacion.Value);
                this.disponibleInterno = checkBoxInternoAltaModificarDisponible.Checked;

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
