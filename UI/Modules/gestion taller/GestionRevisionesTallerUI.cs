using BE;
using BE.taller_entities;
using BLL;
using BLL.taller_components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI.Modules.gestion_taller
{
    public partial class GestionRevisionesTallerUI : Form
    {
        // Mientras se reasignan los DataSource por código, los manejadores de selección no deben
        // refrescar los paneles dependientes: en esos casos el refresco se hace de forma explícita.
        // Es necesario porque SelectedIndexChanged solo se dispara cuando el índice cambia de
        // verdad, y al reasignar un DataSource el índice puede quedar igual y dejar a la vista el
        // detalle de la revisión u orden que estaba activa antes.
        private bool actualizandoDatos;

        public GestionRevisionesTallerUI()
        {
            InitializeComponent();

            Usuario? usuarioActual = SessionManager.getInstance.ObtenerUsuarioActivo();
            if (usuarioActual == null)
            {
                MessageBox.Show("No hay un usuario activo en la sesión.");
                this.Close();
                return;
            }

            groupBoxGestionRevisionesAuditarDetalle.Visible = usuarioActual.Permisos.Any(p => p.ValidarPermiso("PERM-TALLER-AUDITAR"));

            RefrescarListaRevisiones();
        }

        private void buttonGestionRevisionesAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                GestionRevisionesTallerAgregarUI dialog = new GestionRevisionesTallerAgregarUI();
                dialog.ShowDialog();

                if (dialog.DialogResult == DialogResult.OK)
                {
                    RevisionTaller? nuevaRevision = dialog.NuevaRevision;
                    if (nuevaRevision == null) throw new Exception("Error en la creación de la revisión");

                    GestorRevisionesTaller.RegistrarRevision(nuevaRevision);

                    // Se deja seleccionada la revisión recién creada para que el detalle muestre
                    // sus órdenes (vacías si no requiere reparación) y no la anterior.
                    RefrescarListaRevisiones(nuevaRevision.id);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RefrescarListaRevisiones(int? idRevisionASeleccionar = null)
        {
            try
            {
                // Se conserva la selección vigente para que el refresco no salte a otra revisión
                // ni pierda la orden que el usuario estaba mirando.
                int? idRevisionObjetivo = idRevisionASeleccionar ?? ObtenerRevisionSeleccionada()?.id;
                int? idOrdenObjetivo = ObtenerOrdenSeleccionada()?.id;

                List<RevisionTaller> revisiones = GestorRevisionesTaller.ObtenerRevisiones();

                // El listbox no observa cambios sobre la lista enlazada: hay que reemplazar el
                // DataSource (pasando por null) para que las filas se regeneren siempre, incluso
                // cuando el gestor devuelve los mismos elementos que en el refresco anterior.
                actualizandoDatos = true;
                try
                {
                    listBoxGestionRevisionesDisponibles.DataSource = null;
                    listBoxGestionRevisionesDisponibles.DataSource = revisiones;

                    RevisionTaller? revisionAElegir = idRevisionObjetivo.HasValue
                        ? revisiones.FirstOrDefault(revision => revision.id == idRevisionObjetivo.Value)
                        : null;

                    listBoxGestionRevisionesDisponibles.SelectedItem = revisionAElegir ?? revisiones.FirstOrDefault();
                }
                finally
                {
                    actualizandoDatos = false;
                }

                // El detalle de la revisión es solo de display: se refresca con la selección vigente
                RefrescarDetalleRevision(ObtenerRevisionSeleccionada());

                // El refresco de la cadena es explícito: no se depende del evento de selección.
                RefrescarListaOrdenesDeRevision(ObtenerRevisionSeleccionada(), idOrdenObjetivo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // El detalle de la revisión es solo de display: los campos son de solo lectura y se
        // limitan a reflejar la revisión seleccionada en la lista.
        private void RefrescarDetalleRevision(RevisionTaller? revision)
        {
            try
            {
                if (revision == null)
                {
                    textBoxGestionRevisionesDetalleId.Text = string.Empty;
                    textBoxGestionRevisionesDetalleInterno.Text = string.Empty;
                    textBoxGestionRevisionesDetalleFecha.Text = string.Empty;
                    textBoxGestionRevisionesDetalleReparacionRequerida.Text = string.Empty;
                    textBoxGestionRevisionesDetalleDescripcion.Text = string.Empty;
                    return;
                }

                textBoxGestionRevisionesDetalleId.Text = revision.id.ToString();
                textBoxGestionRevisionesDetalleInterno.Text = revision.interno.ToString();
                textBoxGestionRevisionesDetalleFecha.Text = revision.fecha.ToString("dd/MM/yyyy");
                textBoxGestionRevisionesDetalleReparacionRequerida.Text = revision.reparacionRequerida ? "Sí" : "No";
                textBoxGestionRevisionesDetalleDescripcion.Text = revision.descripcion;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RefrescarListaOrdenesDeRevision(RevisionTaller? revision, int? idOrdenASeleccionar = null)
        {
            try
            {
                // Se recuerda el insumo activo para que el rebind de la grilla no mueva la fila
                // que el usuario estaba auditando: el detalle se vuelve a buscar por id dentro de
                // la orden que quede seleccionada.
                int? idDetalleObjetivo = ObtenerDetalleSeleccionado()?.id;

                // Si no hay revisión o la revisión no tiene órdenes, la lista queda vacía y más
                // abajo se limpia la grilla de insumos de la orden que estaba activa antes.
                List<OrdenReparacion> ordenes = revision?.ordenesReparacion ?? new List<OrdenReparacion>();

                actualizandoDatos = true;
                try
                {
                    listBoxGestionRevisionesOrdenesReparacion.DataSource = null;
                    listBoxGestionRevisionesOrdenesReparacion.DataSource = ordenes;

                    OrdenReparacion? ordenAElegir = idOrdenASeleccionar.HasValue
                        ? ordenes.FirstOrDefault(orden => orden.id == idOrdenASeleccionar.Value)
                        : null;

                    listBoxGestionRevisionesOrdenesReparacion.SelectedItem = ordenAElegir ?? ordenes.FirstOrDefault();
                }
                finally
                {
                    actualizandoDatos = false;
                }

                RefrescarGrillaInsumos(ObtenerOrdenSeleccionada(), idDetalleObjetivo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RefrescarGrillaInsumos(OrdenReparacion? orden, int? idDetalleASeleccionar = null)
        {
            try
            {
                // Se pasa por null para forzar el rebind aunque la lista de detalles sea la misma
                // instancia que la que ya estaba enlazada a la grilla. Es imprescindible tras
                // auditar un costo: la grilla no observa los cambios de propiedades del detalle.
                dataGridViewDetallesOrdenReparacion.DataSource = null;
                dataGridViewDetallesOrdenReparacion.DataSource = orden?.detalles;

                DetalleOrdenReparacion? detalleAElegir = idDetalleASeleccionar.HasValue
                    ? orden?.detalles.FirstOrDefault(detalle => detalle.id == idDetalleASeleccionar.Value)
                    : null;

                if (detalleAElegir != null) SeleccionarDetalle(detalleAElegir);

                // El costo que muestra el panel de auditoría depende de la fila activa: se
                // sincroniza de forma explícita porque el rebind puede no cambiar la selección.
                SincronizarCostoInsumoSeleccionado();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void listBoxGestionRevisionesDisponibles_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Si el cambio lo provocó un refresco de datos, la cadena completa ya se actualizó
            if (actualizandoDatos) return;

            RevisionTaller? revision = ObtenerRevisionSeleccionada();
            RefrescarDetalleRevision(revision);
            RefrescarListaOrdenesDeRevision(revision);
        }

        private void listBoxGestionRevisionesOrdenesReparacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (actualizandoDatos) return;

            RefrescarGrillaInsumos(ObtenerOrdenSeleccionada());
        }

        private RevisionTaller? ObtenerRevisionSeleccionada()
        {
            return listBoxGestionRevisionesDisponibles.SelectedItem as RevisionTaller;
        }

        private OrdenReparacion? ObtenerOrdenSeleccionada()
        {
            return listBoxGestionRevisionesOrdenesReparacion.SelectedItem as OrdenReparacion;
        }

        private DetalleOrdenReparacion? ObtenerDetalleSeleccionado()
        {
            return dataGridViewDetallesOrdenReparacion.CurrentRow?.DataBoundItem as DetalleOrdenReparacion;
        }

        // Deja activa la fila del detalle indicado para que el rebind de la grilla no pierda la
        // posición del insumo que el usuario estaba auditando.
        private void SeleccionarDetalle(DetalleOrdenReparacion detalle)
        {
            foreach (DataGridViewRow fila in dataGridViewDetallesOrdenReparacion.Rows)
            {
                if (fila.DataBoundItem is DetalleOrdenReparacion detalleDeFila && detalleDeFila.id == detalle.id)
                {
                    fila.Selected = true;
                    dataGridViewDetallesOrdenReparacion.CurrentCell = fila.Cells[0];
                    return;
                }
            }
        }

        // Refleja en el control de costo el insumo activo y habilita la auditoría solo si hay uno
        private void SincronizarCostoInsumoSeleccionado()
        {
            DetalleOrdenReparacion? detalle = ObtenerDetalleSeleccionado();

            groupBoxGestionRevisionesAuditarDetalle.Enabled = detalle != null;

            if (detalle == null)
            {
                numericUpDownCostoInsumo.Value = 0;
                return;
            }

            // El costo se persiste como entero; se acota al máximo del control por seguridad
            numericUpDownCostoInsumo.Value = Math.Min(detalle.costoUnitario, numericUpDownCostoInsumo.Maximum);
        }

        private void dataGridViewDetallesOrdenReparacion_SelectionChanged(object sender, EventArgs e)
        {
            SincronizarCostoInsumoSeleccionado();
        }

        private void buttonGestionRevisionesAuditarInsumo_Click(object sender, EventArgs e)
        {
            try
            {
                OrdenReparacion? orden = ObtenerOrdenSeleccionada();
                if (orden == null)
                {
                    MessageBox.Show("Seleccione una orden de reparación antes de auditar sus costos.");
                    return;
                }

                DetalleOrdenReparacion? detalle = ObtenerDetalleSeleccionado();
                if (detalle == null)
                {
                    MessageBox.Show("Seleccione el insumo cuyo costo desea auditar.");
                    return;
                }

                int costoUnitario = (int)numericUpDownCostoInsumo.Value;
                GestorRevisionesTaller.ActualizarCostoUnitarioDetalle(detalle, costoUnitario);

                // La grilla enlazada a la lista no detecta el cambio de propiedad: se rehace el
                // enlace conservando la fila del insumo auditado para que se vea el costo nuevo.
                RefrescarGrillaInsumos(orden, detalle.id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
