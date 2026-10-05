using BE.interno_entities;
using BLL.interno_components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI.Modules.gestion_choferes
{
    public partial class GestionChoferUI : FormBaseObserver
    {
        public GestionChoferUI()
        {
            InitializeComponent();
            this.refrescarChoferes();
        }

        private void buttonChoferesAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                using (GestionChoferAgregarModificarUI dialog = new GestionChoferAgregarModificarUI(null, null, null, null))
                {
                    dialog.ShowDialog(this);

                    if (dialog.DialogResult == DialogResult.OK)
                    {
                        if (dialog.dniChofer == null || dialog.nombreCompletoChofer == null || dialog.activoChofer == null)
                        {
                            throw new Exception(T("msg_TodosLosCamposSonObligatorios", "Todos los campos son obligatorios."));
                        }

                        // numero de chofer en -1 porque es temporal, la base de datos lo asigna automaticamente porque es PK con auto increment
                        Chofer nChofer = new Chofer(-1, dialog.dniChofer.Value, dialog.nombreCompletoChofer, dialog.activoChofer.Value);
                        GestorChofer.InsertarChofer(nChofer);
                        this.refrescarChoferes();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(T("msg_ErrorAlAgregarElChofer", "Error al agregar el chofer: ") + ex.Message);
            }
        }

        private void buttonChoferModificar_Click(object sender, EventArgs e)
        {
            try
            {
                Chofer choferSeleccionado = obtenerChoferSeleccionado();

                using (GestionChoferAgregarModificarUI dialog = new GestionChoferAgregarModificarUI(choferSeleccionado.num_chofer, choferSeleccionado.dni, choferSeleccionado.nombreCompleto, choferSeleccionado.activo))
                {
                    dialog.ShowDialog(this);

                    if (dialog.DialogResult == DialogResult.OK)
                    {
                        if (dialog.dniChofer == null || dialog.nombreCompletoChofer == null || dialog.activoChofer == null)
                        {
                            throw new Exception(T("msg_TodosLosCamposSonObligatorios", "Todos los campos son obligatorios."));
                        }

                        // El num_chofer es la clave primaria (IDENTITY), por lo que no se modifica.
                        Chofer choferModificado = new Chofer(choferSeleccionado.num_chofer, dialog.dniChofer.Value, dialog.nombreCompletoChofer, dialog.activoChofer.Value);
                        GestorChofer.ModificarChofer(choferModificado);
                        this.refrescarChoferes();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(T("msg_ErrorAlModificarElChofer", "Error al modificar el chofer: ") + ex.Message);
            }
        }

        private void buttonChoferEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                Chofer choferSeleccionado = obtenerChoferSeleccionado();

                DialogResult confirmacion = MessageBox.Show(
                    string.Format(T("msg_ConfirmarEliminarChofer", "¿Está seguro de que quiere eliminar al chofer {0} (DNI {1})?"), choferSeleccionado.nombreCompleto, choferSeleccionado.dni),
                    T("msg_ConfirmarEliminacion", "Confirmar eliminación"),
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmacion == DialogResult.Yes)
                {
                    GestorChofer.EliminarChofer(choferSeleccionado.num_chofer);
                    this.refrescarChoferes();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(T("msg_ErrorAlEliminarElChofer", "Error al eliminar el chofer: ") + ex.Message);
            }
        }

        private Chofer obtenerChoferSeleccionado()
        {
            return dataGridViewChoferes.CurrentRow?.DataBoundItem as Chofer ?? throw new Exception(T("msg_NoSeHaSeleccionadoNingunChofer", "No se ha seleccionado ningún chofer."));
        }

        private void refrescarChoferes()
        {
            try
            {
                dataGridViewChoferes.DataSource = null;
                dataGridViewChoferes.DataSource = GestorChofer.ObtenerChoferes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(T("msg_ErrorAlRefrescarLosChoferes", "Error al refrescar los choferes: ") + ex.Message);
            }
        }
    }
}
