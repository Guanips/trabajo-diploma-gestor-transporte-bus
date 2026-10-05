using BE;
using BLL;
using servicios;
using UI.Login;
using UI.Modules;
using UI.Modules.gestion_choferes;
using UI.Modules.gestion_cronogramas;
using UI.Modules.gestion_internos;
using UI.Modules.gestion_rutas;
using UI.Modules.gestion_taller;

namespace UI
{
    public partial class MainUI : FormBaseObserver
    {
        private Form? formCargadoActualmente;

        public MainUI()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
            foreach (ToolStripMenuItem item in menuStrip1.Items)
            {
                item.Enabled = false;
            }
        }

        private void cargarFormulario(Form formulario)
        {
            if (formCargadoActualmente != null)
            {
                formCargadoActualmente.Dispose();
                formCargadoActualmente = null;
            }
            formCargadoActualmente = formulario;

            if (formulario is LoginUI loginUI)
            {
                loginUI.SesionIniciada += LoginUI_SesionIniciada;
            }

            formCargadoActualmente.MdiParent = this;
            if (!formCargadoActualmente.IsDisposed)
            {
                formCargadoActualmente.Show();
            }
        }

        private void LoginUI_SesionIniciada(object? sender, EventArgs e)
        {
            try
            {
                mainUIStripMenuItemInicio.Enabled = true;
                mainUIStripMenuItemCerrarSesion.Enabled = true;
                mainUIStripMenuItemIniciarSesion.Enabled = false;

                Usuario? usuarioActual = SessionManager.getInstance.ObtenerUsuarioActivo();

                if (usuarioActual != null)
                {
                    mainUIStripMenuItemGestionDeUsuarios.Enabled = usuarioActual.Permisos.Any(p => p.ValidarPermiso("PERM-GESTIONAR-USR"));
                    mainUIStripMenuItemGestionDePerfiles.Enabled = usuarioActual.Permisos.Any(p => p.ValidarPermiso("PERM-GESTIONAR-PERFIL"));
                    mainUIStripMenuItemBitacora.Enabled = usuarioActual.Permisos.Any(p => p.ValidarPermiso("PERM-CONSULTA-BIT"));
                    mainUIStripMenuItemHistorialUsuario.Enabled = usuarioActual.Permisos.Any(p => p.ValidarPermiso("PERM-GESTIONAR-HISTORIAL"));
                    agregarIdiomaToolStripMenuItem.Enabled = usuarioActual.Permisos.Any(p => p.ValidarPermiso("PERM-AGREGAR-IDM"));
                    planificacionServicioToolStripMenuItem.Enabled = usuarioActual.Permisos.Any(p => p.ValidarPermiso("PERM-PLANIFICACION-SERVICIO"));
                    choferesInternosToolStripMenuItem.Enabled = usuarioActual.Permisos.Any(p => p.ValidarPermiso("PERM-GESTION-CHOFERES-INTERNOS"));
                    gestionDeCronogramasToolStripMenuItem.Enabled = usuarioActual.Permisos.Any(p => p.ValidarPermiso("PERM-GESTION-CRONOGRAMAS"));
                    auditoriaSalidasToolStripMenuItem.Enabled = usuarioActual.Permisos.Any(p => p.ValidarPermiso("PERM-GESTION-CRONOGRAMAS"));
                    sancionesChoferToolStripMenuItem.Enabled = usuarioActual.Permisos.Any(p => p.ValidarPermiso("PERM-GESTION-CRONOGRAMAS"));
                    gestionCargasCombustibleToolStripMenuItem.Enabled =usuarioActual.Permisos.Any(p => p.ValidarPermiso("PERM-GESTION-CARGAS-COMBUSTIBLE"));
                    tallerToolStripMenuItem.Enabled = usuarioActual.Permisos.Any(p => p.ValidarPermiso("PERM-GESTION-TALLER"));
                    gestionRevisionesToolStripMenuItem.Enabled = usuarioActual.Permisos.Any(p => p.ValidarPermiso("PERM-GESTION-REVISIONES-TALLER"));

                    // Al iniciar sesión se pasa al idioma preferido del usuario
                    if (!string.IsNullOrEmpty(usuarioActual.Idioma))
                        comboIdiomasGlobal.SelectedValue = usuarioActual.Idioma;
                }
                else
                {
                    throw new Exception(T("msg_LoginError", "Login error"));
                }

                formCargadoActualmente?.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, T("msg_TituloError", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                // --- AVISO DE SISTEMA CORRUPTO ---
                List<Usuario> corruptos = GestorIntegridad.VerificarIntegridadDVH();
                bool dvvValido = GestorIntegridad.VerificarIntegridadDVV();

                if (corruptos.Count > 0 || !dvvValido)
                {
                    string mensaje = T("err_IntegridadCorrupta",
                        "Alerta Crítica: Se ha detectado una violación en la integridad de la base de datos.\n\nEl sistema ha entrado en Modo de Recuperación. Solo los administradores pueden iniciar sesión.");
                    string titulo = T("msg_TituloErrorIntegridad", "Error Crítico de Integridad");

                    MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(T("err_VerificarIntegridad", "Error al verificar la integridad del sistema: ") + ex.Message, T("msg_TituloError", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // --- CARGA DEL LOGIN Y ELEMENTOS NORMALES ---
            LoginUI loginUI = new LoginUI();
            cargarFormulario(loginUI);

            mainUIStripMenuItemCerrarSesion.Enabled = false;

            CargarComboIdiomas();
        }

        private void CargarComboIdiomas()
        {
            comboIdiomasGlobal.SelectedIndexChanged -= ComboIdiomasGlobal_SelectedIndexChanged;

            comboIdiomasGlobal.DataSource = GestorIdioma.GetInstance.ObtenerIdiomasDisponibles();
            comboIdiomasGlobal.DisplayMember = "Nombre";
            comboIdiomasGlobal.ValueMember = "Codigo";
            comboIdiomasGlobal.SelectedValue = GestorIdioma.GetInstance.IdiomaActual;

            comboIdiomasGlobal.SelectedIndexChanged += ComboIdiomasGlobal_SelectedIndexChanged;
        }

        public override void Update(string username, string action)
        {
            base.Update(username, action);

            // Se agregó o renombró un idioma desde la gestión de idiomas
            if (action == GestorIdioma.AccionIdiomasActualizados)
            {
                CargarComboIdiomas();
            }
        }

        private void mainUIStripMenuItemIniciarSesion_Click(object sender, EventArgs e)
        {
            LoginUI loginUI = new LoginUI();
            cargarFormulario(loginUI);
        }

        private void mainUIStripMenuItemCerrarSesion_Click(object sender, EventArgs e)
        {
            GestorLogin gestorLogin = new GestorLogin();

            gestorLogin.LogOut();

            string mensaje = T("msg_CierreSesionExito", "Sesión cerrada correctamente.");
            string titulo = T("msg_TituloCierreSesion", "Cerrar sesión");

            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);

            LoginUI loginUI = new LoginUI();
            cargarFormulario(loginUI);

            foreach (ToolStripMenuItem item in menuStrip1.Items)
            {
                item.Enabled = false;
            }

            mainUIStripMenuItemCerrarSesion.Enabled = false;
            mainUIStripMenuItemIniciarSesion.Enabled = true;
        }

        private void mainUIStripMenuItemABMUsuarios_Click(object sender, EventArgs e)
        {
            GestionUsuariosUI gestorUsuariosUI = new GestionUsuariosUI();
            cargarFormulario(gestorUsuariosUI);
        }

        private void mainUIStripMenuItemDesbloqueoUsuarios_Click(object sender, EventArgs e)
        {
            BloqueoUsuariosUI bloqueoUsuariosUI = new BloqueoUsuariosUI();
            cargarFormulario(bloqueoUsuariosUI);
        }

        private void mainUIStripMenuItemABMPerfiles_Click(object sender, EventArgs e)
        {
            GestionPerfilesUI gestionPerfilesUI = new GestionPerfilesUI();
            cargarFormulario(gestionPerfilesUI);
        }

        private void mainUIStripMenuItemConsultarBitacora_Click(object sender, EventArgs e)
        {
            BitacoraUI bitacoraUI = new BitacoraUI();
            cargarFormulario(bitacoraUI);
        }

        private void ComboIdiomasGlobal_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (comboIdiomasGlobal.SelectedItem == null) return;

            BE.Idioma idiomaSeleccionado = (BE.Idioma)comboIdiomasGlobal.SelectedItem;

            GestorIdioma.GetInstance.CambiarIdioma(idiomaSeleccionado.Codigo);
        }

        private void mainUIStripMenuItemHistorialUsuario_Click(object sender, EventArgs e)
        {
            GestionHistorialUsuarioUI gestionHistorialUsuarioUI = new GestionHistorialUsuarioUI();
            cargarFormulario(gestionHistorialUsuarioUI);
        }

        private void agregarIdiomaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GestionIdiomasUI gestorIdiomasUI = new GestionIdiomasUI();
            cargarFormulario(gestorIdiomasUI);
        }

        private void gestionParadaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GestionParadasUI gestionParadasUI = new GestionParadasUI();
            cargarFormulario(gestionParadasUI);
        }

        private void gestionDeRutasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GestionRutaUI gestionRutasUI = new GestionRutaUI();
            cargarFormulario(gestionRutasUI);
        }

        private void gestionDeChoferesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GestionChoferUI gestionChoferUI = new GestionChoferUI();
            cargarFormulario(gestionChoferUI);
        }

        private void gestionDeInternosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GestionInternoUI gestionInternoUI = new GestionInternoUI();
            cargarFormulario(gestionInternoUI);
        }

        private void gestionDeCronogramasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GestionCronogramasUI gestionCronogramaUI = new GestionCronogramasUI();
            cargarFormulario(gestionCronogramaUI);
        }

        private void auditoriaSalidasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AuditoriaSalidasUI auditoriaSalidasUI = new AuditoriaSalidasUI();
            cargarFormulario(auditoriaSalidasUI);
        }

        private void sancionesChoferToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SancionesChoferUI sancionesChoferUI = new SancionesChoferUI();
            cargarFormulario(sancionesChoferUI);
        }

        private void gestionCargasCombustibleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GestionCargasCombustibleUI gestionCargasCombustibleUI = new GestionCargasCombustibleUI();
            cargarFormulario(gestionCargasCombustibleUI);
        }

        private void gestionRevisionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GestionRevisionesTallerUI gestionRevisionesTallerUI = new GestionRevisionesTallerUI();
            cargarFormulario(gestionRevisionesTallerUI);
        }
    }
}