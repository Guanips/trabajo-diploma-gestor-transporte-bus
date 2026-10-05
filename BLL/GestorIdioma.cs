using BE;
using DAL;

namespace BLL
{
    public class GestorIdioma : ISujeto
    {
        private const string IdiomaDefaultRespaldo = "ES";

        /// <summary>Acción que se notifica a los observers cuando se agrega o renombra un idioma.</summary>
        public const string AccionIdiomasActualizados = "IdiomasActualizados";

        private static GestorIdioma? _instance;
        private static readonly object _lock = new object();

        private readonly object _sync = new object();
        private readonly List<IObserver> ObserversAttached = new List<IObserver>();
        private readonly RepositorioIdioma _repoIdioma = new RepositorioIdioma();

        // Cache en memoria: evita ir a la base cada vez que se traduce un formulario o un mensaje.
        // Se invalida cuando se guardan traducciones.
        private readonly Dictionary<string, Dictionary<string, string>> _cacheTraducciones = new(StringComparer.OrdinalIgnoreCase);
        private HashSet<string>? _clavesRegistradas;
        private string? _idiomaDefault;
        private string? _idiomaActual;

        private GestorIdioma() { }

        public static GestorIdioma GetInstance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null) _instance = new GestorIdioma();
                    return _instance;
                }
            }
        }

        /// <summary>Idioma en el que se registran las etiquetas nuevas (Idioma.EsDefault).</summary>
        public string IdiomaDefault
        {
            get
            {
                if (_idiomaDefault == null)
                {
                    try
                    {
                        _idiomaDefault = ObtenerIdiomasDisponibles().FirstOrDefault(i => i.EsDefault)?.Codigo ?? IdiomaDefaultRespaldo;
                    }
                    catch
                    {
                        return IdiomaDefaultRespaldo;
                    }
                }
                return _idiomaDefault;
            }
        }

        public string IdiomaActual => _idiomaActual ??= IdiomaDefault;

        public List<Idioma> ObtenerIdiomasDisponibles()
        {
            return _repoIdioma.ObtenerTodosLosIdiomas();
        }

        public Dictionary<string, string> ObtenerTraduccionesActuales(string codigoIdioma)
        {
            lock (_sync)
            {
                if (!_cacheTraducciones.TryGetValue(codigoIdioma, out Dictionary<string, string>? traducciones))
                {
                    traducciones = _repoIdioma.ObtenerTraducciones(codigoIdioma);
                    _cacheTraducciones[codigoIdioma] = traducciones;
                }
                return traducciones;
            }
        }

        // ---------------------------------------------------------
        // Autodetección de etiquetas
        // ---------------------------------------------------------

        /// <summary>
        /// Registra en la base las etiquetas que todavía no existen, usando su texto como traducción
        /// del idioma por defecto. Las etiquetas ya registradas se ignoran (no se pisan textos editados).
        /// </summary>
        public void RegistrarEtiquetas(IEnumerable<(string Clave, string? Formulario, string Texto)> etiquetas)
        {
            lock (_sync)
            {
                _clavesRegistradas ??= _repoIdioma.ObtenerClavesEtiquetas();

                var faltantes = etiquetas
                    .Where(e => !string.IsNullOrWhiteSpace(e.Clave) && !_clavesRegistradas.Contains(e.Clave))
                    .GroupBy(e => e.Clave, StringComparer.OrdinalIgnoreCase)
                    .Select(g => g.First())
                    .ToList();

                if (faltantes.Count == 0) return;

                _repoIdioma.RegistrarEtiquetasFaltantes(faltantes);

                foreach (var etiqueta in faltantes)
                {
                    _clavesRegistradas.Add(etiqueta.Clave);

                    // Una etiqueta nueva solo tiene texto en el idioma por defecto, que es el respaldo de todos los idiomas
                    foreach (Dictionary<string, string> traducciones in _cacheTraducciones.Values)
                    {
                        traducciones.TryAdd(etiqueta.Clave, etiqueta.Texto);
                    }
                }
            }
        }

        /// <summary>
        /// Devuelve el texto de un mensaje en el idioma actual. Si la clave no existe en la base se
        /// registra con <paramref name="mensajePorDefecto"/> como texto del idioma por defecto.
        /// </summary>
        public string TraducirMensaje(string keyEtiqueta, string mensajePorDefecto)
        {
            if (string.IsNullOrWhiteSpace(keyEtiqueta)) return mensajePorDefecto;

            try
            {
                RegistrarEtiquetas(new[] { (keyEtiqueta, (string?)null, mensajePorDefecto) });

                return ObtenerTraduccionesActuales(IdiomaActual).TryGetValue(keyEtiqueta, out string? texto)
                    ? texto
                    : mensajePorDefecto;
            }
            catch
            {
                // Mostrar un mensaje nunca debe fallar por un problema con las traducciones
                return mensajePorDefecto;
            }
        }

        // ---------------------------------------------------------
        // Administración de idiomas y traducciones
        // ---------------------------------------------------------

        public void RegistrarNuevoIdioma(string codigo, string nombre)
        {
            codigo = (codigo ?? string.Empty).Trim().ToUpper();
            nombre = (nombre ?? string.Empty).Trim();

            ValidarDatosIdioma(codigo, nombre);

            bool yaExiste = ObtenerIdiomasDisponibles().Any(i => i.Codigo.Equals(codigo, StringComparison.OrdinalIgnoreCase));
            if (yaExiste)
            {
                throw new Exception(TraducirMensaje("err_IdiomaYaExiste", "El código de idioma ya se encuentra registrado en el sistema."));
            }

            _repoIdioma.InsertarIdioma(new Idioma(codigo, nombre));
            RegistrarEnBitacora($"LOG_IDIOMA_ADD:{codigo}");
            Notificar(UsernameActual(), AccionIdiomasActualizados);
        }

        public void ModificarIdioma(string codigo, string nombre)
        {
            nombre = (nombre ?? string.Empty).Trim();
            ValidarDatosIdioma(codigo, nombre);

            _repoIdioma.ModificarIdioma(new Idioma(codigo, nombre));
            RegistrarEnBitacora($"LOG_IDIOMA_UPDATE:{codigo}");
            Notificar(UsernameActual(), AccionIdiomasActualizados);
        }

        public List<EtiquetaTraduccion> ObtenerMatrizTraduccion(string codigoIdioma)
        {
            return _repoIdioma.ObtenerMatrizTraduccion(codigoIdioma);
        }

        /// <summary>
        /// Guarda las traducciones editadas de un idioma. Un texto vacío quita la traducción y vuelve
        /// a mostrarse el texto del idioma por defecto (en el idioma por defecto, el texto es obligatorio).
        /// </summary>
        public void GuardarTraducciones(string codigoIdioma, Dictionary<string, string?> traducciones)
        {
            if (traducciones.Count == 0) return;

            bool esIdiomaDefault = codigoIdioma.Equals(IdiomaDefault, StringComparison.OrdinalIgnoreCase);
            if (esIdiomaDefault && traducciones.Values.Any(string.IsNullOrWhiteSpace))
            {
                throw new Exception(TraducirMensaje("err_TraduccionDefaultVacia", "Los textos del idioma por defecto no pueden quedar vacíos."));
            }

            _repoIdioma.GuardarTraducciones(codigoIdioma, traducciones.ToDictionary(kvp => kvp.Key, kvp => kvp.Value?.Trim()));

            lock (_sync)
            {
                // Los textos del idioma por defecto son el respaldo de todos los demás
                if (esIdiomaDefault) _cacheTraducciones.Clear();
                else _cacheTraducciones.Remove(codigoIdioma);
            }

            RegistrarEnBitacora($"LOG_TRADUCCIONES_UPDATE:{codigoIdioma}");

            // Si se editó el idioma en uso, se refrescan los formularios abiertos
            if (esIdiomaDefault || codigoIdioma.Equals(IdiomaActual, StringComparison.OrdinalIgnoreCase))
            {
                Notificar(UsernameActual(), $"Idioma:{IdiomaActual}");
            }
        }

        public void CambiarIdioma(string nuevoIdioma)
        {
            _idiomaActual = nuevoIdioma;

            Usuario? usuarioActivo = SessionManager.getInstance.ObtenerUsuarioActivo();
            if (usuarioActivo != null && !nuevoIdioma.Equals(usuarioActivo.Idioma, StringComparison.OrdinalIgnoreCase))
            {
                usuarioActivo.Idioma = nuevoIdioma;
                RepositorioUsuarios.GetInstance.ActualizarIdioma(usuarioActivo.Username, nuevoIdioma);
            }

            Notificar(UsernameActual(), $"Idioma:{nuevoIdioma}");
        }

        private void ValidarDatosIdioma(string codigo, string nombre)
        {
            if (string.IsNullOrWhiteSpace(codigo) || string.IsNullOrWhiteSpace(nombre))
            {
                throw new Exception(TraducirMensaje("err_CodigoNombreObligatorios", "El código y el nombre del idioma son obligatorios."));
            }

            if (codigo.Length > 5)
            {
                throw new Exception(TraducirMensaje("err_CodigoIdiomaLargo", "El código del idioma puede tener como máximo 5 caracteres."));
            }

            if (nombre.Length > 50)
            {
                throw new Exception(TraducirMensaje("err_NombreIdiomaLargo", "El nombre del idioma puede tener como máximo 50 caracteres."));
            }
        }

        private static void RegistrarEnBitacora(string accion)
        {
            Usuario? usuarioActivo = SessionManager.getInstance.ObtenerUsuarioActivo();
            if (usuarioActivo != null)
            {
                GestorBitacora.GetInstance.Update(usuarioActivo.Username, accion);
            }
        }

        private static string UsernameActual()
        {
            return SessionManager.getInstance.ObtenerUsuarioActivo()?.Username ?? "invitado";
        }

        // ---------------------------------------------------------
        // Observer
        // ---------------------------------------------------------

        public void Attach(IObserver observer)
        {
            if (!ObserversAttached.Contains(observer)) ObserversAttached.Add(observer);
            observer.Update(UsernameActual(), $"Idioma:{IdiomaActual}");
        }

        public void Detach(IObserver observer)
        {
            ObserversAttached.Remove(observer);
        }

        public void Notificar(string username, string accion)
        {
            // Copia: un observer puede desuscribirse mientras se notifica
            foreach (IObserver item in ObserversAttached.ToList())
            {
                item.Update(username, accion);
            }
        }
    }
}
