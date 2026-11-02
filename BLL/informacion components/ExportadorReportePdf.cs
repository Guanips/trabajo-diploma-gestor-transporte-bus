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

        // Textos del PDF en el idioma por defecto
        private static readonly CatalogoMensajes Mensajes = new CatalogoMensajes("ReporteGeneralUI", new Dictionary<string, string>
        {
            ["err_ReporteRutaInvalida"] = "La ubicación del archivo no es válida o no se puede acceder a ella. La operación fue cancelada.",
            ["msg_PdfTitulo"] = "Reporte general del servicio",
            ["msg_PdfSubtitulo"] = "Período: {0:dd/MM/yyyy} al {1:dd/MM/yyyy}   |   Generado el {2:dd/MM/yyyy HH:mm}",
            ["msg_PdfPagina"] = "Página",
            ["msg_PdfPaginaDe"] = "de",
            ["msg_PdfSinDatos"] = "Sin datos en el período seleccionado.",
            ["msg_PdfKpiSalidas"] = "Salidas programadas",
            ["msg_PdfKpiPuntualidad"] = "Puntualidad (hasta {0} min)",
            ["msg_PdfKpiRetraso"] = "Retraso promedio",
            ["msg_PdfKpiSuspendidas"] = "Salidas suspendidas",
            ["msg_PdfKpiSanciones"] = "Sanciones a choferes",
            ["msg_PdfKpiCombustible"] = "Gasto en combustible",
            ["msg_PdfKpiTaller"] = "Gasto en taller",
            ["msg_PdfKpiTotal"] = "Gasto total",
            ["msg_PdfSeccionRutas"] = "Rendimiento por ruta",
            ["msg_PdfSeccionSuspensiones"] = "Suspensiones por motivo",
            ["msg_PdfSeccionChoferes"] = "Choferes con más sanciones",
            ["msg_PdfSeccionTaller"] = "Taller: internos con mayor gasto",
            ["msg_PdfSeccionConsumo"] = "Consumo de combustible por interno",
            ["msg_PdfColRuta"] = "Ruta",
            ["msg_PdfColProgramadas"] = "Programadas",
            ["msg_PdfColSuspendidas"] = "Suspendidas",
            ["msg_PdfColConLlegada"] = "Con llegada",
            ["msg_PdfColPuntualidad"] = "Puntualidad",
            ["msg_PdfColRetrasoPromedio"] = "Retraso prom. (min)",
            ["msg_PdfColMotivo"] = "Motivo",
            ["msg_PdfColCantidad"] = "Cantidad",
            ["msg_PdfColChofer"] = "Chofer",
            ["msg_PdfColSanciones"] = "Sanciones",
            ["msg_PdfColInterno"] = "Interno",
            ["msg_PdfColModelo"] = "Modelo",
            ["msg_PdfColRevisiones"] = "Revisiones",
            ["msg_PdfColReparaciones"] = "Reparaciones",
            ["msg_PdfColGasto"] = "Gasto",
            ["msg_PdfColLitros"] = "Litros",
            ["msg_PdfColKmRecorridos"] = "Km recorridos",
            ["msg_PdfColLitrosCada100Km"] = "L/100 km",
            ["msg_PdfNotaPuntualidad"] = "Una llegada se considera puntual si el retraso es de hasta {0} minutos. La puntualidad y el retraso promedio se calculan sobre las salidas con llegada registrada.",
            ["msg_PdfNotaCombustible"] = "Las cargas de combustible anuladas no se incluyen. El consumo se calcula con los litros cargados después de la primera carga del período y los kilómetros entre la primera y la última.",
            ["msg_PdfNotaInsumosSinCosto"] = "{0} insumo(s) de taller sin costo cargado no están incluidos en el gasto de taller."
        });

        private static string T(string clave, params object?[] argumentos) => Mensajes.Traducir(clave, argumentos);

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
                        col.Item().Text(T("msg_PdfTitulo")).FontSize(20).Bold().FontColor(ColorTitulo);
                        col.Item().Text(T("msg_PdfSubtitulo", reporte.desde, reporte.hasta, reporte.fechaGeneracion))
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

                        col.Item().Element(c => Seccion(c, T("msg_PdfSeccionRutas"),
                            new[] { T("msg_PdfColRuta"), T("msg_PdfColProgramadas"), T("msg_PdfColSuspendidas"), T("msg_PdfColConLlegada"), T("msg_PdfColPuntualidad"), T("msg_PdfColRetrasoPromedio") },
                            reporte.porRuta.Select(r => new[] { r.nombre, r.programadas.ToString(), r.suspendidas.ToString(), r.conLlegada.ToString(),
                                Pct(r.puntualidadPct), Num(r.retrasoPromedioMin) }),
                            new float[] { 4, 1.3f, 1.3f, 1.3f, 1.3f, 1.6f }));

                        col.Item().Element(c => Seccion(c, T("msg_PdfSeccionSuspensiones"),
                            new[] { T("msg_PdfColMotivo"), T("msg_PdfColCantidad") },
                            reporte.suspensiones.Select(s => new[] { s.motivo, s.cantidad.ToString() }),
                            new float[] { 5, 1 }));

                        col.Item().Element(c => Seccion(c, T("msg_PdfSeccionChoferes"),
                            new[] { T("msg_PdfColChofer"), T("msg_PdfColSanciones"), T("msg_PdfColRetrasoPromedio") },
                            reporte.choferesConSanciones.Select(ch => new[] { ch.nombreCompleto, ch.sanciones.ToString(), Num(ch.retrasoPromedioMin) }),
                            new float[] { 4, 1.2f, 1.6f }));

                        col.Item().Element(c => Seccion(c, T("msg_PdfSeccionTaller"),
                            new[] { T("msg_PdfColInterno"), T("msg_PdfColModelo"), T("msg_PdfColRevisiones"), T("msg_PdfColReparaciones"), T("msg_PdfColGasto") },
                            reporte.tallerPorInterno.Select(t => new[] { t.patente, t.modelo, t.revisiones.ToString(), t.reparaciones.ToString(), Dinero(t.gasto) }),
                            new float[] { 1.5f, 3, 1.3f, 1.4f, 2 }));

                        col.Item().Element(c => Seccion(c, T("msg_PdfSeccionConsumo"),
                            new[] { T("msg_PdfColInterno"), T("msg_PdfColModelo"), T("msg_PdfColLitros"), T("msg_PdfColKmRecorridos"), T("msg_PdfColLitrosCada100Km") },
                            reporte.consumoPorInterno.Select(i => new[] { i.patente, i.modelo, i.litros.ToString("N0", Cultura), i.km.ToString("N0", Cultura),
                                i.litrosCada100Km?.ToString("0.0", Cultura) ?? "-" }),
                            new float[] { 1.5f, 3, 1.5f, 1.7f, 1.4f }));

                        col.Item().Text(Notas(reporte)).FontSize(8).FontColor(Colors.Grey.Darken1);
                    });

                    pagina.Footer().AlignCenter().Text(t =>
                    {
                        t.DefaultTextStyle(x => x.FontSize(8).FontColor(Colors.Grey.Darken1));
                        t.Span(T("msg_PdfPagina") + " ");
                        t.CurrentPageNumber();
                        t.Span(" " + T("msg_PdfPaginaDe") + " ");
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
                (r.salidasProgramadas.ToString("N0", Cultura), T("msg_PdfKpiSalidas")),
                (Pct(r.puntualidadPct), T("msg_PdfKpiPuntualidad", reporte.toleranciaMinutos)),
                ($"{Num(r.retrasoPromedioMin)} min", T("msg_PdfKpiRetraso")),
                ($"{r.salidasSuspendidas} ({Pct(r.suspensionPct)})", T("msg_PdfKpiSuspendidas")),
                (r.sanciones.ToString(), T("msg_PdfKpiSanciones")),
                (Dinero(r.gastoCombustible), T("msg_PdfKpiCombustible")),
                (Dinero(r.gastoTaller), T("msg_PdfKpiTaller")),
                (Dinero(r.gastoTotal), T("msg_PdfKpiTotal"))
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
                    col.Item().Text(T("msg_PdfSinDatos")).Italic().FontColor(Colors.Grey.Darken1);
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
                T("msg_PdfNotaPuntualidad", reporte.toleranciaMinutos),
                T("msg_PdfNotaCombustible")
            };
            if (reporte.resumen.insumosSinCosto > 0)
                notas.Add(T("msg_PdfNotaInsumosSinCosto", reporte.resumen.insumosSinCosto));
            return string.Join("\n", notas);
        }

        private static string MensajeRutaInvalida() => T("err_ReporteRutaInvalida");

        private static string Pct(double valor) => valor.ToString("0.#", Cultura) + "%";

        private static string Num(double valor) => valor.ToString("0.#", Cultura);

        private static string Dinero(decimal monto) => monto.ToString("C0", Cultura);
    }
}
