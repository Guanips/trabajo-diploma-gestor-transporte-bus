namespace UI.Modules.gestion_internos
{
    partial class GestionInternoUI
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
            dataGridViewInternos = new DataGridView();
            buttonInternoAgregar = new Button();
            buttonInternoModificar = new Button();
            buttonInternoEliminar = new Button();
            groupBoxGestionInterno = new GroupBox();
            ((System.ComponentModel.ISupportInitialize)dataGridViewInternos).BeginInit();
            groupBoxGestionInterno.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridViewInternos
            // 
            dataGridViewInternos.AllowUserToAddRows = false;
            dataGridViewInternos.AllowUserToDeleteRows = false;
            dataGridViewInternos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewInternos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewInternos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewInternos.Location = new Point(17, 22);
            dataGridViewInternos.MultiSelect = false;
            dataGridViewInternos.Name = "dataGridViewInternos";
            dataGridViewInternos.ReadOnly = true;
            dataGridViewInternos.RowHeadersVisible = false;
            dataGridViewInternos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewInternos.Size = new Size(656, 382);
            dataGridViewInternos.TabIndex = 0;
            // 
            // buttonInternoAgregar
            // 
            buttonInternoAgregar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buttonInternoAgregar.Location = new Point(17, 410);
            buttonInternoAgregar.Name = "buttonInternoAgregar";
            buttonInternoAgregar.Size = new Size(142, 39);
            buttonInternoAgregar.TabIndex = 1;
            buttonInternoAgregar.Text = "Agregar interno";
            buttonInternoAgregar.UseVisualStyleBackColor = true;
            buttonInternoAgregar.Click += buttonInternoAgregar_Click;
            // 
            // buttonInternoModificar
            // 
            buttonInternoModificar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buttonInternoModificar.Location = new Point(165, 410);
            buttonInternoModificar.Name = "buttonInternoModificar";
            buttonInternoModificar.Size = new Size(142, 39);
            buttonInternoModificar.TabIndex = 2;
            buttonInternoModificar.Text = "Modificar interno";
            buttonInternoModificar.UseVisualStyleBackColor = true;
            buttonInternoModificar.Click += buttonInternoModificar_Click;
            // 
            // buttonInternoEliminar
            // 
            buttonInternoEliminar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buttonInternoEliminar.Location = new Point(313, 410);
            buttonInternoEliminar.Name = "buttonInternoEliminar";
            buttonInternoEliminar.Size = new Size(142, 39);
            buttonInternoEliminar.TabIndex = 3;
            buttonInternoEliminar.Text = "Eliminar interno";
            buttonInternoEliminar.UseVisualStyleBackColor = true;
            buttonInternoEliminar.Click += buttonInternoEliminar_Click;
            // 
            // groupBoxGestionInterno
            // 
            groupBoxGestionInterno.Controls.Add(buttonInternoEliminar);
            groupBoxGestionInterno.Controls.Add(buttonInternoModificar);
            groupBoxGestionInterno.Controls.Add(buttonInternoAgregar);
            groupBoxGestionInterno.Controls.Add(dataGridViewInternos);
            groupBoxGestionInterno.Location = new Point(12, 61);
            groupBoxGestionInterno.Name = "groupBoxGestionInterno";
            groupBoxGestionInterno.Size = new Size(692, 464);
            groupBoxGestionInterno.TabIndex = 4;
            groupBoxGestionInterno.TabStop = false;
            groupBoxGestionInterno.Text = "Gestion de internos";
            // 
            // GestionInternoUI
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(716, 537);
            ControlBox = false;
            Controls.Add(groupBoxGestionInterno);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.None;
            MinimumSize = new Size(704, 469);
            Name = "GestionInternoUI";
            StartPosition = FormStartPosition.Manual;
            Text = "Gestion internos";
            WindowState = FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)dataGridViewInternos).EndInit();
            groupBoxGestionInterno.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridViewInternos;
        private Button buttonInternoAgregar;
        private Button buttonInternoModificar;
        private Button buttonInternoEliminar;
        private GroupBox groupBoxGestionInterno;
    }
}