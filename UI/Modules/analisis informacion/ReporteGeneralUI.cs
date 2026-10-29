using BE.informacion_entities;
using BLL.informacion_components;
using ScottPlot.WinForms;

namespace UI.Modules.analisis_informacion
{
    public partial class ReporteGeneralUI : FormBaseObserver
    {
        private const string ValorVacio = "-";

        // Valores de los indicadores (cada uno dentro de un GroupBox con el nombre del indicador)
        private readonly Dictionary<string, Label> valoresKpi = new();

        private readonly FormsPlot graficoRutas = CrearGrafico("formsPlotReportePuntualidadRutas");
        private readonly FormsPlot graficoEvolucion = CrearGrafico("formsPlotReporteEvolucion");
        private readonly FormsPlot graficoTramos = CrearGrafico("formsPlotReporteTramos");
        private readonly FormsPlot graficoGastos = CrearGrafico("formsPlotReporteGastos");

        private readonly DataGridView gridRutas = CrearGrilla("dataGridViewReporteRutas");
        private readonly DataGridView gridSuspensiones = CrearGrilla("dataGridViewReporteSuspensiones");
        private readonly DataGridView gridChoferes = CrearGrilla("dataGridViewReporteChoferes");
        private readonly DataGridView gridTaller = CrearGrilla("dataGridViewReporteTaller");
        private readonly DataGridView gridConsumo = CrearGrilla("dataGridViewReporteConsumo");

        public ReporteGeneralUI()
        {
            InitializeComponent();

            dateTimePickerReporteHasta.Value = DateTime.Today;
            dateTimePickerReporteDesde.Value = DateTime.Today.AddDays(-29);

            ArmarIndicadores();
            tableLayoutPanelReporteGraficos.Controls.Add(graficoRutas, 0, 0);
            tableLayoutPanelReporteGraficos.Controls.Add(graficoEvolucion, 1, 0);
            tableLayoutPanelReporteGraficos.Controls.Add(graficoTramos, 0, 1);
            tableLayoutPanelReporteGraficos.Controls.Add(graficoGastos, 1, 1);

            ArmarGrilla(gridRutas, "Rendimiento por ruta", 0, 0, new[]
            {
                Columna(nameof(RutaRendimiento.nombre), "Ruta", null, 260),
                Columna(nameof(RutaRendimiento.programadas), "Programadas"),
                Columna(nameof(RutaRendimiento.suspendidas), "Suspendidas"),
                Columna(nameof(RutaRendimiento.puntualidadPct), "Puntualidad (%)", "0.#"),
                Columna(nameof(RutaRendimiento.retrasoPromedioMin), "Retraso prom. (min)", "0.#")
            });
            ArmarGrilla(gridSuspensiones, "Suspensiones por motivo", 1, 0, new[]
            {
                Columna(nameof(MotivoCantidad.motivo), "Motivo", null, 300),
                Columna(nameof(MotivoCantidad.cantidad), "Cantidad")
            });
            ArmarGrilla(gridChoferes, "Choferes con más sanciones", 0, 1, new[]
            {
                Columna(nameof(ChoferSancionado.nombreCompleto), "Chofer", null, 220),
                Columna(nameof(ChoferSancionado.sanciones), "Sanciones"),
                Columna(nameof(ChoferSancionado.retrasoPromedioMin), "Retraso prom. (min)", "0.#")
            });
            ArmarGrilla(gridTaller, "Taller: internos con mayor gasto", 1, 1, new[]
            {
                Columna(nameof(GastoInterno.patente), "Interno"),
                Columna(nameof(GastoInterno.modelo), "Modelo", null, 200),
                Columna(nameof(GastoInterno.revisiones), "Revisiones"),
                Columna(nameof(GastoInterno.reparaciones), "Reparaciones"),
                Columna(nameof(GastoInterno.gasto), "Gasto ($)", "N0")
            });
            ArmarGrilla(gridConsumo, "Consumo de combustible por interno", 0, 2, new[]
            {
                Columna(nameof(ConsumoInterno.patente), "Interno"),
                Columna(nameof(ConsumoInterno.modelo), "Modelo", null, 200),
                Columna(nameof(ConsumoInterno.litros), "Litros", "N0"),
                Columna(nameof(ConsumoInterno.km), "Km recorridos", "N0"),
                Columna(nameof(ConsumoInterno.litrosCada100Km), "L/100 km", "0.0")
            });
        }

