namespace BE.informacion_entities
{
    // Datos del reporte general del servicio para un rango de fechas. Lo arma una sola vez GestorReporteServicio y lo
    // consumen tanto la pantalla como la exportacion a PDF, asi ambas muestran exactamente lo mismo.
    public class ReporteServicio
    {
        public DateOnly desde { get; private set; }
        public DateOnly hasta { get; private set; }
        public int toleranciaMinutos { get; private set; }
        public DateTime fechaGeneracion { get; private set; }
        public ResumenServicio resumen { get; private set; }
        public List<RutaRendimiento> porRuta { get; private set; }
        public List<PuntoEvolucion> evolucionDiaria { get; private set; }
        public List<TramoRetraso> distribucionRetrasos { get; private set; }
        public List<MotivoCantidad> suspensiones { get; private set; }
        public List<ChoferSancionado> choferesConSanciones { get; private set; }
        public List<GastoMensual> gastosMensuales { get; private set; }
        public List<GastoInterno> tallerPorInterno { get; private set; }
        public List<ConsumoInterno> consumoPorInterno { get; private set; }

        public ReporteServicio(DateOnly desde, DateOnly hasta, int toleranciaMinutos, DateTime fechaGeneracion, ResumenServicio resumen,
                               List<RutaRendimiento> porRuta, List<PuntoEvolucion> evolucionDiaria, List<TramoRetraso> distribucionRetrasos,
                               List<MotivoCantidad> suspensiones, List<ChoferSancionado> choferesConSanciones, List<GastoMensual> gastosMensuales,
                               List<GastoInterno> tallerPorInterno, List<ConsumoInterno> consumoPorInterno)
        {
            this.desde = desde;
            this.hasta = hasta;
            this.toleranciaMinutos = toleranciaMinutos;
            this.fechaGeneracion = fechaGeneracion;
            this.resumen = resumen;
            this.porRuta = porRuta;
            this.evolucionDiaria = evolucionDiaria;
            this.distribucionRetrasos = distribucionRetrasos;
            this.suspensiones = suspensiones;
            this.choferesConSanciones = choferesConSanciones;
            this.gastosMensuales = gastosMensuales;
            this.tallerPorInterno = tallerPorInterno;
            this.consumoPorInterno = consumoPorInterno;
        }

        // Hay algo que reportar si el rango tiene al menos una salida, sancion, carga o revision
        public bool tieneDatos =>
            resumen.salidasProgramadas > 0 || resumen.sanciones > 0 || resumen.revisiones > 0 || resumen.litrosCombustible > 0;
    }
}
