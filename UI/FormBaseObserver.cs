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
    ///
    /// Los textos que no pertenecen a un control (MessageBox, diálogos, textos armados por código) se
    /// declaran en el diccionario <see cref="Mensajes"/> de cada formulario y se obtienen con T("clave").
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
                RegistrarEtiquetasFaltantes(_traductor.RecolectarEtiquetas().Concat(RecolectarMensajes()).ToList());
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
        /// Punto de extensión para textos que no son controles (por ejemplo textos armados por código):
        /// el traductor no pisa un texto que el formulario cambió, así que se vuelven a armar aquí con T().
        /// </summary>
        protected virtual void TraducirElementosParticulares(string codigoIdioma)
        {

        }

        /// <summary>
        /// Mensajes que no son textos de controles (MessageBox, diálogos, textos armados por código) y
        /// que usa más de un formulario, con su texto en el idioma por defecto.
        /// </summary>
        private static readonly Dictionary<string, string> MensajesComunes = new()
        {
            ["err_PassVacia"] = "La contraseña no puede estar vacía.",
            ["msg_ConfirmarEliminacion"] = "Confirmar eliminación",
            ["msg_Error"] = "Error",
            ["msg_FiltroArchivosJson"] = "Archivos JSON (*.json)|*.json",
            ["msg_FiltroArchivosPdf"] = "Archivos PDF (*.pdf)|*.pdf",
            ["msg_Informacion"] = "Información",
            ["msg_No"] = "No",
            ["msg_NoHayUnUsuarioActivoEnLaSesion"] = "No hay un usuario activo en la sesión.",
            ["msg_NoSeHaSeleccionadoNingunChofer"] = "No se ha seleccionado ningún chofer.",
            ["msg_NoSeHaSeleccionadoNingunInterno"] = "No se ha seleccionado ningún interno.",
            ["msg_NoSeHaSeleccionadoNingunaParada"] = "No se ha seleccionado ninguna parada.",
            ["msg_Si"] = "Sí",
            ["msg_TituloError"] = "Error",
            ["msg_TituloExito"] = "Éxito",
            ["msg_TituloExportacionExitosa"] = "Exportación exitosa",
            ["msg_TituloInformacion"] = "Información",
            ["msg_TodosLosCamposSonObligatorios"] = "Todos los campos son obligatorios."
        };

        private static readonly Dictionary<string, string> SinMensajes = new();

        /// <summary>
        /// Mensajes propios del formulario que no son textos de controles (MessageBox, diálogos, textos
        /// armados por código) con su texto en el idioma por defecto. Se registran junto con las
        /// etiquetas de los controles, así se pueden traducir antes de que se muestren por primera vez.
        /// Los textos pueden tener marcadores {0}, {1}... que se completan al llamar a <see cref="T"/>.
        ///
        /// Se lee también sobre instancias creadas sin constructor (ver RegistroInicialEtiquetas), por
        /// lo que cada formulario debe devolver un diccionario estático.
        /// </summary>
        protected virtual IReadOnlyDictionary<string, string> Mensajes => SinMensajes;

        /// <summary>
        /// Devuelve un mensaje del formulario (o común) en el idioma actual, completando sus marcadores
        /// con <paramref name="argumentos"/>.
        /// </summary>
        protected string T(string clave, params object?[] argumentos)
        {
            if (!Mensajes.TryGetValue(clave, out string? textoPorDefecto) && !MensajesComunes.TryGetValue(clave, out textoPorDefecto))
            {
                // Mensaje no declarado: se muestra la clave para que el olvido salte a la vista
                textoPorDefecto = clave;
            }

            string texto = GestorIdioma.GetInstance.TraducirMensaje(clave, textoPorDefecto);
            if (argumentos.Length == 0) return texto;

            try
            {
                return string.Format(texto, argumentos);
            }
            catch (FormatException)
            {
                // Una traducción con marcadores mal escritos no debe impedir mostrar el mensaje
                return string.Format(textoPorDefecto, argumentos);
            }
        }

        /// <summary>Traduce el error de una validación (su clave y texto por defecto vienen del validador).</summary>
        protected static string T(ValidationResult resultado)
        {
            return GestorIdioma.GetInstance.TraducirMensaje(resultado.ErrorMessage, resultado.MensajePorDefecto);
        }

        /// <summary>Mensajes del formulario y comunes, para registrarlos como etiquetas.</summary>
        internal List<(string Clave, string? Formulario, string Texto)> RecolectarMensajes()
        {
            return Mensajes.Select(m => (m.Key, (string?)GetType().Name, m.Value))
                .Concat(MensajesComunes.Select(m => (m.Key, (string?)null, m.Value)))
                .ToList();
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
