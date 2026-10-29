namespace BE.informacion_entities
{
    public class TramoRetraso
    {
        public string nombre { get; private set; }
        public int cantidad { get; private set; }

        public TramoRetraso(string nombre, int cantidad)
        {
            this.nombre = nombre;
            this.cantidad = cantidad;
        }
    }
}
