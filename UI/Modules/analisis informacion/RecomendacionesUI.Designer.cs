namespace UI.Modules.analisis_informacion
{
    partial class RecomendacionesUI
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
            tableLayoutPanelRecomendaciones = new TableLayoutPanel();
            labelRecomendacionesPeriodo = new Label();
            dataGridViewRecomendaciones = new DataGridView();
            groupBoxRecomendacionDetalle = new GroupBox();
            textBoxRecomendacionDetalle = new TextBox();
            labelRecomendacionesAviso = new Label();
            buttonRecomendacionesCerrar = new Button();
            tableLayoutPanelRecomendaciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewRecomendaciones).BeginInit();
            groupBoxRecomendacionDetalle.SuspendLayout();
            SuspendLayout();
            //
            // tableLayoutPanelRecomendaciones
            //
            tableLayoutPanelRecomendaciones.ColumnCount = 2;
            tableLayoutPanelRecomendaciones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelRecomendaciones.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            tableLayoutPanelRecomendaciones.Controls.Add(labelRecomendacionesPeriodo, 0, 0);
            tableLayoutPanelRecomendaciones.Controls.Add(dataGridViewRecomendaciones, 0, 1);
            tableLayoutPanelRecomendaciones.Controls.Add(groupBoxRecomendacionDetalle, 0, 2);
            tableLayoutPanelRecomendaciones.Controls.Add(labelRecomendacionesAviso, 0, 3);
            tableLayoutPanelRecomendaciones.Controls.Add(buttonRecomendacionesCerrar, 1, 3);
            tableLayoutPanelRecomendaciones.Dock = DockStyle.Fill;
            tableLayoutPanelRecomendaciones.Name = "tableLayoutPanelRecomendaciones";
            tableLayoutPanelRecomendaciones.Padding = new Padding(10);
            tableLayoutPanelRecomendaciones.RowCount = 4;
            tableLayoutPanelRecomendaciones.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanelRecomendaciones.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanelRecomendaciones.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanelRecomendaciones.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            //
            // labelRecomendacionesPeriodo (texto armado por codigo)
            //
            labelRecomendacionesPeriodo.Dock = DockStyle.Fill;
            labelRecomendacionesPeriodo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelRecomendacionesPeriodo.Name = "labelRecomendacionesPeriodo";
            labelRecomendacionesPeriodo.Tag = "notranslate";
            labelRecomendacionesPeriodo.TextAlign = ContentAlignment.MiddleLeft;
            tableLayoutPanelRecomendaciones.SetColumnSpan(labelRecomendacionesPeriodo, 2);
            //
            // dataGridViewRecomendaciones
            //
            dataGridViewRecomendaciones.AllowUserToAddRows = false;
            dataGridViewRecomendaciones.AllowUserToDeleteRows = false;
            dataGridViewRecomendaciones.AutoGenerateColumns = false;
            dataGridViewRecomendaciones.BackgroundColor = SystemColors.Window;
            dataGridViewRecomendaciones.Dock = DockStyle.Fill;
            dataGridViewRecomendaciones.MultiSelect = false;
            dataGridViewRecomendaciones.Name = "dataGridViewRecomendaciones";
            dataGridViewRecomendaciones.ReadOnly = true;
            dataGridViewRecomendaciones.RowHeadersVisible = false;
            dataGridViewRecomendaciones.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewRecomendaciones.TabIndex = 0;
            dataGridViewRecomendaciones.CellFormatting += dataGridViewRecomendaciones_CellFormatting;
            dataGridViewRecomendaciones.SelectionChanged += dataGridViewRecomendaciones_SelectionChanged;
            tableLayoutPanelRecomendaciones.SetColumnSpan(dataGridViewRecomendaciones, 2);
            //
            // groupBoxRecomendacionDetalle
            //
            groupBoxRecomendacionDetalle.Controls.Add(textBoxRecomendacionDetalle);
            groupBoxRecomendacionDetalle.Dock = DockStyle.Fill;
            groupBoxRecomendacionDetalle.Name = "groupBoxRecomendacionDetalle";
            groupBoxRecomendacionDetalle.Padding = new Padding(8);
            groupBoxRecomendacionDetalle.Text = "Detalle de la recomendación seleccionada";
            tableLayoutPanelRecomendaciones.SetColumnSpan(groupBoxRecomendacionDetalle, 2);
            //
            // textBoxRecomendacionDetalle
            //
            textBoxRecomendacionDetalle.BackColor = SystemColors.Window;
            textBoxRecomendacionDetalle.BorderStyle = BorderStyle.None;
            textBoxRecomendacionDetalle.Dock = DockStyle.Fill;
            textBoxRecomendacionDetalle.Multiline = true;
            textBoxRecomendacionDetalle.Name = "textBoxRecomendacionDetalle";
            textBoxRecomendacionDetalle.ReadOnly = true;
            textBoxRecomendacionDetalle.ScrollBars = ScrollBars.Vertical;
            textBoxRecomendacionDetalle.TabIndex = 1;
            //
            // labelRecomendacionesAviso
            //
            labelRecomendacionesAviso.Dock = DockStyle.Fill;
            labelRecomendacionesAviso.ForeColor = SystemColors.GrayText;
            labelRecomendacionesAviso.Name = "labelRecomendacionesAviso";
            labelRecomendacionesAviso.Text = "Sugerencias automáticas basadas en los datos del período seleccionado. La decisión final es del gerente.";
            labelRecomendacionesAviso.TextAlign = ContentAlignment.MiddleLeft;
            //
            // buttonRecomendacionesCerrar
            //
            buttonRecomendacionesCerrar.Anchor = AnchorStyles.Right;
            buttonRecomendacionesCerrar.DialogResult = DialogResult.OK;
            buttonRecomendacionesCerrar.Name = "buttonRecomendacionesCerrar";
            buttonRecomendacionesCerrar.Size = new Size(100, 30);
            buttonRecomendacionesCerrar.TabIndex = 2;
            buttonRecomendacionesCerrar.Text = "Cerrar";
            buttonRecomendacionesCerrar.UseVisualStyleBackColor = true;
            //
            // RecomendacionesUI
            //
            AcceptButton = buttonRecomendacionesCerrar;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = buttonRecomendacionesCerrar;
            ClientSize = new Size(980, 620);
            Controls.Add(tableLayoutPanelRecomendaciones);
            FormBorderStyle = FormBorderStyle.Sizable;
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(700, 480);
            Name = "RecomendacionesUI";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Recomendaciones de gestión";
            tableLayoutPanelRecomendaciones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewRecomendaciones).EndInit();
            groupBoxRecomendacionDetalle.ResumeLayout(false);
            groupBoxRecomendacionDetalle.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanelRecomendaciones;
        private Label labelRecomendacionesPeriodo;
        private DataGridView dataGridViewRecomendaciones;
        private GroupBox groupBoxRecomendacionDetalle;
        private TextBox textBoxRecomendacionDetalle;
        private Label labelRecomendacionesAviso;
        private Button buttonRecomendacionesCerrar;
    }
}
