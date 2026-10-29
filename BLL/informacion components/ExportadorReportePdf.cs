using BE.informacion_entities;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Globalization;

namespace BLL.informacion_components
{
    public static class ExportadorReportePdf
    {
        private static readonly CultureInfo Cultura = new CultureInfo("es-AR");
        private const string ColorTitulo = "#1F3B5C";
        private const string ColorSuave = "#F1F4F8";
        private const string ColorBorde = "#C9D2DE";

        /// <summary>
        /// Genera el PDF y lo guarda en rutaArchivo. Lanza una Exception con el mensaje del caso alternativo si la carpeta no existe
        /// o no se puede escribir en ella; en ese caso no se crea ningun archivo.
        /// </summary>
        public static void Exportar(ReporteServicio reporte, string rutaArchivo)
        {
            string? carpeta = string.IsNullOrWhiteSpace(rutaArchivo) ? null : Path.GetDirectoryName(Path.GetFullPath(rutaArchivo));
            if (string.IsNullOrEmpty(carpeta) || !Directory.Exists(carpeta))
                throw new Exception(MensajeRutaInvalida());

            // Primero se genera en memoria y recien despues se escribe: un fallo al generar no deja un archivo a medias
            byte[] pdf = GenerarPdf(reporte);

            try
            {
                File.WriteAllBytes(rutaArchivo, pdf);
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or NotSupportedException or ArgumentException)
            {
                throw new Exception(MensajeRutaInvalida(), ex);
            }
        }

        public static byte[] GenerarPdf(ReporteServicio reporte) => CrearDocumento(reporte).GeneratePdf();

        public static Document CrearDocumento(ReporteServicio reporte)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            byte[] graficoRutas = GraficosReporteServicio.RenderizarPng(GraficosReporteServicio.PuntualidadPorRuta, reporte, 900, 560);
            byte[] graficoEvolucion = GraficosReporteServicio.RenderizarPng(GraficosReporteServicio.EvolucionPuntualidad, reporte, 900, 560);
            byte[] graficoTramos = GraficosReporteServicio.RenderizarPng(GraficosReporteServicio.DistribucionRetrasos, reporte, 900, 560);
            byte[] graficoGastos = GraficosReporteServicio.RenderizarPng(GraficosReporteServicio.GastosMensuales, reporte, 900, 560);

            return Document.Create(documento =>
            {
                documento.Page(pagina =>
                {
                    pagina.Size(PageSizes.A4);
                    pagina.Margin(28);
                    pagina.DefaultTextStyle(x => x.FontSize(9));

                    pagina.Header().Column(col =>
                    {
                        col.Item().Text("Reporte general del servicio").FontSize(20).Bold().FontColor(ColorTitulo);
                        col.Item().Text($"Período: {reporte.desde:dd/MM/yyyy} al {reporte.hasta:dd/MM/yyyy}   |   Generado el {reporte.fechaGeneracion:dd/MM/yyyy HH:mm}")
                            .FontSize(9).FontColor(Colors.Grey.Darken1);
                        col.Item().PaddingTop(4).LineHorizontal(1).LineColor(ColorBorde);
                    });

                    pagina.Content().PaddingVertical(8).Column(col =>
                    {
                        col.Spacing(10);
                        col.Item().Element(c => Indicadores(c, reporte));

                        col.Item().Row(fila =>
                        {
                            fila.RelativeItem().Image(graficoRutas);
                            fila.ConstantItem(8);
                            fila.RelativeItem().Image(graficoEvolucion);
                        });
                        col.Item().Row(fila =>
                        {
                            fila.RelativeItem().Image(graficoTramos);
                            fila.ConstantItem(8);
                            fila.RelativeItem().Image(graficoGastos);
                        });

                        col.Item().PageBreak();

                        col.Item().Element(c => Seccion(c, "Rendimiento por ruta",
                            new[] { "Ruta", "Programadas", "Suspendidas", "Con llegada", "Puntualidad", "Retraso prom. (min)" },
                            reporte.porRuta.Select(r => new[] { r.nombre, r.programadas.ToString(), r.suspendidas.ToString(), r.conLlegada.ToString(),
                                Pct(r.puntualidadPct), Num(r.retrasoPromedioMin) }),
                            new float[] { 4, 1.3f, 1.3f, 1.3f, 1.3f, 1.6f }));

                        col.Item().Element(c => Seccion(c, "Suspensiones por motivo",
                            new[] { "Motivo", "Cantidad" },
                            reporte.suspensiones.Select(s => new[] { s.motivo, s.cantidad.ToString() }),
                            new float[] { 5, 1 }));

                        col.Item().Element(c => Seccion(c, "Choferes con más sanciones",
                            new[] { "Chofer", "Sanciones", "Retraso prom. (min)" },
                            reporte.choferesConSanciones.Select(ch => new[] { ch.nombreCompleto, ch.sanciones.ToString(), Num(ch.retrasoPromedioMin) }),
                            new float[] { 4, 1.2f, 1.6f }));

                        col.Item().Element(c => Seccion(c, "Taller: internos con mayor gasto",
                            new[] { "Interno", "Modelo", "Revisiones", "Reparaciones", "Gasto" },
                            reporte.tallerPorInterno.Select(t => new[] { t.patente, t.modelo, t.revisiones.ToString(), t.reparaciones.ToString(), Dinero(t.gasto) }),
                            new float[] { 1.5f, 3, 1.3f, 1.4f, 2 }));

                        col.Item().Element(c => Seccion(c, "Consumo de combustible por interno",
                            new[] { "Interno", "Modelo", "Litros", "Km recorridos", "L/100 km" },
                            reporte.consumoPorInterno.Select(i => new[] { i.patente, i.modelo, i.litros.ToString("N0", Cultura), i.km.ToString("N0", Cultura),
                                i.litrosCada100Km?.ToString("0.0", Cultura) ?? "-" }),
                            new float[] { 1.5f, 3, 1.5f, 1.7f, 1.4f }));

                        col.Item().Text(Notas(reporte)).FontSize(8).FontColor(Colors.Grey.Darken1);
                    });

                    pagina.Footer().AlignCenter().Text(t =>
                    {
                        t.DefaultTextStyle(x => x.FontSize(8).FontColor(Colors.Grey.Darken1));
                        t.Span("Página ");
                        t.CurrentPageNumber();
                        t.Span(" de ");
                        t.TotalPages();
                    });
                });
            });
        }

