// Distribucion de cada formulario hijo del MainUI. Cada Disponer se ejecuta al cargar y al redimensionar (ver Disposicion.Montar).

namespace UI.Modules
{
    partial class BitacoraUI
    {
        private void Disponer(Disposicion d, Rectangle area)
        {
            var col = d.Columnas(area, 1, -300);
            d.Tarjeta(groupBoxListadoBitacora, col[0], dataGridViewRegistrosBitacora, bitacoraUILabelGrid);

            d.Poner(groupBoxFiltros, new Rectangle(col[1].X, col[1].Y, col[1].Width, col[1].Height));
            int fin = d.Pila(d.Interior(groupBoxFiltros),
                new Fila(bitacoraUILabelComboBoxAccion, comboBoxAccion),
                new Fila(bitacoraUILabelComboBoxUsername, comboBoxUsername),
                new Fila(null, bitacoraUIButtonLimpiarFiltros));
            d.AjustarAlto(groupBoxFiltros, fin);
        }
    }

    partial class BloqueoUsuariosUI
    {
        private void Disponer(Disposicion d, Rectangle area)
        {
            d.Tarjeta(groupBoxBloqueados, area, dataGridViewBloqueados, null, btnDesbloquear);
        }
    }

    partial class GestionHistorialUsuarioUI
    {
        private void Disponer(Disposicion d, Rectangle area)
        {
            var col = d.Columnas(area, 1, 1, -200);
            d.Tarjeta(groupBoxUsuarios, col[0], dataGridViewUsuarios, gestionHistorialUILabelGridUsuarios);
            d.Tarjeta(groupBoxHistorial, col[1], dataGridViewHistorial, gestionHistorialUILabelGridEstadoUsuarios);

            d.Poner(groupBoxAcciones, col[2]);
            var interior = d.Interior(groupBoxAcciones);
            d.Poner(gestionHistorialUIButtonRecuperarEstado, new Rectangle(interior.X, interior.Y, interior.Width, d.E(56)));
        }
    }

    partial class GestionIdiomasUI
    {
        private void Disponer(Disposicion d, Rectangle area)
        {
            var fila = d.Filas(area, -90, 1);

            d.Poner(groupBoxIdioma, fila[0]);
            d.Linea(d.Interior(groupBoxIdioma),
                (labelCodigo, 0), (textBoxCodigo, -120), (labelNombre, 0), (textBoxNombre, 1), (btnGuardarIdioma, -140));

            d.Tarjeta(groupBoxTraducciones, fila[1], dataGridViewTraducciones);
        }
    }

    partial class GestionParadasUI
    {
        private void Disponer(Disposicion d, Rectangle area)
        {
            var col = d.Columnas(area, -340, 1);

            d.Poner(groupBoxAltaParada, col[0]);
            d.Pila(d.Interior(groupBoxAltaParada),
                new Fila(labelParadaAltaId, textBoxParadaAltaID),
                new Fila(labelParadaAltaDescripcion, textBoxParadaAltaDescripcion, Peso: 1),
                new Fila(labelParadaAltaLocalidad, textBoxParadaAltaLocalidad),
                new Fila(labelParadaAltaDireccion, textBoxParadaAltaDireccion),
                new Fila(null, checkBoxParadaAltaHabilitada),
                new Fila(null, buttonParadaAltaConfirmar));

            d.Tarjeta(groupBoxListadoParadas, col[1], dataGridViewParadas, null,
                buttonParadaToggleHabilitacion, buttonParadaModificarCallModal, buttonParadaBajaConfirmar);
        }
    }

