using BE.interno_entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.taller_entities
{
    public class RevisionTaller
    {
        public int id { get; private set; }
        public Interno interno { get; private set; }
        public DateOnly fecha { get; private set; }
        public string descripcion { get; private set; }
        public bool reparacionRequerida { get; private set; }
        public List<OrdenReparacion> ordenesReparacion { get; private set; }

        public RevisionTaller(int id, Interno interno, DateOnly fecha, string descripcion, bool reparacionRequerida)
        {
            this.id = id;
            this.interno = interno;
            this.fecha = fecha;
            this.descripcion = descripcion;
            this.reparacionRequerida = reparacionRequerida;
            this.ordenesReparacion = new List<OrdenReparacion>();
        }

        public void SetId(int id)
        {
            this.id = id;
        }

        public void AgregarOrdenReparacion(OrdenReparacion orden)
        {
            this.ordenesReparacion.Add(orden);
        }

        public void EliminarOrdenReparacion(OrdenReparacion orden)
        {
            this.ordenesReparacion.Remove(orden);
        }

        public override string ToString()
        {
            return $"{this.id} - {this.descripcion}";
        }
    }
}
