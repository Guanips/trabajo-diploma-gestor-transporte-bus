using BE.recorrido_entities;
using BLL.recorrido_components;

namespace UI.Modules
{
    public partial class GestionParadasUI : FormBaseObserver
    {
        private List<Parada> paradasDisponibles;

        public GestionParadasUI()
        {
            InitializeComponent();
            this.AcceptButton = buttonParadaAltaConfirmar;
            this.refrescarParadas();
        }

        private void refrescarParadas()
        {
            paradasDisponibles = GestorParada.ObtenerParadas();
            dataGridViewParadas.DataSource = null;
            dataGridViewParadas.DataSource = paradasDisponibles;
        }

        private void buttonParadaAltaConfirmar_Click(object sender, EventArgs e)
        {
            try
            {

                string id = textBoxParadaAltaID.Text;
                string descripcion = textBoxParadaAltaDescripcion.Text;
                string direccion = textBoxParadaAltaDireccion.Text;
                string localidad = textBoxParadaAltaLocalidad.Text;
                bool habilitada = checkBoxParadaAltaHabilitada.Checked;

                if (id == "" || descripcion == "" || direccion == "" || localidad == "")
                {
                    throw new Exception(T("msg_TodosLosCamposSonObligatorios", "Todos los campos son obligatorios."));
                }

                if (paradasDisponibles.Exists(p => p.id == id))
                {
                    throw new Exception(T("msg_YaExisteUnaParadaConElMismoId", "Ya existe una parada con el mismo ID."));
                }

                GestorParada.InsertarParada(new Parada(id, descripcion, localidad, direccion, habilitada));
                MessageBox.Show(T("msg_ParadaCreadaExitosamente", "Parada creada exitosamente."));
                this.refrescarParadas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(T("msg_ErrorAlCrearLaParada", "Error al crear la parada: ") + ex.Message);
            }
        }

        private void buttonParadaBajaConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedId = dataGridViewParadas.CurrentRow?.Cells["id"].Value?.ToString() ?? throw new Exception(T("msg_NoSeHaSeleccionadoNingunaParada", "No se ha seleccionado ninguna parada."));
                GestorParada.EliminarParada(selectedId);
                MessageBox.Show(T("msg_ParadaEliminadaExitosamente", "Parada eliminada exitosamente."));
                this.refrescarParadas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(T("msg_ErrorAlEliminarLaParada", "Error al eliminar la parada: ") + ex.Message);
            }
        }

        private void buttonParadaModificarCallModal_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedId = dataGridViewParadas.CurrentRow?.Cells["id"].Value?.ToString() ?? throw new Exception(T("msg_NoSeHaSeleccionadoNingunaParada", "No se ha seleccionado ninguna parada."));
                Parada paradaSeleccionada = paradasDisponibles.FirstOrDefault(p => p.id == selectedId) ?? throw new Exception(T("msg_NoSeHaEncontradoLaParadaSeleccionada", "No se ha encontrado la parada seleccionada."));
                using (var modal = new GestionParadasModificacionUI(paradaSeleccionada.descripcion, paradaSeleccionada.localidad, paradaSeleccionada.direccion))
                {
                    if (modal.ShowDialog() == DialogResult.OK)
                    {
                        GestorParada.ActualizarParada(new Parada(selectedId, modal.descripcionModificada, modal.localidadModificada, modal.direccionModificada, paradaSeleccionada.habilitada));
                        MessageBox.Show(T("msg_ParadaModificadaExitosamente", "Parada modificada exitosamente."));
                        this.refrescarParadas();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(T("msg_ErrorAlModificarLaParada", "Error al modificar la parada: ") + ex.Message);
            }
        }

        private void buttonParadaToggleHabilitacion_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedId = dataGridViewParadas.CurrentRow?.Cells["id"].Value?.ToString() ?? throw new Exception(T("msg_NoSeHaSeleccionadoNingunaParada", "No se ha seleccionado ninguna parada."));
                Parada paradaSeleccionada = paradasDisponibles.FirstOrDefault(p => p.id == selectedId) ?? throw new Exception(T("msg_NoSeHaEncontradoLaParadaSeleccionada", "No se ha encontrado la parada seleccionada."));
                GestorParada.ActualizarParada(new Parada(selectedId, paradaSeleccionada.descripcion, paradaSeleccionada.localidad, paradaSeleccionada.direccion, !paradaSeleccionada.habilitada));
                this.refrescarParadas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(T("msg_ErrorAlModificarLaParada", "Error al modificar la parada: ") + ex.Message);
            }
        }
    }
}
