namespace UI.Modules.gestion_internos
{
    partial class GestionInternoAgregarModificarUI
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
            labelInternoAltaModificarNum = new Label();
            textBoxInternoNum = new TextBox();
            textBoxInternoPatente = new TextBox();
            labelInternoAltaModificarPatente = new Label();
            textBoxInternoModelo = new TextBox();
            labelInternoAltaModificarModelo = new Label();
            dateTimePickerInternoFechaIncorporacion = new DateTimePicker();
            labelInternoAltaModificarFechaIncorporacion = new Label();
            checkBoxInternoAltaModificarDisponible = new CheckBox();
            buttonInternoAltaModificarConfirmar = new Button();
            SuspendLayout();
            // 
            // labelInternoAltaModificarNum
            // 
            labelInternoAltaModificarNum.AutoSize = true;
            labelInternoAltaModificarNum.Location = new Point(18, 10);
            labelInternoAltaModificarNum.Name = "labelInternoAltaModificarNum";
            labelInternoAltaModificarNum.Size = new Size(108, 15);
            labelInternoAltaModificarNum.TabIndex = 0;
            labelInternoAltaModificarNum.Text = "Numero de interno";
            // 
            // textBoxInternoNum
            // 
            textBoxInternoNum.Location = new Point(18, 34);
            textBoxInternoNum.MaxLength = 20;
            textBoxInternoNum.Name = "textBoxInternoNum";
            textBoxInternoNum.ReadOnly = true;
            textBoxInternoNum.Size = new Size(257, 23);
            textBoxInternoNum.TabIndex = 1;
            // 
            // textBoxInternoPatente
            // 
            textBoxInternoPatente.Location = new Point(18, 90);
            textBoxInternoPatente.MaxLength = 20;
            textBoxInternoPatente.Name = "textBoxInternoPatente";
            textBoxInternoPatente.Size = new Size(257, 23);
            textBoxInternoPatente.TabIndex = 3;
            // 
            // labelInternoAltaModificarPatente
            // 
            labelInternoAltaModificarPatente.AutoSize = true;
            labelInternoAltaModificarPatente.Location = new Point(18, 66);
            labelInternoAltaModificarPatente.Name = "labelInternoAltaModificarPatente";
            labelInternoAltaModificarPatente.Size = new Size(47, 15);
            labelInternoAltaModificarPatente.TabIndex = 2;
            labelInternoAltaModificarPatente.Text = "Patente";
            // 
            // textBoxInternoModelo
            // 
            textBoxInternoModelo.Location = new Point(18, 146);
            textBoxInternoModelo.MaxLength = 20;
            textBoxInternoModelo.Name = "textBoxInternoModelo";
            textBoxInternoModelo.Size = new Size(257, 23);
            textBoxInternoModelo.TabIndex = 5;
            // 
            // labelInternoAltaModificarModelo
            // 
            labelInternoAltaModificarModelo.AutoSize = true;
            labelInternoAltaModificarModelo.Location = new Point(18, 122);
            labelInternoAltaModificarModelo.Name = "labelInternoAltaModificarModelo";
            labelInternoAltaModificarModelo.Size = new Size(48, 15);
            labelInternoAltaModificarModelo.TabIndex = 4;
            labelInternoAltaModificarModelo.Text = "Modelo";
            // 
            // dateTimePickerInternoFechaIncorporacion
            // 
            dateTimePickerInternoFechaIncorporacion.Format = DateTimePickerFormat.Short;
            dateTimePickerInternoFechaIncorporacion.Location = new Point(18, 202);
            dateTimePickerInternoFechaIncorporacion.Name = "dateTimePickerInternoFechaIncorporacion";
            dateTimePickerInternoFechaIncorporacion.Size = new Size(257, 23);
            dateTimePickerInternoFechaIncorporacion.TabIndex = 7;
            // 
            // labelInternoAltaModificarFechaIncorporacion
            // 
            labelInternoAltaModificarFechaIncorporacion.AutoSize = true;
            labelInternoAltaModificarFechaIncorporacion.Location = new Point(18, 178);
            labelInternoAltaModificarFechaIncorporacion.Name = "labelInternoAltaModificarFechaIncorporacion";
            labelInternoAltaModificarFechaIncorporacion.Size = new Size(131, 15);
            labelInternoAltaModificarFechaIncorporacion.TabIndex = 6;
            labelInternoAltaModificarFechaIncorporacion.Text = "Fecha de incorporacion";
            // 
            // checkBoxInternoAltaModificarDisponible
            // 
            checkBoxInternoAltaModificarDisponible.AutoSize = true;
            checkBoxInternoAltaModificarDisponible.Location = new Point(18, 234);
            checkBoxInternoAltaModificarDisponible.Name = "checkBoxInternoAltaModificarDisponible";
            checkBoxInternoAltaModificarDisponible.Size = new Size(115, 19);
            checkBoxInternoAltaModificarDisponible.TabIndex = 8;
            checkBoxInternoAltaModificarDisponible.Text = "¿Está disponible?";
            checkBoxInternoAltaModificarDisponible.UseVisualStyleBackColor = true;
            // 
            // buttonInternoAltaModificarConfirmar
            // 
            buttonInternoAltaModificarConfirmar.Location = new Point(18, 262);
            buttonInternoAltaModificarConfirmar.Name = "buttonInternoAltaModificarConfirmar";
            buttonInternoAltaModificarConfirmar.Size = new Size(257, 44);
            buttonInternoAltaModificarConfirmar.TabIndex = 9;
            buttonInternoAltaModificarConfirmar.Text = "Confirmar";
            buttonInternoAltaModificarConfirmar.UseVisualStyleBackColor = true;
            buttonInternoAltaModificarConfirmar.Click += buttonInternoAltaModificarConfirmar_Click;
            // 
            // GestionInternoAgregarModificarUI
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(293, 328);
            Controls.Add(buttonInternoAltaModificarConfirmar);
            Controls.Add(checkBoxInternoAltaModificarDisponible);
            Controls.Add(dateTimePickerInternoFechaIncorporacion);
            Controls.Add(labelInternoAltaModificarFechaIncorporacion);
            Controls.Add(textBoxInternoModelo);
            Controls.Add(labelInternoAltaModificarModelo);
            Controls.Add(textBoxInternoPatente);
            Controls.Add(labelInternoAltaModificarPatente);
            Controls.Add(textBoxInternoNum);
            Controls.Add(labelInternoAltaModificarNum);
            Name = "GestionInternoAgregarModificarUI";
            Text = "Gestion interno";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelInternoAltaModificarNum;
        private TextBox textBoxInternoNum;
        private TextBox textBoxInternoPatente;
        private Label labelInternoAltaModificarPatente;
        private TextBox textBoxInternoModelo;
        private Label labelInternoAltaModificarModelo;
        private DateTimePicker dateTimePickerInternoFechaIncorporacion;
        private Label labelInternoAltaModificarFechaIncorporacion;
        private CheckBox checkBoxInternoAltaModificarDisponible;
        private Button buttonInternoAltaModificarConfirmar;
    }
}