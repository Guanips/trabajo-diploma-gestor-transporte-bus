using BE.cronograma_entities;
using BE.interno_entities;
using BLL.cronograma_components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI.Modules.gestion_cronogramas
{
    public partial class AuditoriaSalidasUI : Form
    {
        public AuditoriaSalidasUI()
        {
            InitializeComponent();

            try
            {
                List<Cronograma> cronogramasDisponibles = GestorCronograma.ObtenerCronogramas();
                if (cronogramasDisponibles.Count == 0)
                {
                    MessageBox.Show("No hay cronogramas disponibles para auditar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    return;
                }
                listBoxCronogramas.DataSource = cronogramasDisponibles;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonAuditoriaCronogramasConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                Salida? selectedSalida = dataGridViewSalidasCronogramaSeleccionado.CurrentRow?.DataBoundItem as Salida;
                if (selectedSalida != null)
                {
                    Chofer? choferAsignadoSalida = selectedSalida.choferAsignado;
                    if (choferAsignadoSalida == null)
                    {
                        textBoxNombreChofer.Text = "";
                        MessageBox.Show("La salida seleccionada no tiene un chofer asignado.");
                        return;
                    }

                    string motivoSancion = textBoxMotivoSancion.Text.Trim();
                    if (string.IsNullOrEmpty(motivoSancion))
                    {
                        MessageBox.Show("Debe ingresar un motivo de sanción.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    DateOnly fechaSancion = DateOnly.FromDateTime(dateTimePickerFechaSancion.Value);
                    Sancion sancion = new Sancion(-1, motivoSancion, fechaSancion, choferAsignadoSalida);

                    AuditorCronogramasBLL.InsertarSancion(sancion);
                    textBoxMotivoSancion.Clear();
                    MessageBox.Show("Sanción registrada exitosamente.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonAuditoriaCronogramasRegistrarLlegada_Click(object sender, EventArgs e)
        {
            try
            {
                Salida? selectedSalida = dataGridViewSalidasCronogramaSeleccionado.CurrentRow?.DataBoundItem as Salida;
                if (selectedSalida == null)
                {
                    MessageBox.Show("Debe seleccionar una salida.");
                    return;
                }

                TimeOnly horaLlegadaReal = TimeOnly.FromDateTime(dateTimePickerHoraLlegadaReal.Value);
                AuditorCronogramasBLL.ActualizarHoraLlegadaReal(selectedSalida.id, horaLlegadaReal);
                selectedSalida.ActualizarHoraLlegadaReal(horaLlegadaReal);

                // Se vuelve a enlazar la lista para refrescar la grilla
                Cronograma? selectedCronograma = listBoxCronogramas.SelectedItem as Cronograma;
                dataGridViewSalidasCronogramaSeleccionado.DataSource = null;
                dataGridViewSalidasCronogramaSeleccionado.DataSource = selectedCronograma?.salidas;

                MessageBox.Show("Hora de llegada real registrada exitosamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Solo cambia el aspecto de la grilla: resalta las salidas cuya llegada real supera a la teórica por más de la tolerancia
        private void dataGridViewSalidasCronogramaSeleccionado_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dataGridViewSalidasCronogramaSeleccionado.Rows[e.RowIndex].DataBoundItem is Salida salida && TieneRetraso(salida))
            {
                e.CellStyle.BackColor = Color.MistyRose;
                e.CellStyle.ForeColor = Color.DarkRed;
            }
        }

        private bool TieneRetraso(Salida salida)
        {
            if (salida.horaLlegadaReal == null) return false;

            TimeSpan retraso = salida.horaLlegadaReal.Value - salida.horaLlegadaTeorica;
            return retraso.TotalMinutes > (double)numericUpDownToleranciaRetraso.Value;
        }

        private void numericUpDownToleranciaRetraso_ValueChanged(object sender, EventArgs e)
        {
            dataGridViewSalidasCronogramaSeleccionado.Invalidate();
        }

        private void listBoxCronogramas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Cronograma? selectedCronograma = listBoxCronogramas.SelectedItem as Cronograma;
                if (selectedCronograma != null)
                {
                    List<Salida> salidas = selectedCronograma.salidas;
                    dataGridViewSalidasCronogramaSeleccionado.DataSource = salidas;
                }
                else
                {
                    dataGridViewSalidasCronogramaSeleccionado.DataSource = null;
                    textBoxNombreChofer.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridViewSalidasCronogramaSeleccionado_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                Salida? selectedSalida = dataGridViewSalidasCronogramaSeleccionado.CurrentRow?.DataBoundItem as Salida;
                if (selectedSalida != null)
                {
                    Chofer? choferAsignadoSalida = selectedSalida.choferAsignado;
                    if (choferAsignadoSalida != null)
                    {
                        textBoxNombreChofer.Text = choferAsignadoSalida.nombreCompleto;
                    }
                    else
                    {
                        textBoxNombreChofer.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
