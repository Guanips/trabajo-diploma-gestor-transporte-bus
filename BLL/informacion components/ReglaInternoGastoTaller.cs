using BE.informacion_entities;
using BE.interno_entities;
using BE.taller_entities;

namespace BLL.informacion_components
{
    // Un interno con un gasto de taller muy superior al promedio de la flota y reparaciones repetidas es candidato a
    // una revision integral o a su reemplazo. El sistema solo sugiere evaluarlo: no conoce el valor del vehiculo.
    public class ReglaInternoGastoTaller : ReglaRecomendacion
    {
        private const int FlotaMinima = 3;
        private const int ReparacionesMinimas = 2;
        private const double FactorSobrePromedio = 2;
        private const double FactorCritico = 3;

        // Textos de la recomendacion en el idioma por defecto
        private static readonly CatalogoMensajes Mensajes = new CatalogoMensajes("RecomendacionesUI", new Dictionary<string, string>
        {
            ["msg_RecTallerTitulo"] = "Interno {0} ({1}): gasto de taller elevado",
            ["msg_RecTallerEvidencia"] = "Gasto de {0} en {1} reparaciones, {2} veces el promedio por interno de la flota ({3}) en el período {4}. Incorporado el {5:dd/MM/yyyy} ({6} años)",
            ["msg_RecTallerNoDisponible"] = "actualmente no disponible",
            ["msg_RecTallerCostosPendientes"] = "Hay insumos sin costo cargado: el gasto real puede ser mayor.",
            ["msg_RecTallerAccion"] = "Evaluar una revisión integral del interno y analizar si conviene su reemplazo o baja, comparando el gasto acumulado de reparaciones con el valor del vehículo."
        });

        public override List<Recomendacion> Evaluar(DatosAnalisisServicio datos)
        {
            List<Recomendacion> recomendaciones = new List<Recomendacion>();
            if (datos.internos.Count < FlotaMinima) return recomendaciones;

            // El gasto de un detalle sin costo cargado figura como 0
            decimal gastoTotal = datos.revisiones.Sum(GastoDeRevision);
            decimal gastoPromedio = gastoTotal / datos.internos.Count;
            if (gastoPromedio == 0) return recomendaciones;

            foreach (IGrouping<int, RevisionTaller> grupo in datos.revisiones.GroupBy(r => r.interno.num_interno))
            {
                Interno interno = grupo.First().interno;
                decimal gasto = grupo.Sum(GastoDeRevision);
                int reparaciones = grupo.Count(r => r.reparacionRequerida);
                if (reparaciones < ReparacionesMinimas || gasto <= (decimal)FactorSobrePromedio * gastoPromedio) continue;

                bool hayCostosPendientes = grupo.Any(r => r.ordenesReparacion.Any(o => o.detalles.Any(d => d.costoUnitario == 0)));
                double antiguedadAnios = (DateTime.Today - interno.fechaIncorporacion.ToDateTime(TimeOnly.MinValue)).TotalDays / 365.25;
                double veces = (double)(gasto / gastoPromedio);

                recomendaciones.Add(new Recomendacion(
                    veces >= FactorCritico ? SeveridadRecomendacion.Alta : SeveridadRecomendacion.Media,
                    Mensajes.Traducir("msg_RecTallerTitulo", interno.patente, interno.modelo),
                    Mensajes.Traducir("msg_RecTallerEvidencia", Dinero(gasto), reparaciones, Numero(veces), Dinero(gastoPromedio), Periodo(datos),
                        interno.fechaIncorporacion, Numero(antiguedadAnios)) +
                    (interno.disponible ? "." : ", " + Mensajes.Traducir("msg_RecTallerNoDisponible") + ".") +
                    (hayCostosPendientes ? " " + Mensajes.Traducir("msg_RecTallerCostosPendientes") : ""),
                    Mensajes.Traducir("msg_RecTallerAccion")));
            }

            return recomendaciones;
        }

        private static decimal GastoDeRevision(RevisionTaller revision)
        {
            return revision.ordenesReparacion.Sum(orden => orden.detalles.Sum(detalle => (decimal)detalle.cantidad * detalle.costoUnitario));
        }
    }
}
