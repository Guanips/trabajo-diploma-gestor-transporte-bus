namespace UI.Modules.gestion_taller
{
    partial class GestionCargasCombustibleUI
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
            groupBoxGestionCombustibleRegistrar = new GroupBox();
            buttonGestionCombustibleConfirmarRegistro = new Button();
            comboBoxInterno = new ComboBox();
            labelGestionCombustibleInterno = new Label();
            numericUpDownKilometraje = new NumericUpDown();
            labelGestionCombustibleKilometrajeActual = new Label();
            numericUpDownPrecioLitro = new NumericUpDown();
            labelGestionCombustiblePrecioPorLitro = new Label();
            numericUpDownLitros = new NumericUpDown();
            labelGestionCombustibleLitros = new Label();
            dateTimePickerCargaFechaHora = new DateTimePicker();
            labelGestionCombustibleFechaHora = new Label();
            groupBoxGestionCombustibleCargasHechas = new GroupBox();
            buttonGestionCombustibleAnularCarga = new Button();
            dataGridViewGestionCombustibleCargas = new DataGridView();
            groupBoxGestionCombustibleRegistrar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownKilometraje).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownPrecioLitro).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownLitros).BeginInit();
            groupBoxGestionCombustibleCargasHechas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewGestionCombustibleCargas).BeginInit();
            SuspendLayout();
            // 
            // groupBoxGestionCombustibleRegistrar
            // 
            groupBoxGestionCombustibleRegistrar.Controls.Add(buttonGestionCombustibleConfirmarRegistro);
            groupBoxGestionCombustibleRegistrar.Controls.Add(comboBoxInterno);
            groupBoxGestionCombustibleRegistrar.Controls.Add(labelGestionCombustibleInterno);
            groupBoxGestionCombustibleRegistrar.Controls.Add(numericUpDownKilometraje);
            groupBoxGestionCombustibleRegistrar.Controls.Add(labelGestionCombustibleKilometrajeActual);
            groupBoxGestionCombustibleRegistrar.Controls.Add(numericUpDownPrecioLitro);
            groupBoxGestionCombustibleRegistrar.Controls.Add(labelGestionCombustiblePrecioPorLitro);
            groupBoxGestionCombustibleRegistrar.Controls.Add(numericUpDownLitros);
            groupBoxGestionCombustibleRegistrar.Controls.Add(labelGestionCombustibleLitros);
            groupBoxGestionCombustibleRegistrar.Controls.Add(dateTimePickerCargaFechaHora);
            groupBoxGestionCombustibleRegistrar.Controls.Add(labelGestionCombustibleFechaHora);
            groupBoxGestionCombustibleRegistrar.Location = new Point(12, 59);
            groupBoxGestionCombustibleRegistrar.Name = "groupBoxGestionCombustibleRegistrar";
            groupBoxGestionCombustibleRegistrar.Size = new Size(226, 345);
            groupBoxGestionCombustibleRegistrar.TabIndex = 0;
            groupBoxGestionCombustibleRegistrar.TabStop = false;
            groupBoxGestionCombustibleRegistrar.Text = "Registrar carga";
            // 
            // buttonGestionCombustibleConfirmarRegistro
            // 
            buttonGestionCombustibleConfirmarRegistro.Location = new Point(10, 292);
            buttonGestionCombustibleConfirmarRegistro.Name = "buttonGestionCombustibleConfirmarRegistro";
            buttonGestionCombustibleConfirmarRegistro.Size = new Size(206, 30);
            buttonGestionCombustibleConfirmarRegistro.TabIndex = 10;
            buttonGestionCombustibleConfirmarRegistro.Text = "Registrar";
            buttonGestionCombustibleConfirmarRegistro.UseVisualStyleBackColor = true;
            buttonGestionCombustibleConfirmarRegistro.Click += buttonGestionCombustibleConfirmarRegistro_Click;
            // 
            // comboBoxInterno
            // 
            comboBoxInterno.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxInterno.FormattingEnabled = true;
            comboBoxInterno.Location = new Point(10, 45);
            comboBoxInterno.Name = "comboBoxInterno";
            comboBoxInterno.Size = new Size(206, 23);
            comboBoxInterno.TabIndex = 9;
            // 
            // labelGestionCombustibleInterno
            // 
            labelGestionCombustibleInterno.AutoSize = true;
            labelGestionCombustibleInterno.Location = new Point(10, 22);
            labelGestionCombustibleInterno.Name = "labelGestionCombustibleInterno";
            labelGestionCombustibleInterno.Size = new Size(91, 15);
            labelGestionCombustibleInterno.TabIndex = 8;
            labelGestionCombustibleInterno.Text = "Interno cargado";
            // 
            // numericUpDownKilometraje
            // 
            numericUpDownKilometraje.Location = new Point(10, 261);
            numericUpDownKilometraje.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numericUpDownKilometraje.Name = "numericUpDownKilometraje";
            numericUpDownKilometraje.Size = new Size(206, 23);
            numericUpDownKilometraje.TabIndex = 7;
            // 
            // labelGestionCombustibleKilometrajeActual
            // 
            labelGestionCombustibleKilometrajeActual.AutoSize = true;
            labelGestionCombustibleKilometrajeActual.Location = new Point(10, 238);
            labelGestionCombustibleKilometrajeActual.Name = "labelGestionCombustibleKilometrajeActual";
            labelGestionCombustibleKilometrajeActual.Size = new Size(127, 15);
            labelGestionCombustibleKilometrajeActual.TabIndex = 6;
            labelGestionCombustibleKilometrajeActual.Text = "Kilometraje del interno";
            // 
            // numericUpDownPrecioLitro
            // 
            numericUpDownPrecioLitro.Location = new Point(10, 207);
            numericUpDownPrecioLitro.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numericUpDownPrecioLitro.Name = "numericUpDownPrecioLitro";
            numericUpDownPrecioLitro.Size = new Size(206, 23);
            numericUpDownPrecioLitro.TabIndex = 5;
            // 
            // labelGestionCombustiblePrecioPorLitro
            // 
            labelGestionCombustiblePrecioPorLitro.AutoSize = true;
            labelGestionCombustiblePrecioPorLitro.Location = new Point(10, 184);
            labelGestionCombustiblePrecioPorLitro.Name = "labelGestionCombustiblePrecioPorLitro";
            labelGestionCombustiblePrecioPorLitro.Size = new Size(85, 15);
            labelGestionCombustiblePrecioPorLitro.TabIndex = 4;
            labelGestionCombustiblePrecioPorLitro.Text = "Precio por litro";
            // 
            // numericUpDownLitros
            // 
            numericUpDownLitros.Location = new Point(10, 153);
            numericUpDownLitros.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numericUpDownLitros.Name = "numericUpDownLitros";
            numericUpDownLitros.Size = new Size(206, 23);
            numericUpDownLitros.TabIndex = 3;
            // 
            // labelGestionCombustibleLitros
            // 
            labelGestionCombustibleLitros.AutoSize = true;
            labelGestionCombustibleLitros.Location = new Point(10, 130);
            labelGestionCombustibleLitros.Name = "labelGestionCombustibleLitros";
            labelGestionCombustibleLitros.Size = new Size(87, 15);
            labelGestionCombustibleLitros.TabIndex = 2;
            labelGestionCombustibleLitros.Text = "Litros cargados";
            // 
            // dateTimePickerCargaFechaHora
            // 
            dateTimePickerCargaFechaHora.Format = DateTimePickerFormat.Time;
            dateTimePickerCargaFechaHora.Location = new Point(10, 99);
            dateTimePickerCargaFechaHora.Name = "dateTimePickerCargaFechaHora";
            dateTimePickerCargaFechaHora.Size = new Size(206, 23);
            dateTimePickerCargaFechaHora.TabIndex = 1;
            // 
            // labelGestionCombustibleFechaHora
            // 
            labelGestionCombustibleFechaHora.AutoSize = true;
            labelGestionCombustibleFechaHora.Location = new Point(10, 76);
            labelGestionCombustibleFechaHora.Name = "labelGestionCombustibleFechaHora";
            labelGestionCombustibleFechaHora.Size = new Size(122, 15);
            labelGestionCombustibleFechaHora.TabIndex = 0;
            labelGestionCombustibleFechaHora.Text = "Fecha y hora de carga";
            // 
            // groupBoxGestionCombustibleCargasHechas
            // 
            groupBoxGestionCombustibleCargasHechas.Controls.Add(buttonGestionCombustibleAnularCarga);
            groupBoxGestionCombustibleCargasHechas.Controls.Add(dataGridViewGestionCombustibleCargas);
            groupBoxGestionCombustibleCargasHechas.Location = new Point(261, 59);
            groupBoxGestionCombustibleCargasHechas.Name = "groupBoxGestionCombustibleCargasHechas";
            groupBoxGestionCombustibleCargasHechas.Size = new Size(764, 467);
            groupBoxGestionCombustibleCargasHechas.TabIndex = 1;
            groupBoxGestionCombustibleCargasHechas.TabStop = false;
            groupBoxGestionCombustibleCargasHechas.Text = "Cargas hechas";
            // 
            // buttonGestionCombustibleAnularCarga
            // 
            buttonGestionCombustibleAnularCarga.Location = new Point(583, 429);
            buttonGestionCombustibleAnularCarga.Name = "buttonGestionCombustibleAnularCarga";
            buttonGestionCombustibleAnularCarga.Size = new Size(175, 32);
            buttonGestionCombustibleAnularCarga.TabIndex = 1;
            buttonGestionCombustibleAnularCarga.Text = "Anular Carga";
            buttonGestionCombustibleAnularCarga.UseVisualStyleBackColor = true;
            buttonGestionCombustibleAnularCarga.Click += buttonGestionCombustibleAnularCarga_Click;
            // 
            // dataGridViewGestionCombustibleCargas
            // 
            dataGridViewGestionCombustibleCargas.AllowUserToAddRows = false;
            dataGridViewGestionCombustibleCargas.AllowUserToDeleteRows = false;
            dataGridViewGestionCombustibleCargas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewGestionCombustibleCargas.Location = new Point(6, 22);
            dataGridViewGestionCombustibleCargas.MultiSelect = false;
            dataGridViewGestionCombustibleCargas.Name = "dataGridViewGestionCombustibleCargas";
            dataGridViewGestionCombustibleCargas.ReadOnly = true;
            dataGridViewGestionCombustibleCargas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewGestionCombustibleCargas.Size = new Size(752, 390);
            dataGridViewGestionCombustibleCargas.TabIndex = 0;
            // 
            // GestionCargasCombustibleUI
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1037, 547);
            ControlBox = false;
            Controls.Add(groupBoxGestionCombustibleCargasHechas);
            Controls.Add(groupBoxGestionCombustibleRegistrar);
            FormBorderStyle = FormBorderStyle.None;
            Name = "GestionCargasCombustibleUI";
            Text = "GestionCargasCombustibleUI";
            WindowState = FormWindowState.Maximized;
            groupBoxGestionCombustibleRegistrar.ResumeLayout(false);
            groupBoxGestionCombustibleRegistrar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownKilometraje).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownPrecioLitro).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownLitros).EndInit();
            groupBoxGestionCombustibleCargasHechas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewGestionCombustibleCargas).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBoxGestionCombustibleRegistrar;
        private Label labelGestionCombustibleFechaHora;
        private DateTimePicker dateTimePickerCargaFechaHora;
        private Button buttonGestionCombustibleConfirmarRegistro;
        private ComboBox comboBoxInterno;
        private Label labelGestionCombustibleInterno;
        private NumericUpDown numericUpDownKilometraje;
        private Label labelGestionCombustibleKilometrajeActual;
        private NumericUpDown numericUpDownPrecioLitro;
        private Label labelGestionCombustiblePrecioPorLitro;
        private NumericUpDown numericUpDownLitros;
        private Label labelGestionCombustibleLitros;
        private GroupBox groupBoxGestionCombustibleCargasHechas;
        private Button buttonGestionCombustibleAnularCarga;
        private DataGridView dataGridViewGestionCombustibleCargas;
    }
}