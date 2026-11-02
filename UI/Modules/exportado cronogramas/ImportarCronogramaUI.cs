using BE.cronograma_entities;
using BLL.cronograma_components;

namespace UI.Modules.exportado_cronogramas
{
    public partial class ImportarCronogramaUI : FormBaseObserver
    {
        // Mensajes del formulario (MessageBox y textos armados por código) en el idioma por defecto
        private static readonly Dictionary<string, string> MensajesFormulario = new()
        {
            ["err_LeerArchivoCronograma"] = "No se pudo leer el archivo: {0}",
            ["msg_ImportarSeleccionarArchivo"] = "Seleccionar cronograma exportado",
            ["msg_ResumenCronogramaImportado"] = "{0} - válido para el {1:dd/MM/yyyy} - Ruta {2} ({3}) - {4:HH:mm} a {5:HH:mm}, cada {6} min",
            ["msg_TituloArchivoNoValido"] = "Archivo no válido"
        };

        protected override IReadOnlyDictionary<string, string> Mensajes => MensajesFormulario;

        private CronogramaExportado? cronogramaImportado;

        public ImportarCronogramaUI()
        {
            InitializeComponent();
        }

        private void buttonImportarCronogramaSeleccionarOtro_Click(object sender, EventArgs e)
        {
            using OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = T("msg_FiltroArchivosJson");
            openFileDialog.Title = T("msg_ImportarSeleccionarArchivo");
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
                MessageBox.Show(ex.Message, T("msg_TituloArchivoNoValido"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
            {
                MessageBox.Show(T("err_LeerArchivoCronograma", ex.Message), T("msg_TituloError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Solo visualizacion: no se escribe nada en la base de datos
            cronogramaImportado = cronograma;
            MostrarResumenCronograma();

            GrillaSalidasExportadas.Mostrar(dataGridViewCronogramaSeleccionadoImportado, cronograma.Salidas);
        }

        private void MostrarResumenCronograma()
        {
            if (cronogramaImportado == null) return;

            groupBoxImportarCronogramaSeleccionado.Text = T("msg_ResumenCronogramaImportado",
                cronogramaImportado.Descripcion, cronogramaImportado.FechaValidez, cronogramaImportado.Ruta.Descripcion,
                cronogramaImportado.Ruta.Sentido, cronogramaImportado.HoraInicio, cronogramaImportado.HoraFin,
                cronogramaImportado.FrecuenciaMinutos);
        }

        // El resumen se arma por codigo y el traductor no lo pisa: se vuelve a armar en el nuevo idioma
        protected override void TraducirElementosParticulares(string codigoIdioma)
        {
            MostrarResumenCronograma();
        }
    }
}
