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

            Salida? conflicto = ObtenerConflictosChofer(cronograma, salida, chofer).FirstOrDefault();
            if (conflicto != null)
            {
                // Devuelve la salida en conflicto para que la capa de UI pueda ofrecer la reasignación
                return conflicto;
            }

            GestorSalida.AsignarChofer(salida.id, chofer);
            salida.AsignarChofer(chofer);
            return null;
        }

        // Libera al chofer de todas las salidas que se solapan con la salida destino en la misma
        // fecha y luego lo asigna a esta última. Devuelve las salidas de las que fue liberado.
        public List<Salida> ReasignarChoferASalida(Cronograma cronograma, Salida salidaDestino, Chofer chofer)
        {
            if (salidaDestino.estaSuspendida)
            {
                throw new Exception("No se puede asignar un chofer a una salida suspendida.");
            }

            List<Salida> salidasEnConflicto = ObtenerConflictosChofer(cronograma, salidaDestino, chofer);
            foreach (Salida conflicto in salidasEnConflicto)
            {
                DesasignarChoferDeSalida(conflicto);
            }

            GestorSalida.AsignarChofer(salidaDestino.id, chofer);
            salidaDestino.AsignarChofer(chofer);

            return salidasEnConflicto;
        }

        public Salida? AsignarInternoASalida(Cronograma cronograma, Salida salida, Interno interno)
        {
            if (salida.estaSuspendida)
            {
                throw new Exception("No se puede asignar un interno a una salida suspendida.");
            }

            Salida? conflicto = ObtenerConflictosInterno(cronograma, salida, interno).FirstOrDefault();
            if (conflicto != null)
            {
                // Devuelve la salida en conflicto para que la capa de UI pueda ofrecer la reasignación
                return conflicto;
            }

            GestorSalida.AsignarInterno(salida.id, interno);
            salida.AsignarInterno(interno);
            return null;
        }

        // Libera al interno de todas las salidas que se solapan con la salida destino en la misma
        // fecha y luego lo asigna a esta última. Devuelve las salidas de las que fue liberado.
        public List<Salida> ReasignarInternoASalida(Cronograma cronograma, Salida salidaDestino, Interno interno)
        {
            if (salidaDestino.estaSuspendida)
            {
                throw new Exception("No se puede asignar un interno a una salida suspendida.");
            }

            List<Salida> salidasEnConflicto = ObtenerConflictosInterno(cronograma, salidaDestino, interno);
            foreach (Salida conflicto in salidasEnConflicto)
            {
                DesasignarInternoDeSalida(conflicto);
            }

            GestorSalida.AsignarInterno(salidaDestino.id, interno);
            salidaDestino.AsignarInterno(interno);

            return salidasEnConflicto;
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

        // Salidas de la misma fecha, no suspendidas, ya asignadas al chofer, que se solapan con la salida actual
        public List<Salida> ObtenerConflictosChofer(Cronograma cronograma, Salida salidaActual, Chofer chofer)
        {
            List<Cronograma> cronogramas = GestorCronograma.ObtenerCronogramas();
            return cronogramas
                .Where(c => c.fechaValidez == cronograma.fechaValidez)
                .SelectMany(c => c.salidas)
                .Where(s => !s.estaSuspendida && s.id != salidaActual.id && s.choferAsignado?.num_chofer == chofer.num_chofer)
                .Where(s => SeSolapan(salidaActual, s))
                .ToList();
        }

        // Salidas de la misma fecha, no suspendidas, ya asignadas al interno, que se solapan con la salida actual
        public List<Salida> ObtenerConflictosInterno(Cronograma cronograma, Salida salidaActual, Interno interno)
        {
            List<Cronograma> cronogramas = GestorCronograma.ObtenerCronogramas();
            return cronogramas
                .Where(c => c.fechaValidez == cronograma.fechaValidez)
                .SelectMany(c => c.salidas)
                .Where(s => !s.estaSuspendida && s.id != salidaActual.id && s.internoAsignado?.num_interno == interno.num_interno)
                .Where(s => SeSolapan(salidaActual, s))
                .ToList();
        }

        private bool SeSolapan(Salida salidaA, Salida salidaB)
        {
            return salidaA.horaSalidaTeorica < salidaB.horaLlegadaTeorica && salidaB.horaSalidaTeorica < salidaA.horaLlegadaTeorica;
        }
    }
}
