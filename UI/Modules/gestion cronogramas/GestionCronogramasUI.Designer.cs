namespace UI.Modules.gestion_cronogramas
{
    partial class GestionCronogramasUI
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
            groupBoxGestionCronogramasSeccionCronogramas = new GroupBox();
            groupBoxGestionCronogramasSeccionDetalle = new GroupBox();
            textBoxGestionCronogramaDetalleRuta = new TextBox();
            labelGestioCronogramaDetalleRuta = new Label();
            textBoxGestionCronogramaDetalleDescanso = new TextBox();
            labelGestioCronogramaDetalleDescanso = new Label();
            textBoxGestionCronogramaDetalleFrecuencia = new TextBox();
            labelGestioCronogramaDetalleFrecuencia = new Label();
            textBoxGestionCronogramaDetalleHoraFinal = new TextBox();
            labelGestioCronogramaDetalleHoraFin = new Label();
            textBoxGestionCronogramaDetalleFecha = new TextBox();
            labelGestioCronogramaDetalleFecha = new Label();
            textBoxGestionCronogramaDetalleHoraInicio = new TextBox();
            labelGestioCronogramaDetalleHoraInicio = new Label();
            textBoxGestionCronogramaDetalleId = new TextBox();
            labelGestionCronogramaDetalleId = new Label();
            textBoxGestionCronogramaDetalleDescripcion = new TextBox();
            labelGestionCronogramaDetalleDescripcion = new Label();
            buttonGestionCronogramasEliminarCronograma = new Button();
            buttonGestionCronogramasModificarCronograma = new Button();
            buttonGestionCronogramaAgregarCronograma = new Button();
            listBoxCronogramas = new ListBox();
            groupBoxGestionCronogramasSeccionAsignacionSalidas = new GroupBox();
            buttonGestionCronogramasDesasignarInterno = new Button();
            buttonGestionCronogramasDesasignarChofer = new Button();
            buttonGestionCronogramasSuspenderSalida = new Button();
            buttonGestionCronogramasAsignarInterno = new Button();
            buttonGestionCronogramasAsignarChofer = new Button();
            labelGestionCronogramasInterno = new Label();
            comboBoxGestionCronogramasInterno = new ComboBox();
            labelGestionCronogramasChofer = new Label();
            comboBoxGestionCronogramasChofer = new ComboBox();
            buttonGestionCronogramasGenerarSalidas = new Button();
            dataGridViewAsignacionSalidas = new DataGridView();
            groupBoxGestionCronogramasSeccionCronogramas.SuspendLayout();
            groupBoxGestionCronogramasSeccionDetalle.SuspendLayout();
            groupBoxGestionCronogramasSeccionAsignacionSalidas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewAsignacionSalidas).BeginInit();
            SuspendLayout();
            // 
            // groupBoxGestionCronogramasSeccionCronogramas
            // 
            groupBoxGestionCronogramasSeccionCronogramas.Controls.Add(groupBoxGestionCronogramasSeccionDetalle);
            groupBoxGestionCronogramasSeccionCronogramas.Controls.Add(buttonGestionCronogramasEliminarCronograma);
            groupBoxGestionCronogramasSeccionCronogramas.Controls.Add(buttonGestionCronogramasModificarCronograma);
            groupBoxGestionCronogramasSeccionCronogramas.Controls.Add(buttonGestionCronogramaAgregarCronograma);
            groupBoxGestionCronogramasSeccionCronogramas.Controls.Add(listBoxCronogramas);
            groupBoxGestionCronogramasSeccionCronogramas.Location = new Point(12, 43);
            groupBoxGestionCronogramasSeccionCronogramas.Name = "groupBoxGestionCronogramasSeccionCronogramas";
            groupBoxGestionCronogramasSeccionCronogramas.Size = new Size(416, 497);
            groupBoxGestionCronogramasSeccionCronogramas.TabIndex = 0;
            groupBoxGestionCronogramasSeccionCronogramas.TabStop = false;
            groupBoxGestionCronogramasSeccionCronogramas.Text = "Cronogramas";
            // 
            // groupBoxGestionCronogramasSeccionDetalle
            // 
            groupBoxGestionCronogramasSeccionDetalle.Controls.Add(textBoxGestionCronogramaDetalleRuta);
            groupBoxGestionCronogramasSeccionDetalle.Controls.Add(labelGestioCronogramaDetalleRuta);
            groupBoxGestionCronogramasSeccionDetalle.Controls.Add(textBoxGestionCronogramaDetalleDescanso);
            groupBoxGestionCronogramasSeccionDetalle.Controls.Add(labelGestioCronogramaDetalleDescanso);
            groupBoxGestionCronogramasSeccionDetalle.Controls.Add(textBoxGestionCronogramaDetalleFrecuencia);
            groupBoxGestionCronogramasSeccionDetalle.Controls.Add(labelGestioCronogramaDetalleFrecuencia);
            groupBoxGestionCronogramasSeccionDetalle.Controls.Add(textBoxGestionCronogramaDetalleHoraFinal);
            groupBoxGestionCronogramasSeccionDetalle.Controls.Add(labelGestioCronogramaDetalleHoraFin);
            groupBoxGestionCronogramasSeccionDetalle.Controls.Add(textBoxGestionCronogramaDetalleFecha);
            groupBoxGestionCronogramasSeccionDetalle.Controls.Add(labelGestioCronogramaDetalleFecha);
            groupBoxGestionCronogramasSeccionDetalle.Controls.Add(textBoxGestionCronogramaDetalleHoraInicio);
            groupBoxGestionCronogramasSeccionDetalle.Controls.Add(labelGestioCronogramaDetalleHoraInicio);
            groupBoxGestionCronogramasSeccionDetalle.Controls.Add(textBoxGestionCronogramaDetalleId);
            groupBoxGestionCronogramasSeccionDetalle.Controls.Add(labelGestionCronogramaDetalleId);
            groupBoxGestionCronogramasSeccionDetalle.Controls.Add(textBoxGestionCronogramaDetalleDescripcion);
            groupBoxGestionCronogramasSeccionDetalle.Controls.Add(labelGestionCronogramaDetalleDescripcion);
            groupBoxGestionCronogramasSeccionDetalle.Location = new Point(10, 182);
            groupBoxGestionCronogramasSeccionDetalle.Name = "groupBoxGestionCronogramasSeccionDetalle";
            groupBoxGestionCronogramasSeccionDetalle.Size = new Size(400, 309);
            groupBoxGestionCronogramasSeccionDetalle.TabIndex = 4;
            groupBoxGestionCronogramasSeccionDetalle.TabStop = false;
            groupBoxGestionCronogramasSeccionDetalle.Text = "Detalle del cronograma seleccionado";
            // 
            // textBoxGestionCronogramaDetalleRuta
            // 
            textBoxGestionCronogramaDetalleRuta.Location = new Point(203, 66);
            textBoxGestionCronogramaDetalleRuta.Name = "textBoxGestionCronogramaDetalleRuta";
            textBoxGestionCronogramaDetalleRuta.ReadOnly = true;
            textBoxGestionCronogramaDetalleRuta.Size = new Size(191, 23);
            textBoxGestionCronogramaDetalleRuta.TabIndex = 15;
            // 
            // labelGestioCronogramaDetalleRuta
            // 
            labelGestioCronogramaDetalleRuta.AutoSize = true;
            labelGestioCronogramaDetalleRuta.Location = new Point(203, 48);
            labelGestioCronogramaDetalleRuta.Name = "labelGestioCronogramaDetalleRuta";
            labelGestioCronogramaDetalleRuta.Size = new Size(31, 15);
            labelGestioCronogramaDetalleRuta.TabIndex = 14;
            labelGestioCronogramaDetalleRuta.Text = "Ruta";
            // 
            // textBoxGestionCronogramaDetalleDescanso
            // 
            textBoxGestionCronogramaDetalleDescanso.Location = new Point(316, 237);
            textBoxGestionCronogramaDetalleDescanso.Name = "textBoxGestionCronogramaDetalleDescanso";
            textBoxGestionCronogramaDetalleDescanso.ReadOnly = true;
            textBoxGestionCronogramaDetalleDescanso.Size = new Size(78, 23);
            textBoxGestionCronogramaDetalleDescanso.TabIndex = 13;
            // 
            // labelGestioCronogramaDetalleDescanso
            // 
            labelGestioCronogramaDetalleDescanso.AutoSize = true;
            labelGestioCronogramaDetalleDescanso.Location = new Point(203, 240);
            labelGestioCronogramaDetalleDescanso.Name = "labelGestioCronogramaDetalleDescanso";
            labelGestioCronogramaDetalleDescanso.Size = new Size(84, 15);
            labelGestioCronogramaDetalleDescanso.TabIndex = 12;
            labelGestioCronogramaDetalleDescanso.Text = "Min. Descanso";
            // 
            // textBoxGestionCronogramaDetalleFrecuencia
            // 
            textBoxGestionCronogramaDetalleFrecuencia.Location = new Point(316, 208);
            textBoxGestionCronogramaDetalleFrecuencia.Name = "textBoxGestionCronogramaDetalleFrecuencia";
            textBoxGestionCronogramaDetalleFrecuencia.ReadOnly = true;
            textBoxGestionCronogramaDetalleFrecuencia.Size = new Size(78, 23);
            textBoxGestionCronogramaDetalleFrecuencia.TabIndex = 11;
            // 
            // labelGestioCronogramaDetalleFrecuencia
            // 
            labelGestioCronogramaDetalleFrecuencia.AutoSize = true;
            labelGestioCronogramaDetalleFrecuencia.Location = new Point(203, 211);
            labelGestioCronogramaDetalleFrecuencia.Name = "labelGestioCronogramaDetalleFrecuencia";
            labelGestioCronogramaDetalleFrecuencia.Size = new Size(91, 15);
            labelGestioCronogramaDetalleFrecuencia.TabIndex = 10;
            labelGestioCronogramaDetalleFrecuencia.Text = "Min. Frecuencia";
            // 
            // textBoxGestionCronogramaDetalleHoraFinal
            // 
            textBoxGestionCronogramaDetalleHoraFinal.Location = new Point(316, 177);
            textBoxGestionCronogramaDetalleHoraFinal.Name = "textBoxGestionCronogramaDetalleHoraFinal";
            textBoxGestionCronogramaDetalleHoraFinal.ReadOnly = true;
            textBoxGestionCronogramaDetalleHoraFinal.Size = new Size(78, 23);
            textBoxGestionCronogramaDetalleHoraFinal.TabIndex = 9;
            // 
            // labelGestioCronogramaDetalleHoraFin
            // 
            labelGestioCronogramaDetalleHoraFin.AutoSize = true;
            labelGestioCronogramaDetalleHoraFin.Location = new Point(203, 180);
            labelGestioCronogramaDetalleHoraFin.Name = "labelGestioCronogramaDetalleHoraFin";
            labelGestioCronogramaDetalleHoraFin.Size = new Size(66, 15);
            labelGestioCronogramaDetalleHoraFin.TabIndex = 8;
            labelGestioCronogramaDetalleHoraFin.Text = "Hora de fin";
            // 
            // textBoxGestionCronogramaDetalleFecha
            // 
            textBoxGestionCronogramaDetalleFecha.Location = new Point(203, 118);
            textBoxGestionCronogramaDetalleFecha.Name = "textBoxGestionCronogramaDetalleFecha";
            textBoxGestionCronogramaDetalleFecha.ReadOnly = true;
            textBoxGestionCronogramaDetalleFecha.Size = new Size(191, 23);
            textBoxGestionCronogramaDetalleFecha.TabIndex = 7;
            // 
            // labelGestioCronogramaDetalleFecha
            // 
            labelGestioCronogramaDetalleFecha.AutoSize = true;
            labelGestioCronogramaDetalleFecha.Location = new Point(203, 100);
            labelGestioCronogramaDetalleFecha.Name = "labelGestioCronogramaDetalleFecha";
            labelGestioCronogramaDetalleFecha.Size = new Size(93, 15);
            labelGestioCronogramaDetalleFecha.TabIndex = 6;
            labelGestioCronogramaDetalleFecha.Text = "Fecha de validez";
            // 
            // textBoxGestionCronogramaDetalleHoraInicio
            // 
            textBoxGestionCronogramaDetalleHoraInicio.Location = new Point(316, 147);
            textBoxGestionCronogramaDetalleHoraInicio.Name = "textBoxGestionCronogramaDetalleHoraInicio";
            textBoxGestionCronogramaDetalleHoraInicio.ReadOnly = true;
            textBoxGestionCronogramaDetalleHoraInicio.Size = new Size(78, 23);
            textBoxGestionCronogramaDetalleHoraInicio.TabIndex = 5;
            // 
            // labelGestioCronogramaDetalleHoraInicio
            // 
            labelGestioCronogramaDetalleHoraInicio.AutoSize = true;
            labelGestioCronogramaDetalleHoraInicio.Location = new Point(203, 150);
            labelGestioCronogramaDetalleHoraInicio.Name = "labelGestioCronogramaDetalleHoraInicio";
            labelGestioCronogramaDetalleHoraInicio.Size = new Size(81, 15);
            labelGestioCronogramaDetalleHoraInicio.TabIndex = 4;
            labelGestioCronogramaDetalleHoraInicio.Text = "Hora de inicio";
            // 
            // textBoxGestionCronogramaDetalleId
            // 
            textBoxGestionCronogramaDetalleId.Location = new Point(119, 22);
            textBoxGestionCronogramaDetalleId.Name = "textBoxGestionCronogramaDetalleId";
            textBoxGestionCronogramaDetalleId.ReadOnly = true;
            textBoxGestionCronogramaDetalleId.Size = new Size(78, 23);
            textBoxGestionCronogramaDetalleId.TabIndex = 3;
            // 
            // labelGestionCronogramaDetalleId
            // 
            labelGestionCronogramaDetalleId.AutoSize = true;
            labelGestionCronogramaDetalleId.Location = new Point(6, 25);
            labelGestionCronogramaDetalleId.Name = "labelGestionCronogramaDetalleId";
            labelGestionCronogramaDetalleId.Size = new Size(104, 15);
            labelGestionCronogramaDetalleId.TabIndex = 2;
            labelGestionCronogramaDetalleId.Text = "Id del cronograma";
            // 
            // textBoxGestionCronogramaDetalleDescripcion
            // 
            textBoxGestionCronogramaDetalleDescripcion.Location = new Point(6, 66);
            textBoxGestionCronogramaDetalleDescripcion.Multiline = true;
            textBoxGestionCronogramaDetalleDescripcion.Name = "textBoxGestionCronogramaDetalleDescripcion";
            textBoxGestionCronogramaDetalleDescripcion.ReadOnly = true;
            textBoxGestionCronogramaDetalleDescripcion.Size = new Size(191, 237);
            textBoxGestionCronogramaDetalleDescripcion.TabIndex = 1;
            // 
            // labelGestionCronogramaDetalleDescripcion
            // 
            labelGestionCronogramaDetalleDescripcion.AutoSize = true;
            labelGestionCronogramaDetalleDescripcion.Location = new Point(6, 48);
            labelGestionCronogramaDetalleDescripcion.Name = "labelGestionCronogramaDetalleDescripcion";
            labelGestionCronogramaDetalleDescripcion.Size = new Size(69, 15);
            labelGestionCronogramaDetalleDescripcion.TabIndex = 0;
            labelGestionCronogramaDetalleDescripcion.Text = "Descripción";
            // 
            // buttonGestionCronogramasEliminarCronograma
            // 
            buttonGestionCronogramasEliminarCronograma.Location = new Point(6, 126);
            buttonGestionCronogramasEliminarCronograma.Name = "buttonGestionCronogramasEliminarCronograma";
            buttonGestionCronogramasEliminarCronograma.Size = new Size(130, 46);
            buttonGestionCronogramasEliminarCronograma.TabIndex = 3;
            buttonGestionCronogramasEliminarCronograma.Text = "Eliminar cronograma";
            buttonGestionCronogramasEliminarCronograma.UseVisualStyleBackColor = true;
            buttonGestionCronogramasEliminarCronograma.Click += buttonGestionCronogramasEliminarCronograma_Click;
            // 
            // buttonGestionCronogramasModificarCronograma
            // 
            buttonGestionCronogramasModificarCronograma.Location = new Point(6, 74);
            buttonGestionCronogramasModificarCronograma.Name = "buttonGestionCronogramasModificarCronograma";
            buttonGestionCronogramasModificarCronograma.Size = new Size(130, 46);
            buttonGestionCronogramasModificarCronograma.TabIndex = 2;
            buttonGestionCronogramasModificarCronograma.Text = "Modificar cronograma";
            buttonGestionCronogramasModificarCronograma.UseVisualStyleBackColor = true;
            buttonGestionCronogramasModificarCronograma.Click += buttonGestionCronogramasModificarCronograma_Click;
            // 
            // buttonGestionCronogramaAgregarCronograma
            // 
            buttonGestionCronogramaAgregarCronograma.Location = new Point(6, 22);
            buttonGestionCronogramaAgregarCronograma.Name = "buttonGestionCronogramaAgregarCronograma";
            buttonGestionCronogramaAgregarCronograma.Size = new Size(130, 46);
            buttonGestionCronogramaAgregarCronograma.TabIndex = 1;
            buttonGestionCronogramaAgregarCronograma.Text = "Agregar cronograma";
            buttonGestionCronogramaAgregarCronograma.UseVisualStyleBackColor = true;
            buttonGestionCronogramaAgregarCronograma.Click += buttonGestionCronogramaAgregarCronograma_Click;
            // 
            // listBoxCronogramas
            // 
            listBoxCronogramas.FormattingEnabled = true;
            listBoxCronogramas.ItemHeight = 15;
            listBoxCronogramas.Location = new Point(158, 22);
            listBoxCronogramas.Name = "listBoxCronogramas";
            listBoxCronogramas.Size = new Size(248, 154);
            listBoxCronogramas.TabIndex = 0;
            listBoxCronogramas.SelectedIndexChanged += listBoxCronogramas_SelectedIndexChanged;
            // 
            // groupBoxGestionCronogramasSeccionAsignacionSalidas
            // 
            groupBoxGestionCronogramasSeccionAsignacionSalidas.Controls.Add(buttonGestionCronogramasDesasignarInterno);
            groupBoxGestionCronogramasSeccionAsignacionSalidas.Controls.Add(buttonGestionCronogramasDesasignarChofer);
            groupBoxGestionCronogramasSeccionAsignacionSalidas.Controls.Add(buttonGestionCronogramasSuspenderSalida);
            groupBoxGestionCronogramasSeccionAsignacionSalidas.Controls.Add(buttonGestionCronogramasAsignarInterno);
            groupBoxGestionCronogramasSeccionAsignacionSalidas.Controls.Add(buttonGestionCronogramasAsignarChofer);
            groupBoxGestionCronogramasSeccionAsignacionSalidas.Controls.Add(labelGestionCronogramasInterno);
            groupBoxGestionCronogramasSeccionAsignacionSalidas.Controls.Add(comboBoxGestionCronogramasInterno);
            groupBoxGestionCronogramasSeccionAsignacionSalidas.Controls.Add(labelGestionCronogramasChofer);
            groupBoxGestionCronogramasSeccionAsignacionSalidas.Controls.Add(comboBoxGestionCronogramasChofer);
            groupBoxGestionCronogramasSeccionAsignacionSalidas.Controls.Add(buttonGestionCronogramasGenerarSalidas);
            groupBoxGestionCronogramasSeccionAsignacionSalidas.Controls.Add(dataGridViewAsignacionSalidas);
            groupBoxGestionCronogramasSeccionAsignacionSalidas.Location = new Point(434, 43);
            groupBoxGestionCronogramasSeccionAsignacionSalidas.Name = "groupBoxGestionCronogramasSeccionAsignacionSalidas";
            groupBoxGestionCronogramasSeccionAsignacionSalidas.Size = new Size(697, 497);
            groupBoxGestionCronogramasSeccionAsignacionSalidas.TabIndex = 1;
            groupBoxGestionCronogramasSeccionAsignacionSalidas.TabStop = false;
            groupBoxGestionCronogramasSeccionAsignacionSalidas.Text = "Asignación de salidas";
            // 
            // buttonGestionCronogramasDesasignarInterno
            // 
            buttonGestionCronogramasDesasignarInterno.Font = new Font("Segoe UI", 8F);
            buttonGestionCronogramasDesasignarInterno.Location = new Point(456, 329);
            buttonGestionCronogramasDesasignarInterno.Name = "buttonGestionCronogramasDesasignarInterno";
            buttonGestionCronogramasDesasignarInterno.Size = new Size(108, 36);
            buttonGestionCronogramasDesasignarInterno.TabIndex = 12;
            buttonGestionCronogramasDesasignarInterno.Text = "Desasignar interno";
            buttonGestionCronogramasDesasignarInterno.UseVisualStyleBackColor = true;
            buttonGestionCronogramasDesasignarInterno.Click += buttonGestionCronogramasDesasignarInterno_Click;
            // 
            // buttonGestionCronogramasDesasignarChofer
            // 
            buttonGestionCronogramasDesasignarChofer.Font = new Font("Segoe UI", 8F);
            buttonGestionCronogramasDesasignarChofer.Location = new Point(306, 329);
            buttonGestionCronogramasDesasignarChofer.Name = "buttonGestionCronogramasDesasignarChofer";
            buttonGestionCronogramasDesasignarChofer.Size = new Size(108, 36);
            buttonGestionCronogramasDesasignarChofer.TabIndex = 11;
            buttonGestionCronogramasDesasignarChofer.Text = "Desasignar chofer";
            buttonGestionCronogramasDesasignarChofer.UseVisualStyleBackColor = true;
            buttonGestionCronogramasDesasignarChofer.Click += buttonGestionCronogramasDesasignarChofer_Click;
            // 
            // buttonGestionCronogramasSuspenderSalida
            // 
            buttonGestionCronogramasSuspenderSalida.Location = new Point(156, 329);
            buttonGestionCronogramasSuspenderSalida.Name = "buttonGestionCronogramasSuspenderSalida";
            buttonGestionCronogramasSuspenderSalida.Size = new Size(108, 36);
            buttonGestionCronogramasSuspenderSalida.TabIndex = 10;
            buttonGestionCronogramasSuspenderSalida.Text = "Suspender salida";
            buttonGestionCronogramasSuspenderSalida.UseVisualStyleBackColor = true;
            buttonGestionCronogramasSuspenderSalida.Click += buttonGestionCronogramasSuspenderSalida_Click;
            // 
            // buttonGestionCronogramasAsignarInterno
            // 
            buttonGestionCronogramasAsignarInterno.Location = new Point(225, 427);
            buttonGestionCronogramasAsignarInterno.Name = "buttonGestionCronogramasAsignarInterno";
            buttonGestionCronogramasAsignarInterno.Size = new Size(189, 35);
            buttonGestionCronogramasAsignarInterno.TabIndex = 8;
            buttonGestionCronogramasAsignarInterno.Text = "Asignar interno";
            buttonGestionCronogramasAsignarInterno.UseVisualStyleBackColor = true;
            buttonGestionCronogramasAsignarInterno.Click += buttonGestionCronogramasAsignarInterno_Click;
            // 
            // buttonGestionCronogramasAsignarChofer
            // 
            buttonGestionCronogramasAsignarChofer.Location = new Point(6, 425);
            buttonGestionCronogramasAsignarChofer.Name = "buttonGestionCronogramasAsignarChofer";
            buttonGestionCronogramasAsignarChofer.Size = new Size(189, 37);
            buttonGestionCronogramasAsignarChofer.TabIndex = 7;
            buttonGestionCronogramasAsignarChofer.Text = "Asignar chofer";
            buttonGestionCronogramasAsignarChofer.UseVisualStyleBackColor = true;
            buttonGestionCronogramasAsignarChofer.Click += buttonGestionCronogramasAsignarChofer_Click;
            // 
            // labelGestionCronogramasInterno
            // 
            labelGestionCronogramasInterno.AutoSize = true;
            labelGestionCronogramasInterno.Location = new Point(225, 375);
            labelGestionCronogramasInterno.Name = "labelGestionCronogramasInterno";
            labelGestionCronogramasInterno.Size = new Size(45, 15);
            labelGestionCronogramasInterno.TabIndex = 6;
            labelGestionCronogramasInterno.Text = "Interno";
            // 
            // comboBoxGestionCronogramasInterno
            // 
            comboBoxGestionCronogramasInterno.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxGestionCronogramasInterno.FormattingEnabled = true;
            comboBoxGestionCronogramasInterno.Location = new Point(225, 398);
            comboBoxGestionCronogramasInterno.Name = "comboBoxGestionCronogramasInterno";
            comboBoxGestionCronogramasInterno.Size = new Size(189, 23);
            comboBoxGestionCronogramasInterno.TabIndex = 5;
            // 
            // labelGestionCronogramasChofer
            // 
            labelGestionCronogramasChofer.AutoSize = true;
            labelGestionCronogramasChofer.Location = new Point(6, 375);
            labelGestionCronogramasChofer.Name = "labelGestionCronogramasChofer";
            labelGestionCronogramasChofer.Size = new Size(43, 15);
            labelGestionCronogramasChofer.TabIndex = 4;
            labelGestionCronogramasChofer.Text = "Chofer";
            // 
            // comboBoxGestionCronogramasChofer
            // 
            comboBoxGestionCronogramasChofer.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxGestionCronogramasChofer.FormattingEnabled = true;
            comboBoxGestionCronogramasChofer.Location = new Point(6, 398);
            comboBoxGestionCronogramasChofer.Name = "comboBoxGestionCronogramasChofer";
            comboBoxGestionCronogramasChofer.Size = new Size(189, 23);
            comboBoxGestionCronogramasChofer.TabIndex = 3;
            // 
            // buttonGestionCronogramasGenerarSalidas
            // 
            buttonGestionCronogramasGenerarSalidas.Location = new Point(6, 329);
            buttonGestionCronogramasGenerarSalidas.Name = "buttonGestionCronogramasGenerarSalidas";
            buttonGestionCronogramasGenerarSalidas.Size = new Size(108, 36);
            buttonGestionCronogramasGenerarSalidas.TabIndex = 2;
            buttonGestionCronogramasGenerarSalidas.Text = "Generar salidas";
            buttonGestionCronogramasGenerarSalidas.UseVisualStyleBackColor = true;
            buttonGestionCronogramasGenerarSalidas.Click += buttonGestionCronogramasGenerarSalidas_Click;
            // 
            // dataGridViewAsignacionSalidas
            // 
            dataGridViewAsignacionSalidas.AllowUserToAddRows = false;
            dataGridViewAsignacionSalidas.AllowUserToDeleteRows = false;
            dataGridViewAsignacionSalidas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewAsignacionSalidas.Location = new Point(6, 22);
            dataGridViewAsignacionSalidas.MultiSelect = false;
            dataGridViewAsignacionSalidas.Name = "dataGridViewAsignacionSalidas";
            dataGridViewAsignacionSalidas.ReadOnly = true;
            dataGridViewAsignacionSalidas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewAsignacionSalidas.Size = new Size(685, 301);
            dataGridViewAsignacionSalidas.TabIndex = 0;
            // 
            // GestionCronogramasUI
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1143, 552);
            Controls.Add(groupBoxGestionCronogramasSeccionAsignacionSalidas);
            Controls.Add(groupBoxGestionCronogramasSeccionCronogramas);
            FormBorderStyle = FormBorderStyle.None;
            Name = "GestionCronogramasUI";
            Text = "GestionCronogramasUI";
            WindowState = FormWindowState.Maximized;
            groupBoxGestionCronogramasSeccionCronogramas.ResumeLayout(false);
            groupBoxGestionCronogramasSeccionDetalle.ResumeLayout(false);
            groupBoxGestionCronogramasSeccionDetalle.PerformLayout();
            groupBoxGestionCronogramasSeccionAsignacionSalidas.ResumeLayout(false);
            groupBoxGestionCronogramasSeccionAsignacionSalidas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewAsignacionSalidas).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBoxGestionCronogramasSeccionCronogramas;
        private GroupBox groupBoxGestionCronogramasSeccionDetalle;
        private Button buttonGestionCronogramasEliminarCronograma;
        private Button buttonGestionCronogramasModificarCronograma;
        private Button buttonGestionCronogramaAgregarCronograma;
        private ListBox listBoxCronogramas;
        private GroupBox groupBoxGestionCronogramasSeccionAsignacionSalidas;
        private DataGridView dataGridViewAsignacionSalidas;
        private Label labelGestionCronogramasInterno;
        private ComboBox comboBoxGestionCronogramasInterno;
        private Label labelGestionCronogramasChofer;
        private ComboBox comboBoxGestionCronogramasChofer;
        private Button buttonGestionCronogramasGenerarSalidas;
        private Button buttonGestionCronogramasAsignarChofer;
        private Button buttonGestionCronogramasSuspenderSalida;
        private Button buttonGestionCronogramasAsignarInterno;
        private Button buttonGestionCronogramasDesasignarInterno;
        private Button buttonGestionCronogramasDesasignarChofer;
        private Label labelGestionCronogramaDetalleDescripcion;
        private Label labelGestionCronogramaDetalleId;
        private TextBox textBoxGestionCronogramaDetalleDescripcion;
        private TextBox textBoxGestionCronogramaDetalleId;
        private TextBox textBoxGestionCronogramaDetalleHoraFinal;
        private Label labelGestioCronogramaDetalleHoraFin;
        private TextBox textBoxGestionCronogramaDetalleFecha;
        private Label labelGestioCronogramaDetalleFecha;
        private TextBox textBoxGestionCronogramaDetalleHoraInicio;
        private Label labelGestioCronogramaDetalleHoraInicio;
        private TextBox textBoxGestionCronogramaDetalleDescanso;
        private Label labelGestioCronogramaDetalleDescanso;
        private TextBox textBoxGestionCronogramaDetalleFrecuencia;
        private Label labelGestioCronogramaDetalleFrecuencia;
        private TextBox textBoxGestionCronogramaDetalleRuta;
        private Label labelGestioCronogramaDetalleRuta;
    }
}