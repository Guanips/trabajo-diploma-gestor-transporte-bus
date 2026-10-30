using BE;
using DAL;

namespace BLL
{
    public class GestorBitacora : IObserver
    {
        private static GestorBitacora? Instance;
        private static readonly object _lock = new object();

        // Largo de la columna Accion en la tabla Bitacora
        private const int LongitudMaximaAccion = 50;

        private RepositorioBitacora RepoBitacora { get; set; }

        private GestorBitacora()
        {
            this.RepoBitacora = new RepositorioBitacora();
        }

        public static GestorBitacora GetInstance
        {
            get
            {
                if (Instance == null)
                {
                    lock (_lock)
                    {
                        if (Instance == null)
                        {
                            Instance = new GestorBitacora();
                        }
                    }
                }

                return Instance;
            }
        }

        public List<Registro> ConsultarBitacoraCompleta()
        {
            List<Registro> registros = RepoBitacora.ListarRegistros();
            return registros;
        }

        public List<Registro> ConsultarBitacoraFiltradaPorUsername(string username)
        {
            List<Registro> registros = RepoBitacora.ListarRegistros();
            List<Registro> registrosFiltrados = registros.FindAll(r => r.Username == username);
            return registrosFiltrados;
        }

        public List<Registro> ConsultarBitacoraFiltradaPorAccion(string accion)
        {
            List<Registro> registros = RepoBitacora.ListarRegistros();
            List<Registro> registrosFiltrados = registros.FindAll(r => r.Accion == accion);
            return registrosFiltrados;
        }

        public void Update(string username, string action)
        {
            if (action.Length > LongitudMaximaAccion) action = action.Substring(0, LongitudMaximaAccion);

            Registro nRegistro = new Registro(username, DateTime.Now, action);
            RepoBitacora.AlmacenarRegistro(nRegistro);
        }

        /// <summary>
        /// Registra una acción a nombre del usuario con sesión activa. Pensado para los gestores estáticos,
        /// que no son sujetos observables. Sin usuario activo no registra nada.
        /// </summary>
        public static void RegistrarAccion(string accion)
        {
            Usuario? usuarioActivo = SessionManager.getInstance.ObtenerUsuarioActivo();
            if (usuarioActivo == null) return;

            GetInstance.Update(usuarioActivo.Username, accion);
        }
    }
}
