using BE;
using BLL;
using System.ComponentModel;

namespace UI
{
    /// <summary>
    /// Formulario base con soporte multi-idioma: al cargarse registra las etiquetas que todavía no
    /// existan en la base (con su texto de diseño como texto en el idioma por defecto), se suscribe a
    /// los cambios de idioma y traduce sus controles. Las columnas que se agregan a las grillas después
    /// de cargar (DataSource, columnas creadas por código) también se registran y traducen.
    /// </summary>
    public partial class FormBaseObserver : Form, IObserver
    {
        private TraductorFormulario? _traductor;

        public FormBaseObserver()
        {

        }

        protected override void OnLoad(EventArgs e)
        {
            if (!EsModoDiseno())
            {
                _traductor = new TraductorFormulario(this);
                RegistrarEtiquetasFaltantes(_traductor.RecolectarEtiquetas());
                SuscribirGrillas(Controls);
                GestorIdioma.GetInstance.Attach(this);

                // MainUI cierra los formularios hijos con Dispose (sin pasar por OnFormClosed)
                Disposed += (_, _) => GestorIdioma.GetInstance.Detach(this);
            }

            base.OnLoad(e);
        }

        protected bool EsModoDiseno()
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return true;

            string processName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
            return processName.Contains("devenv") || processName.Contains("DesignToolsServer");
        }

        public virtual void Update(string username, string action)
        {
            if (action.StartsWith("Idioma:"))
            {
                string codigoIdioma = action.Split(':')[1];

                Dictionary<string, string> traducciones = GestorIdioma.GetInstance.ObtenerTraduccionesActuales(codigoIdioma);

                _traductor?.Aplicar(traducciones);

                TraducirElementosParticulares(codigoIdioma);
            }
        }

        /// <summary>
        /// Punto de extensión para textos que no son controles (por ejemplo textos armados por código).
        /// </summary>
        protected virtual void TraducirElementosParticulares(string codigoIdioma)
        {

        }

        /// <summary>Atajo para traducir mensajes desde los formularios.</summary>
        protected static string T(string clave, string textoPorDefecto)
        {
            return GestorIdioma.GetInstance.TraducirMensaje(clave, textoPorDefecto);
        }

        private void SuscribirGrillas(Control.ControlCollection controles)
        {
            foreach (Control control in controles)
            {
                if (control is DataGridView grilla)
                {
                    grilla.ColumnAdded += Grilla_ColumnAdded;
                }
                else if (control.HasChildren)
                {
                    SuscribirGrillas(control.Controls);
                }
            }
        }

        private void Grilla_ColumnAdded(object? sender, DataGridViewColumnEventArgs e)
        {
            if (_traductor == null) return;

            RegistrarEtiquetasFaltantes(_traductor.RecolectarEtiquetas(e.Column));
            _traductor.Aplicar(e.Column, GestorIdioma.GetInstance.ObtenerTraduccionesActuales(GestorIdioma.GetInstance.IdiomaActual));
        }

        private static void RegistrarEtiquetasFaltantes(List<(string Clave, string? Formulario, string Texto)> etiquetas)
        {
            try
            {
                GestorIdioma.GetInstance.RegistrarEtiquetas(etiquetas);
            }
            catch
            {
                // Si no se pudieron registrar, el formulario se muestra igual con sus textos de diseño
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            if (!EsModoDiseno())
            {
                GestorIdioma.GetInstance.Detach(this);
            }
            base.OnFormClosed(e);
        }
    }
}
