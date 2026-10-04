using BE.interno_entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.cronograma_entities
{
    public class Sancion
    {
        public int id { get; private set; }
        public string motivo { get; private set; }
        public DateOnly fecha { get; private set; }
        public Chofer choferSancionado { get; private set; }

        public Sancion(int id, string motivo, DateOnly fecha, Chofer choferSancionado)
        {
            this.id = id;
            this.motivo = motivo;
            this.fecha = fecha;
            this.choferSancionado = choferSancionado;
        }

        public override string ToString()
        {
            return $"Sanción ID: {id}, Motivo: {motivo}, Fecha: {fecha.ToShortDateString()}, Chofer: {choferSancionado.nombreCompleto}";
        }
    }
}
