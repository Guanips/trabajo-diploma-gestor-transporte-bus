using BE.cronograma_entities;
using BLL;
using BLL.cronograma_components;

namespace UI.Modules.exportado_cronogramas
{
    public partial class ExportarCronogramaUI : FormBaseObserver
    {
        // Mensajes del formulario (MessageBox y textos armados por código) en el idioma por defecto
        private static readonly Dictionary<string, string> MensajesFormulario = new()
        {
            ["err_ExportarCronograma"] = "Error al exportar el cronograma: {0}",
            ["msg_CronogramaExportado"] = "Cronograma exportado exitosamente a:\n{0}",
            ["msg_ExportarGuardarComo"] = "Guardar cronograma exportado",
            ["msg_ExportarSinCronogramas"] = "No hay cronogramas disponibles para exportar.",
            ["msg_ExportarSinSeleccion"] = "No se ha seleccionado ningún cronograma para exportar."
        };

        protected override IReadOnlyDictionary<string, string> Mensajes => MensajesFormulario;

        List<Cronograma> cronogramasDisponibles;
        Cronograma? cronogramaSeleccionado = null;
        CronogramaExportado? exportacionActual = null;

        public ExportarCronogramaUI()
        {
            cronogramasDisponibles = GestorCronograma.ObtenerCronogramas();
            if (cronogramasDisponibles.Count == 0)
            {
                MessageBox.Show(T("msg_ExportarSinCronogramas"), T("msg_TituloInformacion"), MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show(T("msg_ExportarSinSeleccion"), T("msg_TituloError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string cleanedCronogramaName = string.Join("_", cronogramaSeleccionado.descripcion.Split(Path.GetInvalidFileNameChars()));
            string defaultFileName = $"{cleanedCronogramaName}_EXPORTADO.json";

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = T("msg_FiltroArchivosJson");
                saveFileDialog.Title = T("msg_ExportarGuardarComo");
                saveFileDialog.FileName = defaultFileName;
                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        File.WriteAllText(saveFileDialog.FileName, SerializadorCronograma.Serializar(exportacionActual));
                        GestorBitacora.RegistrarAccion($"LOG_CRONO_EXPORT:{cronogramaSeleccionado.id}");
                        MessageBox.Show(T("msg_CronogramaExportado", saveFileDialog.FileName), T("msg_TituloExportacionExitosa"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(T("err_ExportarCronograma", ex.Message), T("msg_TituloError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
