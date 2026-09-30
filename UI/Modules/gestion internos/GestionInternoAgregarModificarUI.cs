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
    public partial class GestionInternoAgregarModificarUI : Form
    {
        public int? numeroInterno { get; private set; }
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

            if (numInterno != null)
            {
                textBoxInternoNum.Text = numInterno.ToString();
                textBoxInternoNum.Enabled = false; // El num_interno es IDENTITY, no se modifica
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
                string unparsedNumInterno = textBoxInternoNum.Text.Trim();

                int parsedNumInterno = int.TryParse(unparsedNumInterno, out int numInterno) ? numInterno : throw new Exception("Número de interno inválido");

                if (string.IsNullOrWhiteSpace(textBoxInternoPatente.Text))
                {
                    throw new Exception("La patente del interno no puede estar vacía");
                }

                if (string.IsNullOrWhiteSpace(textBoxInternoModelo.Text))
                {
                    throw new Exception("El modelo del interno no puede estar vacío");
                }

                this.numeroInterno = parsedNumInterno;
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
