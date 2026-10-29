namespace UI.Modules.analisis_informacion
{
    partial class ReporteGeneralUI
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            panelReporteFiltros = new Panel();
            labelReporteDesde = new Label();
            dateTimePickerReporteDesde = new DateTimePicker();
            labelReporteHasta = new Label();
            dateTimePickerReporteHasta = new DateTimePicker();
            buttonReporteGenerar = new Button();
            buttonReporteExportarPdf = new Button();
            tabControlReporte = new TabControl();
            tabPageReporteResumen = new TabPage();
            tableLayoutPanelReporteResumen = new TableLayoutPanel();
            tableLayoutPanelReporteKpis = new TableLayoutPanel();
            tableLayoutPanelReporteGraficos = new TableLayoutPanel();
            tabPageReporteDetalle = new TabPage();
            tableLayoutPanelReporteDetalle = new TableLayoutPanel();
            panelReporteFiltros.SuspendLayout();
            tabControlReporte.SuspendLayout();
            tabPageReporteResumen.SuspendLayout();
            tableLayoutPanelReporteResumen.SuspendLayout();
            tableLayoutPanelReporteKpis.SuspendLayout();
            tableLayoutPanelReporteGraficos.SuspendLayout();
            tabPageReporteDetalle.SuspendLayout();
            tableLayoutPanelReporteDetalle.SuspendLayout();
            SuspendLayout();
            //
            // panelReporteFiltros
            //
            panelReporteFiltros.Controls.Add(labelReporteDesde);
            panelReporteFiltros.Controls.Add(dateTimePickerReporteDesde);
            panelReporteFiltros.Controls.Add(labelReporteHasta);
            panelReporteFiltros.Controls.Add(dateTimePickerReporteHasta);
            panelReporteFiltros.Controls.Add(buttonReporteGenerar);
            panelReporteFiltros.Controls.Add(buttonReporteExportarPdf);
            panelReporteFiltros.Dock = DockStyle.Top;
            panelReporteFiltros.Name = "panelReporteFiltros";
            panelReporteFiltros.Size = new Size(1100, 54);
            panelReporteFiltros.TabIndex = 0;
            //
            // labelReporteDesde
            //
            labelReporteDesde.AutoSize = true;
            labelReporteDesde.Location = new Point(12, 18);
            labelReporteDesde.Name = "labelReporteDesde";
            labelReporteDesde.Text = "Desde";
            //
            // dateTimePickerReporteDesde
            //
            dateTimePickerReporteDesde.Format = DateTimePickerFormat.Short;
            dateTimePickerReporteDesde.Location = new Point(60, 14);
            dateTimePickerReporteDesde.Name = "dateTimePickerReporteDesde";
            dateTimePickerReporteDesde.Size = new Size(110, 23);
            dateTimePickerReporteDesde.TabIndex = 0;
            //
            // labelReporteHasta
            //
            labelReporteHasta.AutoSize = true;
            labelReporteHasta.Location = new Point(190, 18);
            labelReporteHasta.Name = "labelReporteHasta";
            labelReporteHasta.Text = "Hasta";
            //
            // dateTimePickerReporteHasta
            //
            dateTimePickerReporteHasta.Format = DateTimePickerFormat.Short;
            dateTimePickerReporteHasta.Location = new Point(235, 14);
            dateTimePickerReporteHasta.Name = "dateTimePickerReporteHasta";
            dateTimePickerReporteHasta.Size = new Size(110, 23);
            dateTimePickerReporteHasta.TabIndex = 1;
            //
            // buttonReporteGenerar
            //
            buttonReporteGenerar.Location = new Point(370, 11);
            buttonReporteGenerar.Name = "buttonReporteGenerar";
            buttonReporteGenerar.Size = new Size(150, 30);
            buttonReporteGenerar.TabIndex = 2;
            buttonReporteGenerar.Text = "Generar reporte";
            buttonReporteGenerar.UseVisualStyleBackColor = true;
            buttonReporteGenerar.Click += buttonReporteGenerar_Click;
            //
            // buttonReporteExportarPdf
            //
            buttonReporteExportarPdf.Location = new Point(530, 11);
            buttonReporteExportarPdf.Name = "buttonReporteExportarPdf";
            buttonReporteExportarPdf.Size = new Size(150, 30);
            buttonReporteExportarPdf.TabIndex = 3;
            buttonReporteExportarPdf.Text = "Exportar a PDF";
            buttonReporteExportarPdf.UseVisualStyleBackColor = true;
            buttonReporteExportarPdf.Click += buttonReporteExportarPdf_Click;
            //
            // tabControlReporte
            //
            tabControlReporte.Controls.Add(tabPageReporteResumen);
            tabControlReporte.Controls.Add(tabPageReporteDetalle);
            tabControlReporte.Dock = DockStyle.Fill;
            tabControlReporte.Name = "tabControlReporte";
            tabControlReporte.TabIndex = 1;
            //
            // tabPageReporteResumen
            //
            tabPageReporteResumen.Controls.Add(tableLayoutPanelReporteResumen);
            tabPageReporteResumen.Name = "tabPageReporteResumen";
            tabPageReporteResumen.Padding = new Padding(6);
            tabPageReporteResumen.Text = "Resumen y gráficos";
            tabPageReporteResumen.UseVisualStyleBackColor = true;
            //
            // tableLayoutPanelReporteResumen
            //
            tableLayoutPanelReporteResumen.ColumnCount = 1;
            tableLayoutPanelReporteResumen.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelReporteResumen.Controls.Add(tableLayoutPanelReporteKpis, 0, 0);
            tableLayoutPanelReporteResumen.Controls.Add(tableLayoutPanelReporteGraficos, 0, 1);
            tableLayoutPanelReporteResumen.Dock = DockStyle.Fill;
            tableLayoutPanelReporteResumen.Name = "tableLayoutPanelReporteResumen";
            tableLayoutPanelReporteResumen.RowCount = 2;
            tableLayoutPanelReporteResumen.RowStyles.Add(new RowStyle(SizeType.Absolute, 130F));
            tableLayoutPanelReporteResumen.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            //
            // tableLayoutPanelReporteKpis (4 columnas x 2 filas de indicadores; se llena en el constructor)
            //
            tableLayoutPanelReporteKpis.ColumnCount = 4;
            for (int i = 0; i < 4; i++) tableLayoutPanelReporteKpis.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanelReporteKpis.Dock = DockStyle.Fill;
            tableLayoutPanelReporteKpis.Name = "tableLayoutPanelReporteKpis";
            tableLayoutPanelReporteKpis.RowCount = 2;
            tableLayoutPanelReporteKpis.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanelReporteKpis.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            //
            // tableLayoutPanelReporteGraficos (2x2; los graficos se agregan en el constructor)
            //
            tableLayoutPanelReporteGraficos.ColumnCount = 2;
            tableLayoutPanelReporteGraficos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanelReporteGraficos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanelReporteGraficos.Dock = DockStyle.Fill;
            tableLayoutPanelReporteGraficos.Name = "tableLayoutPanelReporteGraficos";
            tableLayoutPanelReporteGraficos.RowCount = 2;
            tableLayoutPanelReporteGraficos.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanelReporteGraficos.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            //
            // tabPageReporteDetalle
            //
            tabPageReporteDetalle.Controls.Add(tableLayoutPanelReporteDetalle);
            tabPageReporteDetalle.Name = "tabPageReporteDetalle";
            tabPageReporteDetalle.Padding = new Padding(6);
            tabPageReporteDetalle.Text = "Detalle";
            tabPageReporteDetalle.UseVisualStyleBackColor = true;
            //
            // tableLayoutPanelReporteDetalle (2 columnas x 3 filas; las grillas se agregan en el constructor)
            //
            tableLayoutPanelReporteDetalle.ColumnCount = 2;
            tableLayoutPanelReporteDetalle.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanelReporteDetalle.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanelReporteDetalle.Dock = DockStyle.Fill;
            tableLayoutPanelReporteDetalle.Name = "tableLayoutPanelReporteDetalle";
            tableLayoutPanelReporteDetalle.RowCount = 3;
            for (int i = 0; i < 3; i++) tableLayoutPanelReporteDetalle.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
            //
            // ReporteGeneralUI
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1100, 650);
            ControlBox = false;
            Controls.Add(tabControlReporte);
            Controls.Add(panelReporteFiltros);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ReporteGeneralUI";
            Text = "Reporte general del servicio";
            WindowState = FormWindowState.Maximized;
            panelReporteFiltros.ResumeLayout(false);
            panelReporteFiltros.PerformLayout();
            tabControlReporte.ResumeLayout(false);
            tabPageReporteResumen.ResumeLayout(false);
            tableLayoutPanelReporteResumen.ResumeLayout(false);
            tableLayoutPanelReporteKpis.ResumeLayout(false);
            tableLayoutPanelReporteGraficos.ResumeLayout(false);
            tabPageReporteDetalle.ResumeLayout(false);
            tableLayoutPanelReporteDetalle.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelReporteFiltros;
        private Label labelReporteDesde;
        private DateTimePicker dateTimePickerReporteDesde;
        private Label labelReporteHasta;
        private DateTimePicker dateTimePickerReporteHasta;
        private Button buttonReporteGenerar;
        private Button buttonReporteExportarPdf;
        private TabControl tabControlReporte;
        private TabPage tabPageReporteResumen;
        private TableLayoutPanel tableLayoutPanelReporteResumen;
        private TableLayoutPanel tableLayoutPanelReporteKpis;
        private TableLayoutPanel tableLayoutPanelReporteGraficos;
        private TabPage tabPageReporteDetalle;
        private TableLayoutPanel tableLayoutPanelReporteDetalle;
    }
}
