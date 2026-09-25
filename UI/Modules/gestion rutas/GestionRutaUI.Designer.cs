namespace UI.Modules.gestion_rutas
{
    partial class GestionRutaUI
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
            groupBoxRutaDisponibles = new GroupBox();
            buttonRutaModificar = new Button();
            buttonRutaEliminar = new Button();
            buttonRutaAlta = new Button();
            dataGridViewRutaDisponibles = new DataGridView();
            groupBoxRutaParadasDisponibles = new GroupBox();
            buttonRutaAsignarParada = new Button();
            listBoxRutaParadasDisponibles = new ListBox();
            groupBoxRutaParadasRuta = new GroupBox();
            buttonRutaBajarParada = new Button();
            buttonRutaSubirParada = new Button();
            listBoxRutaParadasDeRuta = new ListBox();
            groupBoxRutaDisponibles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewRutaDisponibles).BeginInit();
            groupBoxRutaParadasDisponibles.SuspendLayout();
            groupBoxRutaParadasRuta.SuspendLayout();
            SuspendLayout();
            // 
            // groupBoxRutaDisponibles
            // 
            groupBoxRutaDisponibles.Controls.Add(buttonRutaModificar);
            groupBoxRutaDisponibles.Controls.Add(buttonRutaEliminar);
            groupBoxRutaDisponibles.Controls.Add(buttonRutaAlta);
            groupBoxRutaDisponibles.Controls.Add(dataGridViewRutaDisponibles);
            groupBoxRutaDisponibles.Location = new Point(12, 12);
            groupBoxRutaDisponibles.Name = "groupBoxRutaDisponibles";
            groupBoxRutaDisponibles.Size = new Size(400, 449);
            groupBoxRutaDisponibles.TabIndex = 0;
            groupBoxRutaDisponibles.TabStop = false;
            groupBoxRutaDisponibles.Text = "Rutas";
            // 
            // buttonRutaModificar
            // 
            buttonRutaModificar.Location = new Point(290, 394);
            buttonRutaModificar.Name = "buttonRutaModificar";
            buttonRutaModificar.Size = new Size(104, 43);
            buttonRutaModificar.TabIndex = 3;
            buttonRutaModificar.Text = "Modificar Ruta";
            buttonRutaModificar.UseVisualStyleBackColor = true;
            buttonRutaModificar.Click += buttonRutaModificar_Click;
            // 
            // buttonRutaEliminar
            // 
            buttonRutaEliminar.Location = new Point(148, 394);
            buttonRutaEliminar.Name = "buttonRutaEliminar";
            buttonRutaEliminar.Size = new Size(104, 43);
            buttonRutaEliminar.TabIndex = 2;
            buttonRutaEliminar.Text = "Eliminar Ruta";
            buttonRutaEliminar.UseVisualStyleBackColor = true;
            buttonRutaEliminar.Click += buttonRutaEliminar_Click;
            // 
            // buttonRutaAlta
            // 
            buttonRutaAlta.Location = new Point(6, 394);
            buttonRutaAlta.Name = "buttonRutaAlta";
            buttonRutaAlta.Size = new Size(104, 43);
            buttonRutaAlta.TabIndex = 1;
            buttonRutaAlta.Text = "Agregar Ruta";
            buttonRutaAlta.UseVisualStyleBackColor = true;
            buttonRutaAlta.Click += buttonRutaAlta_Click;
            // 
            // dataGridViewRutaDisponibles
            // 
            dataGridViewRutaDisponibles.AllowUserToAddRows = false;
            dataGridViewRutaDisponibles.AllowUserToDeleteRows = false;
            dataGridViewRutaDisponibles.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewRutaDisponibles.Location = new Point(6, 22);
            dataGridViewRutaDisponibles.MultiSelect = false;
            dataGridViewRutaDisponibles.Name = "dataGridViewRutaDisponibles";
            dataGridViewRutaDisponibles.ReadOnly = true;
            dataGridViewRutaDisponibles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewRutaDisponibles.Size = new Size(388, 366);
            dataGridViewRutaDisponibles.TabIndex = 0;
            dataGridViewRutaDisponibles.SelectionChanged += dataGridViewRutaDisponibles_SelectionChanged;
            // 
            // groupBoxRutaParadasDisponibles
            // 
            groupBoxRutaParadasDisponibles.Controls.Add(buttonRutaAsignarParada);
            groupBoxRutaParadasDisponibles.Controls.Add(listBoxRutaParadasDisponibles);
            groupBoxRutaParadasDisponibles.Location = new Point(418, 12);
            groupBoxRutaParadasDisponibles.Name = "groupBoxRutaParadasDisponibles";
            groupBoxRutaParadasDisponibles.Size = new Size(297, 449);
            groupBoxRutaParadasDisponibles.TabIndex = 1;
            groupBoxRutaParadasDisponibles.TabStop = false;
            groupBoxRutaParadasDisponibles.Text = "Paradas disponibles";
            // 
            // buttonRutaAsignarParada
            // 
            buttonRutaAsignarParada.Location = new Point(6, 394);
            buttonRutaAsignarParada.Name = "buttonRutaAsignarParada";
            buttonRutaAsignarParada.Size = new Size(285, 43);
            buttonRutaAsignarParada.TabIndex = 1;
            buttonRutaAsignarParada.Text = "Asignar parada a ruta seleccionada";
            buttonRutaAsignarParada.UseVisualStyleBackColor = true;
            buttonRutaAsignarParada.Click += buttonRutaAsignarParada_Click;
            // 
            // listBoxRutaParadasDisponibles
            // 
            listBoxRutaParadasDisponibles.FormattingEnabled = true;
            listBoxRutaParadasDisponibles.ItemHeight = 15;
            listBoxRutaParadasDisponibles.Location = new Point(6, 22);
            listBoxRutaParadasDisponibles.Name = "listBoxRutaParadasDisponibles";
            listBoxRutaParadasDisponibles.Size = new Size(285, 364);
            listBoxRutaParadasDisponibles.TabIndex = 0;
            // 
            // groupBoxRutaParadasRuta
            // 
            groupBoxRutaParadasRuta.Controls.Add(buttonRutaBajarParada);
            groupBoxRutaParadasRuta.Controls.Add(buttonRutaSubirParada);
            groupBoxRutaParadasRuta.Controls.Add(listBoxRutaParadasDeRuta);
            groupBoxRutaParadasRuta.Location = new Point(721, 12);
            groupBoxRutaParadasRuta.Name = "groupBoxRutaParadasRuta";
            groupBoxRutaParadasRuta.Size = new Size(297, 449);
            groupBoxRutaParadasRuta.TabIndex = 2;
            groupBoxRutaParadasRuta.TabStop = false;
            groupBoxRutaParadasRuta.Text = "Paradas disponibles";
            // 
            // buttonRutaBajarParada
            // 
            buttonRutaBajarParada.Location = new Point(162, 394);
            buttonRutaBajarParada.Name = "buttonRutaBajarParada";
            buttonRutaBajarParada.Size = new Size(129, 43);
            buttonRutaBajarParada.TabIndex = 2;
            buttonRutaBajarParada.Text = "Bajar parada en ruta";
            buttonRutaBajarParada.UseVisualStyleBackColor = true;
            buttonRutaBajarParada.Click += buttonRutaBajarParada_Click;
            // 
            // buttonRutaSubirParada
            // 
            buttonRutaSubirParada.Location = new Point(6, 394);
            buttonRutaSubirParada.Name = "buttonRutaSubirParada";
            buttonRutaSubirParada.Size = new Size(129, 43);
            buttonRutaSubirParada.TabIndex = 1;
            buttonRutaSubirParada.Text = "Subir parada en ruta";
            buttonRutaSubirParada.UseVisualStyleBackColor = true;
            buttonRutaSubirParada.Click += buttonRutaSubirParada_Click;
            // 
            // listBoxRutaParadasDeRuta
            // 
            listBoxRutaParadasDeRuta.FormattingEnabled = true;
            listBoxRutaParadasDeRuta.ItemHeight = 15;
            listBoxRutaParadasDeRuta.Location = new Point(6, 22);
            listBoxRutaParadasDeRuta.Name = "listBoxRutaParadasDeRuta";
            listBoxRutaParadasDeRuta.Size = new Size(285, 364);
            listBoxRutaParadasDeRuta.TabIndex = 0;
            // 
            // GestionRutaUI
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1034, 475);
            ControlBox = false;
            Controls.Add(groupBoxRutaParadasRuta);
            Controls.Add(groupBoxRutaParadasDisponibles);
            Controls.Add(groupBoxRutaDisponibles);
            FormBorderStyle = FormBorderStyle.None;
            Name = "GestionRutaUI";
            Text = "GestionRutaUI";
            groupBoxRutaDisponibles.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewRutaDisponibles).EndInit();
            groupBoxRutaParadasDisponibles.ResumeLayout(false);
            groupBoxRutaParadasRuta.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBoxRutaDisponibles;
        private DataGridView dataGridViewRutaDisponibles;
        private Button buttonRutaModificar;
        private Button buttonRutaEliminar;
        private Button buttonRutaAlta;
        private GroupBox groupBoxRutaParadasDisponibles;
        private ListBox listBoxRutaParadasDisponibles;
        private Button buttonRutaAsignarParada;
        private GroupBox groupBoxRutaParadasRuta;
        private Button buttonRutaBajarParada;
        private Button buttonRutaSubirParada;
        private ListBox listBoxRutaParadasDeRuta;
    }
}