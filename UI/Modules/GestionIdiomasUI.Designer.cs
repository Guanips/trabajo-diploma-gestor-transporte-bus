namespace UI.Modules
{
    partial class GestionIdiomasUI
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.GroupBox groupBoxIdiomas;
        private System.Windows.Forms.DataGridView dataGridViewIdiomas;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIdiomaCodigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIdiomaNombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIdiomaAvance;
        private System.Windows.Forms.GroupBox groupBoxIdioma;
        private System.Windows.Forms.Label labelCodigo;
        private System.Windows.Forms.TextBox textBoxCodigo;
        private System.Windows.Forms.Label labelNombre;
        private System.Windows.Forms.TextBox textBoxNombre;
        private System.Windows.Forms.Button btnNuevoIdioma;
        private System.Windows.Forms.Button btnGuardarIdioma;
        private System.Windows.Forms.GroupBox groupBoxTraducciones;
        private System.Windows.Forms.Label labelBuscar;
        private System.Windows.Forms.TextBox textBoxBuscar;
        private System.Windows.Forms.Label labelFormulario;
        private System.Windows.Forms.ComboBox comboBoxFormulario;
        private System.Windows.Forms.CheckBox checkBoxSoloFaltantes;
        private System.Windows.Forms.DataGridView dataGridViewTraducciones;
        private System.Windows.Forms.DataGridViewTextBoxColumn colClave;
        private System.Windows.Forms.DataGridViewTextBoxColumn colReferencia;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTraduccion;
        private System.Windows.Forms.Label labelProgreso;
        private System.Windows.Forms.Button btnDescartarCambios;
        private System.Windows.Forms.Button btnGuardarTraducciones;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            groupBoxIdiomas = new GroupBox();
            dataGridViewIdiomas = new DataGridView();
            colIdiomaCodigo = new DataGridViewTextBoxColumn();
            colIdiomaNombre = new DataGridViewTextBoxColumn();
            colIdiomaAvance = new DataGridViewTextBoxColumn();
            groupBoxIdioma = new GroupBox();
            labelCodigo = new Label();
            textBoxCodigo = new TextBox();
            labelNombre = new Label();
            textBoxNombre = new TextBox();
            btnNuevoIdioma = new Button();
            btnGuardarIdioma = new Button();
            groupBoxTraducciones = new GroupBox();
            labelBuscar = new Label();
            textBoxBuscar = new TextBox();
            labelFormulario = new Label();
            comboBoxFormulario = new ComboBox();
            checkBoxSoloFaltantes = new CheckBox();
            dataGridViewTraducciones = new DataGridView();
            colClave = new DataGridViewTextBoxColumn();
            colReferencia = new DataGridViewTextBoxColumn();
            colTraduccion = new DataGridViewTextBoxColumn();
            labelProgreso = new Label();
            btnDescartarCambios = new Button();
            btnGuardarTraducciones = new Button();
            groupBoxIdiomas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewIdiomas).BeginInit();
            groupBoxIdioma.SuspendLayout();
            groupBoxTraducciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewTraducciones).BeginInit();
            SuspendLayout();
            //
            // groupBoxIdiomas
            //
            groupBoxIdiomas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            groupBoxIdiomas.Controls.Add(dataGridViewIdiomas);
            groupBoxIdiomas.Location = new Point(16, 16);
            groupBoxIdiomas.Name = "groupBoxIdiomas";
            groupBoxIdiomas.Size = new Size(340, 500);
            groupBoxIdiomas.TabIndex = 0;
            groupBoxIdiomas.TabStop = false;
            groupBoxIdiomas.Text = "Idiomas";
            //
            // dataGridViewIdiomas
            //
            dataGridViewIdiomas.AllowUserToAddRows = false;
            dataGridViewIdiomas.AllowUserToDeleteRows = false;
            dataGridViewIdiomas.AllowUserToResizeRows = false;
            dataGridViewIdiomas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewIdiomas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewIdiomas.BackgroundColor = SystemColors.Window;
            dataGridViewIdiomas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewIdiomas.Columns.AddRange(new DataGridViewColumn[] { colIdiomaCodigo, colIdiomaNombre, colIdiomaAvance });
            dataGridViewIdiomas.Location = new Point(16, 28);
            dataGridViewIdiomas.MultiSelect = false;
            dataGridViewIdiomas.Name = "dataGridViewIdiomas";
            dataGridViewIdiomas.ReadOnly = true;
            dataGridViewIdiomas.RowHeadersVisible = false;
            dataGridViewIdiomas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewIdiomas.Size = new Size(308, 456);
            dataGridViewIdiomas.TabIndex = 0;
            dataGridViewIdiomas.SelectionChanged += dataGridViewIdiomas_SelectionChanged;
            //
            // colIdiomaCodigo
            //
            colIdiomaCodigo.FillWeight = 25F;
            colIdiomaCodigo.HeaderText = "Código";
            colIdiomaCodigo.Name = "colIdiomaCodigo";
            colIdiomaCodigo.ReadOnly = true;
            //
            // colIdiomaNombre
            //
            colIdiomaNombre.FillWeight = 50F;
            colIdiomaNombre.HeaderText = "Nombre";
            colIdiomaNombre.Name = "colIdiomaNombre";
            colIdiomaNombre.ReadOnly = true;
            //
            // colIdiomaAvance
            //
            colIdiomaAvance.FillWeight = 25F;
            colIdiomaAvance.HeaderText = "Avance";
            colIdiomaAvance.Name = "colIdiomaAvance";
            colIdiomaAvance.ReadOnly = true;
            //
            // groupBoxIdioma
            //
            groupBoxIdioma.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            groupBoxIdioma.Controls.Add(labelCodigo);
            groupBoxIdioma.Controls.Add(textBoxCodigo);
            groupBoxIdioma.Controls.Add(labelNombre);
            groupBoxIdioma.Controls.Add(textBoxNombre);
            groupBoxIdioma.Controls.Add(btnNuevoIdioma);
            groupBoxIdioma.Controls.Add(btnGuardarIdioma);
            groupBoxIdioma.Location = new Point(16, 530);
            groupBoxIdioma.Name = "groupBoxIdioma";
            groupBoxIdioma.Size = new Size(340, 176);
            groupBoxIdioma.TabIndex = 1;
            groupBoxIdioma.TabStop = false;
            groupBoxIdioma.Text = "Datos del idioma";
            //
            // labelCodigo
            //
            labelCodigo.AutoSize = true;
            labelCodigo.Location = new Point(16, 34);
            labelCodigo.Name = "labelCodigo";
            labelCodigo.Size = new Size(87, 15);
            labelCodigo.TabIndex = 0;
            labelCodigo.Text = "Código (Ej: FR)";
            //
            // textBoxCodigo
            //
            textBoxCodigo.CharacterCasing = CharacterCasing.Upper;
            textBoxCodigo.Location = new Point(124, 31);
            textBoxCodigo.MaxLength = 5;
            textBoxCodigo.Name = "textBoxCodigo";
            textBoxCodigo.Size = new Size(80, 23);
            textBoxCodigo.TabIndex = 1;
            //
            // labelNombre
            //
            labelNombre.AutoSize = true;
            labelNombre.Location = new Point(16, 72);
            labelNombre.Name = "labelNombre";
            labelNombre.Size = new Size(51, 15);
            labelNombre.TabIndex = 2;
            labelNombre.Text = "Nombre";
            //
            // textBoxNombre
            //
            textBoxNombre.Location = new Point(124, 69);
            textBoxNombre.MaxLength = 50;
            textBoxNombre.Name = "textBoxNombre";
            textBoxNombre.Size = new Size(200, 23);
            textBoxNombre.TabIndex = 3;
            //
            // btnNuevoIdioma
            //
            btnNuevoIdioma.Location = new Point(16, 120);
            btnNuevoIdioma.Name = "btnNuevoIdioma";
            btnNuevoIdioma.Size = new Size(146, 34);
            btnNuevoIdioma.TabIndex = 4;
            btnNuevoIdioma.Text = "Nuevo idioma";
            btnNuevoIdioma.UseVisualStyleBackColor = true;
            btnNuevoIdioma.Click += btnNuevoIdioma_Click;
            //
            // btnGuardarIdioma
            //
            btnGuardarIdioma.Location = new Point(178, 120);
            btnGuardarIdioma.Name = "btnGuardarIdioma";
            btnGuardarIdioma.Size = new Size(146, 34);
            btnGuardarIdioma.TabIndex = 5;
            btnGuardarIdioma.Text = "Guardar idioma";
            btnGuardarIdioma.UseVisualStyleBackColor = true;
            btnGuardarIdioma.Click += btnGuardarIdioma_Click;
            //
            // groupBoxTraducciones
            //
            groupBoxTraducciones.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBoxTraducciones.Controls.Add(labelBuscar);
            groupBoxTraducciones.Controls.Add(textBoxBuscar);
            groupBoxTraducciones.Controls.Add(labelFormulario);
            groupBoxTraducciones.Controls.Add(comboBoxFormulario);
            groupBoxTraducciones.Controls.Add(checkBoxSoloFaltantes);
            groupBoxTraducciones.Controls.Add(dataGridViewTraducciones);
            groupBoxTraducciones.Controls.Add(labelProgreso);
            groupBoxTraducciones.Controls.Add(btnDescartarCambios);
            groupBoxTraducciones.Controls.Add(btnGuardarTraducciones);
            groupBoxTraducciones.Location = new Point(372, 16);
            groupBoxTraducciones.Name = "groupBoxTraducciones";
            groupBoxTraducciones.Size = new Size(864, 690);
            groupBoxTraducciones.TabIndex = 2;
            groupBoxTraducciones.TabStop = false;
            groupBoxTraducciones.Text = "Traducciones";
            //
            // labelBuscar
            //
            labelBuscar.AutoSize = true;
            labelBuscar.Location = new Point(16, 34);
            labelBuscar.Name = "labelBuscar";
            labelBuscar.Size = new Size(42, 15);
            labelBuscar.TabIndex = 0;
            labelBuscar.Text = "Buscar";
            //
            // textBoxBuscar
            //
            textBoxBuscar.Location = new Point(76, 31);
            textBoxBuscar.Name = "textBoxBuscar";
            textBoxBuscar.Size = new Size(240, 23);
            textBoxBuscar.TabIndex = 1;
            textBoxBuscar.TextChanged += filtros_Changed;
            //
            // labelFormulario
            //
            labelFormulario.AutoSize = true;
            labelFormulario.Location = new Point(336, 34);
            labelFormulario.Name = "labelFormulario";
            labelFormulario.Size = new Size(65, 15);
            labelFormulario.TabIndex = 2;
            labelFormulario.Text = "Formulario";
            //
            // comboBoxFormulario
            //
            comboBoxFormulario.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxFormulario.Location = new Point(416, 31);
            comboBoxFormulario.Name = "comboBoxFormulario";
            comboBoxFormulario.Size = new Size(240, 23);
            comboBoxFormulario.TabIndex = 3;
            comboBoxFormulario.SelectedIndexChanged += filtros_Changed;
            //
            // checkBoxSoloFaltantes
            //
            checkBoxSoloFaltantes.AutoSize = true;
            checkBoxSoloFaltantes.Location = new Point(676, 33);
            checkBoxSoloFaltantes.Name = "checkBoxSoloFaltantes";
            checkBoxSoloFaltantes.Size = new Size(113, 19);
            checkBoxSoloFaltantes.TabIndex = 4;
            checkBoxSoloFaltantes.Text = "Solo sin traducir";
            checkBoxSoloFaltantes.UseVisualStyleBackColor = true;
            checkBoxSoloFaltantes.CheckedChanged += filtros_Changed;
            //
            // dataGridViewTraducciones
            //
            dataGridViewTraducciones.AllowUserToAddRows = false;
            dataGridViewTraducciones.AllowUserToDeleteRows = false;
            dataGridViewTraducciones.AllowUserToResizeRows = false;
            dataGridViewTraducciones.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewTraducciones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewTraducciones.BackgroundColor = SystemColors.Window;
            dataGridViewTraducciones.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewTraducciones.Columns.AddRange(new DataGridViewColumn[] { colClave, colReferencia, colTraduccion });
            dataGridViewTraducciones.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
            dataGridViewTraducciones.Location = new Point(16, 68);
            dataGridViewTraducciones.Name = "dataGridViewTraducciones";
            dataGridViewTraducciones.RowHeadersVisible = false;
            dataGridViewTraducciones.Size = new Size(832, 556);
            dataGridViewTraducciones.TabIndex = 5;
            dataGridViewTraducciones.CellEndEdit += dataGridViewTraducciones_CellEndEdit;
            //
            // colClave
            //
            colClave.FillWeight = 30F;
            colClave.HeaderText = "Etiqueta";
            colClave.Name = "colClave";
            colClave.ReadOnly = true;
            //
            // colReferencia
            //
            colReferencia.FillWeight = 35F;
            colReferencia.HeaderText = "Texto en idioma por defecto";
            colReferencia.Name = "colReferencia";
            colReferencia.ReadOnly = true;
            //
            // colTraduccion
            //
            colTraduccion.FillWeight = 35F;
            colTraduccion.HeaderText = "Traducción";
            colTraduccion.Name = "colTraduccion";
            //
            // labelProgreso
            //
            labelProgreso.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            labelProgreso.AutoSize = true;
            labelProgreso.Location = new Point(16, 648);
            labelProgreso.Name = "labelProgreso";
            labelProgreso.Size = new Size(0, 15);
            labelProgreso.TabIndex = 6;
            labelProgreso.Tag = "notranslate";
            //
            // btnDescartarCambios
            //
            btnDescartarCambios.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnDescartarCambios.Location = new Point(546, 638);
            btnDescartarCambios.Name = "btnDescartarCambios";
            btnDescartarCambios.Size = new Size(146, 34);
            btnDescartarCambios.TabIndex = 7;
            btnDescartarCambios.Text = "Descartar cambios";
            btnDescartarCambios.UseVisualStyleBackColor = true;
            btnDescartarCambios.Click += btnDescartarCambios_Click;
            //
            // btnGuardarTraducciones
            //
            btnGuardarTraducciones.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnGuardarTraducciones.Location = new Point(702, 638);
            btnGuardarTraducciones.Name = "btnGuardarTraducciones";
            btnGuardarTraducciones.Size = new Size(146, 34);
            btnGuardarTraducciones.TabIndex = 8;
            btnGuardarTraducciones.Text = "Guardar traducciones";
            btnGuardarTraducciones.UseVisualStyleBackColor = true;
            btnGuardarTraducciones.Click += btnGuardarTraducciones_Click;
            //
            // GestionIdiomasUI
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(1252, 722);
            ControlBox = false;
            Controls.Add(groupBoxTraducciones);
            Controls.Add(groupBoxIdioma);
            Controls.Add(groupBoxIdiomas);
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.None;
            MinimumSize = new Size(1100, 700);
            Name = "GestionIdiomasUI";
            Text = "Gestión de idiomas";
            WindowState = FormWindowState.Maximized;
            Load += GestionIdiomasUI_Load;
            groupBoxIdiomas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewIdiomas).EndInit();
            groupBoxIdioma.ResumeLayout(false);
            groupBoxIdioma.PerformLayout();
            groupBoxTraducciones.ResumeLayout(false);
            groupBoxTraducciones.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewTraducciones).EndInit();
            ResumeLayout(false);
        }
    }
}
