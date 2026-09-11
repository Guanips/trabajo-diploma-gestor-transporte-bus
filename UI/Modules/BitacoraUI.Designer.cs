namespace UI.Modules
{
    partial class BitacoraUI
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
            groupBoxFiltros = new GroupBox();
            bitacoraUIButtonLimpiarFiltros = new Button();
            comboBoxUsername = new ComboBox();
            bitacoraUILabelComboBoxUsername = new Label();
            comboBoxAccion = new ComboBox();
            bitacoraUILabelComboBoxAccion = new Label();
            groupBoxListadoBitacora = new GroupBox();
            bitacoraUILabelGrid = new Label();
            dataGridViewRegistrosBitacora = new DataGridView();
            groupBoxFiltros.SuspendLayout();
            groupBoxListadoBitacora.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewRegistrosBitacora).BeginInit();
            SuspendLayout();
            // 
            // groupBoxFiltros
            // 
            groupBoxFiltros.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            groupBoxFiltros.Controls.Add(bitacoraUIButtonLimpiarFiltros);
            groupBoxFiltros.Controls.Add(comboBoxUsername);
            groupBoxFiltros.Controls.Add(bitacoraUILabelComboBoxUsername);
            groupBoxFiltros.Controls.Add(comboBoxAccion);
            groupBoxFiltros.Controls.Add(bitacoraUILabelComboBoxAccion);
            groupBoxFiltros.Location = new Point(896, 16);
            groupBoxFiltros.Name = "groupBoxFiltros";
            groupBoxFiltros.Size = new Size(300, 218);
            groupBoxFiltros.TabIndex = 0;
            groupBoxFiltros.TabStop = false;
            groupBoxFiltros.Text = "Filtros";
            // 
            // bitacoraUIButtonLimpiarFiltros
            // 
            bitacoraUIButtonLimpiarFiltros.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            bitacoraUIButtonLimpiarFiltros.Location = new Point(16, 170);
            bitacoraUIButtonLimpiarFiltros.Name = "bitacoraUIButtonLimpiarFiltros";
            bitacoraUIButtonLimpiarFiltros.Size = new Size(268, 32);
            bitacoraUIButtonLimpiarFiltros.TabIndex = 4;
            bitacoraUIButtonLimpiarFiltros.Text = "Limpiar filtros";
            bitacoraUIButtonLimpiarFiltros.UseVisualStyleBackColor = true;
            bitacoraUIButtonLimpiarFiltros.Click += bitacoraUIButtonLimpiarFiltros_Click;
            // 
            // comboBoxUsername
            // 
            comboBoxUsername.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comboBoxUsername.FormattingEnabled = true;
            comboBoxUsername.Location = new Point(16, 133);
            comboBoxUsername.Name = "comboBoxUsername";
            comboBoxUsername.Size = new Size(268, 23);
            comboBoxUsername.TabIndex = 3;
            comboBoxUsername.SelectedIndexChanged += comboBoxUsername_SelectedIndexChanged;
            // 
            // bitacoraUILabelComboBoxUsername
            // 
            bitacoraUILabelComboBoxUsername.AutoSize = true;
            bitacoraUILabelComboBoxUsername.Location = new Point(16, 115);
            bitacoraUILabelComboBoxUsername.Name = "bitacoraUILabelComboBoxUsername";
            bitacoraUILabelComboBoxUsername.Size = new Size(123, 15);
            bitacoraUILabelComboBoxUsername.TabIndex = 2;
            bitacoraUILabelComboBoxUsername.Text = "Filtrado por username";
            // 
            // comboBoxAccion
            // 
            comboBoxAccion.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comboBoxAccion.FormattingEnabled = true;
            comboBoxAccion.Location = new Point(16, 48);
            comboBoxAccion.Name = "comboBoxAccion";
            comboBoxAccion.Size = new Size(268, 23);
            comboBoxAccion.TabIndex = 1;
            comboBoxAccion.SelectedIndexChanged += comboBoxAccion_SelectedIndexChanged;
            // 
            // bitacoraUILabelComboBoxAccion
            // 
            bitacoraUILabelComboBoxAccion.AutoSize = true;
            bitacoraUILabelComboBoxAccion.Location = new Point(16, 30);
            bitacoraUILabelComboBoxAccion.Name = "bitacoraUILabelComboBoxAccion";
            bitacoraUILabelComboBoxAccion.Size = new Size(106, 15);
            bitacoraUILabelComboBoxAccion.TabIndex = 0;
            bitacoraUILabelComboBoxAccion.Text = "Filtrado por acción";
            bitacoraUILabelComboBoxAccion.Click += bitacoraUILabelComboBoxAccion_Click;
            // 
            // groupBoxListadoBitacora
            // 
            groupBoxListadoBitacora.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBoxListadoBitacora.Controls.Add(bitacoraUILabelGrid);
            groupBoxListadoBitacora.Controls.Add(dataGridViewRegistrosBitacora);
            groupBoxListadoBitacora.Location = new Point(16, 16);
            groupBoxListadoBitacora.Name = "groupBoxListadoBitacora";
            groupBoxListadoBitacora.Size = new Size(864, 642);
            groupBoxListadoBitacora.TabIndex = 1;
            groupBoxListadoBitacora.TabStop = false;
            groupBoxListadoBitacora.Text = "Registros";
            // 
            // bitacoraUILabelGrid
            // 
            bitacoraUILabelGrid.AutoSize = true;
            bitacoraUILabelGrid.Location = new Point(16, 30);
            bitacoraUILabelGrid.Name = "bitacoraUILabelGrid";
            bitacoraUILabelGrid.Size = new Size(129, 15);
            bitacoraUILabelGrid.TabIndex = 1;
            bitacoraUILabelGrid.Text = "Registros de la bitácora";
            // 
            // dataGridViewRegistrosBitacora
            // 
            dataGridViewRegistrosBitacora.AllowUserToAddRows = false;
            dataGridViewRegistrosBitacora.AllowUserToDeleteRows = false;
            dataGridViewRegistrosBitacora.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewRegistrosBitacora.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewRegistrosBitacora.BackgroundColor = SystemColors.Window;
            dataGridViewRegistrosBitacora.BorderStyle = BorderStyle.FixedSingle;
            dataGridViewRegistrosBitacora.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewRegistrosBitacora.EnableHeadersVisualStyles = false;
            dataGridViewRegistrosBitacora.Location = new Point(16, 48);
            dataGridViewRegistrosBitacora.MultiSelect = false;
            dataGridViewRegistrosBitacora.Name = "dataGridViewRegistrosBitacora";
            dataGridViewRegistrosBitacora.ReadOnly = true;
            dataGridViewRegistrosBitacora.RowHeadersVisible = false;
            dataGridViewRegistrosBitacora.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewRegistrosBitacora.Size = new Size(832, 578);
            dataGridViewRegistrosBitacora.TabIndex = 0;
            // 
            // BitacoraUI
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(1212, 674);
            ControlBox = false;
            Controls.Add(groupBoxListadoBitacora);
            Controls.Add(groupBoxFiltros);
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.None;
            MinimumSize = new Size(1100, 650);
            Name = "BitacoraUI";
            StartPosition = FormStartPosition.Manual;
            Text = "Bitácora";
            WindowState = FormWindowState.Maximized;
            groupBoxFiltros.ResumeLayout(false);
            groupBoxFiltros.PerformLayout();
            groupBoxListadoBitacora.ResumeLayout(false);
            groupBoxListadoBitacora.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewRegistrosBitacora).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBoxFiltros;
        private Button bitacoraUIButtonLimpiarFiltros;
        private ComboBox comboBoxUsername;
        private Label bitacoraUILabelComboBoxUsername;
        private ComboBox comboBoxAccion;
        private Label bitacoraUILabelComboBoxAccion;
        private GroupBox groupBoxListadoBitacora;
        private Label bitacoraUILabelGrid;
        private DataGridView dataGridViewRegistrosBitacora;
    }
}