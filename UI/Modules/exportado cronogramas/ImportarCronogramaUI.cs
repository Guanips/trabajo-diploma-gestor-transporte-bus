using BE.cronograma_entities;
using BLL.cronograma_components;

namespace UI.Modules.exportado_cronogramas
{
    public partial class ImportarCronogramaUI : Form
    {
        public ImportarCronogramaUI()
        {
            InitializeComponent();
        }

        private void buttonImportarCronogramaSeleccionarOtro_Click(object sender, EventArgs e)
        {
            using OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos JSON (*.json)|*.json";
            openFileDialog.Title = "Seleccionar cronograma exportado";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFileDialog.CheckFileExists = true;

            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            CronogramaExportado cronograma;
            try
            {
                cronograma = SerializadorCronograma.Deserializar(File.ReadAllText(openFileDialog.FileName));
            }
            catch (InvalidDataException ex)
            {
                MessageBox.Show(ex.Message, "Archivo no válido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
            {
                MessageBox.Show($"No se pudo leer el archivo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Solo visualizacion: no se escribe nada en la base de datos
            groupBoxImportarCronogramaSeleccionado.Text =
                $"{cronograma.Descripcion} - válido para el {cronograma.FechaValidez:dd/MM/yyyy} - " +
                $"Ruta {cronograma.Ruta.Descripcion} ({cronograma.Ruta.Sentido}) - " +
                $"{cronograma.HoraInicio:HH:mm} a {cronograma.HoraFin:HH:mm}, cada {cronograma.FrecuenciaMinutos} min";

            GrillaSalidasExportadas.Mostrar(dataGridViewCronogramaSeleccionadoImportado, cronograma.Salidas);
        }
    }
}
