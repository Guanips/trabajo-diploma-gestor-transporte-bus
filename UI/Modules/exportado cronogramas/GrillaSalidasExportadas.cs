using BE.cronograma_entities;
using BLL;

namespace UI.Modules.exportado_cronogramas
{
    internal class SalidaExportadaGridRow
    {
        public SalidaExportada Salida { get; }
        public string ChoferAsignado { get; }
        public string InternoAsignado { get; }
        public string HoraSalidaTeorica { get; }
        public string HoraLlegadaTeorica { get; }
        public string EstaSuspendida { get; }
        public string MotivoSuspension { get; }

        public SalidaExportadaGridRow(SalidaExportada salida)
        {
            Salida = salida;
            ChoferAsignado = salida.Chofer?.NombreCompleto ?? "";
            InternoAsignado = salida.Interno?.Patente ?? "";
            HoraSalidaTeorica = salida.HoraSalidaTeorica.ToString("HH:mm");
            HoraLlegadaTeorica = salida.HoraLlegadaTeorica.ToString("HH:mm");
            EstaSuspendida = salida.EstaSuspendida
                ? GestorIdioma.GetInstance.TraducirMensaje("msg_Si", "Sí")
                : GestorIdioma.GetInstance.TraducirMensaje("msg_No", "No");
            MotivoSuspension = salida.MotivoSuspension ?? "";
        }
    }

    // Grilla de salidas compartida por la exportacion y la importacion: ambas muestran el mismo DTO
    internal static class GrillaSalidasExportadas
    {
        public static void Mostrar(DataGridView grilla, IEnumerable<SalidaExportada>? salidas)
        {
            grilla.DataBindingComplete -= GrillaEnlazada;
            grilla.DataBindingComplete += GrillaEnlazada;

            grilla.AutoGenerateColumns = false;
            grilla.Columns.Clear();

            if (salidas == null)
            {
                grilla.DataSource = null;
                return;
            }

            grilla.DataSource = salidas.Select(s => new SalidaExportadaGridRow(s)).ToList();

            AgregarColumna(grilla, nameof(SalidaExportadaGridRow.ChoferAsignado), "Chofer asignado");
            AgregarColumna(grilla, nameof(SalidaExportadaGridRow.InternoAsignado), "Interno asignado");
            AgregarColumna(grilla, nameof(SalidaExportadaGridRow.HoraSalidaTeorica), "Hora salida teórica");
            AgregarColumna(grilla, nameof(SalidaExportadaGridRow.HoraLlegadaTeorica), "Hora llegada teórica");
            AgregarColumna(grilla, nameof(SalidaExportadaGridRow.EstaSuspendida), "Está suspendida");
            AgregarColumna(grilla, nameof(SalidaExportadaGridRow.MotivoSuspension), "Motivo");

            AplicarFormato(grilla);
        }

        private static void AgregarColumna(DataGridView grilla, string propiedad, string encabezado)
        {
            grilla.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = propiedad,
                HeaderText = encabezado,
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
        }

        private static void GrillaEnlazada(object? sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (sender is DataGridView grilla) AplicarFormato(grilla);
        }

        private static void AplicarFormato(DataGridView grilla)
        {
            if (grilla.Columns.Count < 2) return;

            foreach (DataGridViewRow fila in grilla.Rows)
            {
                if (fila.IsNewRow || fila.DataBoundItem is not SalidaExportadaGridRow salidaFila) continue;

                fila.DefaultCellStyle.BackColor = Color.White;
                fila.DefaultCellStyle.ForeColor = Color.Black;
                fila.DefaultCellStyle.SelectionBackColor = Color.White;
                fila.DefaultCellStyle.SelectionForeColor = Color.Black;

                if (salidaFila.Salida.EstaSuspendida)
                {
                    fila.DefaultCellStyle.BackColor = Color.LightCoral;
                    fila.DefaultCellStyle.SelectionBackColor = Color.LightCoral;
                    continue;
                }

                // Naranja: salida sin chofer o sin interno asignado
                Color colorChofer = string.IsNullOrWhiteSpace(salidaFila.ChoferAsignado) ? Color.Orange : Color.White;
                Color colorInterno = string.IsNullOrWhiteSpace(salidaFila.InternoAsignado) ? Color.Orange : Color.White;

                fila.Cells[0].Style.BackColor = colorChofer;
                fila.Cells[0].Style.SelectionBackColor = colorChofer;
                fila.Cells[1].Style.BackColor = colorInterno;
                fila.Cells[1].Style.SelectionBackColor = colorInterno;
            }
        }
    }
}
