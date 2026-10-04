using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.taller_entities
{
    public class OrdenReparacion
    {
        public int id { get; private set; }
        public string motivoReparacion { get; private set; }
        public List<DetalleOrdenReparacion> detalles { get; private set; }
        public OrdenReparacion(int id, string motivoReparacion, List<DetalleOrdenReparacion> detalles)
        {
            this.id = id;
            this.detalles = detalles;
            this.motivoReparacion = motivoReparacion;
        }

        public void SetId(int id)
        {
            this.id = id;
        }

        public void AgregarDetalle(DetalleOrdenReparacion detalle)
        {
            this.detalles.Add(detalle);
        }

        public override string ToString()
        {
            return $"{this.id} - {this.motivoReparacion}";
        }
    }
}
