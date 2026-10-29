namespace BE.informacion_entities
{
    public class ConsumoInterno
    {
        public string patente { get; private set; }
        public string modelo { get; private set; }
        public decimal litros { get; private set; }
        public int km { get; private set; }

        public ConsumoInterno(string patente, string modelo, decimal litros, int km)
        {
            this.patente = patente;
            this.modelo = modelo;
            this.litros = litros;
            this.km = km;
        }

        // Litros cada 100 km; null si no hay kilometros recorridos entre cargas para calcularlo
        public double? litrosCada100Km => km > 0 ? (double)litros / km * 100.0 : null;
    }
}
