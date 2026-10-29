namespace BE.informacion_entities
{
    public class RutaRendimiento
    {
        public string id { get; private set; }
        public string descripcion { get; private set; }
        public string sentido { get; private set; }
        public int programadas { get; private set; }
        public int suspendidas { get; private set; }
        public int conLlegada { get; private set; }
        public int puntuales { get; private set; }
        public double retrasoPromedioMin { get; private set; }

        public RutaRendimiento(string id, string descripcion, string sentido, int programadas, int suspendidas, int conLlegada, int puntuales, double retrasoPromedioMin)
        {
            this.id = id;
            this.descripcion = descripcion;
            this.sentido = sentido;
            this.programadas = programadas;
            this.suspendidas = suspendidas;
            this.conLlegada = conLlegada;
            this.puntuales = puntuales;
            this.retrasoPromedioMin = retrasoPromedioMin;
        }

        public double puntualidadPct => conLlegada == 0 ? 0 : 100.0 * puntuales / conLlegada;

        public string nombre => $"{descripcion} ({sentido})";
    }
}
