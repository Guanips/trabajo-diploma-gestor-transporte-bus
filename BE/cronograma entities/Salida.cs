using BE.interno_entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.cronograma_entities
{
    public class Salida
    {
        public int id { get; private set; }
        public Chofer? choferAsignado { get; private set; }
        public Interno? internoAsignado { get; private set; }
        public TimeOnly horaSalidaTeorica { get; private set; }
        public TimeOnly horaLlegadaTeorica { get; private set; }
        public TimeOnly? horaLlegadaReal { get; private set; }
        public bool estaSuspendida { get; private set; }
        public string? motivoSuspension { get; private set; }

        public Salida(int id, Chofer? choferAsignado, Interno? internoAsignado, TimeOnly horaSalidaTeorica, TimeOnly horaLlegadaTeorica, TimeOnly? horaLlegadaReal, bool estaSuspendida, string? motivoSuspension)
        {
            this.id = id;
            this.choferAsignado = choferAsignado;
            this.internoAsignado = internoAsignado;
            this.horaSalidaTeorica = horaSalidaTeorica;
            this.horaLlegadaTeorica = horaLlegadaTeorica;
            this.horaLlegadaReal = horaLlegadaReal;
            this.estaSuspendida = estaSuspendida;
            this.motivoSuspension = motivoSuspension;
        }

        public void AsignarChofer(Chofer chofer)
        {
            this.choferAsignado = chofer;
        }

        public void AsignarInterno(Interno interno)
        {
            this.internoAsignado = interno;
        }

        public void ToggleSuspension(bool suspendida, string? motivo)
        {
            this.estaSuspendida = suspendida;
            this.motivoSuspension = motivo;
        }

        public void DesasignarChofer()
        {
            this.choferAsignado = null;
        }

        public void DesasignarInterno()
        {
            this.internoAsignado = null;
        }

        public void ActualizarHoraLlegadaReal(TimeOnly? horaLlegadaReal)
        {
            this.horaLlegadaReal = horaLlegadaReal;
        }

        public void SetId(int id)
        {
            this.id = id;
        }
    }
}
