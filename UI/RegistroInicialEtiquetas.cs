using BLL;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace UI
{
    /// <summary>
    /// Registra al iniciar la aplicación las etiquetas de todos los formularios, aunque nunca se hayan
    /// abierto, para que desde el primer arranque puedan traducirse desde la gestión de idiomas.
    ///
    /// Los constructores de varios formularios consultan la base o reciben parámetros, por lo que no
    /// se los invoca: se crea la instancia sin constructor, se ejecuta el constructor de la clase base
    /// (que inicializa Form) y luego solo InitializeComponent, que arma los controles del diseñador.
    /// </summary>
    internal static class RegistroInicialEtiquetas
    {
        public static void Ejecutar(Action<string>? log = null)
        {
            var etiquetas = new List<(string Clave, string? Formulario, string Texto)>();

            IEnumerable<Type> formularios = typeof(RegistroInicialEtiquetas).Assembly.GetTypes()
                .Where(t => typeof(FormBaseObserver).IsAssignableFrom(t) && t != typeof(FormBaseObserver) && !t.IsAbstract);

            foreach (Type tipo in formularios)
            {
                try
                {
                    using Form formulario = CrearSoloConControles(tipo);
                    etiquetas.AddRange(new TraductorFormulario(formulario).RecolectarEtiquetas());
                }
                catch (Exception ex)
                {
                    // Un formulario que no se pueda inspeccionar registra sus etiquetas al abrirse
                    log?.Invoke($"No se pudieron recolectar las etiquetas de {tipo.Name}: {ex.Message}");
                }
            }

            GestorIdioma.GetInstance.RegistrarEtiquetas(etiquetas);
        }

        private static Form CrearSoloConControles(Type tipo)
        {
            object instancia = RuntimeHelpers.GetUninitializedObject(tipo);

            ConstructorInfo constructorBase = tipo.BaseType!.GetConstructor(
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, Type.EmptyTypes)
                ?? throw new InvalidOperationException($"{tipo.BaseType.Name} no tiene constructor sin parámetros.");
            constructorBase.Invoke(instancia, null);

            MethodInfo initializeComponent = tipo.GetMethod("InitializeComponent", BindingFlags.Instance | BindingFlags.NonPublic)
                ?? throw new InvalidOperationException($"{tipo.Name} no tiene InitializeComponent.");
            initializeComponent.Invoke(instancia, null);

            return (Form)instancia;
        }
    }
}
