namespace UI.Modules.gestion_choferes
{
    partial class GestionChoferAgregarModificarUI
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
            labelChoferAltaModificarNumChofer = new Label();
            textBoxChoferNum = new TextBox();
            buttonChoferAltaModificarConfirmar = new Button();
            textBoxChoferDni = new TextBox();
            labelChoferAltaModificarDni = new Label();
            textBoxChoferNombreCompleto = new TextBox();
            labelChoferAltaModificacionNombre = new Label();
            checkBoxChoferAltaModificarActivo = new CheckBox();
            SuspendLayout();
            // 
            // labelChoferAltaModificarNumChofer
            // 
            labelChoferAltaModificarNumChofer.AutoSize = true;
            labelChoferAltaModificarNumChofer.Location = new Point(18, 10);
            labelChoferAltaModificarNumChofer.Name = "labelChoferAltaModificarNumChofer";
            labelChoferAltaModificarNumChofer.Size = new Size(104, 15);
            labelChoferAltaModificarNumChofer.TabIndex = 0;
            labelChoferAltaModificarNumChofer.Text = "Numero de chofer";
            // 
            // textBoxChoferNum
            // 
            textBoxChoferNum.Location = new Point(18, 34);
            textBoxChoferNum.MaxLength = 20;
            textBoxChoferNum.Name = "textBoxChoferNum";
            textBoxChoferNum.Size = new Size(257, 23);
            textBoxChoferNum.TabIndex = 1;
            // 
            // buttonChoferAltaModificarConfirmar
            // 
            buttonChoferAltaModificarConfirmar.Location = new Point(18, 206);
            buttonChoferAltaModificarConfirmar.Name = "buttonChoferAltaModificarConfirmar";
            buttonChoferAltaModificarConfirmar.Size = new Size(257, 44);
            buttonChoferAltaModificarConfirmar.TabIndex = 10;
            buttonChoferAltaModificarConfirmar.Text = "Confirmar";
            buttonChoferAltaModificarConfirmar.UseVisualStyleBackColor = true;
            buttonChoferAltaModificarConfirmar.Click += buttonRutaAltaModificarConfirmar_Click;
            // 
            // textBoxChoferDni
            // 
            textBoxChoferDni.Location = new Point(18, 90);
            textBoxChoferDni.MaxLength = 20;
            textBoxChoferDni.Name = "textBoxChoferDni";
            textBoxChoferDni.Size = new Size(257, 23);
            textBoxChoferDni.TabIndex = 12;
            // 
            // labelChoferAltaModificarDni
            // 
            labelChoferAltaModificarDni.AutoSize = true;
            labelChoferAltaModificarDni.Location = new Point(18, 66);
            labelChoferAltaModificarDni.Name = "labelChoferAltaModificarDni";
            labelChoferAltaModificarDni.Size = new Size(27, 15);
            labelChoferAltaModificarDni.TabIndex = 11;
            labelChoferAltaModificarDni.Text = "DNI";
            // 
            // textBoxChoferNombreCompleto
            // 
            textBoxChoferNombreCompleto.Location = new Point(18, 146);
            textBoxChoferNombreCompleto.MaxLength = 20;
            textBoxChoferNombreCompleto.Name = "textBoxChoferNombreCompleto";
            textBoxChoferNombreCompleto.Size = new Size(257, 23);
            textBoxChoferNombreCompleto.TabIndex = 14;
            // 
            // labelChoferAltaModificacionNombre
            // 
            labelChoferAltaModificacionNombre.AutoSize = true;
            labelChoferAltaModificacionNombre.Location = new Point(18, 122);
            labelChoferAltaModificacionNombre.Name = "labelChoferAltaModificacionNombre";
            labelChoferAltaModificacionNombre.Size = new Size(105, 15);
            labelChoferAltaModificacionNombre.TabIndex = 13;
            labelChoferAltaModificacionNombre.Text = "Nombre completo";
            // 
            // checkBoxChoferAltaModificarActivo
            // 
            checkBoxChoferAltaModificarActivo.AutoSize = true;
            checkBoxChoferAltaModificarActivo.Location = new Point(18, 178);
            checkBoxChoferAltaModificarActivo.Name = "checkBoxChoferAltaModificarActivo";
            checkBoxChoferAltaModificarActivo.Size = new Size(92, 19);
            checkBoxChoferAltaModificarActivo.TabIndex = 15;
            checkBoxChoferAltaModificarActivo.Text = "¿Está activo?";
            checkBoxChoferAltaModificarActivo.UseVisualStyleBackColor = true;
            // 
            // GestionChoferAgregarModificarUI
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(293, 272);
            Controls.Add(checkBoxChoferAltaModificarActivo);
            Controls.Add(textBoxChoferNombreCompleto);
            Controls.Add(labelChoferAltaModificacionNombre);
            Controls.Add(textBoxChoferDni);
            Controls.Add(labelChoferAltaModificarDni);
            Controls.Add(buttonChoferAltaModificarConfirmar);
            Controls.Add(textBoxChoferNum);
            Controls.Add(labelChoferAltaModificarNumChofer);
            Name = "GestionChoferAgregarModificarUI";
            Text = "Gestion chofer";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelChoferAltaModificarNumChofer;
        private TextBox textBoxChoferNum;
        private Label labelRutaAltaModificarDescripcion;
        private Button buttonChoferAltaModificarConfirmar;
        private TextBox textBoxChoferDni;
        private Label labelChoferAltaModificarDni;
        private TextBox textBoxChoferNombreCompleto;
        private Label labelChoferAltaModificacionNombre;
        private CheckBox checkBoxChoferAltaModificarActivo;
    }
}