        // -------------------------------------------------------------------
        // CU-INFO-Generar reporte general del servicio
        // -------------------------------------------------------------------

        private async void buttonReporteGenerar_Click(object? sender, EventArgs e)
        {
            if (!RangoValido(out DateOnly desde, out DateOnly hasta)) return;

            EstablecerOcupado(true);
            try
            {
                ReporteServicio reporte = await Task.Run(() => GestorReporteServicio.Generar(desde, hasta));
                MostrarReporte(reporte);
            }
            catch (Exception ex)
            {
                MostrarReporte(null);
                NotificarError(ex);
            }
            finally
            {
                EstablecerOcupado(false);
            }
        }

        // -------------------------------------------------------------------
        // CU-INFO-Exportar reporte general a PDF
        // -------------------------------------------------------------------

        private async void buttonReporteExportarPdf_Click(object? sender, EventArgs e)
        {
            if (!RangoValido(out DateOnly desde, out DateOnly hasta)) return;

            // El sistema pide la ubicacion antes de obtener los datos
            using SaveFileDialog dialogo = new SaveFileDialog
            {
                Filter = "Archivos PDF (*.pdf)|*.pdf",
                Title = T("msg_ReporteGuardarComo", "Guardar reporte general"),
                FileName = $"Reporte_general_servicio_{desde:yyyyMMdd}_{hasta:yyyyMMdd}.pdf",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                OverwritePrompt = true
            };
            if (dialogo.ShowDialog(this) != DialogResult.OK) return;

            string ruta = dialogo.FileName;
            EstablecerOcupado(true);
            try
            {
                await Task.Run(() => ExportadorReportePdf.Exportar(GestorReporteServicio.Generar(desde, hasta), ruta));
                MessageBox.Show(T("msg_ReporteExportado", "El reporte se exportó correctamente a:") + $"\n{ruta}",
                    T("msg_TituloExito", "Exportación exitosa"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                NotificarError(ex);
            }
            finally
            {
                EstablecerOcupado(false);
            }
        }

        // -------------------------------------------------------------------
        // Utilidades
        // -------------------------------------------------------------------

        private bool RangoValido(out DateOnly desde, out DateOnly hasta)
        {
            desde = DateOnly.FromDateTime(dateTimePickerReporteDesde.Value);
            hasta = DateOnly.FromDateTime(dateTimePickerReporteHasta.Value);
            if (desde <= hasta) return true;

            MessageBox.Show(T("err_ReporteRangoInvalido", "La fecha de inicio no puede ser posterior a la fecha de fin."),
                T("msg_TituloError", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        private void NotificarError(Exception ex)
        {
            MessageBox.Show(ex.Message, T("msg_TituloError", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void EstablecerOcupado(bool ocupado)
        {
            UseWaitCursor = ocupado;
            buttonReporteGenerar.Enabled = !ocupado;
            buttonReporteExportarPdf.Enabled = !ocupado;
            dateTimePickerReporteDesde.Enabled = !ocupado;
            dateTimePickerReporteHasta.Enabled = !ocupado;
        }

        private void MostrarReporte(ReporteServicio? reporte)
        {
            ResumenServicio? r = reporte?.resumen;
            var es = System.Globalization.CultureInfo.GetCultureInfo("es-AR");

            Establecer("salidas", r?.salidasProgramadas.ToString("N0", es));
            Establecer("puntualidad", r == null ? null : $"{r.puntualidadPct.ToString("0.#", es)}%");
            Establecer("retraso", r == null ? null : $"{r.retrasoPromedioMin.ToString("0.#", es)} min");
            Establecer("suspendidas", r == null ? null : $"{r.salidasSuspendidas} ({r.suspensionPct.ToString("0.#", es)}%)");
            Establecer("sanciones", r?.sanciones.ToString());
            Establecer("combustible", r?.gastoCombustible.ToString("C0", es));
            Establecer("taller", r?.gastoTaller.ToString("C0", es));
            Establecer("total", r?.gastoTotal.ToString("C0", es));

            if (reporte != null)
            {
                GraficosReporteServicio.PuntualidadPorRuta(graficoRutas.Plot, reporte);
                GraficosReporteServicio.EvolucionPuntualidad(graficoEvolucion.Plot, reporte);
                GraficosReporteServicio.DistribucionRetrasos(graficoTramos.Plot, reporte);
                GraficosReporteServicio.GastosMensuales(graficoGastos.Plot, reporte);
            }
            else
            {
                foreach (FormsPlot grafico in new[] { graficoRutas, graficoEvolucion, graficoTramos, graficoGastos })
                    grafico.Plot.Clear();
            }
            foreach (FormsPlot grafico in new[] { graficoRutas, graficoEvolucion, graficoTramos, graficoGastos })
                grafico.Refresh();

            gridRutas.DataSource = reporte?.porRuta;
            gridSuspensiones.DataSource = reporte?.suspensiones;
            gridChoferes.DataSource = reporte?.choferesConSanciones;
            gridTaller.DataSource = reporte?.tallerPorInterno;
            gridConsumo.DataSource = reporte?.consumoPorInterno;
        }

        private void Establecer(string clave, string? valor) => valoresKpi[clave].Text = valor ?? ValorVacio;

        // -------------------------------------------------------------------
        // Construccion de controles (indicadores, graficos y grillas)
        // -------------------------------------------------------------------

        private void ArmarIndicadores()
        {
            (string Clave, string Titulo)[] indicadores =
            {
                ("salidas", "Salidas programadas"), ("puntualidad", "Puntualidad"), ("retraso", "Retraso promedio"), ("suspendidas", "Salidas suspendidas"),
                ("sanciones", "Sanciones a choferes"), ("combustible", "Gasto en combustible"), ("taller", "Gasto en taller"), ("total", "Gasto total")
            };

            for (int i = 0; i < indicadores.Length; i++)
            {
                // El valor se arma por codigo: se excluye del traductor. El titulo del GroupBox si se traduce.
                Label valor = new Label
                {
                    Name = $"labelReporteKpi_{indicadores[i].Clave}",
                    Tag = TraductorFormulario.TagNoTraducir,
                    Text = ValorVacio,
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 15F, FontStyle.Bold)
                };
                GroupBox caja = new GroupBox { Name = $"groupBoxReporteKpi_{indicadores[i].Clave}", Text = indicadores[i].Titulo, Dock = DockStyle.Fill };
                caja.Controls.Add(valor);
                tableLayoutPanelReporteKpis.Controls.Add(caja, i % 4, i / 4);
                valoresKpi[indicadores[i].Clave] = valor;
            }
        }

        private static FormsPlot CrearGrafico(string nombre) => new FormsPlot { Name = nombre, Dock = DockStyle.Fill };

        private static DataGridView CrearGrilla(string nombre) => new DataGridView
        {
            Name = nombre,
            Dock = DockStyle.Fill,
            ReadOnly = true,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            AutoGenerateColumns = false,
            RowHeadersVisible = false,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            BackgroundColor = SystemColors.Window
        };

        private static DataGridViewTextBoxColumn Columna(string propiedad, string encabezado, string? formato = null, float peso = 100)
        {
            DataGridViewTextBoxColumn columna = new DataGridViewTextBoxColumn
            {
                Name = propiedad,
                DataPropertyName = propiedad,
                HeaderText = encabezado,
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = peso
            };
            if (formato != null)
            {
                columna.DefaultCellStyle.Format = formato;
                columna.DefaultCellStyle.FormatProvider = System.Globalization.CultureInfo.GetCultureInfo("es-AR"); // igual que el PDF
            }
            return columna;
        }

        private void ArmarGrilla(DataGridView grilla, string titulo, int columna, int fila, DataGridViewColumn[] columnas)
        {
            grilla.Columns.AddRange(columnas);
            GroupBox caja = new GroupBox { Name = $"groupBox_{grilla.Name}", Text = titulo, Dock = DockStyle.Fill, Padding = new Padding(6) };
            caja.Controls.Add(grilla);
            tableLayoutPanelReporteDetalle.Controls.Add(caja, columna, fila);
        }
    }
}
