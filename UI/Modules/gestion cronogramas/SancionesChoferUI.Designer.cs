namespace UI.Modules.gestion_cronogramas
{
    partial class SancionesChoferUI
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
            groupBoxSancionesChoferChoferes = new GroupBox();
            labelSancionesChoferListaChoferes = new Label();
            listBoxChoferes = new ListBox();
            groupBoxSancionesChoferSanciones = new GroupBox();
            labelSancionesChoferListaSanciones = new Label();
            buttonSancionesChoferEliminar = new Button();
            dataGridViewSancionesChofer = new DataGridView();
            groupBoxSancionesChoferChoferes.SuspendLayout();
            groupBoxSancionesChoferSanciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewSancionesChofer).BeginInit();
            SuspendLayout();
            //
            // groupBoxSancionesChoferChoferes
            //
            groupBoxSancionesChoferChoferes.Controls.Add(labelSancionesChoferListaChoferes);
            groupBoxSancionesChoferChoferes.Controls.Add(listBoxChoferes);
            groupBoxSancionesChoferChoferes.Location = new Point(12, 55);
            groupBoxSancionesChoferChoferes.Name = "groupBoxSancionesChoferChoferes";
            groupBoxSancionesChoferChoferes.Size = new Size(241, 426);
            groupBoxSancionesChoferChoferes.TabIndex = 0;
            groupBoxSancionesChoferChoferes.TabStop = false;
            groupBoxSancionesChoferChoferes.Text = "Choferes";
            //
            // labelSancionesChoferListaChoferes
            //
            labelSancionesChoferListaChoferes.AutoSize = true;
            labelSancionesChoferListaChoferes.Location = new Point(6, 23);
            labelSancionesChoferListaChoferes.Name = "labelSancionesChoferListaChoferes";
            labelSancionesChoferListaChoferes.Size = new Size(119, 15);
            labelSancionesChoferListaChoferes.TabIndex = 1;
            labelSancionesChoferListaChoferes.Text = "Choferes disponibles";
            //
            // listBoxChoferes
            //
            listBoxChoferes.FormattingEnabled = true;
            listBoxChoferes.ItemHeight = 15;
            listBoxChoferes.Location = new Point(6, 41);
            listBoxChoferes.Name = "listBoxChoferes";
            listBoxChoferes.Size = new Size(229, 379);
            listBoxChoferes.TabIndex = 0;
            listBoxChoferes.SelectedIndexChanged += listBoxChoferes_SelectedIndexChanged;
            //
            // groupBoxSancionesChoferSanciones
            //
            groupBoxSancionesChoferSanciones.Controls.Add(labelSancionesChoferListaSanciones);
            groupBoxSancionesChoferSanciones.Controls.Add(buttonSancionesChoferEliminar);
            groupBoxSancionesChoferSanciones.Controls.Add(dataGridViewSancionesChofer);
            groupBoxSancionesChoferSanciones.Location = new Point(259, 55);
            groupBoxSancionesChoferSanciones.Name = "groupBoxSancionesChoferSanciones";
            groupBoxSancionesChoferSanciones.Size = new Size(800, 426);
            groupBoxSancionesChoferSanciones.TabIndex = 1;
            groupBoxSancionesChoferSanciones.TabStop = false;
            groupBoxSancionesChoferSanciones.Text = "Sanciones";
            //
            // labelSancionesChoferListaSanciones
            //
            labelSancionesChoferListaSanciones.AutoSize = true;
            labelSancionesChoferListaSanciones.Location = new Point(6, 23);
            labelSancionesChoferListaSanciones.Name = "labelSancionesChoferListaSanciones";
            labelSancionesChoferListaSanciones.Size = new Size(190, 15);
            labelSancionesChoferListaSanciones.TabIndex = 2;
            labelSancionesChoferListaSanciones.Text = "Sanciones del chofer seleccionado";
            //
            // buttonSancionesChoferEliminar
            //
            buttonSancionesChoferEliminar.Location = new Point(619, 382);
            buttonSancionesChoferEliminar.Name = "buttonSancionesChoferEliminar";
            buttonSancionesChoferEliminar.Size = new Size(175, 38);
            buttonSancionesChoferEliminar.TabIndex = 1;
            buttonSancionesChoferEliminar.Text = "Eliminar sanción";
            buttonSancionesChoferEliminar.UseVisualStyleBackColor = true;
            buttonSancionesChoferEliminar.Click += buttonSancionesChoferEliminar_Click;
            //
            // dataGridViewSancionesChofer
            //
            dataGridViewSancionesChofer.AllowUserToAddRows = false;
            dataGridViewSancionesChofer.AllowUserToDeleteRows = false;
            dataGridViewSancionesChofer.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewSancionesChofer.Location = new Point(6, 41);
            dataGridViewSancionesChofer.MultiSelect = false;
            dataGridViewSancionesChofer.Name = "dataGridViewSancionesChofer";
            dataGridViewSancionesChofer.ReadOnly = true;
            dataGridViewSancionesChofer.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewSancionesChofer.Size = new Size(788, 335);
            dataGridViewSancionesChofer.TabIndex = 0;
            //
            // SancionesChoferUI
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1071, 493);
            ControlBox = false;
            Controls.Add(groupBoxSancionesChoferSanciones);
            Controls.Add(groupBoxSancionesChoferChoferes);
            FormBorderStyle = FormBorderStyle.None;
            Name = "SancionesChoferUI";
            StartPosition = FormStartPosition.CenterParent;
            Text = "SancionesChoferUI";
            WindowState = FormWindowState.Maximized;
            groupBoxSancionesChoferChoferes.ResumeLayout(false);
            groupBoxSancionesChoferChoferes.PerformLayout();
            groupBoxSancionesChoferSanciones.ResumeLayout(false);
            groupBoxSancionesChoferSanciones.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewSancionesChofer).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBoxSancionesChoferChoferes;
        private Label labelSancionesChoferListaChoferes;
        private ListBox listBoxChoferes;
        private GroupBox groupBoxSancionesChoferSanciones;
        private Label labelSancionesChoferListaSanciones;
        private Button buttonSancionesChoferEliminar;
        private DataGridView dataGridViewSancionesChofer;
    }
}
