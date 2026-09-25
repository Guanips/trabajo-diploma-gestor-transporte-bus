namespace UI.Modules.gestion_rutas
{
    partial class GestionRutaAgregarModificarUI
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
            labelRutaAltaModificarId = new Label();
            textBoxRutaID = new TextBox();
            textBoxRutaDescripcion = new TextBox();
            labelRutaAltaModificarDescripcion = new Label();
            labelRutaAltaModificarDistancia = new Label();
            labelRutaAltaModificarTiempoEstimado = new Label();
            radioButtonRutaAltaModificarSentidoIda = new RadioButton();
            radioButtonRutaAltaModificarSentidoVuelta = new RadioButton();
            buttonRutaAltaModificarConfirmar = new Button();
            labelRutaAltaModificarSentido = new Label();
            numericUpDownRutaAltaModificarDistanciaTotalKM = new NumericUpDown();
            numericUpDownRutaAltaModificarTiempoEstimado = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)numericUpDownRutaAltaModificarDistanciaTotalKM).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownRutaAltaModificarTiempoEstimado).BeginInit();
            SuspendLayout();
            // 
            // labelRutaAltaModificarId
            // 
            labelRutaAltaModificarId.AutoSize = true;
            labelRutaAltaModificarId.Location = new Point(18, 10);
            labelRutaAltaModificarId.Name = "labelRutaAltaModificarId";
            labelRutaAltaModificarId.Size = new Size(18, 15);
            labelRutaAltaModificarId.TabIndex = 0;
            labelRutaAltaModificarId.Text = "ID";
            // 
            // textBoxRutaID
            // 
            textBoxRutaID.Location = new Point(18, 31);
            textBoxRutaID.MaxLength = 20;
            textBoxRutaID.Name = "textBoxRutaID";
            textBoxRutaID.Size = new Size(257, 23);
            textBoxRutaID.TabIndex = 1;
            // 
            // textBoxRutaDescripcion
            // 
            textBoxRutaDescripcion.Location = new Point(18, 81);
            textBoxRutaDescripcion.MaxLength = 255;
            textBoxRutaDescripcion.Multiline = true;
            textBoxRutaDescripcion.Name = "textBoxRutaDescripcion";
            textBoxRutaDescripcion.ScrollBars = ScrollBars.Vertical;
            textBoxRutaDescripcion.Size = new Size(257, 109);
            textBoxRutaDescripcion.TabIndex = 3;
            // 
            // labelRutaAltaModificarDescripcion
            // 
            labelRutaAltaModificarDescripcion.AutoSize = true;
            labelRutaAltaModificarDescripcion.Location = new Point(18, 60);
            labelRutaAltaModificarDescripcion.Name = "labelRutaAltaModificarDescripcion";
            labelRutaAltaModificarDescripcion.Size = new Size(69, 15);
            labelRutaAltaModificarDescripcion.TabIndex = 2;
            labelRutaAltaModificarDescripcion.Text = "Descripción";
            // 
            // labelRutaAltaModificarDistancia
            // 
            labelRutaAltaModificarDistancia.AutoSize = true;
            labelRutaAltaModificarDistancia.Location = new Point(18, 196);
            labelRutaAltaModificarDistancia.Name = "labelRutaAltaModificarDistancia";
            labelRutaAltaModificarDistancia.Size = new Size(188, 15);
            labelRutaAltaModificarDistancia.TabIndex = 4;
            labelRutaAltaModificarDistancia.Text = "Largo estimado del recorrido (KM)";
            // 
            // labelRutaAltaModificarTiempoEstimado
            // 
            labelRutaAltaModificarTiempoEstimado.AutoSize = true;
            labelRutaAltaModificarTiempoEstimado.Location = new Point(18, 250);
            labelRutaAltaModificarTiempoEstimado.Name = "labelRutaAltaModificarTiempoEstimado";
            labelRutaAltaModificarTiempoEstimado.Size = new Size(225, 15);
            labelRutaAltaModificarTiempoEstimado.TabIndex = 6;
            labelRutaAltaModificarTiempoEstimado.Text = "Tiempo estimado del recorrido (minutos)";
            // 
            // radioButtonRutaAltaModificarSentidoIda
            // 
            radioButtonRutaAltaModificarSentidoIda.AutoSize = true;
            radioButtonRutaAltaModificarSentidoIda.Location = new Point(18, 321);
            radioButtonRutaAltaModificarSentidoIda.Name = "radioButtonRutaAltaModificarSentidoIda";
            radioButtonRutaAltaModificarSentidoIda.Size = new Size(41, 19);
            radioButtonRutaAltaModificarSentidoIda.TabIndex = 8;
            radioButtonRutaAltaModificarSentidoIda.TabStop = true;
            radioButtonRutaAltaModificarSentidoIda.Text = "Ida";
            radioButtonRutaAltaModificarSentidoIda.UseVisualStyleBackColor = true;
            // 
            // radioButtonRutaAltaModificarSentidoVuelta
            // 
            radioButtonRutaAltaModificarSentidoVuelta.AutoSize = true;
            radioButtonRutaAltaModificarSentidoVuelta.Location = new Point(65, 321);
            radioButtonRutaAltaModificarSentidoVuelta.Name = "radioButtonRutaAltaModificarSentidoVuelta";
            radioButtonRutaAltaModificarSentidoVuelta.Size = new Size(58, 19);
            radioButtonRutaAltaModificarSentidoVuelta.TabIndex = 9;
            radioButtonRutaAltaModificarSentidoVuelta.TabStop = true;
            radioButtonRutaAltaModificarSentidoVuelta.Text = "Vuelta";
            radioButtonRutaAltaModificarSentidoVuelta.UseVisualStyleBackColor = true;
            // 
            // buttonRutaAltaModificarConfirmar
            // 
            buttonRutaAltaModificarConfirmar.Location = new Point(18, 346);
            buttonRutaAltaModificarConfirmar.Name = "buttonRutaAltaModificarConfirmar";
            buttonRutaAltaModificarConfirmar.Size = new Size(257, 44);
            buttonRutaAltaModificarConfirmar.TabIndex = 10;
            buttonRutaAltaModificarConfirmar.Text = "Confirmar";
            buttonRutaAltaModificarConfirmar.UseVisualStyleBackColor = true;
            buttonRutaAltaModificarConfirmar.Click += buttonRutaAltaModificarConfirmar_Click;
            // 
            // labelRutaAltaModificarSentido
            // 
            labelRutaAltaModificarSentido.AutoSize = true;
            labelRutaAltaModificarSentido.Location = new Point(18, 303);
            labelRutaAltaModificarSentido.Name = "labelRutaAltaModificarSentido";
            labelRutaAltaModificarSentido.Size = new Size(47, 15);
            labelRutaAltaModificarSentido.TabIndex = 11;
            labelRutaAltaModificarSentido.Text = "Sentido";
            // 
            // numericUpDownRutaAltaModificarDistanciaTotalKM
            // 
            numericUpDownRutaAltaModificarDistanciaTotalKM.Location = new Point(18, 220);
            numericUpDownRutaAltaModificarDistanciaTotalKM.Maximum = new decimal(new int[] { 99999, 0, 0, 0 });
            numericUpDownRutaAltaModificarDistanciaTotalKM.Name = "numericUpDownRutaAltaModificarDistanciaTotalKM";
            numericUpDownRutaAltaModificarDistanciaTotalKM.Size = new Size(257, 23);
            numericUpDownRutaAltaModificarDistanciaTotalKM.TabIndex = 12;
            // 
            // numericUpDownRutaAltaModificarTiempoEstimado
            // 
            numericUpDownRutaAltaModificarTiempoEstimado.Location = new Point(18, 268);
            numericUpDownRutaAltaModificarTiempoEstimado.Maximum = new decimal(new int[] { 99999, 0, 0, 0 });
            numericUpDownRutaAltaModificarTiempoEstimado.Name = "numericUpDownRutaAltaModificarTiempoEstimado";
            numericUpDownRutaAltaModificarTiempoEstimado.Size = new Size(257, 23);
            numericUpDownRutaAltaModificarTiempoEstimado.TabIndex = 13;
            // 
            // GestionRutaAgregarModificarUI
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(293, 401);
            Controls.Add(numericUpDownRutaAltaModificarTiempoEstimado);
            Controls.Add(numericUpDownRutaAltaModificarDistanciaTotalKM);
            Controls.Add(labelRutaAltaModificarSentido);
            Controls.Add(buttonRutaAltaModificarConfirmar);
            Controls.Add(radioButtonRutaAltaModificarSentidoVuelta);
            Controls.Add(radioButtonRutaAltaModificarSentidoIda);
            Controls.Add(labelRutaAltaModificarTiempoEstimado);
            Controls.Add(labelRutaAltaModificarDistancia);
            Controls.Add(textBoxRutaDescripcion);
            Controls.Add(labelRutaAltaModificarDescripcion);
            Controls.Add(textBoxRutaID);
            Controls.Add(labelRutaAltaModificarId);
            Name = "GestionRutaAgregarModificarUI";
            Text = "GestionRutaAgregarModificarUI";
            ((System.ComponentModel.ISupportInitialize)numericUpDownRutaAltaModificarDistanciaTotalKM).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownRutaAltaModificarTiempoEstimado).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelRutaAltaModificarId;
        private TextBox textBoxRutaID;
        private TextBox textBoxRutaDescripcion;
        private Label labelRutaAltaModificarDescripcion;
        private Label labelRutaAltaModificarDistancia;
        private Label labelRutaAltaModificarTiempoEstimado;
        private RadioButton radioButtonRutaAltaModificarSentidoIda;
        private RadioButton radioButtonRutaAltaModificarSentidoVuelta;
        private Button buttonRutaAltaModificarConfirmar;
        private Label labelRutaAltaModificarSentido;
        private NumericUpDown numericUpDownRutaAltaModificarDistanciaTotalKM;
        private NumericUpDown numericUpDownRutaAltaModificarTiempoEstimado;
    }
}