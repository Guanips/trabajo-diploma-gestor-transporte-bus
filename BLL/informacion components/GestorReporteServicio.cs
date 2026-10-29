using BE.informacion_entities;
using Microsoft.Data.SqlClient;
using servicios;
using System.Data;

namespace BLL.informacion_components
{
    public static class GestorReporteServicio
    {
        public const int ToleranciaPorDefectoMin = 5;

        private static readonly string[] NombresTramos =
        {
            "A tiempo", "Retraso leve", "Retraso moderado", "Retraso grave", "Retraso muy grave"
        };

        /// <summary>
        /// Obtiene y procesa los datos del reporte para el rango de fechas (ambas inclusive).
        /// Lanza una Exception con el mensaje del caso alternativo si no se puede acceder a la base de datos o
        /// si el rango no tiene ninguna salida, sancion, carga de combustible ni revision.
        /// </summary>
        public static ReporteServicio Generar(DateOnly desde, DateOnly hasta, int toleranciaMinutos = ToleranciaPorDefectoMin)
        {
            if (desde > hasta)
                throw new ArgumentException("La fecha de inicio no puede ser posterior a la fecha de fin.");

            // Los tramos de la distribucion son hasta 10 / 25 / 50 minutos: la tolerancia tiene que ser menor que el primero
            toleranciaMinutos = Math.Clamp(toleranciaMinutos, 0, 9);

            ReporteServicio reporte;
            using (SqlConnection conn = new SqlConnection(EnvGetterService.GetConnectionString()))
            {
                try
                {
                    conn.Open();
                }
                catch (SqlException ex)
                {
                    throw new Exception(GestorIdioma.GetInstance.TraducirMensaje("err_ReporteConexion", "No se pudo establecer la conexión con la base de datos."), ex);
                }

                using SqlCommand cmd = new SqlCommand("usp_Reporte_General", conn) { CommandType = CommandType.StoredProcedure, CommandTimeout = 120 };
                cmd.Parameters.AddWithValue("@desde", desde.ToDateTime(TimeOnly.MinValue));
                cmd.Parameters.AddWithValue("@hasta", hasta.ToDateTime(TimeOnly.MinValue));
                cmd.Parameters.AddWithValue("@toleranciaMin", toleranciaMinutos);

                using SqlDataReader reader = cmd.ExecuteReader();
                reporte = Leer(reader, desde, hasta, toleranciaMinutos);
            }

            if (!reporte.tieneDatos)
                throw new Exception(GestorIdioma.GetInstance.TraducirMensaje("err_ReporteSinDatos", "No hay datos para generar el reporte en el rango de fechas seleccionado."));

            return reporte;
        }

        // El orden de los result sets es el documentado en usp_Reporte_General
        private static ReporteServicio Leer(SqlDataReader r, DateOnly desde, DateOnly hasta, int tolerancia)
        {
            r.Read();
            ResumenServicio resumen = new ResumenServicio(
                r.GetInt32(0), r.GetInt32(1), r.GetInt32(2), r.GetInt32(3), Convert.ToDouble(r.GetValue(4)),
                r.GetInt32(5), r.GetDecimal(6), r.GetDecimal(7), r.GetInt32(8), r.GetInt32(9), r.GetDecimal(10), r.GetInt32(11));

            List<RutaRendimiento> porRuta = LeerLista(r, x => new RutaRendimiento(
                x.GetString(0), x.GetString(1), x.GetString(2), x.GetInt32(3), x.GetInt32(4), x.GetInt32(5), x.GetInt32(6), Convert.ToDouble(x.GetValue(7))));

            List<PuntoEvolucion> evolucion = LeerLista(r, x => new PuntoEvolucion(
                DateOnly.FromDateTime(x.GetDateTime(0)), x.GetInt32(1), x.GetInt32(2), Convert.ToDouble(x.GetValue(3))));

            List<TramoRetraso> distribucion = LeerLista(r, x => new TramoRetraso(NombresTramos[x.GetInt32(0) - 1], x.GetInt32(1)));

            List<MotivoCantidad> suspensiones = LeerLista(r, x => new MotivoCantidad(x.GetString(0), x.GetInt32(1)));

            List<ChoferSancionado> choferes = LeerLista(r, x => new ChoferSancionado(x.GetString(0), x.GetInt32(1), Convert.ToDouble(x.GetValue(2))));

            Dictionary<string, decimal> combustible = LeerLista(r, x => (mes: x.GetString(0), gasto: x.GetDecimal(2))).ToDictionary(x => x.mes, x => x.gasto);
            Dictionary<string, decimal> taller = LeerLista(r, x => (mes: x.GetString(0), gasto: x.GetDecimal(2))).ToDictionary(x => x.mes, x => x.gasto);
            List<GastoMensual> gastosMensuales = combustible.Keys.Union(taller.Keys).OrderBy(m => m, StringComparer.Ordinal)
                .Select(m => new GastoMensual(m, combustible.GetValueOrDefault(m), taller.GetValueOrDefault(m)))
                .ToList();

            List<GastoInterno> tallerPorInterno = LeerLista(r, x => new GastoInterno(
                x.GetString(0), x.GetString(1), x.GetInt32(2), x.GetInt32(3), x.GetDecimal(4)));

            List<ConsumoInterno> consumo = LeerLista(r, x => new ConsumoInterno(
                x.GetString(0), x.GetString(1), x.GetDecimal(2), x.IsDBNull(3) ? 0 : x.GetInt32(3)));

            return new ReporteServicio(desde, hasta, tolerancia, DateTime.Now, resumen, porRuta, evolucion, distribucion,
                                       suspensiones, choferes, gastosMensuales, tallerPorInterno, consumo);
        }

        // Lee el result set actual completo y avanza al siguiente (si hay)
        private static List<T> LeerLista<T>(SqlDataReader reader, Func<SqlDataReader, T> mapear)
        {
            // Los result sets de la fila 1 ya se consumieron con Read(); NextResult posiciona en el siguiente
            reader.NextResult();
            List<T> lista = new List<T>();
            while (reader.Read()) lista.Add(mapear(reader));
            return lista;
        }
    }
}
