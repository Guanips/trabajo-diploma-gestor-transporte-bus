namespace UI.Modules.gestion_taller
{
    partial class GestionRevisionesTallerUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBoxGestionRevisionesAgregar = new GroupBox();
            buttonGestionRevisionesAgregar = new Button();
            listBoxGestionRevisionesDisponibles = new ListBox();
            groupBoxGestionRevisionesDetalle = new GroupBox();
            textBoxGestionRevisionesDetalleId = new TextBox();
            labelGestionRevisionesDetalleId = new Label();
            textBoxGestionRevisionesDetalleInterno = new TextBox();
            labelGestionRevisionesDetalleInterno = new Label();
            textBoxGestionRevisionesDetalleFecha = new TextBox();
            labelGestionRevisionesDetalleFecha = new Label();
            textBoxGestionRevisionesDetalleReparacionRequerida = new TextBox();
            labelGestionRevisionesDetalleReparacionRequerida = new Label();
            textBoxGestionRevisionesDetalleDescripcion = new TextBox();
            labelGestionRevisionesDetalleDescripcion = new Label();
            groupBoxGestionRevisionesOrdenReparacion = new GroupBox();
            groupBoxGestionRevisionesAuditarDetalle = new GroupBox();
            numericUpDownCostoInsumo = new NumericUpDown();
            labelGestionRevisionesCostoInsumo = new Label();
            buttonGestionRevisionesAuditarInsumo = new Button();
            labelGestionRevisionesOrdenesReparacion = new Label();
            labelGestionRevisionesInsumos = new Label();
            dataGridViewDetallesOrdenReparacion = new DataGridView();
            listBoxGestionRevisionesOrdenesReparacion = new ListBox();
            groupBoxGestionRevisionesAgregar.SuspendLayout();
            groupBoxGestionRevisionesDetalle.SuspendLayout();
            groupBoxGestionRevisionesOrdenReparacion.SuspendLayout();
            groupBoxGestionRevisionesAuditarDetalle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCostoInsumo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewDetallesOrdenReparacion).BeginInit();
            SuspendLayout();
            // 
            // groupBoxGestionRevisionesAgregar
            // 
            groupBoxGestionRevisionesAgregar.Controls.Add(buttonGestionRevisionesAgregar);
            groupBoxGestionRevisionesAgregar.Controls.Add(listBoxGestionRevisionesDisponibles);
            groupBoxGestionRevisionesAgregar.Location = new Point(12, 65);
            groupBoxGestionRevisionesAgregar.Name = "groupBoxGestionRevisionesAgregar";
            groupBoxGestionRevisionesAgregar.Size = new Size(350, 234);
            groupBoxGestionRevisionesAgregar.TabIndex = 0;
            groupBoxGestionRevisionesAgregar.TabStop = false;
            groupBoxGestionRevisionesAgregar.Text = "Revisiones";
            // 
            // buttonGestionRevisionesAgregar
            // 
            buttonGestionRevisionesAgregar.Location = new Point(6, 22);
            buttonGestionRevisionesAgregar.Name = "buttonGestionRevisionesAgregar";
            buttonGestionRevisionesAgregar.Size = new Size(133, 45);
            buttonGestionRevisionesAgregar.TabIndex = 1;
            buttonGestionRevisionesAgregar.Text = "Agregar revisión";
            buttonGestionRevisionesAgregar.UseVisualStyleBackColor = true;
            buttonGestionRevisionesAgregar.Click += buttonGestionRevisionesAgregar_Click;
            // 
            // listBoxGestionRevisionesDisponibles
            // 
            listBoxGestionRevisionesDisponibles.FormattingEnabled = true;
            listBoxGestionRevisionesDisponibles.ItemHeight = 15;
            listBoxGestionRevisionesDisponibles.Location = new Point(145, 22);
            listBoxGestionRevisionesDisponibles.Name = "listBoxGestionRevisionesDisponibles";
            listBoxGestionRevisionesDisponibles.Size = new Size(199, 199);
            listBoxGestionRevisionesDisponibles.TabIndex = 0;
            listBoxGestionRevisionesDisponibles.SelectedIndexChanged += listBoxGestionRevisionesDisponibles_SelectedIndexChanged;
            // 
            // groupBoxGestionRevisionesDetalle
            // 
            groupBoxGestionRevisionesDetalle.Controls.Add(textBoxGestionRevisionesDetalleDescripcion);
            groupBoxGestionRevisionesDetalle.Controls.Add(labelGestionRevisionesDetalleDescripcion);
            groupBoxGestionRevisionesDetalle.Controls.Add(textBoxGestionRevisionesDetalleReparacionRequerida);
            groupBoxGestionRevisionesDetalle.Controls.Add(labelGestionRevisionesDetalleReparacionRequerida);
            groupBoxGestionRevisionesDetalle.Controls.Add(textBoxGestionRevisionesDetalleFecha);
            groupBoxGestionRevisionesDetalle.Controls.Add(labelGestionRevisionesDetalleFecha);
            groupBoxGestionRevisionesDetalle.Controls.Add(textBoxGestionRevisionesDetalleInterno);
            groupBoxGestionRevisionesDetalle.Controls.Add(labelGestionRevisionesDetalleInterno);
            groupBoxGestionRevisionesDetalle.Controls.Add(textBoxGestionRevisionesDetalleId);
            groupBoxGestionRevisionesDetalle.Controls.Add(labelGestionRevisionesDetalleId);
            groupBoxGestionRevisionesDetalle.Location = new Point(12, 316);
            groupBoxGestionRevisionesDetalle.Name = "groupBoxGestionRevisionesDetalle";
            groupBoxGestionRevisionesDetalle.Size = new Size(350, 234);
            groupBoxGestionRevisionesDetalle.TabIndex = 1;
            groupBoxGestionRevisionesDetalle.TabStop = false;
            groupBoxGestionRevisionesDetalle.Text = "Detalle de la revisión seleccionada";
            // 
            // textBoxGestionRevisionesDetalleId
            // 
            textBoxGestionRevisionesDetalleId.Location = new Point(150, 22);
            textBoxGestionRevisionesDetalleId.Name = "textBoxGestionRevisionesDetalleId";
            textBoxGestionRevisionesDetalleId.ReadOnly = true;
            textBoxGestionRevisionesDetalleId.Size = new Size(190, 23);
            textBoxGestionRevisionesDetalleId.TabIndex = 1;
            // 
            // labelGestionRevisionesDetalleId
            // 
            labelGestionRevisionesDetalleId.AutoSize = true;
            labelGestionRevisionesDetalleId.Location = new Point(6, 25);
            labelGestionRevisionesDetalleId.Name = "labelGestionRevisionesDetalleId";
            labelGestionRevisionesDetalleId.Size = new Size(97, 15);
            labelGestionRevisionesDetalleId.TabIndex = 0;
            labelGestionRevisionesDetalleId.Text = "Id de la revisión";
            // 
            // textBoxGestionRevisionesDetalleInterno
            // 
            textBoxGestionRevisionesDetalleInterno.Location = new Point(150, 52);
            textBoxGestionRevisionesDetalleInterno.Name = "textBoxGestionRevisionesDetalleInterno";
            textBoxGestionRevisionesDetalleInterno.ReadOnly = true;
            textBoxGestionRevisionesDetalleInterno.Size = new Size(190, 23);
            textBoxGestionRevisionesDetalleInterno.TabIndex = 3;
            // 
            // labelGestionRevisionesDetalleInterno
            // 
            labelGestionRevisionesDetalleInterno.AutoSize = true;
            labelGestionRevisionesDetalleInterno.Location = new Point(6, 55);
            labelGestionRevisionesDetalleInterno.Name = "labelGestionRevisionesDetalleInterno";
            labelGestionRevisionesDetalleInterno.Size = new Size(45, 15);
            labelGestionRevisionesDetalleInterno.TabIndex = 2;
            labelGestionRevisionesDetalleInterno.Text = "Interno";
            // 
            // textBoxGestionRevisionesDetalleFecha
            // 
            textBoxGestionRevisionesDetalleFecha.Location = new Point(150, 82);
            textBoxGestionRevisionesDetalleFecha.Name = "textBoxGestionRevisionesDetalleFecha";
            textBoxGestionRevisionesDetalleFecha.ReadOnly = true;
            textBoxGestionRevisionesDetalleFecha.Size = new Size(190, 23);
            textBoxGestionRevisionesDetalleFecha.TabIndex = 5;
            // 
            // labelGestionRevisionesDetalleFecha
            // 
            labelGestionRevisionesDetalleFecha.AutoSize = true;
            labelGestionRevisionesDetalleFecha.Location = new Point(6, 85);
            labelGestionRevisionesDetalleFecha.Name = "labelGestionRevisionesDetalleFecha";
            labelGestionRevisionesDetalleFecha.Size = new Size(118, 15);
            labelGestionRevisionesDetalleFecha.TabIndex = 4;
            labelGestionRevisionesDetalleFecha.Text = "Fecha de la revisión";
            // 
            // textBoxGestionRevisionesDetalleReparacionRequerida
            // 
            textBoxGestionRevisionesDetalleReparacionRequerida.Location = new Point(150, 112);
            textBoxGestionRevisionesDetalleReparacionRequerida.Name = "textBoxGestionRevisionesDetalleReparacionRequerida";
            textBoxGestionRevisionesDetalleReparacionRequerida.ReadOnly = true;
            textBoxGestionRevisionesDetalleReparacionRequerida.Size = new Size(190, 23);
            textBoxGestionRevisionesDetalleReparacionRequerida.TabIndex = 7;
            // 
            // labelGestionRevisionesDetalleReparacionRequerida
            // 
            labelGestionRevisionesDetalleReparacionRequerida.AutoSize = true;
            labelGestionRevisionesDetalleReparacionRequerida.Location = new Point(6, 115);
            labelGestionRevisionesDetalleReparacionRequerida.Name = "labelGestionRevisionesDetalleReparacionRequerida";
            labelGestionRevisionesDetalleReparacionRequerida.Size = new Size(125, 15);
            labelGestionRevisionesDetalleReparacionRequerida.TabIndex = 6;
            labelGestionRevisionesDetalleReparacionRequerida.Text = "Reparación requerida";
            // 
            // textBoxGestionRevisionesDetalleDescripcion
            // 
            textBoxGestionRevisionesDetalleDescripcion.Location = new Point(6, 163);
            textBoxGestionRevisionesDetalleDescripcion.Multiline = true;
            textBoxGestionRevisionesDetalleDescripcion.Name = "textBoxGestionRevisionesDetalleDescripcion";
            textBoxGestionRevisionesDetalleDescripcion.ReadOnly = true;
            textBoxGestionRevisionesDetalleDescripcion.ScrollBars = ScrollBars.Vertical;
            textBoxGestionRevisionesDetalleDescripcion.Size = new Size(334, 61);
            textBoxGestionRevisionesDetalleDescripcion.TabIndex = 9;
            // 
            // labelGestionRevisionesDetalleDescripcion
            // 
            labelGestionRevisionesDetalleDescripcion.AutoSize = true;
            labelGestionRevisionesDetalleDescripcion.Location = new Point(6, 145);
            labelGestionRevisionesDetalleDescripcion.Name = "labelGestionRevisionesDetalleDescripcion";
            labelGestionRevisionesDetalleDescripcion.Size = new Size(69, 15);
            labelGestionRevisionesDetalleDescripcion.TabIndex = 8;
            labelGestionRevisionesDetalleDescripcion.Text = "Descripción";
            // 
            // groupBoxGestionRevisionesOrdenReparacion
            // 
            groupBoxGestionRevisionesOrdenReparacion.Controls.Add(groupBoxGestionRevisionesAuditarDetalle);
            groupBoxGestionRevisionesOrdenReparacion.Controls.Add(labelGestionRevisionesOrdenesReparacion);
            groupBoxGestionRevisionesOrdenReparacion.Controls.Add(labelGestionRevisionesInsumos);
            groupBoxGestionRevisionesOrdenReparacion.Controls.Add(dataGridViewDetallesOrdenReparacion);
            groupBoxGestionRevisionesOrdenReparacion.Controls.Add(listBoxGestionRevisionesOrdenesReparacion);
            groupBoxGestionRevisionesOrdenReparacion.Location = new Point(368, 65);
            groupBoxGestionRevisionesOrdenReparacion.Name = "groupBoxGestionRevisionesOrdenReparacion";
            groupBoxGestionRevisionesOrdenReparacion.Size = new Size(746, 485);
            groupBoxGestionRevisionesOrdenReparacion.TabIndex = 2;
            groupBoxGestionRevisionesOrdenReparacion.TabStop = false;
            groupBoxGestionRevisionesOrdenReparacion.Text = "Detalles de orden de reparación";
            // 
            // groupBoxGestionRevisionesAuditarDetalle
            // 
            groupBoxGestionRevisionesAuditarDetalle.Controls.Add(numericUpDownCostoInsumo);
            groupBoxGestionRevisionesAuditarDetalle.Controls.Add(labelGestionRevisionesCostoInsumo);
            groupBoxGestionRevisionesAuditarDetalle.Controls.Add(buttonGestionRevisionesAuditarInsumo);
            groupBoxGestionRevisionesAuditarDetalle.Location = new Point(193, 360);
            groupBoxGestionRevisionesAuditarDetalle.Name = "groupBoxGestionRevisionesAuditarDetalle";
            groupBoxGestionRevisionesAuditarDetalle.Size = new Size(180, 111);
            groupBoxGestionRevisionesAuditarDetalle.TabIndex = 5;
            groupBoxGestionRevisionesAuditarDetalle.TabStop = false;
            groupBoxGestionRevisionesAuditarDetalle.Text = "Auditar costos de insumos";
            // 
            // numericUpDownCostoInsumo
            // 
            numericUpDownCostoInsumo.DecimalPlaces = 0;
            numericUpDownCostoInsumo.Location = new Point(6, 41);
            numericUpDownCostoInsumo.Maximum = new decimal(new int[] { 100000000, 0, 0, 0 });
            numericUpDownCostoInsumo.Name = "numericUpDownCostoInsumo";
            numericUpDownCostoInsumo.Size = new Size(168, 23);
            numericUpDownCostoInsumo.TabIndex = 6;
            // 
            // labelGestionRevisionesCostoInsumo
            // 
            labelGestionRevisionesCostoInsumo.AutoSize = true;
            labelGestionRevisionesCostoInsumo.Location = new Point(6, 18);
            labelGestionRevisionesCostoInsumo.Name = "labelGestionRevisionesCostoInsumo";
            labelGestionRevisionesCostoInsumo.Size = new Size(172, 15);
            labelGestionRevisionesCostoInsumo.TabIndex = 5;
            labelGestionRevisionesCostoInsumo.Text = "Costo del insumo seleccionado";
            // 
            // buttonGestionRevisionesAuditarInsumo
            // 
            buttonGestionRevisionesAuditarInsumo.Location = new Point(6, 72);
            buttonGestionRevisionesAuditarInsumo.Name = "buttonGestionRevisionesAuditarInsumo";
            buttonGestionRevisionesAuditarInsumo.Size = new Size(168, 31);
            buttonGestionRevisionesAuditarInsumo.TabIndex = 4;
            buttonGestionRevisionesAuditarInsumo.Text = "Cargar costo insumo";
            buttonGestionRevisionesAuditarInsumo.UseVisualStyleBackColor = true;
            buttonGestionRevisionesAuditarInsumo.Click += buttonGestionRevisionesAuditarInsumo_Click;
            // 
            // labelGestionRevisionesOrdenesReparacion
            // 
            labelGestionRevisionesOrdenesReparacion.AutoSize = true;
            labelGestionRevisionesOrdenesReparacion.Location = new Point(6, 29);
            labelGestionRevisionesOrdenesReparacion.Name = "labelGestionRevisionesOrdenesReparacion";
            labelGestionRevisionesOrdenesReparacion.Size = new Size(126, 15);
            labelGestionRevisionesOrdenesReparacion.TabIndex = 3;
            labelGestionRevisionesOrdenesReparacion.Text = "Ordenes de reparación";
            // 
            // labelGestionRevisionesInsumos
            // 
            labelGestionRevisionesInsumos.AutoSize = true;
            labelGestionRevisionesInsumos.Location = new Point(193, 29);
            labelGestionRevisionesInsumos.Name = "labelGestionRevisionesInsumos";
            labelGestionRevisionesInsumos.Size = new Size(107, 15);
            labelGestionRevisionesInsumos.TabIndex = 2;
            labelGestionRevisionesInsumos.Text = "Insumos asociados";
            // 
            // dataGridViewDetallesOrdenReparacion
            // 
            dataGridViewDetallesOrdenReparacion.AllowUserToAddRows = false;
            dataGridViewDetallesOrdenReparacion.AllowUserToDeleteRows = false;
            dataGridViewDetallesOrdenReparacion.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewDetallesOrdenReparacion.Location = new Point(193, 47);
            dataGridViewDetallesOrdenReparacion.MultiSelect = false;
            dataGridViewDetallesOrdenReparacion.Name = "dataGridViewDetallesOrdenReparacion";
            dataGridViewDetallesOrdenReparacion.ReadOnly = true;
            dataGridViewDetallesOrdenReparacion.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewDetallesOrdenReparacion.Size = new Size(547, 307);
            dataGridViewDetallesOrdenReparacion.TabIndex = 1;
            dataGridViewDetallesOrdenReparacion.SelectionChanged += dataGridViewDetallesOrdenReparacion_SelectionChanged;
            // 
            // listBoxGestionRevisionesOrdenesReparacion
            // 
            listBoxGestionRevisionesOrdenesReparacion.FormattingEnabled = true;
            listBoxGestionRevisionesOrdenesReparacion.ItemHeight = 15;
            listBoxGestionRevisionesOrdenesReparacion.Location = new Point(6, 47);
            listBoxGestionRevisionesOrdenesReparacion.Name = "listBoxGestionRevisionesOrdenesReparacion";
            listBoxGestionRevisionesOrdenesReparacion.Size = new Size(181, 424);
            listBoxGestionRevisionesOrdenesReparacion.TabIndex = 0;
            listBoxGestionRevisionesOrdenesReparacion.SelectedIndexChanged += listBoxGestionRevisionesOrdenesReparacion_SelectedIndexChanged;
            // 
            // GestionRevisionesTallerUI
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1126, 562);
            ControlBox = false;
            Controls.Add(groupBoxGestionRevisionesOrdenReparacion);
            Controls.Add(groupBoxGestionRevisionesDetalle);
            Controls.Add(groupBoxGestionRevisionesAgregar);
            FormBorderStyle = FormBorderStyle.None;
            Name = "GestionRevisionesTallerUI";
            Text = "GestionRevisionesTallerUI";
            WindowState = FormWindowState.Maximized;
            groupBoxGestionRevisionesAgregar.ResumeLayout(false);
            groupBoxGestionRevisionesDetalle.ResumeLayout(false);
            groupBoxGestionRevisionesDetalle.PerformLayout();
            groupBoxGestionRevisionesOrdenReparacion.ResumeLayout(false);
            groupBoxGestionRevisionesOrdenReparacion.PerformLayout();
            groupBoxGestionRevisionesAuditarDetalle.ResumeLayout(false);
            groupBoxGestionRevisionesAuditarDetalle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCostoInsumo).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewDetallesOrdenReparacion).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBoxGestionRevisionesAgregar;
        private GroupBox groupBoxGestionRevisionesDetalle;
        private TextBox textBoxGestionRevisionesDetalleId;
        private Label labelGestionRevisionesDetalleId;
        private TextBox textBoxGestionRevisionesDetalleInterno;
        private Label labelGestionRevisionesDetalleInterno;
        private TextBox textBoxGestionRevisionesDetalleFecha;
        private Label labelGestionRevisionesDetalleFecha;
        private TextBox textBoxGestionRevisionesDetalleReparacionRequerida;
        private Label labelGestionRevisionesDetalleReparacionRequerida;
        private TextBox textBoxGestionRevisionesDetalleDescripcion;
        private Label labelGestionRevisionesDetalleDescripcion;
        private GroupBox groupBoxGestionRevisionesOrdenReparacion;
        private ListBox listBoxGestionRevisionesOrdenesReparacion;
        private Button buttonGestionRevisionesAgregar;
        private ListBox listBoxGestionRevisionesDisponibles;
        private GroupBox groupBoxGestionRevisionesAuditarDetalle;
        private NumericUpDown numericUpDownCostoInsumo;
        private Label labelGestionRevisionesCostoInsumo;
        private Button buttonGestionRevisionesAuditarInsumo;
        private Label labelGestionRevisionesOrdenesReparacion;
        private Label labelGestionRevisionesInsumos;
        private DataGridView dataGridViewDetallesOrdenReparacion;
    }
}