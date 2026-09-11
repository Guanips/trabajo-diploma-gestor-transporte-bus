namespace UI.Modules
{
    partial class BloqueoUsuariosUI
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
            groupBoxBloqueados = new GroupBox();
            btnDesbloquear = new Button();
            dataGridViewBloqueados = new DataGridView();
            groupBoxBloqueados.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewBloqueados).BeginInit();
            SuspendLayout();
            // 
            // groupBoxBloqueados
            // 
            groupBoxBloqueados.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBoxBloqueados.Controls.Add(btnDesbloquear);
            groupBoxBloqueados.Controls.Add(dataGridViewBloqueados);
            groupBoxBloqueados.Location = new Point(16, 16);
            groupBoxBloqueados.Name = "groupBoxBloqueados";
            groupBoxBloqueados.Size = new Size(1260, 660);
            groupBoxBloqueados.TabIndex = 0;
            groupBoxBloqueados.TabStop = false;
            groupBoxBloqueados.Text = "Usuarios bloqueados";
            // 
            // btnDesbloquear
            // 
            btnDesbloquear.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnDesbloquear.Location = new Point(16, 612);
            btnDesbloquear.Name = "btnDesbloquear";
            btnDesbloquear.Size = new Size(180, 34);
            btnDesbloquear.TabIndex = 1;
            btnDesbloquear.Text = "Desbloquear seleccionado";
            btnDesbloquear.UseVisualStyleBackColor = true;
            // 
            // dataGridViewBloqueados
            // 
            dataGridViewBloqueados.AllowUserToAddRows = false;
            dataGridViewBloqueados.AllowUserToDeleteRows = false;
            dataGridViewBloqueados.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewBloqueados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewBloqueados.BackgroundColor = SystemColors.Window;
            dataGridViewBloqueados.BorderStyle = BorderStyle.FixedSingle;
            dataGridViewBloqueados.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewBloqueados.EnableHeadersVisualStyles = false;
            dataGridViewBloqueados.Location = new Point(16, 28);
            dataGridViewBloqueados.Name = "dataGridViewBloqueados";
            dataGridViewBloqueados.ReadOnly = true;
            dataGridViewBloqueados.RowHeadersVisible = false;
            dataGridViewBloqueados.Size = new Size(1228, 568);
            dataGridViewBloqueados.TabIndex = 0;
            // 
            // BloqueoUsuariosUI
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(1292, 692);
            ControlBox = false;
            Controls.Add(groupBoxBloqueados);
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.None;
            MinimumSize = new Size(1100, 650);
            Name = "BloqueoUsuariosUI";
            Text = "Desbloqueo de usuarios";
            WindowState = FormWindowState.Maximized;
            groupBoxBloqueados.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewBloqueados).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBoxBloqueados;
        private DataGridView dataGridViewBloqueados;
        private Button btnDesbloquear;
    }
}