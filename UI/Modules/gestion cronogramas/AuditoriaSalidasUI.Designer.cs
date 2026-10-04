namespace UI.Modules.gestion_cronogramas
{
    partial class AuditoriaSalidasUI
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
            groupBoxAuditoriaCronogramasSalidas = new GroupBox();
            labelAuditoriaCronogramasSalidas = new Label();
            dataGridViewSalidasCronogramaSeleccionado = new DataGridView();
            labelAuditoriaCronogramasListaCronogramas = new Label();
            listBoxCronogramas = new ListBox();
            groupBoxAuditoriaCronogramasAplicarSancion = new GroupBox();
            buttonAuditoriaCronogramasConfirmar = new Button();
            dateTimePickerFechaSancion = new DateTimePicker();
            labelAuditoriaCronogramasFechaSancion = new Label();
            textBoxMotivoSancion = new TextBox();
            labelAuditoriaCronogramasMotivoSancion = new Label();
            textBoxNombreChofer = new TextBox();
            labelAuditoriaCronogramasNombreChofer = new Label();
            groupBoxAuditoriaCronogramasLlegadaReal = new GroupBox();
            labelAuditoriaCronogramasHoraLlegadaReal = new Label();
            dateTimePickerHoraLlegadaReal = new DateTimePicker();
            buttonAuditoriaCronogramasRegistrarLlegada = new Button();
            labelAuditoriaCronogramasToleranciaRetraso = new Label();
            numericUpDownToleranciaRetraso = new NumericUpDown();
            groupBoxAuditoriaCronogramasSalidas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewSalidasCronogramaSeleccionado).BeginInit();
            groupBoxAuditoriaCronogramasAplicarSancion.SuspendLayout();
            groupBoxAuditoriaCronogramasLlegadaReal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownToleranciaRetraso).BeginInit();
            SuspendLayout();
            // 
            // groupBoxAuditoriaCronogramasSalidas
            // 
            groupBoxAuditoriaCronogramasSalidas.Controls.Add(labelAuditoriaCronogramasSalidas);
            groupBoxAuditoriaCronogramasSalidas.Controls.Add(dataGridViewSalidasCronogramaSeleccionado);
            groupBoxAuditoriaCronogramasSalidas.Controls.Add(labelAuditoriaCronogramasListaCronogramas);
            groupBoxAuditoriaCronogramasSalidas.Controls.Add(listBoxCronogramas);
            groupBoxAuditoriaCronogramasSalidas.Location = new Point(12, 55);
            groupBoxAuditoriaCronogramasSalidas.Name = "groupBoxAuditoriaCronogramasSalidas";
            groupBoxAuditoriaCronogramasSalidas.Size = new Size(804, 426);
            groupBoxAuditoriaCronogramasSalidas.TabIndex = 0;
            groupBoxAuditoriaCronogramasSalidas.TabStop = false;
            groupBoxAuditoriaCronogramasSalidas.Text = "Cronogramas y salidas";
            // 
            // labelAuditoriaCronogramasSalidas
            // 
            labelAuditoriaCronogramasSalidas.AutoSize = true;
            labelAuditoriaCronogramasSalidas.Location = new Point(249, 23);
            labelAuditoriaCronogramasSalidas.Name = "labelAuditoriaCronogramasSalidas";
            labelAuditoriaCronogramasSalidas.Size = new Size(202, 15);
            labelAuditoriaCronogramasSalidas.TabIndex = 3;
            labelAuditoriaCronogramasSalidas.Text = "Salidas del cronograma seleccionado";
            // 
            // dataGridViewSalidasCronogramaSeleccionado
            // 
            dataGridViewSalidasCronogramaSeleccionado.AllowUserToAddRows = false;
            dataGridViewSalidasCronogramaSeleccionado.AllowUserToDeleteRows = false;
            dataGridViewSalidasCronogramaSeleccionado.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewSalidasCronogramaSeleccionado.Location = new Point(249, 39);
            dataGridViewSalidasCronogramaSeleccionado.MultiSelect = false;
            dataGridViewSalidasCronogramaSeleccionado.Name = "dataGridViewSalidasCronogramaSeleccionado";
            dataGridViewSalidasCronogramaSeleccionado.ReadOnly = true;
            dataGridViewSalidasCronogramaSeleccionado.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewSalidasCronogramaSeleccionado.Size = new Size(549, 381);
            dataGridViewSalidasCronogramaSeleccionado.TabIndex = 2;
            dataGridViewSalidasCronogramaSeleccionado.CellFormatting += dataGridViewSalidasCronogramaSeleccionado_CellFormatting;
            dataGridViewSalidasCronogramaSeleccionado.SelectionChanged +=dataGridViewSalidasCronogramaSeleccionado_SelectionChanged;
            // 
            // labelAuditoriaCronogramasListaCronogramas
            // 
            labelAuditoriaCronogramasListaCronogramas.AutoSize = true;
            labelAuditoriaCronogramasListaCronogramas.Location = new Point(6, 23);
            labelAuditoriaCronogramasListaCronogramas.Name = "labelAuditoriaCronogramasListaCronogramas";
            labelAuditoriaCronogramasListaCronogramas.Size = new Size(142, 15);
            labelAuditoriaCronogramasListaCronogramas.TabIndex = 1;
            labelAuditoriaCronogramasListaCronogramas.Text = "Cronogramas disponibles";
            // 
            // listBoxCronogramas
            // 
            listBoxCronogramas.FormattingEnabled = true;
            listBoxCronogramas.ItemHeight = 15;
            listBoxCronogramas.Location = new Point(6, 41);
            listBoxCronogramas.Name = "listBoxCronogramas";
            listBoxCronogramas.Size = new Size(223, 379);
            listBoxCronogramas.TabIndex = 0;
            listBoxCronogramas.SelectedIndexChanged += listBoxCronogramas_SelectedIndexChanged;
            // 
            // groupBoxAuditoriaCronogramasAplicarSancion
            // 
            groupBoxAuditoriaCronogramasAplicarSancion.Controls.Add(buttonAuditoriaCronogramasConfirmar);
            groupBoxAuditoriaCronogramasAplicarSancion.Controls.Add(dateTimePickerFechaSancion);
            groupBoxAuditoriaCronogramasAplicarSancion.Controls.Add(labelAuditoriaCronogramasFechaSancion);
            groupBoxAuditoriaCronogramasAplicarSancion.Controls.Add(textBoxMotivoSancion);
            groupBoxAuditoriaCronogramasAplicarSancion.Controls.Add(labelAuditoriaCronogramasMotivoSancion);
            groupBoxAuditoriaCronogramasAplicarSancion.Controls.Add(textBoxNombreChofer);
            groupBoxAuditoriaCronogramasAplicarSancion.Controls.Add(labelAuditoriaCronogramasNombreChofer);
            groupBoxAuditoriaCronogramasAplicarSancion.Location = new Point(822, 55);
            groupBoxAuditoriaCronogramasAplicarSancion.Name = "groupBoxAuditoriaCronogramasAplicarSancion";
            groupBoxAuditoriaCronogramasAplicarSancion.Size = new Size(237, 426);
            groupBoxAuditoriaCronogramasAplicarSancion.TabIndex = 1;
            groupBoxAuditoriaCronogramasAplicarSancion.TabStop = false;
            groupBoxAuditoriaCronogramasAplicarSancion.Text = "Aplicar sancion";
            // 
            // buttonAuditoriaCronogramasConfirmar
            // 
            buttonAuditoriaCronogramasConfirmar.Location = new Point(6, 341);
            buttonAuditoriaCronogramasConfirmar.Name = "buttonAuditoriaCronogramasConfirmar";
            buttonAuditoriaCronogramasConfirmar.Size = new Size(225, 38);
            buttonAuditoriaCronogramasConfirmar.TabIndex = 6;
            buttonAuditoriaCronogramasConfirmar.Text = "Confirmar";
            buttonAuditoriaCronogramasConfirmar.UseVisualStyleBackColor = true;
            buttonAuditoriaCronogramasConfirmar.Click += buttonAuditoriaCronogramasConfirmar_Click;
            // 
            // dateTimePickerFechaSancion
            // 
            dateTimePickerFechaSancion.Location = new Point(6, 312);
            dateTimePickerFechaSancion.Name = "dateTimePickerFechaSancion";
            dateTimePickerFechaSancion.Size = new Size(225, 23);
            dateTimePickerFechaSancion.TabIndex = 5;
            // 
            // labelAuditoriaCronogramasFechaSancion
            // 
            labelAuditoriaCronogramasFechaSancion.AutoSize = true;
            labelAuditoriaCronogramasFechaSancion.Location = new Point(6, 293);
            labelAuditoriaCronogramasFechaSancion.Name = "labelAuditoriaCronogramasFechaSancion";
            labelAuditoriaCronogramasFechaSancion.Size = new Size(38, 15);
            labelAuditoriaCronogramasFechaSancion.TabIndex = 4;
            labelAuditoriaCronogramasFechaSancion.Text = "Fecha";
            // 
            // textBoxMotivoSancion
            // 
            textBoxMotivoSancion.Location = new Point(6, 115);
            textBoxMotivoSancion.Multiline = true;
            textBoxMotivoSancion.Name = "textBoxMotivoSancion";
            textBoxMotivoSancion.Size = new Size(225, 174);
            textBoxMotivoSancion.TabIndex = 3;
            // 
            // labelAuditoriaCronogramasMotivoSancion
            // 
            labelAuditoriaCronogramasMotivoSancion.AutoSize = true;
            labelAuditoriaCronogramasMotivoSancion.Location = new Point(6, 96);
            labelAuditoriaCronogramasMotivoSancion.Name = "labelAuditoriaCronogramasMotivoSancion";
            labelAuditoriaCronogramasMotivoSancion.Size = new Size(45, 15);
            labelAuditoriaCronogramasMotivoSancion.TabIndex = 2;
            labelAuditoriaCronogramasMotivoSancion.Text = "Motivo";
            // 
            // textBoxNombreChofer
            // 
            textBoxNombreChofer.Location = new Point(6, 69);
            textBoxNombreChofer.Name = "textBoxNombreChofer";
            textBoxNombreChofer.ReadOnly = true;
            textBoxNombreChofer.Size = new Size(225, 23);
            textBoxNombreChofer.TabIndex = 1;
            // 
            // labelAuditoriaCronogramasNombreChofer
            // 
            labelAuditoriaCronogramasNombreChofer.AutoSize = true;
            labelAuditoriaCronogramasNombreChofer.Location = new Point(6, 50);
            labelAuditoriaCronogramasNombreChofer.Name = "labelAuditoriaCronogramasNombreChofer";
            labelAuditoriaCronogramasNombreChofer.Size = new Size(106, 15);
            labelAuditoriaCronogramasNombreChofer.TabIndex = 0;
            labelAuditoriaCronogramasNombreChofer.Text = "Chofer a sancionar";
            // 
            // groupBoxAuditoriaCronogramasLlegadaReal
            //
            groupBoxAuditoriaCronogramasLlegadaReal.Controls.Add(numericUpDownToleranciaRetraso);
            groupBoxAuditoriaCronogramasLlegadaReal.Controls.Add(labelAuditoriaCronogramasToleranciaRetraso);
            groupBoxAuditoriaCronogramasLlegadaReal.Controls.Add(buttonAuditoriaCronogramasRegistrarLlegada);
            groupBoxAuditoriaCronogramasLlegadaReal.Controls.Add(dateTimePickerHoraLlegadaReal);
            groupBoxAuditoriaCronogramasLlegadaReal.Controls.Add(labelAuditoriaCronogramasHoraLlegadaReal);
            groupBoxAuditoriaCronogramasLlegadaReal.Location = new Point(12, 487);
            groupBoxAuditoriaCronogramasLlegadaReal.Name = "groupBoxAuditoriaCronogramasLlegadaReal";
            groupBoxAuditoriaCronogramasLlegadaReal.Size = new Size(804, 70);
            groupBoxAuditoriaCronogramasLlegadaReal.TabIndex = 2;
            groupBoxAuditoriaCronogramasLlegadaReal.TabStop = false;
            groupBoxAuditoriaCronogramasLlegadaReal.Text = "Llegada real";
            //
            // labelAuditoriaCronogramasHoraLlegadaReal
            //
            labelAuditoriaCronogramasHoraLlegadaReal.AutoSize = true;
            labelAuditoriaCronogramasHoraLlegadaReal.Location = new Point(6, 31);
            labelAuditoriaCronogramasHoraLlegadaReal.Name = "labelAuditoriaCronogramasHoraLlegadaReal";
            labelAuditoriaCronogramasHoraLlegadaReal.Size = new Size(116, 15);
            labelAuditoriaCronogramasHoraLlegadaReal.TabIndex = 0;
            labelAuditoriaCronogramasHoraLlegadaReal.Text = "Hora de llegada real";
            //
            // dateTimePickerHoraLlegadaReal
            //
            dateTimePickerHoraLlegadaReal.Format = DateTimePickerFormat.Time;
            dateTimePickerHoraLlegadaReal.Location = new Point(149, 27);
            dateTimePickerHoraLlegadaReal.Name = "dateTimePickerHoraLlegadaReal";
            dateTimePickerHoraLlegadaReal.ShowUpDown = true;
            dateTimePickerHoraLlegadaReal.Size = new Size(120, 23);
            dateTimePickerHoraLlegadaReal.TabIndex = 1;
            //
            // buttonAuditoriaCronogramasRegistrarLlegada
            //
            buttonAuditoriaCronogramasRegistrarLlegada.Location = new Point(289, 22);
            buttonAuditoriaCronogramasRegistrarLlegada.Name = "buttonAuditoriaCronogramasRegistrarLlegada";
            buttonAuditoriaCronogramasRegistrarLlegada.Size = new Size(160, 32);
            buttonAuditoriaCronogramasRegistrarLlegada.TabIndex = 2;
            buttonAuditoriaCronogramasRegistrarLlegada.Text = "Registrar llegada";
            buttonAuditoriaCronogramasRegistrarLlegada.UseVisualStyleBackColor = true;
            buttonAuditoriaCronogramasRegistrarLlegada.Click += buttonAuditoriaCronogramasRegistrarLlegada_Click;
            //
            // labelAuditoriaCronogramasToleranciaRetraso
            //
            labelAuditoriaCronogramasToleranciaRetraso.AutoSize = true;
            labelAuditoriaCronogramasToleranciaRetraso.Location = new Point(490, 31);
            labelAuditoriaCronogramasToleranciaRetraso.Name = "labelAuditoriaCronogramasToleranciaRetraso";
            labelAuditoriaCronogramasToleranciaRetraso.Size = new Size(187, 15);
            labelAuditoriaCronogramasToleranciaRetraso.TabIndex = 3;
            labelAuditoriaCronogramasToleranciaRetraso.Text = "Tolerancia de retraso (minutos)";
            //
            // numericUpDownToleranciaRetraso
            //
            numericUpDownToleranciaRetraso.Location = new Point(690, 27);
            numericUpDownToleranciaRetraso.Maximum = new decimal(new int[] { 1440, 0, 0, 0 });
            numericUpDownToleranciaRetraso.Name = "numericUpDownToleranciaRetraso";
            numericUpDownToleranciaRetraso.Size = new Size(80, 23);
            numericUpDownToleranciaRetraso.TabIndex = 4;
            numericUpDownToleranciaRetraso.Value = new decimal(new int[] { 20, 0, 0, 0 });
            numericUpDownToleranciaRetraso.ValueChanged += numericUpDownToleranciaRetraso_ValueChanged;
            //
            // AuditoriaSalidasUI
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1071, 570);
            ControlBox = false;
            Controls.Add(groupBoxAuditoriaCronogramasLlegadaReal);
            Controls.Add(groupBoxAuditoriaCronogramasAplicarSancion);
            Controls.Add(groupBoxAuditoriaCronogramasSalidas);
            FormBorderStyle = FormBorderStyle.None;
            Name = "AuditoriaSalidasUI";
            StartPosition = FormStartPosition.CenterParent;
            Text = "AuditoriaSalidasUI";
            WindowState = FormWindowState.Maximized;
            groupBoxAuditoriaCronogramasSalidas.ResumeLayout(false);
            groupBoxAuditoriaCronogramasSalidas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewSalidasCronogramaSeleccionado).EndInit();
            groupBoxAuditoriaCronogramasAplicarSancion.ResumeLayout(false);
            groupBoxAuditoriaCronogramasAplicarSancion.PerformLayout();
            groupBoxAuditoriaCronogramasLlegadaReal.ResumeLayout(false);
            groupBoxAuditoriaCronogramasLlegadaReal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownToleranciaRetraso).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBoxAuditoriaCronogramasSalidas;
        private Label labelAuditoriaCronogramasSalidas;
        private DataGridView dataGridViewSalidasCronogramaSeleccionado;
        private Label labelAuditoriaCronogramasListaCronogramas;
        private ListBox listBoxCronogramas;
        private GroupBox groupBoxAuditoriaCronogramasAplicarSancion;
        private TextBox textBoxMotivoSancion;
        private Label labelAuditoriaCronogramasMotivoSancion;
        private TextBox textBoxNombreChofer;
        private Label labelAuditoriaCronogramasNombreChofer;
        private Button buttonAuditoriaCronogramasConfirmar;
        private DateTimePicker dateTimePickerFechaSancion;
        private Label labelAuditoriaCronogramasFechaSancion;
        private GroupBox groupBoxAuditoriaCronogramasLlegadaReal;
        private Label labelAuditoriaCronogramasHoraLlegadaReal;
        private DateTimePicker dateTimePickerHoraLlegadaReal;
        private Button buttonAuditoriaCronogramasRegistrarLlegada;
        private Label labelAuditoriaCronogramasToleranciaRetraso;
        private NumericUpDown numericUpDownToleranciaRetraso;
    }
}