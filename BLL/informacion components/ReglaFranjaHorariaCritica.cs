using BE.informacion_entities;

namespace BLL.informacion_components
{
    // Si una franja horaria rinde mucho peor que otra, el problema suele ser de esa franja (congestion, falta de
    // refuerzo) y no del servicio en general.
    public class ReglaFranjaHorariaCritica : ReglaRecomendacion
    {
        private const int MuestraMinima = 30;
        private const double DiferenciaMinimaPuntos = 15;

        private static readonly TimeOnly FinManana = new TimeOnly(12, 0);
        private static readonly TimeOnly FinTarde = new TimeOnly(19, 0);

        public override List<Recomendacion> Evaluar(DatosAnalisisServicio datos)
        {
            List<Recomendacion> recomendaciones = new List<Recomendacion>();

            List<(string franja, int salidas, double puntualidad, double retrasoPromedio)> franjas = datos.salidasAuditadas
                .GroupBy(s => Franja(s.salida.horaSalidaTeorica))
                .Where(grupo => grupo.Count() >= MuestraMinima)
                .Select(grupo => (grupo.Key, grupo.Count(), datos.PuntualidadPct(grupo), datos.RetrasoPromedioMin(grupo)))
                .ToList();
            if (franjas.Count < 2) return recomendaciones;

            var mejor = franjas.MaxBy(f => f.puntualidad);
            var peor = franjas.MinBy(f => f.puntualidad);
            if (mejor.puntualidad - peor.puntualidad < DiferenciaMinimaPuntos) return recomendaciones;

            recomendaciones.Add(new Recomendacion(
                SeveridadRecomendacion.Media,
                $"Franja {peor.franja}: puntualidad inferior al resto del servicio",
                $"Puntualidad de {Porcentaje(peor.puntualidad)} ({Numero(peor.retrasoPromedio)} min de retraso promedio, {peor.salidas} salidas) " +
                $"contra {Porcentaje(mejor.puntualidad)} en la franja {mejor.franja}, en el período {Periodo(datos)}.",
                $"Evaluar reforzar el servicio en la franja {peor.franja} con más internos y choferes, o reprogramar sus horarios ampliando el tiempo entre salidas."));

            return recomendaciones;
        }

        private static string Franja(TimeOnly hora)
        {
            if (hora < FinManana) return "de la mañana (antes de las 12:00)";
            if (hora < FinTarde) return "de la tarde (de 12:00 a 18:59)";
            return "de la noche (desde las 19:00)";
        }
    }
}
