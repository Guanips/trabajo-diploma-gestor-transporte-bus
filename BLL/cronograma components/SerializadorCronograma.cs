using BE.cronograma_entities;
using System.Text.Json;

namespace BLL.cronograma_components
{
    public static class SerializadorCronograma
    {
        private static readonly JsonSerializerOptions Opciones = new()
        {
            WriteIndented = true,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping, // acentos legibles en el archivo
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public static CronogramaExportado CrearExportacion(Cronograma cronograma)
        {
            return new CronogramaExportado(
                CronogramaExportado.VersionActual,
                DateTime.Now,
                cronograma.descripcion,
                cronograma.fechaValidez,
                cronograma.horaInicio,
                cronograma.horaFin,
                cronograma.frecuenciaMinutos,
                cronograma.tiempoDescansoMinutos,
                new RutaExportada(cronograma.ruta.id, cronograma.ruta.descripcion, cronograma.ruta.sentido,
                                  cronograma.ruta.distanciaTotalKM, cronograma.ruta.tiempoEstimadoMin),
                cronograma.salidas
                    .OrderBy(s => s.horaSalidaTeorica)
                    .Select(s => new SalidaExportada(
                        s.horaSalidaTeorica,
                        s.horaLlegadaTeorica,
                        s.choferAsignado == null ? null : new ChoferExportado(s.choferAsignado.dni, s.choferAsignado.nombreCompleto),
                        s.internoAsignado == null ? null : new InternoExportado(s.internoAsignado.patente, s.internoAsignado.modelo),
                        s.estaSuspendida,
                        s.motivoSuspension))
                    .ToList());
        }

        public static string Serializar(CronogramaExportado exportacion)
        {
            return JsonSerializer.Serialize(exportacion, Opciones);
        }

        /// <summary>
        /// Lee y valida un archivo exportado. Lanza InvalidDataException con un mensaje apto para el usuario
        /// si el contenido no es JSON, es de un formato anterior/futuro o le faltan datos.
        /// </summary>
        public static CronogramaExportado Deserializar(string json)
        {
            CronogramaExportado? exportacion;
            try
            {
                using JsonDocument documento = JsonDocument.Parse(json);
                if (documento.RootElement.ValueKind != JsonValueKind.Object)
                    throw new InvalidDataException("El archivo no tiene el formato de un cronograma exportado (puede ser de una versión anterior).");

                exportacion = documento.RootElement.Deserialize<CronogramaExportado>(Opciones);
            }
            catch (JsonException ex)
            {
                throw new InvalidDataException("El archivo no es un JSON válido de cronograma exportado.", ex);
            }

            if (exportacion == null)
                throw new InvalidDataException("El archivo está vacío.");

            if (exportacion.FormatoVersion != CronogramaExportado.VersionActual)
                throw new InvalidDataException(
                    $"La versión del formato del archivo ({exportacion.FormatoVersion}) no es compatible con esta aplicación (versión {CronogramaExportado.VersionActual}).");

            if (string.IsNullOrWhiteSpace(exportacion.Descripcion) || exportacion.Ruta == null || exportacion.Salidas == null)
                throw new InvalidDataException("Al archivo le faltan datos obligatorios del cronograma (descripción, ruta o salidas).");

            return exportacion;
        }
    }
}
