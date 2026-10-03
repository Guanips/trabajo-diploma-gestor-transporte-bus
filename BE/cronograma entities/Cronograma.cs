using BE.recorrido_entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.cronograma_entities
{
    public class Cronograma
    {
        public int id { get; private set; }
        public string descripcion { get; private set; }
        public DateOnly fechaValidez { get; private set; }
        public TimeOnly horaInicio { get; private set; }
        public TimeOnly horaFin { get; private set; }
        public int frecuenciaMinutos { get; private set; }
        public int tiempoDescansoMinutos { get; private set; }

        public List<Salida> salidas { get; private set; }
        public Ruta ruta { get; private set; }

        public Cronograma(int id, string descripcion, DateOnly fechaValidez, TimeOnly horaInicio, TimeOnly horaFin, int frecuenciaMinutos, int tiempoDescansoMinutos, Ruta ruta)
        {
            this.id = id;
            this.descripcion = descripcion;
            this.fechaValidez = fechaValidez;
            this.horaInicio = horaInicio;
            this.horaFin = horaFin;
            this.frecuenciaMinutos = frecuenciaMinutos;
            this.tiempoDescansoMinutos = tiempoDescansoMinutos;
            this.ruta = ruta;
            this.salidas = new List<Salida>();
        }

        public void AgregarSalida(Salida salida)
        {
            this.salidas.Add(salida);
        }
        public void EliminarSalida(Salida salida)
        {
            this.salidas.Remove(salida);
        }

        public override string ToString()
        {
            return $"{id} - {descripcion}";
        }
    }
}