    partial class GestionPerfilesUI
    {
        private void Disponer(Disposicion d, Rectangle area)
        {
            var col = d.Columnas(area, 1, 1, 2.4f);

            // Arbol de perfiles + alta de perfil
            d.Poner(perfilesUIGroupBoxTreeView, col[0]);
            var arbol = d.Interior(perfilesUIGroupBoxTreeView);
            var alta = d.CortarAbajo(ref arbol, d.E(18) + d.E(3) + d.E(24) + d.E(10) + d.E(36));
            d.Pila(alta,
                new Fila(perfilesUILabelNombrePerfil, textBoxNombrePerfil),
                new Fila(null, perfilesUIButtonCrearPerfil));
            d.Poner(treeViewCompositePermisos, arbol);

            // Perfiles y permisos disponibles, apilados
            var medio = d.Filas(col[1], 1, 1);
            d.Tarjeta(perfilesUIGroupBoxListBoxPerfiles, medio[0], listBoxPerfiles, null, perfilUIButtonAsignarPerfil);
            d.Tarjeta(perfilesUIGroupBoxListBoxPermisos, medio[1], listBoxPermisos, null, perfilUIButtonAsignarPermiso);

            // Usuarios: grilla y permisos efectivos lado a lado
            d.Poner(perfilesUIGroupBoxUsuarios, col[2]);
            var usuarios = d.Interior(perfilesUIGroupBoxUsuarios);
            d.Botones(d.CortarAbajo(ref usuarios, d.E(44)), perfilUIButtonAsignarPerfilUsuario, perfilUIButtonDesasignarPerfilUsuario);
            var lados = d.Columnas(usuarios, 1.3f, 1);
            d.Poner(dataGridViewUsuarios, lados[0]);
            d.Poner(treeViewPermisosUsuario, lados[1]);
        }
    }

    partial class GestionUsuariosUI
    {
        private void Disponer(Disposicion d, Rectangle area)
        {
            var col = d.Columnas(area, 1, -340);
            d.Tarjeta(gestionUsuariosUIGroupBoxListadoUsuarios, col[0], dataGridViewListadoUsuarios, null,
                gestionUsuariosUIButtonConfirmarEliminarUsuario);

            var derecha = col[1];

            d.Poner(gestionUsuariosUIGroupBoxModificacionUsuarios, new Rectangle(derecha.X, derecha.Y, derecha.Width, derecha.Height));
            int fin = d.Pila(d.Interior(gestionUsuariosUIGroupBoxModificacionUsuarios),
                new Fila(gestionUsuariosUIModificacionLabelEmail, textBoxModificacionEmail),
                new Fila(gestionUsuariosUIModificacionLabelNumTelefono, textBoxModificacionNumTelefono),
                new Fila(null, gestionUsuariosUIModificacionButtonConfirmarModificarUsuario));
            d.AjustarAlto(gestionUsuariosUIGroupBoxModificacionUsuarios, fin);

            int yAlta = gestionUsuariosUIGroupBoxModificacionUsuarios.Bottom + d.Espacio;
            d.Poner(gestionUsuariosUIGroupBoxAltaUsuario, new Rectangle(derecha.X, yAlta, derecha.Width, Math.Max(derecha.Bottom - yAlta, d.E(200))));
            fin = d.Pila(d.Interior(gestionUsuariosUIGroupBoxAltaUsuario),
                new Fila(gestionUsuariosUIRegistroLabelUsername, textBoxRegistroUsername),
                new Fila(gestionUsuariosUIRegistroLabelEmail, textBoxRegistroEmail),
                new Fila(gestionUsuariosUIRegistroLabelNumTelefono, textBoxRegistroNumTelefono),
                new Fila(gestionUsuariosUIRegistroLabelContrasena, textBoxRegistroContrasena),
                new Fila(gestionUsuariosUIRegistroLabelConfirmContrasena, textBoxRegistroRepetirConstrasena),
                new Fila(null, gestionUsuariosUIButtonConfirmarRegistrarUsuario));
            d.AjustarAlto(gestionUsuariosUIGroupBoxAltaUsuario, fin);
        }
    }
}

namespace UI.Modules.gestion_choferes
{
    partial class GestionChoferUI
    {
        private void Disponer(Disposicion d, Rectangle area)
        {
            d.Tarjeta(groupBoxGestionChofer, area, dataGridViewChoferes, null,
                buttonChoferesAgregar, buttonChoferModificar, buttonChoferEliminar);
        }
    }
}

