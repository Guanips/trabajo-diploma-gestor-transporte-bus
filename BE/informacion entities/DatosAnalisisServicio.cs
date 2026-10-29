using BE.cronograma_entities;
using BE.interno_entities;
using BE.taller_entities;

namespace BE.informacion_entities
{
    // Datos del servicio para un rango de fechas sobre los que las reglas generan las recomendaciones.
    // Las listas llegan ya filtradas por el rango.
    public class DatosAnalisisServicio
    {
        // Una llegada con hasta esta cantidad de minutos de retraso se considera puntual
        public const int ToleranciaMinutos = 5;

        public DateOnly desde { get; private set; }
        public DateOnly hasta { get; private set; }
        public List<Cronograma> cronogramas { get; private set; }
        public List<SalidaAnalizada> salidasAuditadas { get; private set; }
        public List<Sancion> sanciones { get; private set; }
        public List<CargaCombustible> cargas { get; private set; }
        public List<RevisionTaller> revisiones { get; private set; }
        public List<Interno> internos { get; private set; }

        public DatosAnalisisServicio(DateOnly desde, DateOnly hasta, List<Cronograma> cronogramas, List<Sancion> sanciones,
                                     List<CargaCombustible> cargas, List<RevisionTaller> revisiones, List<Interno> internos)
        {
            this.desde = desde;
            this.hasta = hasta;
            this.cronogramas = cronogramas;
            this.sanciones = sanciones;
            this.cargas = cargas;
            this.revisiones = revisiones;
            this.internos = internos;

            this.salidasAuditadas = cronogramas
                .SelectMany(cronograma => cronograma.salidas
                    .Where(salida => !salida.estaSuspendida && salida.horaLlegadaReal != null)
                    .Select(salida => new SalidaAnalizada(cronograma, salida)))
                .ToList();
        }

        // Hay algo que analizar si el rango tiene al menos una salida, sancion, carga o revision
        public bool tieneDatos =>
            cronogramas.Any(cronograma => cronograma.salidas.Count > 0) || sanciones.Count > 0 || cargas.Count > 0 || revisiones.Count > 0;

        public double PuntualidadPct(IEnumerable<SalidaAnalizada> salidas)
        {
            List<SalidaAnalizada> lista = salidas.ToList();
            return lista.Count == 0 ? 0 : 100.0 * lista.Count(s => s.retrasoMin <= ToleranciaMinutos) / lista.Count;
        }

        // Las llegadas anticipadas cuentan como 0 minutos de retraso
        public double RetrasoPromedioMin(IEnumerable<SalidaAnalizada> salidas)
        {
            List<SalidaAnalizada> lista = salidas.ToList();
            return lista.Count == 0 ? 0 : lista.Average(s => Math.Max(0, s.retrasoMin));
        }
    }
}
