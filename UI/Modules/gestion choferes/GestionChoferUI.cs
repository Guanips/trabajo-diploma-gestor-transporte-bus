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
        // Mensajes del formulario (MessageBox y textos armados por código) en el idioma por defecto
        private static readonly Dictionary<string, string> MensajesFormulario = new()
        {
            ["msg_ConfirmarEliminarChofer"] = "¿Está seguro de que quiere eliminar al chofer {0} (DNI {1})?",
            ["msg_ErrorAlAgregarElChofer"] = "Error al agregar el chofer: ",
            ["msg_ErrorAlEliminarElChofer"] = "Error al eliminar el chofer: ",
            ["msg_ErrorAlModificarElChofer"] = "Error al modificar el chofer: ",
            ["msg_ErrorAlRefrescarLosChoferes"] = "Error al refrescar los choferes: "
        };

        protected override IReadOnlyDictionary<string, string> Mensajes => MensajesFormulario;

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
                            throw new Exception(T("msg_TodosLosCamposSonObligatorios"));
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
                MessageBox.Show(T("msg_ErrorAlAgregarElChofer") + ex.Message);
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
                            throw new Exception(T("msg_TodosLosCamposSonObligatorios"));
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
                MessageBox.Show(T("msg_ErrorAlModificarElChofer") + ex.Message);
            }
        }

        private void buttonChoferEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                Chofer choferSeleccionado = obtenerChoferSeleccionado();

                DialogResult confirmacion = MessageBox.Show(
                    string.Format(T("msg_ConfirmarEliminarChofer"), choferSeleccionado.nombreCompleto, choferSeleccionado.dni),
                    T("msg_ConfirmarEliminacion"),
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
                MessageBox.Show(T("msg_ErrorAlEliminarElChofer") + ex.Message);
            }
        }

        private Chofer obtenerChoferSeleccionado()
        {
            return dataGridViewChoferes.CurrentRow?.DataBoundItem as Chofer ?? throw new Exception(T("msg_NoSeHaSeleccionadoNingunChofer"));
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
                MessageBox.Show(T("msg_ErrorAlRefrescarLosChoferes") + ex.Message);
            }
        }
    }
}
