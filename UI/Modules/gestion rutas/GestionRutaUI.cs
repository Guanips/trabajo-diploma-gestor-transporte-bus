using BE.recorrido_entities;
using BLL.recorrido_components;

namespace UI.Modules.gestion_rutas
{
    public partial class GestionRutaUI : Form
    {
        private List<Ruta> rutasDisponibles;
        private List<Parada> paradasDisponibles;

        public GestionRutaUI()
        {
            InitializeComponent();
            this.refrescarRutas();
            this.paradasDisponibles = GestorParada.ObtenerParadas().FindAll(parada => parada.habilitada);
            listBoxRutaParadasDisponibles.DataSource = paradasDisponibles;
        }

        private void buttonRutaAlta_Click(object sender, EventArgs e)
        {
            try
            {
                using (var modal = new GestionRutaAgregarModificarUI())
                {
                    if (modal.ShowDialog() == DialogResult.OK)
                    {
                        string rutaID = modal.rutaID;
                        string rutaDescripcion = modal.rutaDescripcion;
                        string rutaSentido = modal.rutaSentido;
                        int rutaDistanciaTotalKM = modal.rutaDistanciaTotalKM;
                        int rutaTiempoEstimadoMin = modal.rutaTiempoEstimadoMin;

                        Ruta nRuta = new Ruta(rutaID, rutaDescripcion, rutaSentido, rutaDistanciaTotalKM, rutaTiempoEstimadoMin);
                        GestorRuta.InsertarRuta(nRuta);
                        this.refrescarRutas();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear la ruta: {ex.Message}");
            }
        }

        private void buttonRutaEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedRutaId = dataGridViewRutaDisponibles.CurrentRow?.Cells["id"].Value?.ToString() ?? throw new Exception("No se ha seleccionado ninguna ruta.");
                Ruta selectedRuta = rutasDisponibles.FirstOrDefault(r => r.id == selectedRutaId) ?? throw new Exception("Ruta no encontrada.");

                if (MessageBox.Show("¿Estas seguro de que quieres eliminar esta ruta?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    GestorRuta.EliminarRuta(selectedRutaId);
                    this.refrescarRutas();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar la ruta: {ex.Message}");
            }
        }

        private void buttonRutaModificar_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedRutaId = dataGridViewRutaDisponibles.CurrentRow?.Cells["id"].Value?.ToString() ?? throw new Exception("No se ha seleccionado ninguna ruta.");
                Ruta selectedRuta = rutasDisponibles.FirstOrDefault(r => r.id == selectedRutaId) ?? throw new Exception("Ruta no encontrada.");

                using (var modal = new GestionRutaAgregarModificarUI(selectedRutaId, selectedRuta.descripcion, selectedRuta.sentido, selectedRuta.distanciaTotalKM, selectedRuta.tiempoEstimadoMin))
                {
                    if (modal.ShowDialog() == DialogResult.OK)
                    {
                        Ruta nRuta = new Ruta(selectedRutaId, modal.rutaDescripcion, modal.rutaSentido, modal.rutaDistanciaTotalKM, modal.rutaTiempoEstimadoMin);
                        GestorRuta.ActualizarRuta(nRuta);
                        this.refrescarRutas();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar la ruta: {ex.Message}");
            }
        }

        private void buttonRutaAsignarParada_Click(object sender, EventArgs e)
        {
            try
            {
                string? selectedRutaId = dataGridViewRutaDisponibles.CurrentRow?.Cells["id"].Value?.ToString();
                if (selectedRutaId == null)
                {
                    throw new Exception("No se ha seleccionado ninguna ruta.");
                }

                Parada selectedParada = listBoxRutaParadasDisponibles.SelectedItem is Parada p ? p : throw new Exception("No se ha seleccionado ninguna parada.");
                Ruta selectedRuta = rutasDisponibles.FirstOrDefault(r => r.id == selectedRutaId) ?? throw new Exception("Ruta no encontrada.");

                if (selectedRuta.recorrido.Any(p => p.id == selectedParada.id))
                {
                    throw new Exception("La parada ya está asignada a la ruta.");
                }

                selectedRuta.AgregarParada(selectedParada);

                int ordenParada = selectedRuta.recorrido.FindIndex(p => p.id == selectedParada.id) + 1;
                GestorRuta.AgregarParadaEnRuta(selectedParada, selectedRuta, ordenParada);
                this.refrescarParadasDeRutaSeleccionada();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al asignar la parada a la ruta: {ex.Message}");
            }
        }

        private void buttonRutaSubirParada_Click(object sender, EventArgs e)
        {
            try
            {
                string? selectedRutaId = dataGridViewRutaDisponibles.CurrentRow?.Cells["id"].Value?.ToString();
                if (selectedRutaId == null)
                {
                    throw new Exception("No se ha seleccionado ninguna ruta.");
                }

                Ruta selectedRuta = rutasDisponibles.FirstOrDefault(r => r.id == selectedRutaId) ?? throw new Exception("Ruta no encontrada.");
                Parada selectedParada = listBoxRutaParadasDeRuta.SelectedItem is Parada p ? p : throw new Exception("No se ha seleccionado ninguna parada.");

                int currentIndex = selectedRuta.recorrido.FindIndex(p => p.id == selectedParada.id);
                if (currentIndex == -1)
                {
                    throw new Exception("La parada no existe en el recorrido de la ruta seleccionada.");
                }

                if (currentIndex == 0)
                {
                    // Ya está en la primera posición
                    return;
                }

                int nuevaPosicion = currentIndex - 1;

                // Guardar id para mantener selección
                string selectedParadaId = selectedParada.id;

                // Actualizar en la entidad en memoria
                selectedRuta.ModificarOrdenParada(nuevaPosicion, selectedParada);

                // Persistir el nuevo orden (la capa espera orden 1-based)
                GestorRuta.ModificarOrdenParadaEnRuta(selectedParada, selectedRuta, nuevaPosicion + 1);

                this.refrescarParadasDeRutaSeleccionada(selectedParadaId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al mover la parada hacia arriba: {ex.Message}");
            }
        }

        private void buttonRutaBajarParada_Click(object sender, EventArgs e)
        {
            try
            {
                string? selectedRutaId = dataGridViewRutaDisponibles.CurrentRow?.Cells["id"].Value?.ToString();
                if (selectedRutaId == null)
                {
                    throw new Exception("No se ha seleccionado ninguna ruta.");
                }

                Ruta selectedRuta = rutasDisponibles.FirstOrDefault(r => r.id == selectedRutaId) ?? throw new Exception("Ruta no encontrada.");
                Parada selectedParada = listBoxRutaParadasDeRuta.SelectedItem is Parada p ? p : throw new Exception("No se ha seleccionado ninguna parada.");

                int currentIndex = selectedRuta.recorrido.FindIndex(p => p.id == selectedParada.id);
                if (currentIndex == -1)
                {
                    throw new Exception("La parada no existe en el recorrido de la ruta seleccionada.");
                }

                if (currentIndex >= selectedRuta.recorrido.Count - 1)
                {
                    // Ya está en la última posición
                    return;
                }

                int nuevaPosicion = currentIndex + 1;

                // Guardar id para mantener selección
                string selectedParadaId = selectedParada.id;

                // Actualizar en la entidad en memoria
                selectedRuta.ModificarOrdenParada(nuevaPosicion, selectedParada);

                // Persistir el nuevo orden (la capa espera orden 1-based)
                GestorRuta.ModificarOrdenParadaEnRuta(selectedParada, selectedRuta, nuevaPosicion + 1);

                this.refrescarParadasDeRutaSeleccionada(selectedParadaId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al mover la parada hacia abajo: {ex.Message}");
            }
        }

        private void dataGridViewRutaDisponibles_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                this.refrescarParadasDeRutaSeleccionada();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al seleccionar la ruta: {ex.Message}");
            }
        }

        private void refrescarRutas()
        {
            try
            {
                rutasDisponibles = GestorRuta.ObtenerRutas();
                dataGridViewRutaDisponibles.DataSource = null;
                dataGridViewRutaDisponibles.DataSource = rutasDisponibles;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al refrescar las rutas: {ex.Message}");
            }
        }

        private void refrescarParadasDeRutaSeleccionada(string? selectedParadaId = null)
        {
            try
            {
                listBoxRutaParadasDeRuta.DataSource = null;

                string? selectedRutaId = dataGridViewRutaDisponibles.CurrentRow?.Cells["id"].Value?.ToString();
                if (selectedRutaId != null)
                {
                    Ruta? selectedRuta = rutasDisponibles.FirstOrDefault(r => r.id == selectedRutaId);
                    if (selectedRuta != null)
                    {
                        listBoxRutaParadasDeRuta.DataSource = selectedRuta.recorrido;

                        if (!string.IsNullOrEmpty(selectedParadaId))
                        {
                            int index = selectedRuta.recorrido.FindIndex(p => p.id == selectedParadaId);
                            if (index >= 0 && index < listBoxRutaParadasDeRuta.Items.Count)
                            {
                                listBoxRutaParadasDeRuta.SelectedIndex = index;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al refrescar las paradas de la ruta seleccionada: {ex.Message}");
            }
        }
    }
}
