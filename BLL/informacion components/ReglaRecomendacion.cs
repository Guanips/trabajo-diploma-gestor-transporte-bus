using BE.informacion_entities;
using System.Globalization;

namespace BLL.informacion_components
{
    // Cada regla analiza los datos del periodo y devuelve cero o mas recomendaciones.
    // Para agregar una regla nueva alcanza con heredar de esta clase y sumarla en GestorRecomendaciones.
    public abstract class ReglaRecomendacion
    {
        private static readonly CultureInfo Cultura = new CultureInfo("es-AR");

        public abstract List<Recomendacion> Evaluar(DatosAnalisisServicio datos);

        protected static string Numero(double valor) => valor.ToString("0.#", Cultura);

        protected static string Porcentaje(double valor) => valor.ToString("0.#", Cultura) + "%";

        protected static string Dinero(decimal monto) => monto.ToString("C0", Cultura);

        protected static string Periodo(DatosAnalisisServicio datos) => $"{datos.desde:dd/MM/yyyy} al {datos.hasta:dd/MM/yyyy}";
    }
}
