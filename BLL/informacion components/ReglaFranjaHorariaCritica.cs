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

        // Textos de la recomendacion en el idioma por defecto
        private static readonly CatalogoMensajes Mensajes = new CatalogoMensajes("RecomendacionesUI", new Dictionary<string, string>
        {
            ["msg_RecFranjaTitulo"] = "Franja {0}: puntualidad inferior al resto del servicio",
            ["msg_RecFranjaEvidencia"] = "Puntualidad de {0} ({1} min de retraso promedio, {2} salidas) contra {3} en la franja {4}, en el período {5}.",
            ["msg_RecFranjaAccion"] = "Evaluar reforzar el servicio en la franja {0} con más internos y choferes, o reprogramar sus horarios ampliando el tiempo entre salidas.",
            ["msg_RecFranjaManana"] = "de la mañana (antes de las 12:00)",
            ["msg_RecFranjaTarde"] = "de la tarde (de 12:00 a 18:59)",
            ["msg_RecFranjaNoche"] = "de la noche (desde las 19:00)"
        });

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
                Mensajes.Traducir("msg_RecFranjaTitulo", peor.franja),
                Mensajes.Traducir("msg_RecFranjaEvidencia", Porcentaje(peor.puntualidad), Numero(peor.retrasoPromedio), peor.salidas,
                    Porcentaje(mejor.puntualidad), mejor.franja, Periodo(datos)),
                Mensajes.Traducir("msg_RecFranjaAccion", peor.franja)));

            return recomendaciones;
        }

        private static string Franja(TimeOnly hora)
        {
            if (hora < FinManana) return Mensajes.Traducir("msg_RecFranjaManana");
            if (hora < FinTarde) return Mensajes.Traducir("msg_RecFranjaTarde");
            return Mensajes.Traducir("msg_RecFranjaNoche");
        }
    }
}
