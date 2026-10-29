using BE.cronograma_entities;

namespace BE.informacion_entities
{
    // Salida no suspendida con llegada registrada, junto con su cronograma y su retraso de llegada
    public class SalidaAnalizada
    {
        public Cronograma cronograma { get; private set; }
        public Salida salida { get; private set; }
        public int retrasoMin { get; private set; }

        public SalidaAnalizada(Cronograma cronograma, Salida salida)
        {
            if (salida.horaLlegadaReal == null)
            {
                throw new ArgumentException("La salida no tiene registrada la hora de llegada real.");
            }

            this.cronograma = cronograma;
            this.salida = salida;
            // Se restan TimeSpan y no TimeOnly: la resta de TimeOnly da la distancia circular (una llegada 2 min antes daria 23:58)
            this.retrasoMin = (int)(salida.horaLlegadaReal.Value.ToTimeSpan() - salida.horaLlegadaTeorica.ToTimeSpan()).TotalMinutes;
        }
    }
}
