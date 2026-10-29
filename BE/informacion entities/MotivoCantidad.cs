namespace BE.informacion_entities
{
    public class MotivoCantidad
    {
        public string motivo { get; private set; }
        public int cantidad { get; private set; }

        public MotivoCantidad(string motivo, int cantidad)
        {
            this.motivo = motivo;
            this.cantidad = cantidad;
        }
    }
}
