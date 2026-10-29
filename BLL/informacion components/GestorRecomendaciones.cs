using BE.informacion_entities;
using BLL.cronograma_components;
using BLL.interno_components;
using BLL.taller_components;
using Microsoft.Data.SqlClient;
using servicios;

namespace BLL.informacion_components
{
    public static class GestorRecomendaciones
    {
        private static readonly List<ReglaRecomendacion> Reglas = new List<ReglaRecomendacion>
        {
            new ReglaRutaRetrasoSistematico(),
            new ReglaFranjaHorariaCritica(),
            new ReglaChoferRetrasosYSanciones(),
            new ReglaInternoGastoTaller(),
            new ReglaInternoConsumoAnomalo()
        };

        /// <summary>
        /// Obtiene los datos del rango de fechas (ambas inclusive) con los gestores existentes, aplica las reglas y devuelve las
        /// recomendaciones ordenadas de mayor a menor severidad. La lista queda vacia si ninguna regla detecta nada.
        /// Lanza una Exception con el mensaje del caso alternativo si no se puede acceder a la base de datos o no hay datos.
        /// </summary>
        public static List<Recomendacion> ObtenerRecomendaciones(DateOnly desde, DateOnly hasta)
        {
            if (desde > hasta)
                throw new ArgumentException("La fecha de inicio no puede ser posterior a la fecha de fin.");

            VerificarConexion();

            DatosAnalisisServicio datos = new DatosAnalisisServicio(
                desde,
                hasta,
                GestorCronograma.ObtenerCronogramas().Where(c => c.fechaValidez >= desde && c.fechaValidez <= hasta).ToList(),
                AuditorCronogramasBLL.ObtenerSanciones().Where(s => s.fecha >= desde && s.fecha <= hasta).ToList(),
                GestorCargaCombustible.ObtenerCargas().Where(c => !c.anulada && DateOnly.FromDateTime(c.fechaHora) >= desde && DateOnly.FromDateTime(c.fechaHora) <= hasta).ToList(),
                GestorRevisionesTaller.ObtenerRevisiones().Where(r => r.fecha >= desde && r.fecha <= hasta).ToList(),
                GestorInterno.ObtenerInternos());

            if (!datos.tieneDatos)
                throw new Exception(GestorIdioma.GetInstance.TraducirMensaje("err_RecomendacionesSinDatos", "No hay datos para generar las recomendaciones en el rango de fechas seleccionado."));

            return Reglas
                .SelectMany(regla => regla.Evaluar(datos))
                .OrderByDescending(recomendacion => recomendacion.severidad)
                .ThenBy(recomendacion => recomendacion.titulo)
                .ToList();
        }

        // Distingue la falla de conexion (caso alternativo) de cualquier otro error al leer los datos
        private static void VerificarConexion()
        {
            try
            {
                using SqlConnection conn = new SqlConnection(EnvGetterService.GetConnectionString());
                conn.Open();
            }
            catch (SqlException ex)
            {
                throw new Exception(GestorIdioma.GetInstance.TraducirMensaje("err_RecomendacionesConexion", "No se pudo establecer la conexión con la base de datos."), ex);
            }
        }
    }
}
