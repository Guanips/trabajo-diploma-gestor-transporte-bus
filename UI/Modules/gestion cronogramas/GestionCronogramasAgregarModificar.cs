using BE.recorrido_entities;
using BLL.recorrido_components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI.Modules.gestion_cronogramas
{
    public partial class GestionCronogramasAgregarModificar : Form
    {
        private const string formatoHora = "HH:mm";

        private readonly bool creacion;

        public string? descripcionCronograma { get; private set; }
        public DateTime? fechaValidez { get; private set; }
        public TimeOnly? horaInicioCronograma { get; private set; }
        public TimeOnly? horaFinCronograma { get; private set; }
        public int? frecuenciaCronograma { get; private set; }
        public int? tiempoDeDescanso { get; private set; }
        public Ruta? rutaSeleccionada { get; private set; }

        public GestionCronogramasAgregarModificar(bool creacion, DateTime? fechaValidez, string? descripcion, Ruta? ruta, TimeOnly? horaInicio, TimeOnly? horaFin, int? frecuencia, int? tiempoDescanso)
        {
            InitializeComponent();

            this.creacion = creacion;

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterParent;

            this.AcceptButton = buttonCronogramaAgregarModificarConfirmar;

            // La ruta de un cronograma existente no se puede cambiar, sólo se muestra
            this.listBoxRutas.Enabled = creacion;
            this.listBoxRutas.DataSource = GestorRuta.ObtenerRutas();

            if (ruta != null)
            {
                int indiceRuta = ((List<Ruta>)this.listBoxRutas.DataSource).FindIndex(r => r.id == ruta.id);
                if (indiceRuta >= 0)
                {
                    this.listBoxRutas.SelectedIndex = indiceRuta;
                }
            }

            if (descripcion != null)
            {
                this.textBoxDescripcion.Text = descripcion;
            }

            if (fechaValidez != null)
            {
                this.dateTimePickerFechaValidez.Value = fechaValidez.Value;
            }

            if (horaInicio != null)
            {
                this.maskedTextBoxHoraInicio.Text = horaInicio.Value.ToString(formatoHora);
            }

            if (horaFin != null)
            {
                this.maskedTextBoxHoraFin.Text = horaFin.Value.ToString(formatoHora);
            }

            if (frecuencia != null)
            {
                this.numericUpDownFrecuencia.Value = frecuencia.Value;
            }

            if (tiempoDescanso != null)
            {
                this.numericUpDownTiempoDescanso.Value = tiempoDescanso.Value;
            }

            this.descripcionCronograma = descripcion;
            this.fechaValidez = fechaValidez;
            this.horaInicioCronograma = horaInicio;
            this.horaFinCronograma = horaFin;
            this.frecuenciaCronograma = frecuencia;
            this.tiempoDeDescanso = tiempoDescanso;
            this.rutaSeleccionada = ruta;
        }

        private void maskedTextBoxHoraInicio_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void buttonCronogramaAgregarModificarConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(this.textBoxDescripcion.Text))
                {
                    throw new Exception("La descripción del cronograma no puede estar vacía.");
                }

                TimeOnly horaInicio = parsearHora(this.maskedTextBoxHoraInicio, "hora de inicio");
                TimeOnly horaFin = parsearHora(this.maskedTextBoxHoraFin, "hora de fin");

                if (horaFin <= horaInicio)
                {
                    throw new Exception("La hora de fin debe ser posterior a la hora de inicio.");
                }

                if (this.numericUpDownFrecuencia.Value <= 0)
                {
                    throw new Exception("La frecuencia debe ser mayor a 0 minutos.");
                }

                Ruta rutaDelCronograma;

                if (this.creacion)
                {
                    rutaDelCronograma = this.listBoxRutas.SelectedItem as Ruta
                        ?? throw new Exception("Debe seleccionar una ruta para el cronograma.");
                }
                else
                {
                    rutaDelCronograma = this.rutaSeleccionada
                        ?? throw new Exception("No se pudo determinar la ruta del cronograma a modificar.");
                }

                this.descripcionCronograma = this.textBoxDescripcion.Text.Trim();
                this.fechaValidez = this.dateTimePickerFechaValidez.Value;
                this.horaInicioCronograma = horaInicio;
                this.horaFinCronograma = horaFin;
                this.frecuenciaCronograma = (int)this.numericUpDownFrecuencia.Value;
                this.tiempoDeDescanso = (int)this.numericUpDownTiempoDescanso.Value;
                this.rutaSeleccionada = rutaDelCronograma;

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Datos inválidos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // El MaskedTextBox usa el separador horario de la cultura actual, por eso se normaliza antes de parsear
        private static TimeOnly parsearHora(MaskedTextBox maskedTextBoxHora, string nombreCampo)
        {
            string separadorHorario = CultureInfo.CurrentCulture.DateTimeFormat.TimeSeparator;
            string horaSinParsear = maskedTextBoxHora.Text.Trim();

            if (!string.IsNullOrEmpty(separadorHorario) && separadorHorario != ":")
            {
                horaSinParsear = horaSinParsear.Replace(separadorHorario, ":");
            }

            if (!TimeOnly.TryParseExact(horaSinParsear, new[] { formatoHora, "H:mm" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out TimeOnly hora))
            {
                throw new Exception($"La {nombreCampo} no es válida. Complete el horario en formato HH:mm (por ejemplo, 08:30).");
            }

            return hora;
        }

        private void maskedTextBoxHoraFin_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
    }
}
