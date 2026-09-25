namespace BE.recorrido_entities
{
    public class Ruta
    {
        public string id { get; private set; }
        public string descripcion { get; private set; }
        public string sentido { get; private set; }
        public int distanciaTotalKM { get; private set; }
        public int tiempoEstimadoMin { get; private set; }

        public List<Parada> recorrido { get; private set; }

        public Ruta(string id, string descripcion, string sentido, int distanciaTotalKM, int tiempoEstimadoMin)
        {
            this.id = id;
            this.descripcion = descripcion;
            this.sentido = sentido;
            this.distanciaTotalKM = distanciaTotalKM;
            this.tiempoEstimadoMin = tiempoEstimadoMin;
            recorrido = new List<Parada>();
        }

        public void AgregarParada(Parada parada)
        {
            if (recorrido.Exists(p => p.id == parada.id))
            {
                throw new Exception("La parada ya existe en el recorrido.");
            }

            recorrido.Add(parada);
        }

        public void EliminarParada(string idParada)
        {
            Parada? paradaAEliminar = recorrido.Find(p => p.id == idParada);
            if (paradaAEliminar == null)
            {
                throw new Exception("La parada no existe en el recorrido.");
            }
            recorrido.Remove(paradaAEliminar);
        }

        public void ModificarOrdenParada(int nuevaPosicion, Parada parada)
        {
            int posicionActual = recorrido.FindIndex(p => p.id == parada.id);
            if (posicionActual == -1)
            {
                throw new Exception("La parada no existe en el recorrido.");
            }

            if (nuevaPosicion < 0 || nuevaPosicion >= recorrido.Count)
            {
                throw new Exception("La nueva posición está fuera de los límites del recorrido.");
            }

            if (posicionActual == nuevaPosicion)
            {
                return;
            }

            recorrido.RemoveAt(posicionActual);

            recorrido.Insert(nuevaPosicion, parada);
        }
    }
}
