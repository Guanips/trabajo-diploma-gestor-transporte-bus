namespace UI.Modules
{
    partial class GestionHistorialUsuarioUI
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
            groupBoxUsuarios = new GroupBox();
            dataGridViewUsuarios = new DataGridView();
            gestionHistorialUILabelGridUsuarios = new Label();
            groupBoxHistorial = new GroupBox();
            dataGridViewHistorial = new DataGridView();
            gestionHistorialUILabelGridEstadoUsuarios = new Label();
            groupBoxAcciones = new GroupBox();
            gestionHistorialUIButtonRecuperarEstado = new Button();
            groupBoxUsuarios.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewUsuarios).BeginInit();
            groupBoxHistorial.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewHistorial).BeginInit();
            groupBoxAcciones.SuspendLayout();
            SuspendLayout();
            // 
            // groupBoxUsuarios
            // 
            groupBoxUsuarios.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            groupBoxUsuarios.Controls.Add(dataGridViewUsuarios);
            groupBoxUsuarios.Controls.Add(gestionHistorialUILabelGridUsuarios);
            groupBoxUsuarios.Location = new Point(16, 16);
            groupBoxUsuarios.Name = "groupBoxUsuarios";
            groupBoxUsuarios.Size = new Size(590, 662);
            groupBoxUsuarios.TabIndex = 0;
            groupBoxUsuarios.TabStop = false;
            groupBoxUsuarios.Text = "Usuarios disponibles";
            // 
            // dataGridViewUsuarios
            // 
            dataGridViewUsuarios.AllowUserToAddRows = false;
            dataGridViewUsuarios.AllowUserToDeleteRows = false;
            dataGridViewUsuarios.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewUsuarios.BackgroundColor = SystemColors.Window;
            dataGridViewUsuarios.BorderStyle = BorderStyle.FixedSingle;
            dataGridViewUsuarios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewUsuarios.EnableHeadersVisualStyles = false;
            dataGridViewUsuarios.Location = new Point(16, 48);
            dataGridViewUsuarios.MultiSelect = false;
            dataGridViewUsuarios.Name = "dataGridViewUsuarios";
            dataGridViewUsuarios.ReadOnly = true;
            dataGridViewUsuarios.RowHeadersVisible = false;
            dataGridViewUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewUsuarios.Size = new Size(558, 598);
            dataGridViewUsuarios.TabIndex = 0;
            dataGridViewUsuarios.SelectionChanged += dataGridViewUsuarios_SelectionChanged;
            // 
            // gestionHistorialUILabelGridUsuarios
            // 
            gestionHistorialUILabelGridUsuarios.AutoSize = true;
            gestionHistorialUILabelGridUsuarios.Location = new Point(16, 30);
            gestionHistorialUILabelGridUsuarios.Name = "gestionHistorialUILabelGridUsuarios";
            gestionHistorialUILabelGridUsuarios.Size = new Size(115, 15);
            gestionHistorialUILabelGridUsuarios.TabIndex = 3;
            gestionHistorialUILabelGridUsuarios.Text = "Usuarios disponibles";
            // 
            // groupBoxHistorial
            // 
            groupBoxHistorial.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBoxHistorial.Controls.Add(dataGridViewHistorial);
            groupBoxHistorial.Controls.Add(gestionHistorialUILabelGridEstadoUsuarios);
            groupBoxHistorial.Location = new Point(614, 16);
            groupBoxHistorial.Name = "groupBoxHistorial";
            groupBoxHistorial.Size = new Size(386, 662);
            groupBoxHistorial.TabIndex = 1;
            groupBoxHistorial.TabStop = false;
            groupBoxHistorial.Text = "Historial";
            // 
            // dataGridViewHistorial
            // 
            dataGridViewHistorial.AllowUserToAddRows = false;
            dataGridViewHistorial.AllowUserToDeleteRows = false;
            dataGridViewHistorial.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewHistorial.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewHistorial.BackgroundColor = SystemColors.Window;
            dataGridViewHistorial.BorderStyle = BorderStyle.FixedSingle;
            dataGridViewHistorial.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewHistorial.EnableHeadersVisualStyles = false;
            dataGridViewHistorial.Location = new Point(16, 48);
            dataGridViewHistorial.MultiSelect = false;
            dataGridViewHistorial.Name = "dataGridViewHistorial";
            dataGridViewHistorial.ReadOnly = true;
            dataGridViewHistorial.RowHeadersVisible = false;
            dataGridViewHistorial.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewHistorial.Size = new Size(354, 598);
            dataGridViewHistorial.TabIndex = 1;
            // 
            // gestionHistorialUILabelGridEstadoUsuarios
            // 
            gestionHistorialUILabelGridEstadoUsuarios.AutoSize = true;
            gestionHistorialUILabelGridEstadoUsuarios.Location = new Point(16, 30);
            gestionHistorialUILabelGridEstadoUsuarios.Name = "gestionHistorialUILabelGridEstadoUsuarios";
            gestionHistorialUILabelGridEstadoUsuarios.Size = new Size(184, 15);
            gestionHistorialUILabelGridEstadoUsuarios.TabIndex = 4;
            gestionHistorialUILabelGridEstadoUsuarios.Text = "Historial del usuario seleccionado";
            // 
            // groupBoxAcciones
            // 
            groupBoxAcciones.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            groupBoxAcciones.Controls.Add(gestionHistorialUIButtonRecuperarEstado);
            groupBoxAcciones.Location = new Point(1018, 16);
            groupBoxAcciones.Name = "groupBoxAcciones";
            groupBoxAcciones.Size = new Size(184, 662);
            groupBoxAcciones.TabIndex = 2;
            groupBoxAcciones.TabStop = false;
            groupBoxAcciones.Text = "Acciones";
            // 
            // gestionHistorialUIButtonRecuperarEstado
            // 
            gestionHistorialUIButtonRecuperarEstado.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            gestionHistorialUIButtonRecuperarEstado.Location = new Point(16, 48);
            gestionHistorialUIButtonRecuperarEstado.Name = "gestionHistorialUIButtonRecuperarEstado";
            gestionHistorialUIButtonRecuperarEstado.Size = new Size(152, 120);
            gestionHistorialUIButtonRecuperarEstado.TabIndex = 2;
            gestionHistorialUIButtonRecuperarEstado.Text = "Recuperar estado";
            gestionHistorialUIButtonRecuperarEstado.UseVisualStyleBackColor = true;
            gestionHistorialUIButtonRecuperarEstado.Click += gestionHistorialUIButtonRecuperarEstado_Click;
            // 
            // GestionHistorialUsuarioUI
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(1218, 694);
            ControlBox = false;
            Controls.Add(groupBoxAcciones);
            Controls.Add(groupBoxHistorial);
            Controls.Add(groupBoxUsuarios);
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.None;
            MinimumSize = new Size(1100, 650);
            Name = "GestionHistorialUsuarioUI";
            Text = "Historial de usuarios";
            WindowState = FormWindowState.Maximized;
            groupBoxUsuarios.ResumeLayout(false);
            groupBoxUsuarios.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewUsuarios).EndInit();
            groupBoxHistorial.ResumeLayout(false);
            groupBoxHistorial.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewHistorial).EndInit();
            groupBoxAcciones.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBoxUsuarios;
        private DataGridView dataGridViewUsuarios;
        private Label gestionHistorialUILabelGridUsuarios;
        private GroupBox groupBoxHistorial;
        private DataGridView dataGridViewHistorial;
        private Label gestionHistorialUILabelGridEstadoUsuarios;
        private GroupBox groupBoxAcciones;
        private Button gestionHistorialUIButtonRecuperarEstado;
    }
}