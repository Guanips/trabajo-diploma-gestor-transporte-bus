namespace UI.Modules.gestion_choferes
{
    partial class GestionChoferUI
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
            dataGridViewChoferes = new DataGridView();
            buttonChoferesAgregar = new Button();
            buttonChoferModificar = new Button();
            buttonChoferEliminar = new Button();
            groupBoxGestionChofer = new GroupBox();
            ((System.ComponentModel.ISupportInitialize)dataGridViewChoferes).BeginInit();
            groupBoxGestionChofer.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridViewChoferes
            // 
            dataGridViewChoferes.AllowUserToAddRows = false;
            dataGridViewChoferes.AllowUserToDeleteRows = false;
            dataGridViewChoferes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewChoferes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewChoferes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewChoferes.Location = new Point(15, 22);
            dataGridViewChoferes.MultiSelect = false;
            dataGridViewChoferes.Name = "dataGridViewChoferes";
            dataGridViewChoferes.ReadOnly = true;
            dataGridViewChoferes.RowHeadersVisible = false;
            dataGridViewChoferes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewChoferes.Size = new Size(652, 401);
            dataGridViewChoferes.TabIndex = 0;
            // 
            // buttonChoferesAgregar
            // 
            buttonChoferesAgregar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buttonChoferesAgregar.Location = new Point(15, 429);
            buttonChoferesAgregar.Name = "buttonChoferesAgregar";
            buttonChoferesAgregar.Size = new Size(142, 39);
            buttonChoferesAgregar.TabIndex = 1;
            buttonChoferesAgregar.Text = "Agregar chofer";
            buttonChoferesAgregar.UseVisualStyleBackColor = true;
            buttonChoferesAgregar.Click += buttonChoferesAgregar_Click;
            // 
            // buttonChoferModificar
            // 
            buttonChoferModificar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buttonChoferModificar.Location = new Point(163, 429);
            buttonChoferModificar.Name = "buttonChoferModificar";
            buttonChoferModificar.Size = new Size(142, 39);
            buttonChoferModificar.TabIndex = 2;
            buttonChoferModificar.Text = "Modificar chofer";
            buttonChoferModificar.UseVisualStyleBackColor = true;
            buttonChoferModificar.Click += buttonChoferModificar_Click;
            // 
            // buttonChoferEliminar
            // 
            buttonChoferEliminar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buttonChoferEliminar.Location = new Point(311, 429);
            buttonChoferEliminar.Name = "buttonChoferEliminar";
            buttonChoferEliminar.Size = new Size(142, 39);
            buttonChoferEliminar.TabIndex = 3;
            buttonChoferEliminar.Text = "Eliminar chofer";
            buttonChoferEliminar.UseVisualStyleBackColor = true;
            buttonChoferEliminar.Click += buttonChoferEliminar_Click;
            // 
            // groupBoxGestionChofer
            // 
            groupBoxGestionChofer.Controls.Add(buttonChoferEliminar);
            groupBoxGestionChofer.Controls.Add(buttonChoferModificar);
            groupBoxGestionChofer.Controls.Add(buttonChoferesAgregar);
            groupBoxGestionChofer.Controls.Add(dataGridViewChoferes);
            groupBoxGestionChofer.Location = new Point(12, 60);
            groupBoxGestionChofer.Name = "groupBoxGestionChofer";
            groupBoxGestionChofer.Size = new Size(679, 478);
            groupBoxGestionChofer.TabIndex = 5;
            groupBoxGestionChofer.TabStop = false;
            groupBoxGestionChofer.Text = "Gestion de choferes";
            // 
            // GestionChoferUI
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(704, 550);
            ControlBox = false;
            Controls.Add(groupBoxGestionChofer);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.None;
            MinimumSize = new Size(704, 469);
            Name = "GestionChoferUI";
            StartPosition = FormStartPosition.Manual;
            Text = "Gestion choferes";
            WindowState = FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)dataGridViewChoferes).EndInit();
            groupBoxGestionChofer.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridViewChoferes;
        private Button buttonChoferesAgregar;
        private Button buttonChoferModificar;
        private Button buttonChoferEliminar;
        private GroupBox groupBoxGestionChofer;
    }
}