using BE.interno_entities;
using BLL.interno_components;
using System;
using System.Windows.Forms;

namespace UI.Modules.gestion_internos
{
    public partial class GestionInternoUI : FormBaseObserver
    {
        public GestionInternoUI()
        {
            InitializeComponent();
            this.refrescarInternos();
        }

        private void buttonInternoAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                using (GestionInternoAgregarModificarUI dialog = new GestionInternoAgregarModificarUI(null, null, null, null, null))
                {
                    dialog.ShowDialog(this);

                    if (dialog.DialogResult == DialogResult.OK)
                    {
                        if (dialog.numeroInterno == null || dialog.patenteInterno == null || dialog.modeloInterno == null || dialog.fechaIncorporacionInterno == null || dialog.disponibleInterno == null)
                        {
                            throw new Exception(T("msg_TodosLosCamposSonObligatorios", "Todos los campos son obligatorios."));
                        }

                        Interno nInterno = new Interno(dialog.numeroInterno.Value, dialog.patenteInterno, dialog.modeloInterno, dialog.fechaIncorporacionInterno.Value, dialog.disponibleInterno.Value);
                        GestorInterno.InsertarInterno(nInterno);
                        this.refrescarInternos();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(T("msg_ErrorAlAgregarElInterno", "Error al agregar el interno: ") + ex.Message);
            }
        }

        private void buttonInternoModificar_Click(object sender, EventArgs e)
        {
            try
            {
                Interno internoSeleccionado = obtenerInternoSeleccionado();

                using (GestionInternoAgregarModificarUI dialog = new GestionInternoAgregarModificarUI(internoSeleccionado.num_interno, internoSeleccionado.patente, internoSeleccionado.modelo, internoSeleccionado.fechaIncorporacion, internoSeleccionado.disponible))
                {
                    dialog.ShowDialog(this);

                    if (dialog.DialogResult == DialogResult.OK)
                    {
                        if (dialog.patenteInterno == null || dialog.modeloInterno == null || dialog.fechaIncorporacionInterno == null || dialog.disponibleInterno == null)
                        {
                            throw new Exception(T("msg_TodosLosCamposSonObligatorios", "Todos los campos son obligatorios."));
                        }

                        // El num_interno es la clave primaria (IDENTITY), por lo que no se modifica.
                        Interno internoModificado = new Interno(internoSeleccionado.num_interno, dialog.patenteInterno, dialog.modeloInterno, dialog.fechaIncorporacionInterno.Value, dialog.disponibleInterno.Value);
                        GestorInterno.ModificarInterno(internoModificado);
                        this.refrescarInternos();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(T("msg_ErrorAlModificarElInterno", "Error al modificar el interno: ") + ex.Message);
            }
        }

        private void buttonInternoEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                Interno internoSeleccionado = obtenerInternoSeleccionado();

                DialogResult confirmacion = MessageBox.Show(
                    string.Format(T("msg_ConfirmarEliminarInterno", "¿Está seguro de que quiere eliminar el interno {0} (patente {1})?"), internoSeleccionado.num_interno, internoSeleccionado.patente),
                    T("msg_ConfirmarEliminacion", "Confirmar eliminación"),
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmacion == DialogResult.Yes)
                {
                    GestorInterno.EliminarInterno(internoSeleccionado.num_interno);
                    this.refrescarInternos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(T("msg_ErrorAlEliminarElInterno", "Error al eliminar el interno: ") + ex.Message);
            }
        }

        private Interno obtenerInternoSeleccionado()
        {
            return dataGridViewInternos.CurrentRow?.DataBoundItem as Interno ?? throw new Exception(T("msg_NoSeHaSeleccionadoNingunInterno", "No se ha seleccionado ningún interno."));
        }

        private void refrescarInternos()
        {
            try
            {
                dataGridViewInternos.DataSource = null;
                dataGridViewInternos.DataSource = GestorInterno.ObtenerInternos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(T("msg_ErrorAlRefrescarLosInternos", "Error al refrescar los internos: ") + ex.Message);
            }
        }
    }
}
