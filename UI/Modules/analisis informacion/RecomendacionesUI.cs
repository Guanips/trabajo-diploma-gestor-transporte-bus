using BE.informacion_entities;

namespace UI.Modules.analisis_informacion
{
    public partial class RecomendacionesUI : FormBaseObserver
    {
        private readonly List<Recomendacion> recomendaciones;

        public RecomendacionesUI(DateOnly desde, DateOnly hasta, List<Recomendacion> recomendaciones)
        {
            InitializeComponent();
            this.recomendaciones = recomendaciones;

            labelRecomendacionesPeriodo.Text = $"Período analizado: {desde:dd/MM/yyyy} al {hasta:dd/MM/yyyy} — {recomendaciones.Count} recomendación(es)";

            dataGridViewRecomendaciones.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "severidad",
                DataPropertyName = nameof(Recomendacion.severidad),
                HeaderText = "Severidad",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                Width = 90
            });
            dataGridViewRecomendaciones.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "titulo",
                DataPropertyName = nameof(Recomendacion.titulo),
                HeaderText = "Recomendación",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            dataGridViewRecomendaciones.DataSource = recomendaciones;
        }

        private void dataGridViewRecomendaciones_SelectionChanged(object? sender, EventArgs e)
        {
            // SelectedRows y no CurrentRow: en este evento la fila actual puede no estar actualizada todavia
            if (dataGridViewRecomendaciones.SelectedRows.Count > 0 && dataGridViewRecomendaciones.SelectedRows[0].DataBoundItem is Recomendacion seleccionada)
            {
                textBoxRecomendacionDetalle.Text =
                    $"Situación detectada:{Environment.NewLine}{seleccionada.evidencia}{Environment.NewLine}{Environment.NewLine}" +
                    $"Acción sugerida:{Environment.NewLine}{seleccionada.accionSugerida}";
            }
            else
            {
                textBoxRecomendacionDetalle.Clear();
            }
        }

        // Colorea la severidad: rojo (alta), naranja (media) y amarillo (baja)
        private void dataGridViewRecomendaciones_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || dataGridViewRecomendaciones.Columns[e.ColumnIndex].Name != "severidad") return;
            if (dataGridViewRecomendaciones.Rows[e.RowIndex].DataBoundItem is not Recomendacion recomendacion) return;

            e.CellStyle.BackColor = recomendacion.severidad switch
            {
                SeveridadRecomendacion.Alta => Color.LightCoral,
                SeveridadRecomendacion.Media => Color.Orange,
                _ => Color.Khaki
            };
            e.CellStyle.SelectionBackColor = e.CellStyle.BackColor;
            e.CellStyle.ForeColor = Color.Black;
            e.CellStyle.SelectionForeColor = Color.Black;
        }
    }
}
