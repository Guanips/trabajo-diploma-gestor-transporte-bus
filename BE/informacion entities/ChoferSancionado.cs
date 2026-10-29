namespace BE.informacion_entities
{
    public class ChoferSancionado
    {
        public string nombreCompleto { get; private set; }
        public int sanciones { get; private set; }
        public double retrasoPromedioMin { get; private set; }

        public ChoferSancionado(string nombreCompleto, int sanciones, double retrasoPromedioMin)
        {
            this.nombreCompleto = nombreCompleto;
            this.sanciones = sanciones;
            this.retrasoPromedioMin = retrasoPromedioMin;
        }
    }
}
