namespace UI.Modules
{
    partial class GestionParadasUI
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
            groupBoxAltaParada = new GroupBox();
            buttonParadaAltaConfirmar = new Button();
            checkBoxParadaAltaHabilitada = new CheckBox();
            textBoxParadaAltaDireccion = new TextBox();
            labelParadaAltaDireccion = new Label();
            textBoxParadaAltaLocalidad = new TextBox();
            labelParadaAltaLocalidad = new Label();
            textBoxParadaAltaDescripcion = new TextBox();
            labelParadaAltaDescripcion = new Label();
            textBoxParadaAltaID = new TextBox();
            labelParadaAltaId = new Label();
            groupBoxListadoParadas = new GroupBox();
            buttonParadaBajaConfirmar = new Button();
            dataGridViewParadas = new DataGridView();
            groupBoxAltaParada.SuspendLayout();
            groupBoxListadoParadas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewParadas).BeginInit();
            SuspendLayout();
            // 
            // groupBoxAltaParada
            // 
            groupBoxAltaParada.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            groupBoxAltaParada.Controls.Add(buttonParadaAltaConfirmar);
            groupBoxAltaParada.Controls.Add(checkBoxParadaAltaHabilitada);
            groupBoxAltaParada.Controls.Add(textBoxParadaAltaDireccion);
            groupBoxAltaParada.Controls.Add(labelParadaAltaDireccion);
            groupBoxAltaParada.Controls.Add(textBoxParadaAltaLocalidad);
            groupBoxAltaParada.Controls.Add(labelParadaAltaLocalidad);
            groupBoxAltaParada.Controls.Add(textBoxParadaAltaDescripcion);
            groupBoxAltaParada.Controls.Add(labelParadaAltaDescripcion);
            groupBoxAltaParada.Controls.Add(textBoxParadaAltaID);
            groupBoxAltaParada.Controls.Add(labelParadaAltaId);
            groupBoxAltaParada.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            groupBoxAltaParada.Location = new Point(16, 16);
            groupBoxAltaParada.Name = "groupBoxAltaParada";
            groupBoxAltaParada.Size = new Size(360, 662);
            groupBoxAltaParada.TabIndex = 0;
            groupBoxAltaParada.TabStop = false;
            groupBoxAltaParada.Text = "Nueva parada";
            // 
            // buttonParadaAltaConfirmar
            // 
            buttonParadaAltaConfirmar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            buttonParadaAltaConfirmar.Location = new Point(24, 616);
            buttonParadaAltaConfirmar.Name = "buttonParadaAltaConfirmar";
            buttonParadaAltaConfirmar.Size = new Size(312, 34);
            buttonParadaAltaConfirmar.TabIndex = 9;
            buttonParadaAltaConfirmar.Text = "Confirmar alta";
            buttonParadaAltaConfirmar.UseVisualStyleBackColor = true;
            buttonParadaAltaConfirmar.Click += buttonParadaAltaConfirmar_Click;
            // 
            // checkBoxParadaAltaHabilitada
            // 
            checkBoxParadaAltaHabilitada.AutoSize = true;
            checkBoxParadaAltaHabilitada.Location = new Point(24, 378);
            checkBoxParadaAltaHabilitada.Name = "checkBoxParadaAltaHabilitada";
            checkBoxParadaAltaHabilitada.Size = new Size(112, 19);
            checkBoxParadaAltaHabilitada.TabIndex = 8;
            checkBoxParadaAltaHabilitada.Text = "¿Está habilitada?";
            checkBoxParadaAltaHabilitada.UseVisualStyleBackColor = true;
            // 
            // textBoxParadaAltaDireccion
            // 
            textBoxParadaAltaDireccion.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxParadaAltaDireccion.Location = new Point(24, 344);
            textBoxParadaAltaDireccion.MaxLength = 200;
            textBoxParadaAltaDireccion.Name = "textBoxParadaAltaDireccion";
            textBoxParadaAltaDireccion.Size = new Size(312, 23);
            textBoxParadaAltaDireccion.TabIndex = 7;
            // 
            // labelParadaAltaDireccion
            // 
            labelParadaAltaDireccion.AutoSize = true;
            labelParadaAltaDireccion.Location = new Point(24, 326);
            labelParadaAltaDireccion.Name = "labelParadaAltaDireccion";
            labelParadaAltaDireccion.Size = new Size(57, 15);
            labelParadaAltaDireccion.TabIndex = 6;
            labelParadaAltaDireccion.Text = "Dirección";
            // 
            // textBoxParadaAltaLocalidad
            // 
            textBoxParadaAltaLocalidad.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxParadaAltaLocalidad.Location = new Point(24, 289);
            textBoxParadaAltaLocalidad.MaxLength = 100;
            textBoxParadaAltaLocalidad.Name = "textBoxParadaAltaLocalidad";
            textBoxParadaAltaLocalidad.Size = new Size(312, 23);
            textBoxParadaAltaLocalidad.TabIndex = 5;
            // 
            // labelParadaAltaLocalidad
            // 
            labelParadaAltaLocalidad.AutoSize = true;
            labelParadaAltaLocalidad.Location = new Point(24, 271);
            labelParadaAltaLocalidad.Name = "labelParadaAltaLocalidad";
            labelParadaAltaLocalidad.Size = new Size(58, 15);
            labelParadaAltaLocalidad.TabIndex = 4;
            labelParadaAltaLocalidad.Text = "Localidad";
            // 
            // textBoxParadaAltaDescripcion
            // 
            textBoxParadaAltaDescripcion.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxParadaAltaDescripcion.Location = new Point(24, 112);
            textBoxParadaAltaDescripcion.MaxLength = 255;
            textBoxParadaAltaDescripcion.Multiline = true;
            textBoxParadaAltaDescripcion.Name = "textBoxParadaAltaDescripcion";
            textBoxParadaAltaDescripcion.ScrollBars = ScrollBars.Vertical;
            textBoxParadaAltaDescripcion.Size = new Size(312, 145);
            textBoxParadaAltaDescripcion.TabIndex = 3;
            // 
            // labelParadaAltaDescripcion
            // 
            labelParadaAltaDescripcion.AutoSize = true;
            labelParadaAltaDescripcion.Location = new Point(24, 94);
            labelParadaAltaDescripcion.Name = "labelParadaAltaDescripcion";
            labelParadaAltaDescripcion.Size = new Size(69, 15);
            labelParadaAltaDescripcion.TabIndex = 2;
            labelParadaAltaDescripcion.Text = "Descripción";
            // 
            // textBoxParadaAltaID
            // 
            textBoxParadaAltaID.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxParadaAltaID.Location = new Point(24, 58);
            textBoxParadaAltaID.MaxLength = 20;
            textBoxParadaAltaID.Name = "textBoxParadaAltaID";
            textBoxParadaAltaID.Size = new Size(312, 23);
            textBoxParadaAltaID.TabIndex = 1;
            // 
            // labelParadaAltaId
            // 
            labelParadaAltaId.AutoSize = true;
            labelParadaAltaId.Location = new Point(24, 40);
            labelParadaAltaId.Name = "labelParadaAltaId";
            labelParadaAltaId.Size = new Size(18, 15);
            labelParadaAltaId.TabIndex = 0;
            labelParadaAltaId.Text = "ID";
            // 
            // groupBoxListadoParadas
            // 
            groupBoxListadoParadas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBoxListadoParadas.Controls.Add(buttonParadaBajaConfirmar);
            groupBoxListadoParadas.Controls.Add(dataGridViewParadas);
            groupBoxListadoParadas.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            groupBoxListadoParadas.Location = new Point(392, 16);
            groupBoxListadoParadas.Name = "groupBoxListadoParadas";
            groupBoxListadoParadas.Size = new Size(802, 662);
            groupBoxListadoParadas.TabIndex = 1;
            groupBoxListadoParadas.TabStop = false;
            groupBoxListadoParadas.Text = "Listado de paradas";
            // 
            // buttonParadaBajaConfirmar
            // 
            buttonParadaBajaConfirmar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buttonParadaBajaConfirmar.Location = new Point(12, 616);
            buttonParadaBajaConfirmar.Name = "buttonParadaBajaConfirmar";
            buttonParadaBajaConfirmar.Size = new Size(258, 34);
            buttonParadaBajaConfirmar.TabIndex = 1;
            buttonParadaBajaConfirmar.Text = "Eliminar parada seleccionada";
            buttonParadaBajaConfirmar.UseVisualStyleBackColor = true;
            buttonParadaBajaConfirmar.Click += buttonParadaBajaConfirmar_Click;
            // 
            // dataGridViewParadas
            // 
            dataGridViewParadas.AllowUserToAddRows = false;
            dataGridViewParadas.AllowUserToDeleteRows = false;
            dataGridViewParadas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewParadas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewParadas.BackgroundColor = SystemColors.Window;
            dataGridViewParadas.BorderStyle = BorderStyle.FixedSingle;
            dataGridViewParadas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewParadas.EnableHeadersVisualStyles = false;
            dataGridViewParadas.Location = new Point(12, 28);
            dataGridViewParadas.MultiSelect = false;
            dataGridViewParadas.Name = "dataGridViewParadas";
            dataGridViewParadas.ReadOnly = true;
            dataGridViewParadas.RowHeadersVisible = false;
            dataGridViewParadas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewParadas.Size = new Size(778, 572);
            dataGridViewParadas.TabIndex = 0;
            // 
            // GestionParadasUI
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(1210, 694);
            ControlBox = false;
            Controls.Add(groupBoxListadoParadas);
            Controls.Add(groupBoxAltaParada);
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.None;
            MinimumSize = new Size(1100, 680);
            Name = "GestionParadasUI";
            StartPosition = FormStartPosition.Manual;
            Text = "Gestión de paradas";
            WindowState = FormWindowState.Maximized;
            groupBoxAltaParada.ResumeLayout(false);
            groupBoxAltaParada.PerformLayout();
            groupBoxListadoParadas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewParadas).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBoxAltaParada;
        private Button buttonParadaAltaConfirmar;
        private CheckBox checkBoxParadaAltaHabilitada;
        private TextBox textBoxParadaAltaDireccion;
        private Label labelParadaAltaDireccion;
        private TextBox textBoxParadaAltaLocalidad;
        private Label labelParadaAltaLocalidad;
        private TextBox textBoxParadaAltaDescripcion;
        private Label labelParadaAltaDescripcion;
        private TextBox textBoxParadaAltaID;
        private Label labelParadaAltaId;
        private GroupBox groupBoxListadoParadas;
        private Button buttonParadaBajaConfirmar;
        private DataGridView dataGridViewParadas;
    }
}