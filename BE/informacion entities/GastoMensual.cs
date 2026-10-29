namespace BE.informacion_entities
{
    public class GastoMensual
    {
        // Formato yyyy-MM
        public string mes { get; private set; }
        public decimal combustible { get; private set; }
        public decimal taller { get; private set; }

        public GastoMensual(string mes, decimal combustible, decimal taller)
        {
            this.mes = mes;
            this.combustible = combustible;
            this.taller = taller;
        }
    }
}
