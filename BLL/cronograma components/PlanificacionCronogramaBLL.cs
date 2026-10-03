using BE.cronograma_entities;
using BE.interno_entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.cronograma_components
{
    public class PlanificacionCronogramaBLL
    {
        public PlanificacionCronogramaBLL() { }

        public List<Salida> AutogenerarSalidasDeCronograma (Cronograma cronograma)
        {
            List<Salida> salidasGeneradas = new List<Salida>();

            TimeOnly horaDeInicio = cronograma.horaInicio;
            TimeOnly horaDeFin = cronograma.horaFin;
            int frecuencia = cronograma.frecuenciaMinutos;

            for (TimeOnly hora = horaDeInicio; hora < horaDeFin; hora = hora.AddMinutes(frecuencia))
            {
                TimeOnly horaSalidaTeorica = hora;
                TimeOnly horaLlegadaTeorica = hora.AddMinutes(cronograma.ruta.tiempoEstimadoMin);
                salidasGeneradas.Add(new Salida(-1, null, null, horaSalidaTeorica, horaLlegadaTeorica, null, false, null));
            }

            GestorSalida.BatchInsertarSalidas(cronograma.id, salidasGeneradas);

            return salidasGeneradas;
        }

        public Salida? AsignarChoferASalida(Cronograma cronograma, Salida salida, Chofer chofer)
        {
            if (salida.estaSuspendida)
            {
                throw new Exception("No se puede asignar un chofer a una salida suspendida.");
            }

            Salida? conflicto = ValidarSolapamientoChofer(cronograma, salida, chofer);
            if (conflicto != null)
            {
                // Devuelve la salida en conflicto para que la capa de UI pueda ofrecer la reasignación
                return conflicto;
            }

            GestorSalida.AsignarChofer(salida.id, chofer);
            salida.AsignarChofer(chofer);
            return null;
        }

        public Salida? AsignarInternoASalida(Cronograma cronograma, Salida salida, Interno interno)
        {
            if (salida.estaSuspendida)
            {
                throw new Exception("No se puede asignar un interno a una salida suspendida.");
            }

            Salida? conflicto = ValidarSolapamientoInterno(cronograma, salida, interno);
            if (conflicto != null)
            {
                // Devuelve la salida en conflicto para que la capa de UI pueda ofrecer la reasignación
                return conflicto;
            }

            GestorSalida.AsignarInterno(salida.id, interno);
            salida.AsignarInterno(interno);
            return null;
        }

        public void DesasignarChoferDeSalida(Salida salida)
        {
            GestorSalida.AsignarChofer(salida.id, null);
            salida.DesasignarChofer();
        }

        public void DesasignarInternoDeSalida(Salida salida)
        {
            GestorSalida.AsignarInterno(salida.id, null);
            salida.DesasignarInterno();
        }

        private Salida? ValidarSolapamientoChofer(Cronograma cronograma, Salida salidaActual, Chofer chofer)
        {
            List<Cronograma> cronogramas = GestorCronograma.ObtenerCronogramas();
            List<Salida> salidasMismaFecha = cronogramas
                .Where(c => c.fechaValidez == cronograma.fechaValidez)
                .SelectMany(c => c.salidas)
                .Where(s => !s.estaSuspendida && s.id != salidaActual.id && s.choferAsignado?.num_chofer == chofer.num_chofer)
                .ToList();

            Salida? conflicto = salidasMismaFecha.FirstOrDefault(s => SeSolapan(salidaActual, s));
            return conflicto;
        }

        private Salida? ValidarSolapamientoInterno(Cronograma cronograma, Salida salidaActual, Interno interno)
        {
            List<Cronograma> cronogramas = GestorCronograma.ObtenerCronogramas();
            List<Salida> salidasMismaFecha = cronogramas
                .Where(c => c.fechaValidez == cronograma.fechaValidez)
                .SelectMany(c => c.salidas)
                .Where(s => !s.estaSuspendida && s.id != salidaActual.id && s.internoAsignado?.num_interno == interno.num_interno)
                .ToList();

            Salida? conflicto = salidasMismaFecha.FirstOrDefault(s => SeSolapan(salidaActual, s));
            return conflicto;
        }

        private bool SeSolapan(Salida salidaA, Salida salidaB)
        {
            return salidaA.horaSalidaTeorica < salidaB.horaLlegadaTeorica && salidaB.horaSalidaTeorica < salidaA.horaLlegadaTeorica;
        }
    }
}
