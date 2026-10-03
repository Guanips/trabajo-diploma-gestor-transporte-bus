using BE.interno_entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.taller_entities
{
    public class CargaCombustible
    {
        public int id { get; private set; }
        public Interno interno { get; private set; }
        public DateTime fechaHora { get; private set; }
        public decimal litrosCargados { get; private set; }
        public decimal precioPorLitro { get; private set; }
        public int kilometrajeActual { get; private set; }
        public bool anulada { get; private set; }
        public string? motivoAnulacion { get; private set; }

        public CargaCombustible(int id, Interno interno, DateTime fechaHora, decimal litrosCargados, decimal precioPorLitro, int kilometrajeActual, bool anulada, string? motivoAnulacion)
        {
            this.id = id;
            this.interno = interno;
            this.fechaHora = fechaHora;
            this.litrosCargados = litrosCargados;
            this.precioPorLitro = precioPorLitro;
            this.kilometrajeActual = kilometrajeActual;
            this.anulada = anulada;
            this.motivoAnulacion = motivoAnulacion;
        }

        public void Anular(string motivo)
        {
            this.anulada = true;
            this.motivoAnulacion = motivo;
        }
    }
}
