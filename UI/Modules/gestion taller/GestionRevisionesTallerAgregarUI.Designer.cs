namespace UI.Modules.gestion_taller
{
    partial class GestionRevisionesTallerAgregarUI
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
            labelGestionRevisionAgregarInterno = new Label();
            comboBoxInternos = new ComboBox();
            labelGestionRevisionAgregarFecha = new Label();
            dateTimePickerFecha = new DateTimePicker();
            labelGestionRevisionAgregarDescripcion = new Label();
            textBoxDescripcion = new TextBox();
            checkBoxGestionRevisionAgregarRequiereReparacion = new CheckBox();
            buttonGestionRevisionAgregarConfirmar = new Button();
            SuspendLayout();
            // 
            // labelGestionRevisionAgregarInterno
            // 
            labelGestionRevisionAgregarInterno.AutoSize = true;
            labelGestionRevisionAgregarInterno.Location = new Point(12, 11);
            labelGestionRevisionAgregarInterno.Name = "labelGestionRevisionAgregarInterno";
            labelGestionRevisionAgregarInterno.Size = new Size(45, 15);
            labelGestionRevisionAgregarInterno.TabIndex = 0;
            labelGestionRevisionAgregarInterno.Text = "Interno";
            // 
            // comboBoxInternos
            // 
            comboBoxInternos.FormattingEnabled = true;
            comboBoxInternos.Location = new Point(12, 31);
            comboBoxInternos.Name = "comboBoxInternos";
            comboBoxInternos.Size = new Size(252, 23);
            comboBoxInternos.TabIndex = 1;
            // 
            // labelGestionRevisionAgregarFecha
            // 
            labelGestionRevisionAgregarFecha.AutoSize = true;
            labelGestionRevisionAgregarFecha.Location = new Point(12, 59);
            labelGestionRevisionAgregarFecha.Name = "labelGestionRevisionAgregarFecha";
            labelGestionRevisionAgregarFecha.Size = new Size(38, 15);
            labelGestionRevisionAgregarFecha.TabIndex = 2;
            labelGestionRevisionAgregarFecha.Text = "Fecha";
            // 
            // dateTimePickerFecha
            // 
            dateTimePickerFecha.Location = new Point(12, 79);
            dateTimePickerFecha.Name = "dateTimePickerFecha";
            dateTimePickerFecha.Size = new Size(252, 23);
            dateTimePickerFecha.TabIndex = 3;
            // 
            // labelGestionRevisionAgregarDescripcion
            // 
            labelGestionRevisionAgregarDescripcion.AutoSize = true;
            labelGestionRevisionAgregarDescripcion.Location = new Point(12, 107);
            labelGestionRevisionAgregarDescripcion.Name = "labelGestionRevisionAgregarDescripcion";
            labelGestionRevisionAgregarDescripcion.Size = new Size(69, 15);
            labelGestionRevisionAgregarDescripcion.TabIndex = 4;
            labelGestionRevisionAgregarDescripcion.Text = "Descripción";
            // 
            // textBoxDescripcion
            // 
            textBoxDescripcion.Location = new Point(12, 127);
            textBoxDescripcion.Multiline = true;
            textBoxDescripcion.Name = "textBoxDescripcion";
            textBoxDescripcion.Size = new Size(252, 161);
            textBoxDescripcion.TabIndex = 5;
            // 
            // checkBoxGestionRevisionAgregarRequiereReparacion
            // 
            checkBoxGestionRevisionAgregarRequiereReparacion.AutoSize = true;
            checkBoxGestionRevisionAgregarRequiereReparacion.Location = new Point(12, 293);
            checkBoxGestionRevisionAgregarRequiereReparacion.Name = "checkBoxGestionRevisionAgregarRequiereReparacion";
            checkBoxGestionRevisionAgregarRequiereReparacion.Size = new Size(131, 19);
            checkBoxGestionRevisionAgregarRequiereReparacion.TabIndex = 6;
            checkBoxGestionRevisionAgregarRequiereReparacion.Text = "Requiere reparación";
            checkBoxGestionRevisionAgregarRequiereReparacion.UseVisualStyleBackColor = true;
            // 
            // buttonGestionRevisionAgregarConfirmar
            // 
            buttonGestionRevisionAgregarConfirmar.Location = new Point(12, 317);
            buttonGestionRevisionAgregarConfirmar.Name = "buttonGestionRevisionAgregarConfirmar";
            buttonGestionRevisionAgregarConfirmar.Size = new Size(252, 31);
            buttonGestionRevisionAgregarConfirmar.TabIndex = 7;
            buttonGestionRevisionAgregarConfirmar.Text = "Confirmar";
            buttonGestionRevisionAgregarConfirmar.UseVisualStyleBackColor = true;
            buttonGestionRevisionAgregarConfirmar.Click += buttonGestionRevisionAgregarConfirmar_Click;
            // 
            // GestionRevisionesTallerAgregar
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(276, 359);
            Controls.Add(buttonGestionRevisionAgregarConfirmar);
            Controls.Add(checkBoxGestionRevisionAgregarRequiereReparacion);
            Controls.Add(textBoxDescripcion);
            Controls.Add(labelGestionRevisionAgregarDescripcion);
            Controls.Add(dateTimePickerFecha);
            Controls.Add(labelGestionRevisionAgregarFecha);
            Controls.Add(comboBoxInternos);
            Controls.Add(labelGestionRevisionAgregarInterno);
            Name = "GestionRevisionesTallerAgregar";
            Text = "Agregar revisión";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelGestionRevisionAgregarInterno;
        private ComboBox comboBoxInternos;
        private Label labelGestionRevisionAgregarFecha;
        private DateTimePicker dateTimePickerFecha;
        private Label labelGestionRevisionAgregarDescripcion;
        private TextBox textBoxDescripcion;
        private CheckBox checkBoxGestionRevisionAgregarRequiereReparacion;
        private Button buttonGestionRevisionAgregarConfirmar;
    }
}