        private static void Indicadores(IContainer contenedor, ReporteServicio reporte)
        {
            ResumenServicio r = reporte.resumen;
            (string Valor, string Etiqueta)[] kpis =
            {
                (r.salidasProgramadas.ToString("N0", Cultura), "Salidas programadas"),
                (Pct(r.puntualidadPct), $"Puntualidad (hasta {reporte.toleranciaMinutos} min)"),
                ($"{Num(r.retrasoPromedioMin)} min", "Retraso promedio"),
                ($"{r.salidasSuspendidas} ({Pct(r.suspensionPct)})", "Salidas suspendidas"),
                (r.sanciones.ToString(), "Sanciones a choferes"),
                (Dinero(r.gastoCombustible), "Gasto en combustible"),
                (Dinero(r.gastoTaller), "Gasto en taller"),
                (Dinero(r.gastoTotal), "Gasto total")
            };

            contenedor.Table(tabla =>
            {
                tabla.ColumnsDefinition(c => { for (int i = 0; i < 4; i++) c.RelativeColumn(); });
                foreach ((string valor, string etiqueta) in kpis)
                {
                    tabla.Cell().Border(1).BorderColor(ColorBorde).Background(ColorSuave).Padding(6).Column(c =>
                    {
                        c.Item().Text(valor).FontSize(13).Bold().FontColor(ColorTitulo);
                        c.Item().Text(etiqueta).FontSize(8).FontColor(Colors.Grey.Darken1);
                    });
                }
            });
        }

        private static void Seccion(IContainer contenedor, string titulo, string[] encabezados, IEnumerable<string[]> filas, float[] anchos)
        {
            List<string[]> datos = filas.ToList();
            contenedor.ShowEntire().Column(col =>
            {
                col.Item().PaddingBottom(3).Text(titulo).FontSize(11).Bold().FontColor(ColorTitulo);

                if (datos.Count == 0)
                {
                    col.Item().Text("Sin datos en el período seleccionado.").Italic().FontColor(Colors.Grey.Darken1);
                    return;
                }

                col.Item().Table(tabla =>
                {
                    tabla.ColumnsDefinition(c => { foreach (float ancho in anchos) c.RelativeColumn(ancho); });

                    tabla.Header(h =>
                    {
                        foreach (string encabezado in encabezados)
                            h.Cell().Background(ColorTitulo).Padding(4).Text(encabezado).Bold().FontColor(Colors.White);
                    });

                    for (int i = 0; i < datos.Count; i++)
                    {
                        string fondo = i % 2 == 0 ? Colors.White : ColorSuave;
                        foreach (string celda in datos[i])
                            tabla.Cell().Background(fondo).BorderBottom(0.5f).BorderColor(ColorBorde).Padding(4).Text(celda);
                    }
                });
            });
        }

        private static string Notas(ReporteServicio reporte)
        {
            List<string> notas = new List<string>
            {
                $"Una llegada se considera puntual si el retraso es de hasta {reporte.toleranciaMinutos} minutos. La puntualidad y el retraso promedio se calculan sobre las salidas con llegada registrada.",
                "Las cargas de combustible anuladas no se incluyen. El consumo se calcula con los litros cargados después de la primera carga del período y los kilómetros entre la primera y la última."
            };
            if (reporte.resumen.insumosSinCosto > 0)
                notas.Add($"{reporte.resumen.insumosSinCosto} insumo(s) de taller sin costo cargado no están incluidos en el gasto de taller.");
            return string.Join("\n", notas);
        }

        private static string MensajeRutaInvalida() =>
            GestorIdioma.GetInstance.TraducirMensaje("err_ReporteRutaInvalida", "La ubicación del archivo no es válida o no se puede acceder a ella. La operación fue cancelada.");

        private static string Pct(double valor) => valor.ToString("0.#", Cultura) + "%";

        private static string Num(double valor) => valor.ToString("0.#", Cultura);

        private static string Dinero(decimal monto) => monto.ToString("C0", Cultura);
    }
}
