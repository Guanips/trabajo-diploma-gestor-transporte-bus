namespace BE.informacion_entities
{
    public class GastoInterno
    {
        public string patente { get; private set; }
        public string modelo { get; private set; }
        public int revisiones { get; private set; }
        public int reparaciones { get; private set; }
        public decimal gasto { get; private set; }

        public GastoInterno(string patente, string modelo, int revisiones, int reparaciones, decimal gasto)
        {
            this.patente = patente;
            this.modelo = modelo;
            this.revisiones = revisiones;
            this.reparaciones = reparaciones;
            this.gasto = gasto;
        }
    }
}
