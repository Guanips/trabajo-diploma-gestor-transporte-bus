using BE.informacion_entities;
using BE.interno_entities;
using BE.taller_entities;

namespace BLL.informacion_components
{
    // Un interno que consume bastante mas combustible por kilometro que la mediana de la flota puede tener un
    // problema mecanico o de conduccion.
    public class ReglaInternoConsumoAnomalo : ReglaRecomendacion
    {
        private const int InternosMinimos = 3;
        private const double FactorSobreMediana = 1.25;

        public override List<Recomendacion> Evaluar(DatosAnalisisServicio datos)
        {
            List<Recomendacion> recomendaciones = new List<Recomendacion>();

            List<(Interno interno, double litrosCada100Km)> consumos = new List<(Interno, double)>();
            foreach (IGrouping<int, CargaCombustible> grupo in datos.cargas.GroupBy(c => c.interno.num_interno))
            {
                List<CargaCombustible> cargas = grupo.OrderBy(c => c.fechaHora).ToList();
                int km = cargas[^1].kilometrajeActual - cargas[0].kilometrajeActual;
                if (cargas.Count < 2 || km <= 0) continue;

                // Los litros de la primera carga rellenaron lo recorrido antes del periodo, por eso no se cuentan
                decimal litros = cargas.Skip(1).Sum(c => c.litrosCargados);
                consumos.Add((cargas[0].interno, (double)litros / km * 100.0));
            }
            if (consumos.Count < InternosMinimos) return recomendaciones;

            double mediana = Mediana(consumos.Select(c => c.litrosCada100Km).ToList());
            foreach ((Interno interno, double litrosCada100Km) in consumos.Where(c => c.litrosCada100Km > FactorSobreMediana * mediana))
            {
                recomendaciones.Add(new Recomendacion(
                    SeveridadRecomendacion.Media,
                    $"Interno {interno.patente} ({interno.modelo}): consumo de combustible elevado",
                    $"Consumo de {Numero(litrosCada100Km)} L/100 km contra una mediana de {Numero(mediana)} L/100 km de la flota en el período {Periodo(datos)}.",
                    "Revisar el estado mecánico del interno (inyección, filtros, neumáticos) y el estilo de conducción de sus choferes."));
            }

            return recomendaciones;
        }

        private static double Mediana(List<double> valores)
        {
            valores.Sort();
            int medio = valores.Count / 2;
            return valores.Count % 2 == 1 ? valores[medio] : (valores[medio - 1] + valores[medio]) / 2;
        }
    }
}
