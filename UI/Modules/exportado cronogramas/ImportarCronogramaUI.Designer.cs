namespace UI.Modules.exportado_cronogramas
{
    partial class ImportarCronogramaUI
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
            groupBoxImportarCronogramaSeleccionado = new GroupBox();
            buttonImportarCronogramaSeleccionarOtro = new Button();
            dataGridViewCronogramaSeleccionadoImportado = new DataGridView();
            groupBoxImportarCronogramaSeleccionado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewCronogramaSeleccionadoImportado).BeginInit();
            SuspendLayout();
            // 
            // groupBoxImportarCronogramaSeleccionado
            // 
            groupBoxImportarCronogramaSeleccionado.Controls.Add(buttonImportarCronogramaSeleccionarOtro);
            groupBoxImportarCronogramaSeleccionado.Controls.Add(dataGridViewCronogramaSeleccionadoImportado);
            groupBoxImportarCronogramaSeleccionado.Location = new Point(12, 59);
            groupBoxImportarCronogramaSeleccionado.Name = "groupBoxImportarCronogramaSeleccionado";
            groupBoxImportarCronogramaSeleccionado.Size = new Size(1060, 550);
            groupBoxImportarCronogramaSeleccionado.TabIndex = 3;
            groupBoxImportarCronogramaSeleccionado.TabStop = false;
            groupBoxImportarCronogramaSeleccionado.Text = "Cronograma seleccionado";
            // 
            // buttonImportarCronogramaSeleccionarOtro
            // 
            buttonImportarCronogramaSeleccionarOtro.Location = new Point(6, 22);
            buttonImportarCronogramaSeleccionarOtro.Name = "buttonImportarCronogramaSeleccionarOtro";
            buttonImportarCronogramaSeleccionarOtro.Size = new Size(274, 32);
            buttonImportarCronogramaSeleccionarOtro.TabIndex = 2;
            buttonImportarCronogramaSeleccionarOtro.Text = "Importar cronograma";
            buttonImportarCronogramaSeleccionarOtro.UseVisualStyleBackColor = true;
            buttonImportarCronogramaSeleccionarOtro.Click += buttonImportarCronogramaSeleccionarOtro_Click;
            // 
            // dataGridViewCronogramaSeleccionadoImportado
            // 
            dataGridViewCronogramaSeleccionadoImportado.AllowUserToAddRows = false;
            dataGridViewCronogramaSeleccionadoImportado.AllowUserToDeleteRows = false;
            dataGridViewCronogramaSeleccionadoImportado.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCronogramaSeleccionadoImportado.Location = new Point(6, 60);
            dataGridViewCronogramaSeleccionadoImportado.Name = "dataGridViewCronogramaSeleccionadoImportado";
            dataGridViewCronogramaSeleccionadoImportado.ReadOnly = true;
            dataGridViewCronogramaSeleccionadoImportado.Size = new Size(1048, 484);
            dataGridViewCronogramaSeleccionadoImportado.TabIndex = 1;
            // 
            // ImportarCronogramaUI
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1084, 621);
            ControlBox = false;
            Controls.Add(groupBoxImportarCronogramaSeleccionado);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ImportarCronogramaUI";
            Text = "ImportarCronogramaUI";
            WindowState = FormWindowState.Maximized;
            groupBoxImportarCronogramaSeleccionado.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewCronogramaSeleccionadoImportado).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBoxImportarCronogramaSeleccionado;
        private Button buttonImportarCronogramaSeleccionarOtro;
        private DataGridView dataGridViewCronogramaSeleccionadoImportado;
    }
}