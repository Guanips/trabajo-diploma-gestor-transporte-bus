using BE.informacion_entities;
using BE.recorrido_entities;

namespace BLL.informacion_components
{
    // Una ruta con baja puntualidad y retraso promedio alto de forma sostenida sugiere que el tiempo estimado
    // de recorrido (o el espaciado entre salidas) no es realista.
    public class ReglaRutaRetrasoSistematico : ReglaRecomendacion
    {
        private const int MuestraMinima = 30;
        private const double PuntualidadMaximaPct = 70;
        private const double PuntualidadCriticaPct = 50;
        private const double RetrasoPromedioMinimoMin = 10;
        private const double PercentilSugerido = 0.8;

        // Textos de la recomendacion en el idioma por defecto
        private static readonly CatalogoMensajes Mensajes = new CatalogoMensajes("RecomendacionesUI", new Dictionary<string, string>
        {
            ["msg_RecRutaRetrasoTitulo"] = "Ruta {0} ({1}): retrasos sostenidos",
            ["msg_RecRutaRetrasoEvidencia"] = "Puntualidad de {0} y retraso promedio de {1} min en {2} salidas del {3}.",
            ["msg_RecRutaRetrasoAccion"] = "Evaluar aumentar el tiempo estimado de recorrido de la ruta de {0} min a unos {1} min, o bien ampliar el tiempo entre salidas y el tiempo de descanso en los cronogramas de esta ruta."
        });

        public override List<Recomendacion> Evaluar(DatosAnalisisServicio datos)
        {
            List<Recomendacion> recomendaciones = new List<Recomendacion>();

            foreach (IGrouping<string, SalidaAnalizada> grupo in datos.salidasAuditadas.GroupBy(s => s.cronograma.ruta.id))
            {
                List<SalidaAnalizada> salidas = grupo.ToList();
                if (salidas.Count < MuestraMinima) continue;

                double puntualidad = datos.PuntualidadPct(salidas);
                double retrasoPromedio = datos.RetrasoPromedioMin(salidas);
                if (puntualidad >= PuntualidadMaximaPct || retrasoPromedio <= RetrasoPromedioMinimoMin) continue;

                Ruta ruta = salidas[0].cronograma.ruta;
                int tiempoSugerido = RedondearA5(ruta.tiempoEstimadoMin + Percentil(salidas.Select(s => Math.Max(0, s.retrasoMin)).ToList(), PercentilSugerido));

                recomendaciones.Add(new Recomendacion(
                    puntualidad < PuntualidadCriticaPct ? SeveridadRecomendacion.Alta : SeveridadRecomendacion.Media,
                    Mensajes.Traducir("msg_RecRutaRetrasoTitulo", ruta.descripcion, ruta.sentido),
                    Mensajes.Traducir("msg_RecRutaRetrasoEvidencia", Porcentaje(puntualidad), Numero(retrasoPromedio), salidas.Count, Periodo(datos)),
                    Mensajes.Traducir("msg_RecRutaRetrasoAccion", ruta.tiempoEstimadoMin, tiempoSugerido)));
            }

            return recomendaciones;
        }

        private static double Percentil(List<int> valores, double percentil)
        {
            valores.Sort();
            int indice = (int)Math.Ceiling(percentil * valores.Count) - 1;
            return valores[Math.Clamp(indice, 0, valores.Count - 1)];
        }

        private static int RedondearA5(double minutos) => (int)(Math.Ceiling(minutos / 5) * 5);
    }
}
