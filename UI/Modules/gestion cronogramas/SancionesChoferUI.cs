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
        // Mensajes del formulario (MessageBox y textos armados por código) en el idioma por defecto
        private static readonly Dictionary<string, string> MensajesFormulario = new()
        {
            ["msg_ErrorAlActualizarLaGrillaDeSanciones"] = "Error al actualizar la grilla de sanciones: ",
            ["msg_EstaSeguroDeQueDeseaEliminarLaSancionSeleccionada"] = "¿Está seguro de que desea eliminar la sanción seleccionada?",
            ["msg_NoHayChoferesDisponibles"] = "No hay choferes disponibles.",
            ["msg_NoSeHaSeleccionadoNingunaSancion"] = "No se ha seleccionado ninguna sanción.",
            ["msg_SancionEliminadaExitosamente"] = "Sanción eliminada exitosamente."
        };

        protected override IReadOnlyDictionary<string, string> Mensajes => MensajesFormulario;

        public SancionesChoferUI()
        {
            InitializeComponent();

            try
            {
                Usuario? usuarioActual = SessionManager.getInstance.ObtenerUsuarioActivo();
                if (usuarioActual == null)
                {
                    MessageBox.Show(T("msg_NoHayUnUsuarioActivoEnLaSesion"));
                    this.Close();
                    return;
                }

                buttonSancionesChoferEliminar.Enabled = usuarioActual.Permisos.Any(p => p.ValidarPermiso("PERM-ELIMINAR-SANCION"));

                List<Chofer> choferes = GestorChofer.ObtenerChoferes();
                if (choferes.Count == 0)
                {
                    MessageBox.Show(T("msg_NoHayChoferesDisponibles"), T("msg_Informacion"), MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    MessageBox.Show(T("msg_NoSeHaSeleccionadoNingunaSancion"));
                    return;
                }

                if (MessageBox.Show(T("msg_EstaSeguroDeQueDeseaEliminarLaSancionSeleccionada"), T("msg_ConfirmarEliminacion"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }

                AuditorCronogramasBLL.EliminarSancion(sancionSeleccionada.id);
                MessageBox.Show(T("msg_SancionEliminadaExitosamente"));
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
                MessageBox.Show(T("msg_ErrorAlActualizarLaGrillaDeSanciones") + ex.Message);
            }
        }
    }
}
