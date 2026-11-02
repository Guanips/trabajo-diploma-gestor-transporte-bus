using BE.informacion_entities;
using ScottPlot;

namespace BLL.informacion_components
{
    /// <summary>
    /// Define los graficos del reporte sobre un Plot de ScottPlot. Los usa la pantalla (control FormsPlot) y la
    /// exportacion a PDF (que renderiza el mismo Plot a una imagen), asi ambos muestran exactamente lo mismo.
    /// </summary>
    public static class GraficosReporteServicio
    {
        private static readonly Color Azul = Color.FromHex("#2F6DB5");
        private static readonly Color Naranja = Color.FromHex("#E08A1E");
        private static readonly Color Verde = Color.FromHex("#3F9F5F");
        private static readonly Color Rojo = Color.FromHex("#C8423B");
        private static readonly Color Amarillo = Color.FromHex("#E5C23A");

        // Colores de los tramos de retraso: de "a tiempo" a "muy grave"
        private static readonly Color[] ColoresTramos = { Verde, Color.FromHex("#9CC653"), Amarillo, Naranja, Rojo };

        // Textos de los graficos en el idioma por defecto
        private static readonly CatalogoMensajes Mensajes = new CatalogoMensajes("ReporteGeneralUI", new Dictionary<string, string>
        {
            ["msg_GraficoPuntualidadRutaTitulo"] = "Puntualidad por ruta (%)",
            ["msg_GraficoPuntualidadRutaEjeX"] = "Ruta (puntual: hasta {0} min de retraso)",
            ["msg_GraficoPorcentajePuntuales"] = "% de llegadas puntuales",
            ["msg_GraficoEvolucionTitulo"] = "Evolución de la puntualidad (%)",
            ["msg_GraficoDistribucionTitulo"] = "Distribución de retrasos de llegada",
            ["msg_GraficoGastosTitulo"] = "Gasto mensual: combustible y taller",
            ["msg_GraficoCombustible"] = "Combustible",
            ["msg_GraficoTaller"] = "Taller",
            ["msg_GraficoMillonesPesos"] = "Millones de pesos ($)",
            ["msg_GraficoSinDatos"] = "Sin datos en el rango seleccionado",
            ["msg_TramoRetraso1"] = "A tiempo",
            ["msg_TramoRetraso2"] = "Retraso leve",
            ["msg_TramoRetraso3"] = "Retraso moderado",
            ["msg_TramoRetraso4"] = "Retraso grave",
            ["msg_TramoRetraso5"] = "Retraso muy grave"
        });

        /// <summary>Nombre del tramo de retraso (1 = a tiempo ... 5 = muy grave) en el idioma actual.</summary>
        public static string NombreTramo(int numero) => Mensajes.Traducir($"msg_TramoRetraso{numero}");

        public static void PuntualidadPorRuta(Plot plot, ReporteServicio reporte)
        {
            plot.Clear();
            plot.Title(Mensajes.Traducir("msg_GraficoPuntualidadRutaTitulo"));
            if (reporte.porRuta.Count(r => r.conLlegada > 0) == 0) { SinDatos(plot); return; }

            List<RutaRendimiento> rutas = reporte.porRuta.Where(r => r.conLlegada > 0).ToList();
            List<Bar> barras = new List<Bar>();
            for (int i = 0; i < rutas.Count; i++)
                barras.Add(new Bar { Position = i, Value = Math.Round(rutas[i].puntualidadPct, 1), FillColor = ColorPuntualidad(rutas[i].puntualidadPct), Size = 0.6 });

            plot.Add.Bars(barras);
            plot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericManual(
                Enumerable.Range(0, rutas.Count).Select(i => new Tick(i, rutas[i].id)).ToArray());
            plot.Axes.AutoScale(); // un Plot reutilizado (pantalla) conserva los limites anteriores
            plot.Axes.SetLimitsY(0, 105);
            plot.YLabel(Mensajes.Traducir("msg_GraficoPorcentajePuntuales"));
            plot.XLabel(Mensajes.Traducir("msg_GraficoPuntualidadRutaEjeX", reporte.toleranciaMinutos));
        }

