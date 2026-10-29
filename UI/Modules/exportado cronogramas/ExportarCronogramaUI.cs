using BE.cronograma_entities;
using BLL.cronograma_components;

namespace UI.Modules.exportado_cronogramas
{
    public partial class ExportarCronogramaUI : Form
    {
        List<Cronograma> cronogramasDisponibles;
        Cronograma? cronogramaSeleccionado = null;
        CronogramaExportado? exportacionActual = null;

        public ExportarCronogramaUI()
        {
            cronogramasDisponibles = GestorCronograma.ObtenerCronogramas();
            if (cronogramasDisponibles.Count == 0)
            {
                MessageBox.Show("No hay cronogramas disponibles para exportar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }

            InitializeComponent();
            listBoxExportarCronogramaDisponibles.DataSource = cronogramasDisponibles;
        }

        private void listBoxExportarCronogramaDisponibles_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.mostrarSalidasCronograma();
        }

        private void mostrarSalidasCronograma()
        {
            Cronograma? cronogramaActual = listBoxExportarCronogramaDisponibles.SelectedItem as Cronograma;

            if (cronogramaActual == null)
            {
                exportacionActual = null;
                GrillaSalidasExportadas.Mostrar(dataGridViewCronogramaSeleccionadoExportado, null);
                return;
            }

            cronogramaSeleccionado = cronogramaActual;

            // Lo que se ve en la grilla es exactamente lo que se escribe en el archivo
            exportacionActual = SerializadorCronograma.CrearExportacion(cronogramaActual);
            GrillaSalidasExportadas.Mostrar(dataGridViewCronogramaSeleccionadoExportado, exportacionActual.Salidas);
        }

        private void buttonExportarCronogramasConfirmar_Click(object sender, EventArgs e)
        {
            if (cronogramaSeleccionado == null || exportacionActual == null)
            {
                MessageBox.Show("No se ha seleccionado ningún cronograma para exportar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string cleanedCronogramaName = string.Join("_", cronogramaSeleccionado.descripcion.Split(Path.GetInvalidFileNameChars()));
            string defaultFileName = $"{cleanedCronogramaName}_EXPORTADO.json";

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Archivos JSON (*.json)|*.json";
                saveFileDialog.Title = "Guardar cronograma exportado";
                saveFileDialog.FileName = defaultFileName;
                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        File.WriteAllText(saveFileDialog.FileName, SerializadorCronograma.Serializar(exportacionActual));
                        MessageBox.Show($"Cronograma exportado exitosamente a:\n{saveFileDialog.FileName}", "Exportación exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al exportar el cronograma: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
