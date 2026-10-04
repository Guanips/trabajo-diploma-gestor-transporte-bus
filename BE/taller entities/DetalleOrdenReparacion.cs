using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.taller_entities
{
    public class DetalleOrdenReparacion
    {
        public int id { get; private set; }
        public string insumo { get; private set; }
        public int cantidad { get; private set; }
        public int costoUnitario { get; private set; }

        public DetalleOrdenReparacion(int id, string insumo, int cantidad, int costoUnitario)
        {
            this.id = id;
            this.insumo = insumo;
            this.cantidad = cantidad;
            this.costoUnitario = costoUnitario;
        }

        public void SetId(int id)
        {
            this.id = id;
        }

        public void ActualizarCostoUnitario(int costoUnitario)
        {
            this.costoUnitario = costoUnitario;
        }
    }
}
