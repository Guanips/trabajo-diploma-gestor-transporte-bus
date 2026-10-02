using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.interno_entities
{
    public class Chofer
    {
        public int num_chofer { get; private set; }
        public int dni { get; private set; }
        public string nombreCompleto { get; private set; }
        public bool activo { get; private set; }

        public Chofer(int num_chofer, int dni, string nombreCompleto, bool activo)
        {
            this.num_chofer = num_chofer;
            this.dni = dni;
            this.nombreCompleto = nombreCompleto;
            this.activo = activo;
        }

        public override string ToString()
        {
            return $"{num_chofer} - {nombreCompleto}";
        }
    }
}
