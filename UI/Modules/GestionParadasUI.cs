using BE.recorrido_entities;
using BLL.recorrido_components;

namespace UI.Modules
{
    public partial class GestionParadasUI : Form
    {
        private List<Parada> paradasDisponibles;

        public GestionParadasUI()
        {
            InitializeComponent();
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
                    throw new Exception("Todos los campos son obligatorios.");
                }

                if (paradasDisponibles.Exists(p => p.id == id))
                {
                    throw new Exception("Ya existe una parada con el mismo ID.");
                }

                GestorParada.InsertarParada(new Parada(id, descripcion, localidad, direccion, habilitada));
                MessageBox.Show("Parada creada exitosamente.");
                this.refrescarParadas();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear la parada: {ex.Message}");
            }
        }

        private void buttonParadaBajaConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedId = dataGridViewParadas.CurrentRow?.Cells["id"].Value?.ToString() ?? throw new Exception("No se ha seleccionado ninguna parada.");
                GestorParada.EliminarParada(selectedId);
                MessageBox.Show("Parada eliminada exitosamente.");
                this.refrescarParadas();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar la parada: {ex.Message}");
            }
        }
    }
}