namespace UI.Modules.gestion_internos
{
    partial class GestionInternoUI
    {
        private void Disponer(Disposicion d, Rectangle area)
        {
            d.Tarjeta(groupBoxGestionInterno, area, dataGridViewInternos, null,
                buttonInternoAgregar, buttonInternoModificar, buttonInternoEliminar);
        }
    }
}

namespace UI.Modules.gestion_rutas
{
    partial class GestionRutaUI
    {
        private void Disponer(Disposicion d, Rectangle area)
        {
            var col = d.Columnas(area, 1.5f, 1, 1);
            d.Tarjeta(groupBoxRutaDisponibles, col[0], dataGridViewRutaDisponibles, null,
                buttonRutaAlta, buttonRutaModificar, buttonRutaEliminar);
            d.Tarjeta(groupBoxRutaParadasDisponibles, col[1], listBoxRutaParadasDisponibles, null, buttonRutaAsignarParada);
            d.Tarjeta(groupBoxRutaParadasRuta, col[2], listBoxRutaParadasDeRuta, null,
                buttonRutaSubirParada, buttonRutaBajarParada);
        }
    }
}

namespace UI.Modules.gestion_cronogramas
{
    partial class GestionCronogramasUI
    {
        private void Disponer(Disposicion d, Rectangle area)
        {
            var col = d.Columnas(area, 1, 1.5f);

            // Izquierda: lista de cronogramas con sus acciones y, debajo, el detalle del seleccionado
            d.Poner(groupBoxGestionCronogramasSeccionCronogramas, col[0]);
            var izq = d.Interior(groupBoxGestionCronogramasSeccionCronogramas);
            var detalle = d.CortarAbajo(ref izq, d.E(330));
            d.Botones(d.CortarAbajo(ref izq, d.AltoBoton),
                buttonGestionCronogramaAgregarCronograma, buttonGestionCronogramasModificarCronograma, buttonGestionCronogramasEliminarCronograma);
            d.Poner(listBoxCronogramas, izq);

            d.Poner(groupBoxGestionCronogramasSeccionDetalle, detalle);
            var campos = d.Columnas(d.Interior(groupBoxGestionCronogramasSeccionDetalle), 1, 1);
            d.Pila(campos[0],
                new Fila(labelGestionCronogramaDetalleId, textBoxGestionCronogramaDetalleId),
                new Fila(labelGestionCronogramaDetalleDescripcion, textBoxGestionCronogramaDetalleDescripcion, Peso: 1));
            d.Pila(campos[1],
                new Fila(labelGestioCronogramaDetalleRuta, textBoxGestionCronogramaDetalleRuta),
                new Fila(labelGestioCronogramaDetalleFecha, textBoxGestionCronogramaDetalleFecha),
                new Fila(labelGestioCronogramaDetalleHoraInicio, textBoxGestionCronogramaDetalleHoraInicio,
                         labelGestioCronogramaDetalleHoraFin, textBoxGestionCronogramaDetalleHoraFinal),
                new Fila(labelGestioCronogramaDetalleFrecuencia, textBoxGestionCronogramaDetalleFrecuencia,
                         labelGestioCronogramaDetalleDescanso, textBoxGestionCronogramaDetalleDescanso));

            // Derecha: grilla de salidas, acciones sobre la salida y asignacion de chofer / interno
            d.Poner(groupBoxGestionCronogramasSeccionAsignacionSalidas, col[1]);
            var der = d.Interior(groupBoxGestionCronogramasSeccionAsignacionSalidas);

            var asignar = d.CortarAbajo(ref der, d.E(18) + d.E(3) + d.E(24) + d.E(10) + d.E(36));
            var lados = d.Columnas(asignar, 1, 1);
            d.Pila(lados[0],
                new Fila(labelGestionCronogramasChofer, comboBoxGestionCronogramasChofer),
                new Fila(null, buttonGestionCronogramasAsignarChofer));
            d.Pila(lados[1],
                new Fila(labelGestionCronogramasInterno, comboBoxGestionCronogramasInterno),
                new Fila(null, buttonGestionCronogramasAsignarInterno));

            d.Botones(d.CortarAbajo(ref der, d.AltoBoton),
                buttonGestionCronogramasGenerarSalidas, buttonGestionCronogramasSuspenderSalida,
                buttonGestionCronogramasDesasignarChofer, buttonGestionCronogramasDesasignarInterno);
            d.Poner(dataGridViewAsignacionSalidas, der);
        }
    }
}