        public static void EvolucionPuntualidad(Plot plot, ReporteServicio reporte)
        {
            plot.Clear();
            plot.Title(Mensajes.Traducir("msg_GraficoEvolucionTitulo"));
            if (reporte.evolucionDiaria.Count == 0) { SinDatos(plot); return; }

            double[] xs = reporte.evolucionDiaria.Select(p => p.fecha.ToDateTime(TimeOnly.MinValue).ToOADate()).ToArray();
            double[] ys = reporte.evolucionDiaria.Select(p => Math.Round(p.puntualidadPct, 1)).ToArray();

            var linea = plot.Add.Scatter(xs, ys);
            linea.Color = Azul;
            linea.LineWidth = 2;
            linea.MarkerSize = xs.Length > 40 ? 3 : 6;

            plot.Axes.DateTimeTicksBottom();
            plot.Axes.AutoScale();
            plot.Axes.SetLimitsY(0, 105);
            plot.YLabel(Mensajes.Traducir("msg_GraficoPorcentajePuntuales"));
        }

        public static void DistribucionRetrasos(Plot plot, ReporteServicio reporte)
        {
            plot.Clear();
            plot.Title(Mensajes.Traducir("msg_GraficoDistribucionTitulo"));
            if (reporte.distribucionRetrasos.Sum(t => t.cantidad) == 0) { SinDatos(plot); return; }

            List<PieSlice> porciones = new List<PieSlice>();
            for (int i = 0; i < reporte.distribucionRetrasos.Count; i++)
            {
                TramoRetraso tramo = reporte.distribucionRetrasos[i];
                if (tramo.cantidad == 0) continue;
                // El nombre se toma por posicion (y no de tramo.nombre) para que salga en el idioma actual aunque
                // el reporte se haya generado antes de cambiar de idioma
                porciones.Add(new PieSlice { Value = tramo.cantidad, FillColor = ColoresTramos[i], Label = $"{NombreTramo(i + 1)} ({tramo.cantidad})" });
            }

            var torta = plot.Add.Pie(porciones);
            torta.DonutFraction = 0.5;
            torta.SliceLabelDistance = 1.45; // etiquetas fuera de la dona para que no se pisen con el grafico
            plot.Axes.Frameless();
            plot.HideGrid();
            plot.Axes.SetLimits(-2.6, 2.6, -1.5, 1.5); // espacio lateral para las etiquetas
        }

        public static void GastosMensuales(Plot plot, ReporteServicio reporte)
        {
            plot.Clear();
            plot.Title(Mensajes.Traducir("msg_GraficoGastosTitulo"));
            if (reporte.gastosMensuales.Count == 0) { SinDatos(plot); return; }

            List<GastoMensual> meses = reporte.gastosMensuales;
            List<Bar> combustible = new List<Bar>();
            List<Bar> taller = new List<Bar>();
            for (int i = 0; i < meses.Count; i++)
            {
                combustible.Add(new Bar { Position = i - 0.2, Value = (double)meses[i].combustible / 1_000_000, FillColor = Azul, Size = 0.38 });
                taller.Add(new Bar { Position = i + 0.2, Value = (double)meses[i].taller / 1_000_000, FillColor = Naranja, Size = 0.38 });
            }

            plot.Add.Bars(combustible).LegendText = Mensajes.Traducir("msg_GraficoCombustible");
            plot.Add.Bars(taller).LegendText = Mensajes.Traducir("msg_GraficoTaller");
            plot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericManual(
                Enumerable.Range(0, meses.Count).Select(i => new Tick(i, meses[i].mes)).ToArray());
            plot.Axes.AutoScale();
            plot.Axes.Margins(bottom: 0, top: 0.15);
            plot.YLabel(Mensajes.Traducir("msg_GraficoMillonesPesos"));
            plot.ShowLegend(Alignment.UpperRight);
        }

        /// <summary>Renderiza un grafico a PNG (para incrustarlo en el PDF).</summary>
        public static byte[] RenderizarPng(Action<Plot, ReporteServicio> graficar, ReporteServicio reporte, int ancho, int alto)
        {
            Plot plot = new Plot();
            graficar(plot, reporte);
            return plot.GetImageBytes(ancho, alto, ImageFormat.Png);
        }

        private static Color ColorPuntualidad(double pct) => pct >= 85 ? Verde : pct >= 70 ? Amarillo : Rojo;

        private static void SinDatos(Plot plot)
        {
            plot.Axes.Frameless();
            plot.HideGrid();
            plot.Add.Annotation(Mensajes.Traducir("msg_GraficoSinDatos"), Alignment.MiddleCenter);
        }
    }
}
