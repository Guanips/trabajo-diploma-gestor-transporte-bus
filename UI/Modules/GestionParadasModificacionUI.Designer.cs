namespace UI.Modules
{
    partial class GestionParadasModificacionUI
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
            labelParadaModificarTitle = new Label();
            labelParadaModificarDescripcion = new Label();
            label1 = new Label();
            textBoxParadaModificarDireccion = new TextBox();
            textBoxParadaModificarDescripcion = new TextBox();
            label2 = new Label();
            textBoxParadaModificarLocalidad = new TextBox();
            buttonParadaModificarConfirmar = new Button();
            SuspendLayout();
            // 
            // labelParadaModificarTitle
            // 
            labelParadaModificarTitle.AutoSize = true;
            labelParadaModificarTitle.Font = new Font("Segoe UI", 14F);
            labelParadaModificarTitle.Location = new Point(29, 9);
            labelParadaModificarTitle.Name = "labelParadaModificarTitle";
            labelParadaModificarTitle.Size = new Size(233, 25);
            labelParadaModificarTitle.TabIndex = 0;
            labelParadaModificarTitle.Text = "Ingrese los nuevos valores";
            // 
            // labelParadaModificarDescripcion
            // 
            labelParadaModificarDescripcion.AutoSize = true;
            labelParadaModificarDescripcion.Location = new Point(12, 42);
            labelParadaModificarDescripcion.Name = "labelParadaModificarDescripcion";
            labelParadaModificarDescripcion.Size = new Size(69, 15);
            labelParadaModificarDescripcion.TabIndex = 3;
            labelParadaModificarDescripcion.Text = "Descripcion";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 248);
            label1.Name = "label1";
            label1.Size = new Size(57, 15);
            label1.TabIndex = 1;
            label1.Text = "Direccion";
            // 
            // textBoxParadaModificarDireccion
            // 
            textBoxParadaModificarDireccion.Location = new Point(12, 271);
            textBoxParadaModificarDireccion.Name = "textBoxParadaModificarDireccion";
            textBoxParadaModificarDireccion.Size = new Size(261, 23);
            textBoxParadaModificarDireccion.TabIndex = 2;
            // 
            // textBoxParadaModificarDescripcion
            // 
            textBoxParadaModificarDescripcion.Location = new Point(12, 65);
            textBoxParadaModificarDescripcion.Multiline = true;
            textBoxParadaModificarDescripcion.Name = "textBoxParadaModificarDescripcion";
            textBoxParadaModificarDescripcion.ScrollBars = ScrollBars.Vertical;
            textBoxParadaModificarDescripcion.Size = new Size(261, 121);
            textBoxParadaModificarDescripcion.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 194);
            label2.Name = "label2";
            label2.Size = new Size(58, 15);
            label2.TabIndex = 5;
            label2.Text = "Localidad";
            // 
            // textBoxParadaModificarLocalidad
            // 
            textBoxParadaModificarLocalidad.Location = new Point(12, 217);
            textBoxParadaModificarLocalidad.Name = "textBoxParadaModificarLocalidad";
            textBoxParadaModificarLocalidad.Size = new Size(261, 23);
            textBoxParadaModificarLocalidad.TabIndex = 6;
            // 
            // buttonParadaModificarConfirmar
            // 
            buttonParadaModificarConfirmar.Location = new Point(12, 302);
            buttonParadaModificarConfirmar.Name = "buttonParadaModificarConfirmar";
            buttonParadaModificarConfirmar.Size = new Size(261, 44);
            buttonParadaModificarConfirmar.TabIndex = 7;
            buttonParadaModificarConfirmar.Text = "Confirmar";
            buttonParadaModificarConfirmar.UseVisualStyleBackColor = true;
            buttonParadaModificarConfirmar.Click += buttonParadaModificarConfirmar_Click;
            // 
            // GestionParadasModificacionUI
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(291, 360);
            Controls.Add(buttonParadaModificarConfirmar);
            Controls.Add(textBoxParadaModificarLocalidad);
            Controls.Add(label2);
            Controls.Add(textBoxParadaModificarDescripcion);
            Controls.Add(labelParadaModificarDescripcion);
            Controls.Add(textBoxParadaModificarDireccion);
            Controls.Add(label1);
            Controls.Add(labelParadaModificarTitle);
            Name = "GestionParadasModificacionUI";
            Text = "Modificar parada";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelParadaModificarTitle;
        private Label labelParadaModificarDescripcion;
        private Label label1;
        private TextBox textBoxParadaModificarDireccion;
        private TextBox textBoxParadaModificarDescripcion;
        private Label label2;
        private TextBox textBoxParadaModificarLocalidad;
        private Button buttonParadaModificarConfirmar;
    }
}