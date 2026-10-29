namespace BE.informacion_entities
{
    public class ResumenServicio
    {
        public int salidasProgramadas { get; private set; }
        public int salidasSuspendidas { get; private set; }
        public int salidasConLlegada { get; private set; }
        public int salidasPuntuales { get; private set; }
        public double retrasoPromedioMin { get; private set; }
        public int sanciones { get; private set; }
        public decimal litrosCombustible { get; private set; }
        public decimal gastoCombustible { get; private set; }
        public int revisiones { get; private set; }
        public int reparaciones { get; private set; }
        public decimal gastoTaller { get; private set; }
        public int insumosSinCosto { get; private set; }

        public ResumenServicio(int salidasProgramadas, int salidasSuspendidas, int salidasConLlegada, int salidasPuntuales, double retrasoPromedioMin,
                               int sanciones, decimal litrosCombustible, decimal gastoCombustible, int revisiones, int reparaciones,
                               decimal gastoTaller, int insumosSinCosto)
        {
            this.salidasProgramadas = salidasProgramadas;
            this.salidasSuspendidas = salidasSuspendidas;
            this.salidasConLlegada = salidasConLlegada;
            this.salidasPuntuales = salidasPuntuales;
            this.retrasoPromedioMin = retrasoPromedioMin;
            this.sanciones = sanciones;
            this.litrosCombustible = litrosCombustible;
            this.gastoCombustible = gastoCombustible;
            this.revisiones = revisiones;
            this.reparaciones = reparaciones;
            this.gastoTaller = gastoTaller;
            this.insumosSinCosto = insumosSinCosto;
        }

        // Sobre las salidas con llegada registrada
        public double puntualidadPct => salidasConLlegada == 0 ? 0 : 100.0 * salidasPuntuales / salidasConLlegada;

        public double suspensionPct => salidasProgramadas == 0 ? 0 : 100.0 * salidasSuspendidas / salidasProgramadas;

        public decimal gastoTotal => gastoCombustible + gastoTaller;
    }
}
