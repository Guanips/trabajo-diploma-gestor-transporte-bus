namespace BE.recorrido_entities
{
    public class Parada
    {
        public string id { get; private set; }
        public string descripcion { get; private set; }
        public string localidad { get; private set; }
        public string direccion { get; private set; }
        public bool habilitada { get; private set; }

        public Parada(string id, string descripcion, string localidad, string direccion, bool habilitada)
        {
            this.id = id;
            this.descripcion = descripcion;
            this.localidad = localidad;
            this.direccion = direccion;
            this.habilitada = habilitada;
        }
    }
}
