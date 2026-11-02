using System.Reflection;

namespace BLL
{
    /// <summary>
    /// Mensajes traducibles de un componente de negocio (gráficos, recomendaciones, PDF) con su texto en el
    /// idioma por defecto. Es el equivalente al diccionario de mensajes de los formularios: cada componente
    /// declara su catálogo en un campo estático y al iniciar la aplicación se registran todos, así se pueden
    /// traducir antes de que se muestren por primera vez.
    /// Los textos pueden tener marcadores {0}, {1}... que se completan al traducir.
    /// </summary>
    public class CatalogoMensajes
    {
        private readonly Dictionary<string, string> _mensajes;

        /// <summary>Formulario con el que se agrupan los mensajes en la gestión de idiomas.</summary>
        public string? Formulario { get; private set; }

        public CatalogoMensajes(string? formulario, Dictionary<string, string> mensajes)
        {
            Formulario = formulario;
            _mensajes = mensajes;
        }

        /// <summary>Devuelve el mensaje en el idioma actual, completando sus marcadores con <paramref name="argumentos"/>.</summary>
        public string Traducir(string clave, params object?[] argumentos)
        {
            // Mensaje no declarado: se muestra la clave para que el olvido salte a la vista
            string textoPorDefecto = _mensajes.TryGetValue(clave, out string? texto) ? texto : clave;

            return Formatear(GestorIdioma.GetInstance.TraducirMensaje(clave, textoPorDefecto), textoPorDefecto, argumentos);
        }

        public List<(string Clave, string? Formulario, string Texto)> ObtenerEtiquetas()
        {
            return _mensajes.Select(m => (m.Key, Formulario, m.Value)).ToList();
        }

        /// <summary>
        /// Completa los marcadores de una traducción. Si la traducción tiene los marcadores mal escritos se usa
        /// el texto por defecto: mostrar un mensaje nunca debe fallar por un problema con las traducciones.
        /// </summary>
        public static string Formatear(string traduccion, string textoPorDefecto, object?[] argumentos)
        {
            if (argumentos.Length == 0) return traduccion;

            try
            {
                return string.Format(traduccion, argumentos);
            }
            catch (FormatException)
            {
                return string.Format(textoPorDefecto, argumentos);
            }
        }

        /// <summary>Busca por reflexión los catálogos declarados como campos estáticos en la capa de negocio.</summary>
        public static List<(string Clave, string? Formulario, string Texto)> RecolectarEtiquetas()
        {
            return typeof(CatalogoMensajes).Assembly.GetTypes()
                .Where(t => !t.ContainsGenericParameters)
                .SelectMany(t => t.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
                .Where(f => f.FieldType == typeof(CatalogoMensajes))
                .Select(f => f.GetValue(null))
                .OfType<CatalogoMensajes>()
                .SelectMany(c => c.ObtenerEtiquetas())
                .ToList();
        }
    }
}
