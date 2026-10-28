namespace UI.Modules.exportado_cronogramas
{
    partial class ExportarCronogramaUI
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
            groupBoxExportarCronogramaSeleccion = new GroupBox();
            buttonExportarCronogramasConfirmar = new Button();
            listBoxExportarCronogramaDisponibles = new ListBox();
            dataGridViewCronogramaSeleccionadoExportado = new DataGridView();
            groupBoxExportarCronogramaSeleccionado = new GroupBox();
            groupBoxExportarCronogramaSeleccion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewCronogramaSeleccionadoExportado).BeginInit();
            groupBoxExportarCronogramaSeleccionado.SuspendLayout();
            SuspendLayout();
            // 
            // groupBoxExportarCronogramaSeleccion
            // 
            groupBoxExportarCronogramaSeleccion.Controls.Add(buttonExportarCronogramasConfirmar);
            groupBoxExportarCronogramaSeleccion.Controls.Add(listBoxExportarCronogramaDisponibles);
            groupBoxExportarCronogramaSeleccion.Location = new Point(12, 63);
            groupBoxExportarCronogramaSeleccion.Name = "groupBoxExportarCronogramaSeleccion";
            groupBoxExportarCronogramaSeleccion.Size = new Size(413, 550);
            groupBoxExportarCronogramaSeleccion.TabIndex = 0;
            groupBoxExportarCronogramaSeleccion.TabStop = false;
            groupBoxExportarCronogramaSeleccion.Text = "Selección de cronograma";
            // 
            // buttonExportarCronogramasConfirmar
            // 
            buttonExportarCronogramasConfirmar.Location = new Point(6, 504);
            buttonExportarCronogramasConfirmar.Name = "buttonExportarCronogramasConfirmar";
            buttonExportarCronogramasConfirmar.Size = new Size(401, 40);
            buttonExportarCronogramasConfirmar.TabIndex = 1;
            buttonExportarCronogramasConfirmar.Text = "Exportar";
            buttonExportarCronogramasConfirmar.UseVisualStyleBackColor = true;
            buttonExportarCronogramasConfirmar.Click += buttonExportarCronogramasConfirmar_Click;
            // 
            // listBoxExportarCronogramaDisponibles
            // 
            listBoxExportarCronogramaDisponibles.FormattingEnabled = true;
            listBoxExportarCronogramaDisponibles.ItemHeight = 15;
            listBoxExportarCronogramaDisponibles.Location = new Point(6, 22);
            listBoxExportarCronogramaDisponibles.Name = "listBoxExportarCronogramaDisponibles";
            listBoxExportarCronogramaDisponibles.Size = new Size(401, 469);
            listBoxExportarCronogramaDisponibles.TabIndex = 0;
            listBoxExportarCronogramaDisponibles.SelectedIndexChanged += listBoxExportarCronogramaDisponibles_SelectedIndexChanged;
            // 
            // dataGridViewCronogramaSeleccionadoExportado
            // 
            dataGridViewCronogramaSeleccionadoExportado.AllowUserToAddRows = false;
            dataGridViewCronogramaSeleccionadoExportado.AllowUserToDeleteRows = false;
            dataGridViewCronogramaSeleccionadoExportado.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCronogramaSeleccionadoExportado.Location = new Point(6, 22);
            dataGridViewCronogramaSeleccionadoExportado.Name = "dataGridViewCronogramaSeleccionadoExportado";
            dataGridViewCronogramaSeleccionadoExportado.ReadOnly = true;
            dataGridViewCronogramaSeleccionadoExportado.Size = new Size(707, 522);
            dataGridViewCronogramaSeleccionadoExportado.TabIndex = 1;
            // 
            // groupBoxExportarCronogramaSeleccionado
            // 
            groupBoxExportarCronogramaSeleccionado.Controls.Add(dataGridViewCronogramaSeleccionadoExportado);
            groupBoxExportarCronogramaSeleccionado.Location = new Point(431, 63);
            groupBoxExportarCronogramaSeleccionado.Name = "groupBoxExportarCronogramaSeleccionado";
            groupBoxExportarCronogramaSeleccionado.Size = new Size(719, 550);
            groupBoxExportarCronogramaSeleccionado.TabIndex = 2;
            groupBoxExportarCronogramaSeleccionado.TabStop = false;
            groupBoxExportarCronogramaSeleccionado.Text = "Cronograma seleccionado";
            // 
            // ExportarCronogramaUI
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1162, 660);
            ControlBox = false;
            Controls.Add(groupBoxExportarCronogramaSeleccionado);
            Controls.Add(groupBoxExportarCronogramaSeleccion);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ExportarCronogramaUI";
            Text = "ExportarCronogramaUI";
            WindowState = FormWindowState.Maximized;
            groupBoxExportarCronogramaSeleccion.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewCronogramaSeleccionadoExportado).EndInit();
            groupBoxExportarCronogramaSeleccionado.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBoxExportarCronogramaSeleccion;
        private Button buttonExportarCronogramasConfirmar;
        private ListBox listBoxExportarCronogramaDisponibles;
        private DataGridView dataGridViewCronogramaSeleccionadoExportado;
        private GroupBox groupBoxExportarCronogramaSeleccionado;
    }
}