namespace UI.Modules.gestion_taller
{
    partial class GestionCargasCombustibleUI
    {
        private void Disponer(Disposicion d, Rectangle area)
        {
            var col = d.Columnas(area, -320, 1);

            d.Poner(groupBoxGestionCombustibleRegistrar, col[0]);
            int fin = d.Pila(d.Interior(groupBoxGestionCombustibleRegistrar),
                new Fila(labelGestionCombustibleInterno, comboBoxInterno),
                new Fila(labelGestionCombustibleFechaHora, dateTimePickerCargaFechaHora),
                new Fila(labelGestionCombustibleLitros, numericUpDownLitros),
                new Fila(labelGestionCombustiblePrecioPorLitro, numericUpDownPrecioLitro),
                new Fila(labelGestionCombustibleKilometrajeActual, numericUpDownKilometraje),
                new Fila(null, buttonGestionCombustibleConfirmarRegistro));
            d.AjustarAlto(groupBoxGestionCombustibleRegistrar, fin);

            d.Tarjeta(groupBoxGestionCombustibleCargasHechas, col[1], dataGridViewGestionCombustibleCargas, null,
                buttonGestionCombustibleAnularCarga);
        }
    }

    partial class GestionRevisionesTallerUI
    {
        private void Disponer(Disposicion d, Rectangle area)
        {
            var col = d.Columnas(area, -380, 1);
            var izq = d.Filas(col[0], 1, -310);

            // Revisiones disponibles: boton de alta arriba y lista debajo
            d.Poner(groupBoxGestionRevisionesAgregar, izq[0]);
            var lista = d.Interior(groupBoxGestionRevisionesAgregar);
            d.Poner(buttonGestionRevisionesAgregar, d.CortarArriba(ref lista, d.AltoBoton));
            d.Poner(listBoxGestionRevisionesDisponibles, lista);

            d.Poner(groupBoxGestionRevisionesDetalle, izq[1]);
            d.Pila(d.Interior(groupBoxGestionRevisionesDetalle),
                new Fila(labelGestionRevisionesDetalleId, textBoxGestionRevisionesDetalleId,
                         labelGestionRevisionesDetalleInterno, textBoxGestionRevisionesDetalleInterno),
                new Fila(labelGestionRevisionesDetalleFecha, textBoxGestionRevisionesDetalleFecha,
                         labelGestionRevisionesDetalleReparacionRequerida, textBoxGestionRevisionesDetalleReparacionRequerida),
                new Fila(labelGestionRevisionesDetalleDescripcion, textBoxGestionRevisionesDetalleDescripcion, Peso: 1));

            // Ordenes de reparacion (izquierda) e insumos asociados (derecha) con la auditoria de costos debajo
            d.Poner(groupBoxGestionRevisionesOrdenReparacion, col[1]);
            var ordenes = d.Columnas(d.Interior(groupBoxGestionRevisionesOrdenReparacion), -240, 1);

            var listaOrdenes = ordenes[0];
            d.Titulo(ref listaOrdenes, labelGestionRevisionesOrdenesReparacion);
            d.Poner(listBoxGestionRevisionesOrdenesReparacion, listaOrdenes);

            var insumos = ordenes[1];
            d.Titulo(ref insumos, labelGestionRevisionesInsumos);
            if (groupBoxGestionRevisionesAuditarDetalle.Visible)
            {
                var auditar = d.CortarAbajo(ref insumos, d.E(150));
                d.Poner(groupBoxGestionRevisionesAuditarDetalle, auditar);
                d.Pila(d.Interior(groupBoxGestionRevisionesAuditarDetalle),
                    new Fila(labelGestionRevisionesCostoInsumo, numericUpDownCostoInsumo),
                    new Fila(null, buttonGestionRevisionesAuditarInsumo));
            }
            d.Poner(dataGridViewDetallesOrdenReparacion, insumos);
        }
    }
}
