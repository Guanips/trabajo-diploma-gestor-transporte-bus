using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.interno_entities
{
    public class Interno
    {
        public int num_interno { get; private set; }
        public string patente { get; private set; }
        public string modelo { get; private set; }
        public DateOnly fechaIncorporacion { get; private set; }
        public bool disponible { get; private set; }

        public Interno(int num_interno, string patente, string modelo, DateOnly fechaIncorporacion,bool habilitado)
        {
            this.num_interno = num_interno;
            this.patente = patente;
            this.modelo = modelo;
            this.disponible = habilitado;
            this.fechaIncorporacion = fechaIncorporacion;
        }

        public override string ToString()
        {
            return $"{num_interno} - {patente}";
        }
    }
}
