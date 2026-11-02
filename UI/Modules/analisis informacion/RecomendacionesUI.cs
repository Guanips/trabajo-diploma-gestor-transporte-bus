using BE.informacion_entities;

namespace UI.Modules.analisis_informacion
{
    public partial class RecomendacionesUI : FormBaseObserver
    {
        // Mensajes del formulario (MessageBox y textos armados por código) en el idioma por defecto
        private static readonly Dictionary<string, string> MensajesFormulario = new()
        {
            ["msg_RecomendacionDetalle"] = "Situación detectada:\r\n{0}\r\n\r\nAcción sugerida:\r\n{1}",
            ["msg_RecomendacionesPeriodo"] = "Período analizado: {0:dd/MM/yyyy} al {1:dd/MM/yyyy} — {2} recomendación(es)"
        };

        protected override IReadOnlyDictionary<string, string> Mensajes => MensajesFormulario;

        private readonly DateOnly desde;
        private readonly DateOnly hasta;
        private readonly List<Recomendacion> recomendaciones;

        public RecomendacionesUI(DateOnly desde, DateOnly hasta, List<Recomendacion> recomendaciones)
        {
            InitializeComponent();
            this.desde = desde;
            this.hasta = hasta;
            this.recomendaciones = recomendaciones;

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

        // El periodo y el detalle se arman por codigo: se vuelven a armar cada vez que cambia el idioma
        protected override void TraducirElementosParticulares(string codigoIdioma)
        {
            labelRecomendacionesPeriodo.Text = T("msg_RecomendacionesPeriodo", desde, hasta, recomendaciones.Count);
            MostrarDetalleSeleccionado();
        }

        private void dataGridViewRecomendaciones_SelectionChanged(object? sender, EventArgs e)
        {
            MostrarDetalleSeleccionado();
        }

        private void MostrarDetalleSeleccionado()
        {
            // SelectedRows y no CurrentRow: en este evento la fila actual puede no estar actualizada todavia
            if (dataGridViewRecomendaciones.SelectedRows.Count > 0 && dataGridViewRecomendaciones.SelectedRows[0].DataBoundItem is Recomendacion seleccionada)
            {
                textBoxRecomendacionDetalle.Text = T("msg_RecomendacionDetalle", seleccionada.evidencia, seleccionada.accionSugerida);
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
