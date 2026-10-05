using BE;
using BE.cronograma_entities;
using BE.interno_entities;
using BLL.cronograma_components;
using BLL;
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

namespace UI.Modules.gestion_cronogramas
{
    public partial class SancionesChoferUI : FormBaseObserver
    {
        public SancionesChoferUI()
        {
            InitializeComponent();

            try
            {
                Usuario? usuarioActual = SessionManager.getInstance.ObtenerUsuarioActivo();
                if (usuarioActual == null)
                {
                    MessageBox.Show(T("msg_NoHayUnUsuarioActivoEnLaSesion", "No hay un usuario activo en la sesión."));
                    this.Close();
                    return;
                }

                buttonSancionesChoferEliminar.Enabled = usuarioActual.Permisos.Any(p => p.ValidarPermiso("PERM-ELIMINAR-SANCION"));

                List<Chofer> choferes = GestorChofer.ObtenerChoferes();
                if (choferes.Count == 0)
                {
                    MessageBox.Show(T("msg_NoHayChoferesDisponibles", "No hay choferes disponibles."), T("msg_Informacion", "Información"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    return;
                }
                listBoxChoferes.DataSource = choferes;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void listBoxChoferes_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarGrillaSanciones();
        }

        private void buttonSancionesChoferEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                Sancion? sancionSeleccionada = dataGridViewSancionesChofer.CurrentRow?.DataBoundItem as Sancion;
                if (sancionSeleccionada == null)
                {
                    MessageBox.Show(T("msg_NoSeHaSeleccionadoNingunaSancion", "No se ha seleccionado ninguna sanción."));
                    return;
                }

                if (MessageBox.Show(T("msg_EstaSeguroDeQueDeseaEliminarLaSancionSeleccionada", "¿Está seguro de que desea eliminar la sanción seleccionada?"), T("msg_ConfirmarEliminacion", "Confirmar eliminación"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }

                AuditorCronogramasBLL.EliminarSancion(sancionSeleccionada.id);
                MessageBox.Show(T("msg_SancionEliminadaExitosamente", "Sanción eliminada exitosamente."));
                ActualizarGrillaSanciones();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ActualizarGrillaSanciones()
        {
            try
            {
                if (listBoxChoferes.SelectedItem is Chofer choferSeleccionado)
                {
                    dataGridViewSancionesChofer.DataSource = AuditorCronogramasBLL.ObtenerSancionesPorChofer(choferSeleccionado.num_chofer);
                }
                else
                {
                    dataGridViewSancionesChofer.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(T("msg_ErrorAlActualizarLaGrillaDeSanciones", "Error al actualizar la grilla de sanciones: ") + ex.Message);
            }
        }
    }
}
