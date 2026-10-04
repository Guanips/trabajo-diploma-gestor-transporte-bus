namespace UI.Modules.gestion_taller
{
    partial class GestionRevisionesTallerOrdenReparacionAgregarDetalle
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
            groupBoxGestionRevisionesOrdenReparacion = new GroupBox();
            groupBoxGestionRevisionesOrdenesInsumos = new GroupBox();
            buttonGestionRevisionesAltaOrdenAgregarInsumo = new Button();
            numericUpDownCantidadInsumo = new NumericUpDown();
            labelGestionRevisionesAltaOrdenCantidadInsumo = new Label();
            textBoxNombreInsumo = new TextBox();
            labelGestionRevisionesAltaOrdenNombreInsumo = new Label();
            dataGridViewGestionRevisionesOrdenesInsumos = new DataGridView();
            groupBoxGestionRevisionesAltaOrden = new GroupBox();
            textBoxMotivoReparacion = new TextBox();
            labelGestionRevisionesAltaOrdenMotivoReparacion = new Label();
            listBoxGestionRevisionesOrdenesReparacion = new ListBox();
            buttonGestionRevisionesAltaOrdenesReparacionConfirmar = new Button();
            buttonGestionRevisionesAltaOrdenAgregarOrden = new Button();
            groupBoxGestionRevisionesOrdenReparacion.SuspendLayout();
            groupBoxGestionRevisionesOrdenesInsumos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCantidadInsumo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewGestionRevisionesOrdenesInsumos).BeginInit();
            groupBoxGestionRevisionesAltaOrden.SuspendLayout();
            SuspendLayout();
            // 
            // groupBoxGestionRevisionesOrdenReparacion
            // 
            groupBoxGestionRevisionesOrdenReparacion.Controls.Add(groupBoxGestionRevisionesOrdenesInsumos);
            groupBoxGestionRevisionesOrdenReparacion.Controls.Add(groupBoxGestionRevisionesAltaOrden);
            groupBoxGestionRevisionesOrdenReparacion.Controls.Add(buttonGestionRevisionesAltaOrdenesReparacionConfirmar);
            groupBoxGestionRevisionesOrdenReparacion.Location = new Point(12, 12);
            groupBoxGestionRevisionesOrdenReparacion.Name = "groupBoxGestionRevisionesOrdenReparacion";
            groupBoxGestionRevisionesOrdenReparacion.Size = new Size(888, 485);
            groupBoxGestionRevisionesOrdenReparacion.TabIndex = 3;
            groupBoxGestionRevisionesOrdenReparacion.TabStop = false;
            groupBoxGestionRevisionesOrdenReparacion.Text = "Detalles de orden de reparación";
            // 
            // groupBoxGestionRevisionesOrdenesInsumos
            // 
            groupBoxGestionRevisionesOrdenesInsumos.Controls.Add(buttonGestionRevisionesAltaOrdenAgregarInsumo);
            groupBoxGestionRevisionesOrdenesInsumos.Controls.Add(numericUpDownCantidadInsumo);
            groupBoxGestionRevisionesOrdenesInsumos.Controls.Add(labelGestionRevisionesAltaOrdenCantidadInsumo);
            groupBoxGestionRevisionesOrdenesInsumos.Controls.Add(textBoxNombreInsumo);
            groupBoxGestionRevisionesOrdenesInsumos.Controls.Add(labelGestionRevisionesAltaOrdenNombreInsumo);
            groupBoxGestionRevisionesOrdenesInsumos.Controls.Add(dataGridViewGestionRevisionesOrdenesInsumos);
            groupBoxGestionRevisionesOrdenesInsumos.Location = new Point(310, 22);
            groupBoxGestionRevisionesOrdenesInsumos.Name = "groupBoxGestionRevisionesOrdenesInsumos";
            groupBoxGestionRevisionesOrdenesInsumos.Size = new Size(572, 407);
            groupBoxGestionRevisionesOrdenesInsumos.TabIndex = 6;
            groupBoxGestionRevisionesOrdenesInsumos.TabStop = false;
            groupBoxGestionRevisionesOrdenesInsumos.Text = "Insumos";
            // 
            // buttonGestionRevisionesAltaOrdenAgregarInsumo
            // 
            buttonGestionRevisionesAltaOrdenAgregarInsumo.Location = new Point(6, 113);
            buttonGestionRevisionesAltaOrdenAgregarInsumo.Name = "buttonGestionRevisionesAltaOrdenAgregarInsumo";
            buttonGestionRevisionesAltaOrdenAgregarInsumo.Size = new Size(141, 29);
            buttonGestionRevisionesAltaOrdenAgregarInsumo.TabIndex = 6;
            buttonGestionRevisionesAltaOrdenAgregarInsumo.Text = "Agregar insumo";
            buttonGestionRevisionesAltaOrdenAgregarInsumo.UseVisualStyleBackColor = true;
            buttonGestionRevisionesAltaOrdenAgregarInsumo.Click += buttonGestionRevisionesAltaOrdenAgregarInsumo_Click;
            // 
            // numericUpDownCantidadInsumo
            // 
            numericUpDownCantidadInsumo.Location = new Point(6, 84);
            numericUpDownCantidadInsumo.Name = "numericUpDownCantidadInsumo";
            numericUpDownCantidadInsumo.Size = new Size(141, 23);
            numericUpDownCantidadInsumo.TabIndex = 5;
            // 
            // labelGestionRevisionesAltaOrdenCantidadInsumo
            // 
            labelGestionRevisionesAltaOrdenCantidadInsumo.AutoSize = true;
            labelGestionRevisionesAltaOrdenCantidadInsumo.Location = new Point(6, 66);
            labelGestionRevisionesAltaOrdenCantidadInsumo.Name = "labelGestionRevisionesAltaOrdenCantidadInsumo";
            labelGestionRevisionesAltaOrdenCantidadInsumo.Size = new Size(55, 15);
            labelGestionRevisionesAltaOrdenCantidadInsumo.TabIndex = 4;
            labelGestionRevisionesAltaOrdenCantidadInsumo.Text = "Cantidad";
            // 
            // textBoxNombreInsumo
            // 
            textBoxNombreInsumo.Location = new Point(6, 40);
            textBoxNombreInsumo.Name = "textBoxNombreInsumo";
            textBoxNombreInsumo.Size = new Size(141, 23);
            textBoxNombreInsumo.TabIndex = 3;
            // 
            // labelGestionRevisionesAltaOrdenNombreInsumo
            // 
            labelGestionRevisionesAltaOrdenNombreInsumo.AutoSize = true;
            labelGestionRevisionesAltaOrdenNombreInsumo.Location = new Point(6, 22);
            labelGestionRevisionesAltaOrdenNombreInsumo.Name = "labelGestionRevisionesAltaOrdenNombreInsumo";
            labelGestionRevisionesAltaOrdenNombreInsumo.Size = new Size(113, 15);
            labelGestionRevisionesAltaOrdenNombreInsumo.TabIndex = 2;
            labelGestionRevisionesAltaOrdenNombreInsumo.Text = "Nombre del insumo";
            // 
            // dataGridViewGestionRevisionesOrdenesInsumos
            // 
            dataGridViewGestionRevisionesOrdenesInsumos.AllowUserToAddRows = false;
            dataGridViewGestionRevisionesOrdenesInsumos.AllowUserToDeleteRows = false;
            dataGridViewGestionRevisionesOrdenesInsumos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewGestionRevisionesOrdenesInsumos.Location = new Point(153, 19);
            dataGridViewGestionRevisionesOrdenesInsumos.Name = "dataGridViewGestionRevisionesOrdenesInsumos";
            dataGridViewGestionRevisionesOrdenesInsumos.ReadOnly = true;
            dataGridViewGestionRevisionesOrdenesInsumos.Size = new Size(413, 379);
            dataGridViewGestionRevisionesOrdenesInsumos.TabIndex = 1;
            // 
            // groupBoxGestionRevisionesAltaOrden
            // 
            groupBoxGestionRevisionesAltaOrden.Controls.Add(buttonGestionRevisionesAltaOrdenAgregarOrden);
            groupBoxGestionRevisionesAltaOrden.Controls.Add(textBoxMotivoReparacion);
            groupBoxGestionRevisionesAltaOrden.Controls.Add(labelGestionRevisionesAltaOrdenMotivoReparacion);
            groupBoxGestionRevisionesAltaOrden.Controls.Add(listBoxGestionRevisionesOrdenesReparacion);
            groupBoxGestionRevisionesAltaOrden.Location = new Point(6, 22);
            groupBoxGestionRevisionesAltaOrden.Name = "groupBoxGestionRevisionesAltaOrden";
            groupBoxGestionRevisionesAltaOrden.Size = new Size(298, 329);
            groupBoxGestionRevisionesAltaOrden.TabIndex = 5;
            groupBoxGestionRevisionesAltaOrden.TabStop = false;
            groupBoxGestionRevisionesAltaOrden.Text = "Ordenes de reparación";
            // 
            // textBoxMotivoReparacion
            // 
            textBoxMotivoReparacion.Location = new Point(6, 37);
            textBoxMotivoReparacion.Multiline = true;
            textBoxMotivoReparacion.Name = "textBoxMotivoReparacion";
            textBoxMotivoReparacion.Size = new Size(282, 54);
            textBoxMotivoReparacion.TabIndex = 2;
            // 
            // labelGestionRevisionesAltaOrdenMotivoReparacion
            // 
            labelGestionRevisionesAltaOrdenMotivoReparacion.AutoSize = true;
            labelGestionRevisionesAltaOrdenMotivoReparacion.Location = new Point(6, 19);
            labelGestionRevisionesAltaOrdenMotivoReparacion.Name = "labelGestionRevisionesAltaOrdenMotivoReparacion";
            labelGestionRevisionesAltaOrdenMotivoReparacion.Size = new Size(120, 15);
            labelGestionRevisionesAltaOrdenMotivoReparacion.TabIndex = 1;
            labelGestionRevisionesAltaOrdenMotivoReparacion.Text = "Motivo de reparacion";
            // 
            // listBoxGestionRevisionesOrdenesReparacion
            // 
            listBoxGestionRevisionesOrdenesReparacion.FormattingEnabled = true;
            listBoxGestionRevisionesOrdenesReparacion.ItemHeight = 15;
            listBoxGestionRevisionesOrdenesReparacion.Location = new Point(6, 139);
            listBoxGestionRevisionesOrdenesReparacion.Name = "listBoxGestionRevisionesOrdenesReparacion";
            listBoxGestionRevisionesOrdenesReparacion.Size = new Size(282, 184);
            listBoxGestionRevisionesOrdenesReparacion.TabIndex = 0;
            listBoxGestionRevisionesOrdenesReparacion.SelectedIndexChanged += listBoxGestionRevisionesOrdenesReparacion_SelectedIndexChanged;
            // 
            // buttonGestionRevisionesAltaOrdenesReparacionConfirmar
            // 
            buttonGestionRevisionesAltaOrdenesReparacionConfirmar.Location = new Point(6, 435);
            buttonGestionRevisionesAltaOrdenesReparacionConfirmar.Name = "buttonGestionRevisionesAltaOrdenesReparacionConfirmar";
            buttonGestionRevisionesAltaOrdenesReparacionConfirmar.Size = new Size(876, 44);
            buttonGestionRevisionesAltaOrdenesReparacionConfirmar.TabIndex = 4;
            buttonGestionRevisionesAltaOrdenesReparacionConfirmar.Text = "Confirmar";
            buttonGestionRevisionesAltaOrdenesReparacionConfirmar.UseVisualStyleBackColor = true;
            buttonGestionRevisionesAltaOrdenesReparacionConfirmar.Click += buttonGestionRevisionesAltaOrdenesReparacionConfirmar_Click;
            // 
            // buttonGestionRevisionesAltaOrdenAgregarOrden
            // 
            buttonGestionRevisionesAltaOrdenAgregarOrden.Location = new Point(6, 97);
            buttonGestionRevisionesAltaOrdenAgregarOrden.Name = "buttonGestionRevisionesAltaOrdenAgregarOrden";
            buttonGestionRevisionesAltaOrdenAgregarOrden.Size = new Size(282, 29);
            buttonGestionRevisionesAltaOrdenAgregarOrden.TabIndex = 7;
            buttonGestionRevisionesAltaOrdenAgregarOrden.Text = "Agregar orden de reparación";
            buttonGestionRevisionesAltaOrdenAgregarOrden.UseVisualStyleBackColor = true;
            buttonGestionRevisionesAltaOrdenAgregarOrden.Click += buttonGestionRevisionesAltaOrdenAgregarOrden_Click;
            // 
            // GestionRevisionesTallerOrdenReparacionAgregarDetalle
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(912, 508);
            Controls.Add(groupBoxGestionRevisionesOrdenReparacion);
            Name = "GestionRevisionesTallerOrdenReparacionAgregarDetalle";
            Text = "Alta de ordenes de reparación";
            groupBoxGestionRevisionesOrdenReparacion.ResumeLayout(false);
            groupBoxGestionRevisionesOrdenesInsumos.ResumeLayout(false);
            groupBoxGestionRevisionesOrdenesInsumos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCantidadInsumo).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewGestionRevisionesOrdenesInsumos).EndInit();
            groupBoxGestionRevisionesAltaOrden.ResumeLayout(false);
            groupBoxGestionRevisionesAltaOrden.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBoxGestionRevisionesOrdenReparacion;
        private GroupBox groupBoxGestionRevisionesOrdenesInsumos;
        private DataGridView dataGridViewGestionRevisionesOrdenesInsumos;
        private GroupBox groupBoxGestionRevisionesAltaOrden;
        private ListBox listBoxGestionRevisionesOrdenesReparacion;
        private Button buttonGestionRevisionesAltaOrdenesReparacionConfirmar;
        private TextBox textBoxMotivoReparacion;
        private Label labelGestionRevisionesAltaOrdenMotivoReparacion;
        private Button buttonGestionRevisionesAltaOrdenAgregarInsumo;
        private NumericUpDown numericUpDownCantidadInsumo;
        private Label labelGestionRevisionesAltaOrdenCantidadInsumo;
        private TextBox textBoxNombreInsumo;
        private Label labelGestionRevisionesAltaOrdenNombreInsumo;
        private Button buttonGestionRevisionesAltaOrdenAgregarOrden;
    }
}