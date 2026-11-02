using BE.informacion_entities;
using BE.interno_entities;

namespace BLL.informacion_components
{
    // Un chofer con varias sanciones y retrasos muy superiores al promedio de la flota sugiere un problema propio
    // del chofer (no de la ruta), que se atiende con seguimiento y no cambiando los horarios.
    public class ReglaChoferRetrasosYSanciones : ReglaRecomendacion
    {
        private const int MuestraMinima = 20;
        private const int SancionesMinimas = 3;
        private const int SancionesCriticas = 6;
        private const double FactorSobreFlota = 1.5;
        private const double RetrasoPromedioMinimoMin = 10;

        // Textos de la recomendacion en el idioma por defecto
        private static readonly CatalogoMensajes Mensajes = new CatalogoMensajes("RecomendacionesUI", new Dictionary<string, string>
        {
            ["msg_RecChoferTitulo"] = "Chofer {0}: retrasos reiterados y sanciones",
            ["msg_RecChoferEvidencia"] = "{0} sanciones y retraso promedio de {1} min en {2} salidas, contra {3} min del conjunto de choferes ({4}).",
            ["msg_RecChoferAccion"] = "Evaluar un seguimiento o capacitación del chofer y revisar sus asignaciones de rutas y horarios."
        });

        public override List<Recomendacion> Evaluar(DatosAnalisisServicio datos)
        {
            List<Recomendacion> recomendaciones = new List<Recomendacion>();

            List<SalidaAnalizada> conChofer = datos.salidasAuditadas.Where(s => s.salida.choferAsignado != null).ToList();
            if (conChofer.Count == 0) return recomendaciones;
            double retrasoFlota = datos.RetrasoPromedioMin(conChofer);

            foreach (IGrouping<int, SalidaAnalizada> grupo in conChofer.GroupBy(s => s.salida.choferAsignado!.num_chofer))
            {
                List<SalidaAnalizada> salidas = grupo.ToList();
                if (salidas.Count < MuestraMinima) continue;

                Chofer chofer = salidas[0].salida.choferAsignado!;
                int sanciones = datos.sanciones.Count(s => s.choferSancionado.num_chofer == chofer.num_chofer);
                double retrasoChofer = datos.RetrasoPromedioMin(salidas);
                if (sanciones < SancionesMinimas || retrasoChofer <= RetrasoPromedioMinimoMin || retrasoChofer < FactorSobreFlota * retrasoFlota) continue;

                recomendaciones.Add(new Recomendacion(
                    sanciones >= SancionesCriticas ? SeveridadRecomendacion.Alta : SeveridadRecomendacion.Media,
                    Mensajes.Traducir("msg_RecChoferTitulo", chofer.nombreCompleto),
                    Mensajes.Traducir("msg_RecChoferEvidencia", sanciones, Numero(retrasoChofer), salidas.Count, Numero(retrasoFlota), Periodo(datos)),
                    Mensajes.Traducir("msg_RecChoferAccion")));
            }

            return recomendaciones;
        }
    }
}
