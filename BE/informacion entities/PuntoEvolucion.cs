namespace BE.informacion_entities
{
    public class PuntoEvolucion
    {
        public DateOnly fecha { get; private set; }
        public int conLlegada { get; private set; }
        public int puntuales { get; private set; }
        public double retrasoPromedioMin { get; private set; }

        public PuntoEvolucion(DateOnly fecha, int conLlegada, int puntuales, double retrasoPromedioMin)
        {
            this.fecha = fecha;
            this.conLlegada = conLlegada;
            this.puntuales = puntuales;
            this.retrasoPromedioMin = retrasoPromedioMin;
        }

        public double puntualidadPct => conLlegada == 0 ? 0 : 100.0 * puntuales / conLlegada;
    }
}
