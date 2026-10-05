namespace UI
{
    partial class MainUI
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            mainUIStripMenuItemInicio = new ToolStripMenuItem();
            mainUIStripMenuItemIniciarSesion = new ToolStripMenuItem();
            mainUIStripMenuItemCerrarSesion = new ToolStripMenuItem();
            mainUIStripMenuItemGestionDeUsuarios = new ToolStripMenuItem();
            mainUIStripMenuItemABMUsuarios = new ToolStripMenuItem();
            mainUIStripMenuItemDesbloqueoUsuarios = new ToolStripMenuItem();
            mainUIStripMenuItemGestionDePerfiles = new ToolStripMenuItem();
            mainUIStripMenuItemABMPerfiles = new ToolStripMenuItem();
            mainUIStripMenuItemBitacora = new ToolStripMenuItem();
            mainUIStripMenuItemConsultarBitacora = new ToolStripMenuItem();
            mainUIStripMenuItemHistorialUsuario = new ToolStripMenuItem();
            agregarIdiomaToolStripMenuItem = new ToolStripMenuItem();
            planificacionServicioToolStripMenuItem = new ToolStripMenuItem();
            gestionParadaToolStripMenuItem = new ToolStripMenuItem();
            gestionDeRutasToolStripMenuItem = new ToolStripMenuItem();
            gestionDeCronogramasToolStripMenuItem = new ToolStripMenuItem();
            auditoriaSalidasToolStripMenuItem = new ToolStripMenuItem();
            sancionesChoferToolStripMenuItem = new ToolStripMenuItem();
            choferesInternosToolStripMenuItem = new ToolStripMenuItem();
            gestionDeChoferesToolStripMenuItem = new ToolStripMenuItem();
            gestionDeInternosToolStripMenuItem = new ToolStripMenuItem();
            tallerToolStripMenuItem = new ToolStripMenuItem();
            gestionCargasCombustibleToolStripMenuItem = new ToolStripMenuItem();
            comboIdiomasGlobal = new ComboBox();
            label1 = new Label();
            gestionRevisionesToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { mainUIStripMenuItemInicio, mainUIStripMenuItemGestionDeUsuarios, mainUIStripMenuItemGestionDePerfiles, mainUIStripMenuItemBitacora, mainUIStripMenuItemHistorialUsuario, agregarIdiomaToolStripMenuItem, planificacionServicioToolStripMenuItem, choferesInternosToolStripMenuItem, tallerToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1127, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // mainUIStripMenuItemInicio
            // 
            mainUIStripMenuItemInicio.DropDownItems.AddRange(new ToolStripItem[] { mainUIStripMenuItemIniciarSesion, mainUIStripMenuItemCerrarSesion });
            mainUIStripMenuItemInicio.Name = "mainUIStripMenuItemInicio";
            mainUIStripMenuItemInicio.Size = new Size(48, 20);
            mainUIStripMenuItemInicio.Text = "Inicio";
            // 
            // mainUIStripMenuItemIniciarSesion
            // 
            mainUIStripMenuItemIniciarSesion.Name = "mainUIStripMenuItemIniciarSesion";
            mainUIStripMenuItemIniciarSesion.Size = new Size(142, 22);
            mainUIStripMenuItemIniciarSesion.Text = "Iniciar sesión";
            mainUIStripMenuItemIniciarSesion.Click += mainUIStripMenuItemIniciarSesion_Click;
            // 
            // mainUIStripMenuItemCerrarSesion
            // 
            mainUIStripMenuItemCerrarSesion.Name = "mainUIStripMenuItemCerrarSesion";
            mainUIStripMenuItemCerrarSesion.Size = new Size(142, 22);
            mainUIStripMenuItemCerrarSesion.Text = "Cerrar sesión";
            mainUIStripMenuItemCerrarSesion.Click += mainUIStripMenuItemCerrarSesion_Click;
            // 
            // mainUIStripMenuItemGestionDeUsuarios
            // 
            mainUIStripMenuItemGestionDeUsuarios.DropDownItems.AddRange(new ToolStripItem[] { mainUIStripMenuItemABMUsuarios, mainUIStripMenuItemDesbloqueoUsuarios });
            mainUIStripMenuItemGestionDeUsuarios.Name = "mainUIStripMenuItemGestionDeUsuarios";
            mainUIStripMenuItemGestionDeUsuarios.Size = new Size(122, 20);
            mainUIStripMenuItemGestionDeUsuarios.Text = "Gestión de usuarios";
            // 
            // mainUIStripMenuItemABMUsuarios
            // 
            mainUIStripMenuItemABMUsuarios.Name = "mainUIStripMenuItemABMUsuarios";
            mainUIStripMenuItemABMUsuarios.Size = new Size(200, 22);
            mainUIStripMenuItemABMUsuarios.Text = "ABM Usuarios";
            mainUIStripMenuItemABMUsuarios.Click += mainUIStripMenuItemABMUsuarios_Click;
            // 
            // mainUIStripMenuItemDesbloqueoUsuarios
            // 
            mainUIStripMenuItemDesbloqueoUsuarios.Name = "mainUIStripMenuItemDesbloqueoUsuarios";
            mainUIStripMenuItemDesbloqueoUsuarios.Size = new Size(200, 22);
            mainUIStripMenuItemDesbloqueoUsuarios.Text = "Desbloqueo de usuarios";
            mainUIStripMenuItemDesbloqueoUsuarios.Click += mainUIStripMenuItemDesbloqueoUsuarios_Click;
            // 
            // mainUIStripMenuItemGestionDePerfiles
            // 
            mainUIStripMenuItemGestionDePerfiles.DropDownItems.AddRange(new ToolStripItem[] { mainUIStripMenuItemABMPerfiles });
            mainUIStripMenuItemGestionDePerfiles.Name = "mainUIStripMenuItemGestionDePerfiles";
            mainUIStripMenuItemGestionDePerfiles.Size = new Size(116, 20);
            mainUIStripMenuItemGestionDePerfiles.Text = "Gestión de perfiles";
            // 
            // mainUIStripMenuItemABMPerfiles
            // 
            mainUIStripMenuItemABMPerfiles.Name = "mainUIStripMenuItemABMPerfiles";
            mainUIStripMenuItemABMPerfiles.Size = new Size(221, 22);
            mainUIStripMenuItemABMPerfiles.Text = "Alta y asignación de perfiles";
            mainUIStripMenuItemABMPerfiles.Click += mainUIStripMenuItemABMPerfiles_Click;
            // 
            // mainUIStripMenuItemBitacora
            // 
            mainUIStripMenuItemBitacora.DropDownItems.AddRange(new ToolStripItem[] { mainUIStripMenuItemConsultarBitacora });
            mainUIStripMenuItemBitacora.Name = "mainUIStripMenuItemBitacora";
            mainUIStripMenuItemBitacora.Size = new Size(62, 20);
            mainUIStripMenuItemBitacora.Text = "Bitácora";
            // 
            // mainUIStripMenuItemConsultarBitacora
            // 
            mainUIStripMenuItemConsultarBitacora.Name = "mainUIStripMenuItemConsultarBitacora";
            mainUIStripMenuItemConsultarBitacora.Size = new Size(171, 22);
            mainUIStripMenuItemConsultarBitacora.Text = "Consultar bitácora";
            mainUIStripMenuItemConsultarBitacora.Click += mainUIStripMenuItemConsultarBitacora_Click;
            // 
            // mainUIStripMenuItemHistorialUsuario
            // 
            mainUIStripMenuItemHistorialUsuario.Name = "mainUIStripMenuItemHistorialUsuario";
            mainUIStripMenuItemHistorialUsuario.Size = new Size(105, 20);
            mainUIStripMenuItemHistorialUsuario.Text = "Historial usuario";
            mainUIStripMenuItemHistorialUsuario.Click += mainUIStripMenuItemHistorialUsuario_Click;
            // 
            // agregarIdiomaToolStripMenuItem
            // 
            agregarIdiomaToolStripMenuItem.Name = "agregarIdiomaToolStripMenuItem";
            agregarIdiomaToolStripMenuItem.Size = new Size(101, 20);
            agregarIdiomaToolStripMenuItem.Text = "Gestión de idiomas";
            agregarIdiomaToolStripMenuItem.Click += agregarIdiomaToolStripMenuItem_Click;
            // 
            // planificacionServicioToolStripMenuItem
            // 
            planificacionServicioToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { gestionParadaToolStripMenuItem, gestionDeRutasToolStripMenuItem, gestionDeCronogramasToolStripMenuItem, auditoriaSalidasToolStripMenuItem, sancionesChoferToolStripMenuItem });
            planificacionServicioToolStripMenuItem.Name = "planificacionServicioToolStripMenuItem";
            planificacionServicioToolStripMenuItem.Size = new Size(146, 20);
            planificacionServicioToolStripMenuItem.Text = "Planificación de servicio";
            // 
            // gestionParadaToolStripMenuItem
            // 
            gestionParadaToolStripMenuItem.Name = "gestionParadaToolStripMenuItem";
            gestionParadaToolStripMenuItem.Size = new Size(203, 22);
            gestionParadaToolStripMenuItem.Text = "Gestión de paradas";
            gestionParadaToolStripMenuItem.Click += gestionParadaToolStripMenuItem_Click;
            // 
            // gestionDeRutasToolStripMenuItem
            // 
            gestionDeRutasToolStripMenuItem.Name = "gestionDeRutasToolStripMenuItem";
            gestionDeRutasToolStripMenuItem.Size = new Size(203, 22);
            gestionDeRutasToolStripMenuItem.Text = "Gestión de rutas";
            gestionDeRutasToolStripMenuItem.Click += gestionDeRutasToolStripMenuItem_Click;
            // 
            // gestionDeCronogramasToolStripMenuItem
            // 
            gestionDeCronogramasToolStripMenuItem.Name = "gestionDeCronogramasToolStripMenuItem";
            gestionDeCronogramasToolStripMenuItem.Size = new Size(203, 22);
            gestionDeCronogramasToolStripMenuItem.Text = "Gestión de cronogramas";
            gestionDeCronogramasToolStripMenuItem.Click += gestionDeCronogramasToolStripMenuItem_Click;
            // 
            // auditoriaSalidasToolStripMenuItem
            // 
            auditoriaSalidasToolStripMenuItem.Name = "auditoriaSalidasToolStripMenuItem";
            auditoriaSalidasToolStripMenuItem.Size = new Size(203, 22);
            auditoriaSalidasToolStripMenuItem.Text = "Auditoría de salidas";
            auditoriaSalidasToolStripMenuItem.Click += auditoriaSalidasToolStripMenuItem_Click;
            //
            // sancionesChoferToolStripMenuItem
            //
            sancionesChoferToolStripMenuItem.Name = "sancionesChoferToolStripMenuItem";
            sancionesChoferToolStripMenuItem.Size = new Size(203, 22);
            sancionesChoferToolStripMenuItem.Text = "Sanciones por chofer";
            sancionesChoferToolStripMenuItem.Click += sancionesChoferToolStripMenuItem_Click;
            //
            // choferesInternosToolStripMenuItem
            // 
            choferesInternosToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { gestionDeChoferesToolStripMenuItem, gestionDeInternosToolStripMenuItem });
            choferesInternosToolStripMenuItem.Name = "choferesInternosToolStripMenuItem";
            choferesInternosToolStripMenuItem.Size = new Size(114, 20);
            choferesInternosToolStripMenuItem.Text = "Choferes/Internos";
            // 
            // gestionDeChoferesToolStripMenuItem
            // 
            gestionDeChoferesToolStripMenuItem.Name = "gestionDeChoferesToolStripMenuItem";
            gestionDeChoferesToolStripMenuItem.Size = new Size(178, 22);
            gestionDeChoferesToolStripMenuItem.Text = "Gestión de choferes";
            gestionDeChoferesToolStripMenuItem.Click += gestionDeChoferesToolStripMenuItem_Click;
            // 
            // gestionDeInternosToolStripMenuItem
            // 
            gestionDeInternosToolStripMenuItem.Name = "gestionDeInternosToolStripMenuItem";
            gestionDeInternosToolStripMenuItem.Size = new Size(178, 22);
            gestionDeInternosToolStripMenuItem.Text = "Gestión de internos";
            gestionDeInternosToolStripMenuItem.Click += gestionDeInternosToolStripMenuItem_Click;
            // 
            // tallerToolStripMenuItem
            // 
            tallerToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { gestionCargasCombustibleToolStripMenuItem, gestionRevisionesToolStripMenuItem });
            tallerToolStripMenuItem.Name = "tallerToolStripMenuItem";
            tallerToolStripMenuItem.Size = new Size(47, 20);
            tallerToolStripMenuItem.Text = "Taller";
            // 
            // gestionCargasCombustibleToolStripMenuItem
            // 
            gestionCargasCombustibleToolStripMenuItem.Name = "gestionCargasCombustibleToolStripMenuItem";
            gestionCargasCombustibleToolStripMenuItem.Size = new Size(252, 22);
            gestionCargasCombustibleToolStripMenuItem.Text = "Gestion de cargas de combustible";
            gestionCargasCombustibleToolStripMenuItem.Click += gestionCargasCombustibleToolStripMenuItem_Click;
            // 
            // comboIdiomasGlobal
            // 
            comboIdiomasGlobal.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            comboIdiomasGlobal.FormattingEnabled = true;
            comboIdiomasGlobal.Location = new Point(1050, 38);
            comboIdiomasGlobal.Name = "comboIdiomasGlobal";
            comboIdiomasGlobal.Size = new Size(65, 23);
            comboIdiomasGlobal.TabIndex = 1;
            comboIdiomasGlobal.SelectedIndexChanged += ComboIdiomasGlobal_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new Point(1050, 20);
            label1.Name = "label1";
            label1.Size = new Size(44, 15);
            label1.TabIndex = 2;
            label1.Text = "Idioma";
            // 
            // gestionRevisionesToolStripMenuItem
            // 
            gestionRevisionesToolStripMenuItem.Name = "gestionRevisionesToolStripMenuItem";
            gestionRevisionesToolStripMenuItem.Size = new Size(252, 22);
            gestionRevisionesToolStripMenuItem.Text = "Gestion de revisiones";
            gestionRevisionesToolStripMenuItem.Click += gestionRevisionesToolStripMenuItem_Click;
            // 
            // MainUI
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(1127, 564);
            Controls.Add(label1);
            Controls.Add(comboIdiomasGlobal);
            Controls.Add(menuStrip1);
            Font = new Font("Segoe UI", 9F);
            MainMenuStrip = menuStrip1;
            Name = "MainUI";
            Text = "Sistema de gestión";
            Load += Form1_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem mainUIStripMenuItemInicio;
        private ToolStripMenuItem mainUIStripMenuItemIniciarSesion;
        private ToolStripMenuItem mainUIStripMenuItemCerrarSesion;
        private ToolStripMenuItem mainUIStripMenuItemGestionDeUsuarios;
        private ToolStripMenuItem mainUIStripMenuItemABMUsuarios;
        private ToolStripMenuItem mainUIStripMenuItemDesbloqueoUsuarios;
        private ToolStripMenuItem mainUIStripMenuItemGestionDePerfiles;
        private ToolStripMenuItem mainUIStripMenuItemABMPerfiles;
        private ToolStripMenuItem mainUIStripMenuItemBitacora;
        private ToolStripMenuItem mainUIStripMenuItemConsultarBitacora;
        private ComboBox comboIdiomasGlobal;
        private Label label1;
        private ToolStripMenuItem mainUIStripMenuItemHistorialUsuario;
        private ToolStripMenuItem agregarIdiomaToolStripMenuItem;
        private ToolStripMenuItem planificacionServicioToolStripMenuItem;
        private ToolStripMenuItem gestionParadaToolStripMenuItem;
        private ToolStripMenuItem gestionDeRutasToolStripMenuItem;
        private ToolStripMenuItem choferesInternosToolStripMenuItem;
        private ToolStripMenuItem gestionDeChoferesToolStripMenuItem;
        private ToolStripMenuItem gestionDeInternosToolStripMenuItem;
        private ToolStripMenuItem gestionDeCronogramasToolStripMenuItem;
        private ToolStripMenuItem auditoriaSalidasToolStripMenuItem;
        private ToolStripMenuItem sancionesChoferToolStripMenuItem;
        private ToolStripMenuItem tallerToolStripMenuItem;
        private ToolStripMenuItem gestionCargasCombustibleToolStripMenuItem;
        private ToolStripMenuItem gestionRevisionesToolStripMenuItem;
    }
}
