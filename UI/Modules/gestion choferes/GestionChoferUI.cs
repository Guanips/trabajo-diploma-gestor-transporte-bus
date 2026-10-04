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
    public partial class GestionChoferUI : Form
    {
        public GestionChoferUI()
        {
            InitializeComponent();
            Tema.Aplicar(this);
            Disposicion.Montar(this, Disponer);
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
                            throw new Exception("Todos los campos son obligatorios.");
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
                MessageBox.Show($"Error al agregar el chofer: {ex.Message}");
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
                            throw new Exception("Todos los campos son obligatorios.");
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
                MessageBox.Show($"Error al modificar el chofer: {ex.Message}");
            }
        }

        private void buttonChoferEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                Chofer choferSeleccionado = obtenerChoferSeleccionado();

                DialogResult confirmacion = MessageBox.Show(
                    $"¿Está seguro de que quiere eliminar al chofer {choferSeleccionado.nombreCompleto} (DNI {choferSeleccionado.dni})?",
                    "Confirmar eliminación",
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
                MessageBox.Show($"Error al eliminar el chofer: {ex.Message}");
            }
        }

        private Chofer obtenerChoferSeleccionado()
        {
            return dataGridViewChoferes.CurrentRow?.DataBoundItem as Chofer ?? throw new Exception("No se ha seleccionado ningún chofer.");
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
                MessageBox.Show($"Error al refrescar los choferes: {ex.Message}");
            }
        }
    }
}
