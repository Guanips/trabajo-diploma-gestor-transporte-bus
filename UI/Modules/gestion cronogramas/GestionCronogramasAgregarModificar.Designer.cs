namespace UI.Modules.gestion_cronogramas
{
    partial class GestionCronogramasAgregarModificar
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
            labelCronogramaAgregarModificarDescripcion = new Label();
            textBoxDescripcion = new TextBox();
            dateTimePickerFechaValidez = new DateTimePicker();
            labelCronogramaAgregarModificarFechaValidez = new Label();
            maskedTextBoxHoraInicio = new MaskedTextBox();
            labelCronogramaAgregarModificarHoraInicio = new Label();
            labelCronogramaAgregarModificarHoraFin = new Label();
            maskedTextBoxHoraFin = new MaskedTextBox();
            labelCronogramaAgregarModificarFrecuenciaMin = new Label();
            numericUpDownFrecuencia = new NumericUpDown();
            numericUpDownTiempoDescanso = new NumericUpDown();
            labelCronogramaAgregarModificarTiempoDescanso = new Label();
            buttonCronogramaAgregarModificarConfirmar = new Button();
            groupBoxCronogramaAltaModificarSeleccionRuta = new GroupBox();
            listBoxRutas = new ListBox();
            ((System.ComponentModel.ISupportInitialize)numericUpDownFrecuencia).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownTiempoDescanso).BeginInit();
            groupBoxCronogramaAltaModificarSeleccionRuta.SuspendLayout();
            SuspendLayout();
            // 
            // labelCronogramaAgregarModificarDescripcion
            // 
            labelCronogramaAgregarModificarDescripcion.AutoSize = true;
            labelCronogramaAgregarModificarDescripcion.Location = new Point(15, 281);
            labelCronogramaAgregarModificarDescripcion.Name = "labelCronogramaAgregarModificarDescripcion";
            labelCronogramaAgregarModificarDescripcion.Size = new Size(69, 15);
            labelCronogramaAgregarModificarDescripcion.TabIndex = 2;
            labelCronogramaAgregarModificarDescripcion.Text = "Descripcion";
            // 
            // textBoxDescripcion
            // 
            textBoxDescripcion.Location = new Point(15, 299);
            textBoxDescripcion.Multiline = true;
            textBoxDescripcion.Name = "textBoxDescripcion";
            textBoxDescripcion.Size = new Size(232, 128);
            textBoxDescripcion.TabIndex = 3;
            // 
            // dateTimePickerFechaValidez
            // 
            dateTimePickerFechaValidez.Location = new Point(15, 43);
            dateTimePickerFechaValidez.Name = "dateTimePickerFechaValidez";
            dateTimePickerFechaValidez.Size = new Size(232, 23);
            dateTimePickerFechaValidez.TabIndex = 4;
            // 
            // labelCronogramaAgregarModificarFechaValidez
            // 
            labelCronogramaAgregarModificarFechaValidez.AutoSize = true;
            labelCronogramaAgregarModificarFechaValidez.Location = new Point(15, 25);
            labelCronogramaAgregarModificarFechaValidez.Name = "labelCronogramaAgregarModificarFechaValidez";
            labelCronogramaAgregarModificarFechaValidez.Size = new Size(77, 15);
            labelCronogramaAgregarModificarFechaValidez.TabIndex = 5;
            labelCronogramaAgregarModificarFechaValidez.Text = "Fecha validez";
            // 
            // maskedTextBoxHoraInicio
            // 
            maskedTextBoxHoraInicio.Location = new Point(15, 93);
            maskedTextBoxHoraInicio.Mask = "00:00";
            maskedTextBoxHoraInicio.Name = "maskedTextBoxHoraInicio";
            maskedTextBoxHoraInicio.Size = new Size(232, 23);
            maskedTextBoxHoraInicio.TabIndex = 6;
            maskedTextBoxHoraInicio.MaskInputRejected += maskedTextBoxHoraInicio_MaskInputRejected;
            // 
            // labelCronogramaAgregarModificarHoraInicio
            // 
            labelCronogramaAgregarModificarHoraInicio.AutoSize = true;
            labelCronogramaAgregarModificarHoraInicio.Location = new Point(15, 75);
            labelCronogramaAgregarModificarHoraInicio.Name = "labelCronogramaAgregarModificarHoraInicio";
            labelCronogramaAgregarModificarHoraInicio.Size = new Size(65, 15);
            labelCronogramaAgregarModificarHoraInicio.TabIndex = 7;
            labelCronogramaAgregarModificarHoraInicio.Text = "Hora inicio";
            // 
            // labelCronogramaAgregarModificarHoraFin
            // 
            labelCronogramaAgregarModificarHoraFin.AutoSize = true;
            labelCronogramaAgregarModificarHoraFin.Location = new Point(15, 125);
            labelCronogramaAgregarModificarHoraFin.Name = "labelCronogramaAgregarModificarHoraFin";
            labelCronogramaAgregarModificarHoraFin.Size = new Size(50, 15);
            labelCronogramaAgregarModificarHoraFin.TabIndex = 9;
            labelCronogramaAgregarModificarHoraFin.Text = "Hora fin";
            // 
            // maskedTextBoxHoraFin
            // 
            maskedTextBoxHoraFin.Location = new Point(15, 143);
            maskedTextBoxHoraFin.Mask = "00:00";
            maskedTextBoxHoraFin.Name = "maskedTextBoxHoraFin";
            maskedTextBoxHoraFin.Size = new Size(232, 23);
            maskedTextBoxHoraFin.TabIndex = 8;
            maskedTextBoxHoraFin.MaskInputRejected += maskedTextBoxHoraFin_MaskInputRejected;
            // 
            // labelCronogramaAgregarModificarFrecuenciaMin
            // 
            labelCronogramaAgregarModificarFrecuenciaMin.AutoSize = true;
            labelCronogramaAgregarModificarFrecuenciaMin.Location = new Point(15, 178);
            labelCronogramaAgregarModificarFrecuenciaMin.Name = "labelCronogramaAgregarModificarFrecuenciaMin";
            labelCronogramaAgregarModificarFrecuenciaMin.Size = new Size(119, 15);
            labelCronogramaAgregarModificarFrecuenciaMin.TabIndex = 10;
            labelCronogramaAgregarModificarFrecuenciaMin.Text = "Frecuencia (minutos)";
            // 
            // numericUpDownFrecuencia
            // 
            numericUpDownFrecuencia.Location = new Point(15, 196);
            numericUpDownFrecuencia.Maximum = new decimal(new int[] { 1440, 0, 0, 0 });
            numericUpDownFrecuencia.Name = "numericUpDownFrecuencia";
            numericUpDownFrecuencia.Size = new Size(232, 23);
            numericUpDownFrecuencia.TabIndex = 11;
            // 
            // numericUpDownTiempoDescanso
            // 
            numericUpDownTiempoDescanso.Location = new Point(15, 246);
            numericUpDownTiempoDescanso.Maximum = new decimal(new int[] { 1440, 0, 0, 0 });
            numericUpDownTiempoDescanso.Name = "numericUpDownTiempoDescanso";
            numericUpDownTiempoDescanso.Size = new Size(232, 23);
            numericUpDownTiempoDescanso.TabIndex = 13;
            // 
            // labelCronogramaAgregarModificarTiempoDescanso
            // 
            labelCronogramaAgregarModificarTiempoDescanso.AutoSize = true;
            labelCronogramaAgregarModificarTiempoDescanso.Location = new Point(15, 228);
            labelCronogramaAgregarModificarTiempoDescanso.Name = "labelCronogramaAgregarModificarTiempoDescanso";
            labelCronogramaAgregarModificarTiempoDescanso.Size = new Size(239, 15);
            labelCronogramaAgregarModificarTiempoDescanso.TabIndex = 12;
            labelCronogramaAgregarModificarTiempoDescanso.Text = "Tiempo de descanso entre salidas (minutos)";
            // 
            // buttonCronogramaAgregarModificarConfirmar
            // 
            buttonCronogramaAgregarModificarConfirmar.Location = new Point(15, 441);
            buttonCronogramaAgregarModificarConfirmar.Name = "buttonCronogramaAgregarModificarConfirmar";
            buttonCronogramaAgregarModificarConfirmar.Size = new Size(232, 38);
            buttonCronogramaAgregarModificarConfirmar.TabIndex = 14;
            buttonCronogramaAgregarModificarConfirmar.Text = "Confirmar";
            buttonCronogramaAgregarModificarConfirmar.UseVisualStyleBackColor = true;
            buttonCronogramaAgregarModificarConfirmar.Click += buttonCronogramaAgregarModificarConfirmar_Click;
            // 
            // groupBoxCronogramaAltaModificarSeleccionRuta
            // 
            groupBoxCronogramaAltaModificarSeleccionRuta.Controls.Add(listBoxRutas);
            groupBoxCronogramaAltaModificarSeleccionRuta.Location = new Point(267, 25);
            groupBoxCronogramaAltaModificarSeleccionRuta.Name = "groupBoxCronogramaAltaModificarSeleccionRuta";
            groupBoxCronogramaAltaModificarSeleccionRuta.Size = new Size(252, 453);
            groupBoxCronogramaAltaModificarSeleccionRuta.TabIndex = 15;
            groupBoxCronogramaAltaModificarSeleccionRuta.TabStop = false;
            groupBoxCronogramaAltaModificarSeleccionRuta.Text = "Selección de ruta";
            // 
            // listBoxRutas
            // 
            listBoxRutas.FormattingEnabled = true;
            listBoxRutas.ItemHeight = 15;
            listBoxRutas.Location = new Point(11, 30);
            listBoxRutas.Name = "listBoxRutas";
            listBoxRutas.Size = new Size(235, 409);
            listBoxRutas.TabIndex = 0;
            // 
            // GestionCronogramasAgregarModificar
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(531, 505);
            Controls.Add(groupBoxCronogramaAltaModificarSeleccionRuta);
            Controls.Add(buttonCronogramaAgregarModificarConfirmar);
            Controls.Add(numericUpDownTiempoDescanso);
            Controls.Add(labelCronogramaAgregarModificarTiempoDescanso);
            Controls.Add(numericUpDownFrecuencia);
            Controls.Add(labelCronogramaAgregarModificarFrecuenciaMin);
            Controls.Add(labelCronogramaAgregarModificarHoraFin);
            Controls.Add(maskedTextBoxHoraFin);
            Controls.Add(labelCronogramaAgregarModificarHoraInicio);
            Controls.Add(maskedTextBoxHoraInicio);
            Controls.Add(labelCronogramaAgregarModificarFechaValidez);
            Controls.Add(dateTimePickerFechaValidez);
            Controls.Add(textBoxDescripcion);
            Controls.Add(labelCronogramaAgregarModificarDescripcion);
            Name = "GestionCronogramasAgregarModificar";
            Text = "Gestion cronograma";
            ((System.ComponentModel.ISupportInitialize)numericUpDownFrecuencia).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownTiempoDescanso).EndInit();
            groupBoxCronogramaAltaModificarSeleccionRuta.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelCronogramaAgregarModificarDescripcion;
        private TextBox textBoxDescripcion;
        private DateTimePicker dateTimePickerFechaValidez;
        private Label labelCronogramaAgregarModificarFechaValidez;
        private MaskedTextBox maskedTextBoxHoraInicio;
        private Label labelCronogramaAgregarModificarHoraInicio;
        private Label labelCronogramaAgregarModificarHoraFin;
        private MaskedTextBox maskedTextBoxHoraFin;
        private Label labelCronogramaAgregarModificarFrecuenciaMin;
        private NumericUpDown numericUpDownFrecuencia;
        private NumericUpDown numericUpDownTiempoDescanso;
        private Label labelCronogramaAgregarModificarTiempoDescanso;
        private Button buttonCronogramaAgregarModificarConfirmar;
        private GroupBox groupBoxCronogramaAltaModificarSeleccionRuta;
        private ListBox listBoxRutas;
    }
}