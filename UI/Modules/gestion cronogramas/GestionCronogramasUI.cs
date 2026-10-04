using BE;
using BE.cronograma_entities;
using BE.interno_entities;
using BLL;
using BLL.cronograma_components;
using BLL.interno_components;
using Microsoft.VisualBasic;
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
    public partial class GestionCronogramasUI : Form
    {
        private class SalidaCronogramaGridRow
        {
            public Salida Salida { get; }
            public string ChoferAsignado { get; set; }
            public string InternoAsignado { get; set; }
            public string HoraSalidaTeorica { get; set; }
            public string HoraLlegadaTeorica { get; set; }
            public string EstaSuspendida { get; set; }
            public string MotivoSuspension { get; set; }

            public SalidaCronogramaGridRow(Salida salida)
            {
                Salida = salida;
                ChoferAsignado = salida.choferAsignado?.nombreCompleto ?? "";
                InternoAsignado = salida.internoAsignado?.patente ?? "";
                HoraSalidaTeorica = salida.horaSalidaTeorica.ToString("HH:mm");
                HoraLlegadaTeorica = salida.horaLlegadaTeorica.ToString("HH:mm");
                EstaSuspendida = salida.estaSuspendida ? "Sí" : "No";
                MotivoSuspension = salida.motivoSuspension ?? "";
            }
        }

        List<Cronograma> cronogramasActuales;
        Cronograma? cronogramaSeleccionado;

        PlanificacionCronogramaBLL planificacionCronogramaBLL;

        public GestionCronogramasUI()
        {
            InitializeComponent();
            dataGridViewAsignacionSalidas.DataBindingComplete += dataGridViewAsignacionSalidas_DataBindingComplete;
            this.refrescarListaCronogramas();
            cronogramaSeleccionado = null;
            planificacionCronogramaBLL = new PlanificacionCronogramaBLL();
            inicializarComponentes();
        }

        private void inicializarComponentes()
        {
            try
            {
                List<Chofer> choferes = GestorChofer.ObtenerChoferes();
                List<Interno> internos = GestorInterno.ObtenerInternos();

                if (choferes.Count == 0)
                {
                    comboBoxGestionCronogramasChofer.Enabled = false;
                } else
                {
                    comboBoxGestionCronogramasChofer.DataSource = choferes;
                }

                if (internos.Count == 0)
                {
                    comboBoxGestionCronogramasInterno.Enabled = false;
                }
                else
                {
                    comboBoxGestionCronogramasInterno.DataSource = internos;
                }

                ChequearPermisosDeUsuario();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ChequearPermisosDeUsuario ()
        {
            Usuario? usuarioActual = SessionManager.getInstance.ObtenerUsuarioActivo();
            if (usuarioActual == null)
            {
                MessageBox.Show("No hay un usuario activo en la sesión.");
                this.Close();
                return;
            }

            comboBoxGestionCronogramasChofer.Enabled = usuarioActual.Permisos.Any(p => p.ValidarPermiso("PERM-GESTION-CRONOGRAMAS-MODIFICAR"));
            buttonGestionCronogramasAsignarChofer.Enabled = usuarioActual.Permisos.Any(p => p.ValidarPermiso("PERM-GESTION-CRONOGRAMAS-MODIFICAR"));
            buttonGestionCronogramasDesasignarChofer.Enabled = usuarioActual.Permisos.Any(p => p.ValidarPermiso("PERM-GESTION-CRONOGRAMAS-MODIFICAR"));

            comboBoxGestionCronogramasInterno.Enabled = usuarioActual.Permisos.Any(p => p.ValidarPermiso("PERM-GESTION-CRONOGRAMAS-MODIFICAR"));
            buttonGestionCronogramasAsignarInterno.Enabled = usuarioActual.Permisos.Any(p => p.ValidarPermiso("PERM-GESTION-CRONOGRAMAS-MODIFICAR"));
            buttonGestionCronogramasDesasignarInterno.Enabled = usuarioActual.Permisos.Any(p => p.ValidarPermiso("PERM-GESTION-CRONOGRAMAS-MODIFICAR"));

            buttonGestionCronogramasGenerarSalidas.Enabled = usuarioActual.Permisos.Any(p => p.ValidarPermiso("PERM-GESTION-CRONOGRAMAS-MODIFICAR"));
            buttonGestionCronogramasSuspenderSalida.Enabled = usuarioActual.Permisos.Any(p => p.ValidarPermiso("PERM-GESTION-CRONOGRAMAS-MODIFICAR"));

            buttonGestionCronogramaAgregarCronograma.Enabled = usuarioActual.Permisos.Any(p => p.ValidarPermiso("PERM-GESTION-CRONOGRAMAS-MODIFICAR"));
            buttonGestionCronogramasEliminarCronograma.Enabled = usuarioActual.Permisos.Any(p => p.ValidarPermiso("PERM-GESTION-CRONOGRAMAS-MODIFICAR"));
            buttonGestionCronogramasModificarCronograma.Enabled = usuarioActual.Permisos.Any(p => p.ValidarPermiso("PERM-GESTION-CRONOGRAMAS-MODIFICAR"));
        }

        private void buttonGestionCronogramaAgregarCronograma_Click(object sender, EventArgs e)
        {
            try
            {
                using (GestionCronogramasAgregarModificar dialog = new GestionCronogramasAgregarModificar(true, null, null, null, null, null, null, null))
                {
                    dialog.ShowDialog(this);

                    if (dialog.DialogResult != DialogResult.OK)
                    {
                        return;
                    }

                    if (dialog.descripcionCronograma == null || dialog.fechaValidez == null || dialog.horaInicioCronograma == null ||
                        dialog.horaFinCronograma == null || dialog.frecuenciaCronograma == null || dialog.tiempoDeDescanso == null ||
                        dialog.rutaSeleccionada == null)
                    {
                        throw new Exception("Todos los campos son obligatorios.");
                    }

                    Cronograma nCronograma = new Cronograma(
                        // El id es IDENTITY, la base de datos lo asigna al insertar
                        -1,
                        dialog.descripcionCronograma,
                        DateOnly.FromDateTime(dialog.fechaValidez.Value),
                        dialog.horaInicioCronograma.Value,
                        dialog.horaFinCronograma.Value,
                        dialog.frecuenciaCronograma.Value,
                        dialog.tiempoDeDescanso.Value,
                        dialog.rutaSeleccionada
                    );

                    GestorCronograma.InsertarCronograma(nCronograma);
                    this.refrescarListaCronogramas();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar el cronograma: {ex.Message}");
            }
        }

        private void buttonGestionCronogramasModificarCronograma_Click(object sender, EventArgs e)
        {
            try
            {
                cronogramaSeleccionado = obtenerCronogramaSeleccionado();
                if (cronogramaSeleccionado == null)
                {
                    MessageBox.Show("No se ha seleccionado ningún cronograma para modificar.");
                    return;
                }

                using (GestionCronogramasAgregarModificar dialog = new GestionCronogramasAgregarModificar(
                    false,
                    cronogramaSeleccionado.fechaValidez.ToDateTime(TimeOnly.MinValue),
                    cronogramaSeleccionado.descripcion,
                    cronogramaSeleccionado.ruta,
                    cronogramaSeleccionado.horaInicio,
                    cronogramaSeleccionado.horaFin,
                    cronogramaSeleccionado.frecuenciaMinutos,
                    cronogramaSeleccionado.tiempoDescansoMinutos))
                {
                    dialog.ShowDialog(this);

                    if (dialog.DialogResult != DialogResult.OK)
                    {
                        return;
                    }

                    if (dialog.descripcionCronograma == null || dialog.fechaValidez == null || dialog.horaInicioCronograma == null ||
                        dialog.horaFinCronograma == null || dialog.frecuenciaCronograma == null || dialog.tiempoDeDescanso == null)
                    {
                        throw new Exception("Todos los campos son obligatorios.");
                    }

                    // La ruta de un cronograma existente no se modifica, por eso se conserva la original
                    Cronograma cronogramaModificado = new Cronograma(
                        cronogramaSeleccionado.id,
                        dialog.descripcionCronograma,
                        DateOnly.FromDateTime(dialog.fechaValidez.Value),
                        dialog.horaInicioCronograma.Value,
                        dialog.horaFinCronograma.Value,
                        dialog.frecuenciaCronograma.Value,
                        dialog.tiempoDeDescanso.Value,
                        cronogramaSeleccionado.ruta
                    );

                    GestorCronograma.ModificarCronograma(cronogramaModificado);
                    this.refrescarListaCronogramas();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al modificar el cronograma: {ex.Message}");
            }
        }

        private void buttonGestionCronogramasEliminarCronograma_Click(object sender, EventArgs e)
        {
            try
            {
                Cronograma? cronogramaSeleccionado = obtenerCronogramaSeleccionado();
                if (cronogramaSeleccionado == null)
                {
                    MessageBox.Show("No se ha seleccionado ningún cronograma para eliminar.");
                    return;
                }

                DialogResult confirmacion = MessageBox.Show(
                    $"¿Está seguro de que quiere eliminar el cronograma {cronogramaSeleccionado.id} ({cronogramaSeleccionado.descripcion})?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmacion == DialogResult.Yes)
                {
                    GestorCronograma.EliminarCronograma(cronogramaSeleccionado.id);
                    this.refrescarListaCronogramas();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar el cronograma: {ex.Message}");
            }
        }

        private void listBoxCronogramas_SelectedIndexChanged(object sender, EventArgs e)
        {
            cronogramaSeleccionado = listBoxCronogramas.SelectedItem as Cronograma;
            this.refrescarDetalleCronograma();
            this.mostrarSalidasCronograma();
        }

        private void mostrarSalidasCronograma ()
        {
            dataGridViewAsignacionSalidas.AutoGenerateColumns = false;
            dataGridViewAsignacionSalidas.Columns.Clear();

            Cronograma? cronogramaActual = obtenerCronogramaSeleccionado();

            if (cronogramaActual == null)
            {
                dataGridViewAsignacionSalidas.DataSource = null;
                return;
            }

            var filas = cronogramaActual.salidas
                .Select(salida => new SalidaCronogramaGridRow(salida))
                .ToList();

            dataGridViewAsignacionSalidas.DataSource = filas;

            dataGridViewAsignacionSalidas.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(SalidaCronogramaGridRow.ChoferAsignado),
                HeaderText = "Chofer asignado",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dataGridViewAsignacionSalidas.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(SalidaCronogramaGridRow.InternoAsignado),
                HeaderText = "Interno asignado",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dataGridViewAsignacionSalidas.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(SalidaCronogramaGridRow.HoraSalidaTeorica),
                HeaderText = "Hora salida teórica",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dataGridViewAsignacionSalidas.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(SalidaCronogramaGridRow.HoraLlegadaTeorica),
                HeaderText = "Hora llegada teórica",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dataGridViewAsignacionSalidas.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(SalidaCronogramaGridRow.EstaSuspendida),
                HeaderText = "Está suspendida",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dataGridViewAsignacionSalidas.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(SalidaCronogramaGridRow.MotivoSuspension),
                HeaderText = "Motivo",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            AplicarFormatoSalidas();
        }

        private void AplicarFormatoSalidas()
        {
            if (dataGridViewAsignacionSalidas.Columns.Count < 2)
            {
                return;
            }

            foreach (DataGridViewRow fila in dataGridViewAsignacionSalidas.Rows)
            {
                if (fila.IsNewRow || fila.DataBoundItem is not SalidaCronogramaGridRow salidaFila)
                {
                    continue;
                }

                fila.DefaultCellStyle.BackColor = Color.White;
                fila.DefaultCellStyle.ForeColor = Color.Black;
                fila.DefaultCellStyle.SelectionBackColor = Color.White;
                fila.DefaultCellStyle.SelectionForeColor = Color.Black;

                if (salidaFila.Salida.estaSuspendida)
                {
                    fila.DefaultCellStyle.BackColor = Color.LightCoral;
                    fila.DefaultCellStyle.SelectionBackColor = Color.LightCoral;
                    continue;
                }

                DataGridViewCell celdaChofer = fila.Cells[0];
                DataGridViewCell celdaInterno = fila.Cells[1];

                Color colorChofer = string.IsNullOrWhiteSpace(salidaFila.ChoferAsignado) ? Color.Orange : Color.White;
                Color colorInterno = string.IsNullOrWhiteSpace(salidaFila.InternoAsignado) ? Color.Orange : Color.White;

                celdaChofer.Style.BackColor = colorChofer;
                celdaChofer.Style.SelectionBackColor = colorChofer;

                celdaInterno.Style.BackColor = colorInterno;
                celdaInterno.Style.SelectionBackColor = colorInterno;
            }
        }

        private void dataGridViewAsignacionSalidas_DataBindingComplete(object? sender, DataGridViewBindingCompleteEventArgs e)
        {
            AplicarFormatoSalidas();
        }

        private void refrescarDetalleCronograma()
        {
            if (cronogramaSeleccionado != null)
            {
                textBoxGestionCronogramaDetalleId.Text = cronogramaSeleccionado.id.ToString();
                textBoxGestionCronogramaDetalleDescripcion.Text = cronogramaSeleccionado.descripcion;
                textBoxGestionCronogramaDetalleFecha.Text = cronogramaSeleccionado.fechaValidez.ToString("dd/MM/yyyy");
                textBoxGestionCronogramaDetalleHoraInicio.Text = cronogramaSeleccionado.horaInicio.ToString("HH:mm");
                textBoxGestionCronogramaDetalleHoraFinal.Text = cronogramaSeleccionado.horaFin.ToString("HH:mm");
                textBoxGestionCronogramaDetalleFrecuencia.Text = cronogramaSeleccionado.frecuenciaMinutos.ToString();
                textBoxGestionCronogramaDetalleDescanso.Text = cronogramaSeleccionado.tiempoDescansoMinutos.ToString();
                textBoxGestionCronogramaDetalleRuta.Text = cronogramaSeleccionado.ruta.ToString();
            }
            else
            {
                textBoxGestionCronogramaDetalleId.Text = "";
                textBoxGestionCronogramaDetalleDescripcion.Text = "";
                textBoxGestionCronogramaDetalleFecha.Text = "";
                textBoxGestionCronogramaDetalleHoraInicio.Text = "";
                textBoxGestionCronogramaDetalleHoraFinal.Text = "";
                textBoxGestionCronogramaDetalleFrecuencia.Text = "";
                textBoxGestionCronogramaDetalleDescanso.Text = "";
                textBoxGestionCronogramaDetalleRuta.Text = "";
            }
        }

        private void buttonGestionCronogramasGenerarSalidas_Click(object sender, EventArgs e)
        {
            try
            {
                Cronograma? cronograma = obtenerCronogramaSeleccionado();
                if (cronograma == null) return;

                if (cronograma.salidas.Count > 0)
                {
                    MessageBox.Show("El cronograma ya tiene salidas generadas, no se pueden generar automaticamente salidas nuevas.");
                    return;
                }

                List<Salida> salidas = planificacionCronogramaBLL.AutogenerarSalidasDeCronograma(cronograma);

                cronograma.salidas.Clear();
                foreach (var salida in salidas)
                {
                    cronograma.AgregarSalida(salida);
                }

                this.mostrarSalidasCronograma();

                MessageBox.Show("Las salidas fueron generadas exitosamente");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonGestionCronogramasSuspenderSalida_Click(object sender, EventArgs e)
        {
            try
            {
                Salida salida = obtenerSalidaSeleccionada();
                if (salida.estaSuspendida)
                {
                    MessageBox.Show("La salida seleccionada ya está suspendida.");
                    return;
                }

                if (MessageBox.Show("¿Está seguro de que desea suspender la salida seleccionada?", "Confirmar suspensión", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }

                string motivoSuspensión = Interaction.InputBox("Ingrese el motivo de la suspensión:", "Suspender salida", "");
                salida.ToggleSuspension(true, motivoSuspensión);

                // desasignar chofer e interno si estaban asignados ya que ahora la salida está suspendida, por lo que se liberan
                if (salida.choferAsignado != null) GestorSalida.AsignarChofer(salida.id, null);
                if (salida.internoAsignado != null) GestorSalida.AsignarInterno(salida.id, null);

                GestorSalida.SuspenderSalida(salida.id, motivoSuspensión);
                this.mostrarSalidasCronograma();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonGestionCronogramasDesasignarChofer_Click(object sender, EventArgs e)
        {
            try
            {
                Salida salida = obtenerSalidaSeleccionada();

                if (salida.choferAsignado == null)
                {
                    MessageBox.Show("La salida seleccionada no tiene chofer asignado.");
                    return;
                }

                planificacionCronogramaBLL.DesasignarChoferDeSalida(salida);
                this.mostrarSalidasCronograma();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonGestionCronogramasDesasignarInterno_Click(object sender, EventArgs e)
        {
            try
            {
                Salida salida = obtenerSalidaSeleccionada();

                if (salida.internoAsignado == null)
                {
                    MessageBox.Show("La salida seleccionada no tiene interno asignado.");
                    return;
                }

                planificacionCronogramaBLL.DesasignarInternoDeSalida(salida);
                this.mostrarSalidasCronograma();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonGestionCronogramasAsignarChofer_Click(object sender, EventArgs e)
        {
            try
            {
                Cronograma? cronograma = obtenerCronogramaSeleccionado();
                if (cronograma == null) return;
                Salida salida = obtenerSalidaSeleccionada();
                Chofer chofer = comboBoxGestionCronogramasChofer.SelectedItem as Chofer
                    ?? throw new Exception("No se ha seleccionado ningún chofer.");

                if (salida.choferAsignado?.num_chofer == chofer.num_chofer)
                {
                    MessageBox.Show("El chofer seleccionado ya está asignado a la salida.");
                    return;
                }

                List<Salida> conflictos = planificacionCronogramaBLL.ObtenerConflictosChofer(cronograma, salida, chofer);

                if (conflictos.Count == 0)
                {
                    planificacionCronogramaBLL.AsignarChoferASalida(cronograma, salida, chofer);
                }
                else
                {
                    if (!ConfirmarReasignacion($"El chofer {chofer.nombreCompleto}", conflictos))
                    {
                        return;
                    }

                    List<Salida> liberadas = planificacionCronogramaBLL.ReasignarChoferASalida(cronograma, salida, chofer);
                    SincronizarSalidasLiberadas(liberadas);
                }

                this.mostrarSalidasCronograma();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonGestionCronogramasAsignarInterno_Click(object sender, EventArgs e)
        {
            try
            {
                Cronograma? cronograma = obtenerCronogramaSeleccionado();
                if (cronograma == null) return;
                Salida salida = obtenerSalidaSeleccionada();
                Interno interno = comboBoxGestionCronogramasInterno.SelectedItem as Interno
                    ?? throw new Exception("No se ha seleccionado ningún interno.");

                if (salida.internoAsignado?.num_interno == interno.num_interno)
                {
                    MessageBox.Show("El interno seleccionado ya está asignado a la salida.");
                    return;
                }

                List<Salida> conflictos = planificacionCronogramaBLL.ObtenerConflictosInterno(cronograma, salida, interno);

                if (conflictos.Count == 0)
                {
                    planificacionCronogramaBLL.AsignarInternoASalida(cronograma, salida, interno);
                }
                else
                {
                    if (!ConfirmarReasignacion($"El interno {interno.patente}", conflictos))
                    {
                        return;
                    }

                    List<Salida> liberadas = planificacionCronogramaBLL.ReasignarInternoASalida(cronograma, salida, interno);
                    SincronizarSalidasLiberadas(liberadas);
                }

                this.mostrarSalidasCronograma();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Pregunta al usuario si desea liberar al recurso de las salidas en conflicto para reasignarlo
        private bool ConfirmarReasignacion(string descripcionRecurso, List<Salida> salidasEnConflicto)
        {
            string encabezado = salidasEnConflicto.Count == 1
                ? $"{descripcionRecurso} ya está asignado a la siguiente salida, que se solapa con la seleccionada:"
                : $"{descripcionRecurso} ya está asignado a las siguientes salidas, que se solapan con la seleccionada:";

            string liberacion = salidasEnConflicto.Count == 1
                ? "Se liberará de la salida indicada."
                : "Se liberará de las salidas indicadas.";

            string detalle = string.Join("\n", salidasEnConflicto.Select(s =>
                $"- Salida de {s.horaSalidaTeorica:HH:mm} a {s.horaLlegadaTeorica:HH:mm}"));

            string mensaje = $"{encabezado}\n\n{detalle}\n\n¿Desea reasignarlo a la salida seleccionada? {liberacion}";

            return MessageBox.Show(mensaje, "Conflicto de asignación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
        }

        // Refleja en los cronogramas cargados en memoria las desasignaciones hechas en la base de datos
        private void SincronizarSalidasLiberadas(List<Salida> liberadas)
        {
            if (liberadas.Count == 0 || cronogramasActuales == null)
            {
                return;
            }

            List<Salida> salidasEnMemoria = cronogramasActuales.SelectMany(c => c.salidas).ToList();

            foreach (Salida liberada in liberadas)
            {
                Salida? enMemoria = salidasEnMemoria.FirstOrDefault(s => s.id == liberada.id);
                if (enMemoria == null)
                {
                    continue;
                }

                if (liberada.choferAsignado == null) enMemoria.DesasignarChofer();
                if (liberada.internoAsignado == null) enMemoria.DesasignarInterno();
            }
        }

        private Cronograma? obtenerCronogramaSeleccionado() 
        {
            return listBoxCronogramas.SelectedItem as Cronograma ?? null;
        }

        private Salida obtenerSalidaSeleccionada ()
        {
            return (dataGridViewAsignacionSalidas.CurrentRow?.DataBoundItem as SalidaCronogramaGridRow)?.Salida
                ?? throw new Exception("No se ha seleccionado ninguna salida.");
        }

        private void refrescarListaCronogramas()
        {
            try
            {
                cronogramasActuales = GestorCronograma.ObtenerCronogramas();
                listBoxCronogramas.DataSource = null;

                if (cronogramasActuales.Count > 0)
                {
                    listBoxCronogramas.DataSource = cronogramasActuales;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al refrescar los cronogramas: {ex.Message}");
            }
        }
    }
}
