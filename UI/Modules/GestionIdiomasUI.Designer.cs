namespace UI.Modules
{
    partial class GestionIdiomasUI
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.GroupBox groupBoxIdioma;
        private System.Windows.Forms.GroupBox groupBoxTraducciones;
        private System.Windows.Forms.TextBox textBoxCodigo;
        private System.Windows.Forms.TextBox textBoxNombre;
        private System.Windows.Forms.Label labelCodigo;
        private System.Windows.Forms.Label labelNombre;
        private System.Windows.Forms.DataGridView dataGridViewTraducciones;
        private System.Windows.Forms.Button btnGuardarIdioma;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            groupBoxIdioma = new GroupBox();
            btnGuardarIdioma = new Button();
            textBoxCodigo = new TextBox();
            textBoxNombre = new TextBox();
            labelCodigo = new Label();
            labelNombre = new Label();
            groupBoxTraducciones = new GroupBox();
            dataGridViewTraducciones = new DataGridView();
            groupBoxIdioma.SuspendLayout();
            groupBoxTraducciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewTraducciones).BeginInit();
            SuspendLayout();
            // 
            // groupBoxIdioma
            // 
            groupBoxIdioma.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBoxIdioma.Controls.Add(btnGuardarIdioma);
            groupBoxIdioma.Controls.Add(textBoxCodigo);
            groupBoxIdioma.Controls.Add(textBoxNombre);
            groupBoxIdioma.Controls.Add(labelCodigo);
            groupBoxIdioma.Controls.Add(labelNombre);
            groupBoxIdioma.Location = new Point(16, 16);
            groupBoxIdioma.Name = "groupBoxIdioma";
            groupBoxIdioma.Size = new Size(1220, 100);
            groupBoxIdioma.TabIndex = 0;
            groupBoxIdioma.TabStop = false;
            groupBoxIdioma.Text = "Datos del idioma";
            // 
            // btnGuardarIdioma
            // 
            btnGuardarIdioma.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnGuardarIdioma.Location = new Point(1074, 35);
            btnGuardarIdioma.Name = "btnGuardarIdioma";
            btnGuardarIdioma.Size = new Size(130, 34);
            btnGuardarIdioma.TabIndex = 4;
            btnGuardarIdioma.Text = "Guardar idioma";
            btnGuardarIdioma.UseVisualStyleBackColor = true;
            btnGuardarIdioma.Click += btnGuardarIdioma_Click;
            // 
            // textBoxCodigo
            // 
            textBoxCodigo.Location = new Point(122, 38);
            textBoxCodigo.MaxLength = 5;
            textBoxCodigo.Name = "textBoxCodigo";
            textBoxCodigo.Size = new Size(120, 23);
            textBoxCodigo.TabIndex = 1;
            // 
            // textBoxNombre
            // 
            textBoxNombre.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxNombre.Location = new Point(389, 38);
            textBoxNombre.Name = "textBoxNombre";
            textBoxNombre.Size = new Size(669, 23);
            textBoxNombre.TabIndex = 3;
            // 
            // labelCodigo
            // 
            labelCodigo.AutoSize = true;
            labelCodigo.Location = new Point(24, 41);
            labelCodigo.Name = "labelCodigo";
            labelCodigo.Size = new Size(92, 15);
            labelCodigo.TabIndex = 0;
            labelCodigo.Text = "Código (Ej: FR)";
            // 
            // labelNombre
            // 
            labelNombre.AutoSize = true;
            labelNombre.Location = new Point(280, 41);
            labelNombre.Name = "labelNombre";
            labelNombre.Size = new Size(92, 15);
            labelNombre.TabIndex = 2;
            labelNombre.Text = "Nombre idioma";
            // 
            // groupBoxTraducciones
            // 
            groupBoxTraducciones.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBoxTraducciones.Controls.Add(dataGridViewTraducciones);
            groupBoxTraducciones.Location = new Point(16, 128);
            groupBoxTraducciones.Name = "groupBoxTraducciones";
            groupBoxTraducciones.Size = new Size(1220, 578);
            groupBoxTraducciones.TabIndex = 1;
            groupBoxTraducciones.TabStop = false;
            groupBoxTraducciones.Text = "Traducciones";
            // 
            // dataGridViewTraducciones
            // 
            dataGridViewTraducciones.AllowUserToAddRows = false;
            dataGridViewTraducciones.AllowUserToDeleteRows = false;
            dataGridViewTraducciones.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewTraducciones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewTraducciones.BackgroundColor = SystemColors.Window;
            dataGridViewTraducciones.BorderStyle = BorderStyle.FixedSingle;
            dataGridViewTraducciones.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewTraducciones.EnableHeadersVisualStyles = false;
            dataGridViewTraducciones.Location = new Point(16, 28);
            dataGridViewTraducciones.Name = "dataGridViewTraducciones";
            dataGridViewTraducciones.RowHeadersVisible = false;
            dataGridViewTraducciones.Size = new Size(1188, 534);
            dataGridViewTraducciones.TabIndex = 0;
            // 
            // GestionIdiomasUI
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(1252, 722);
            ControlBox = false;
            Controls.Add(groupBoxTraducciones);
            Controls.Add(groupBoxIdioma);
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.None;
            MinimumSize = new Size(1100, 700);
            Name = "GestionIdiomasUI";
            Text = "Configuración de nuevos idiomas";
            WindowState = FormWindowState.Maximized;
            Load += GestionIdiomasUI_Load;
            groupBoxIdioma.ResumeLayout(false);
            groupBoxIdioma.PerformLayout();
            groupBoxTraducciones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewTraducciones).EndInit();
            ResumeLayout(false);
        }
    }
}