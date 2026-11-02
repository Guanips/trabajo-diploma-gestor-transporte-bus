using BE;
using BE.interno_entities;
using BE.taller_entities;
using BLL;
using BLL.interno_components;
using BLL.taller_components;
using Microsoft.VisualBasic;
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
    public partial class GestionCargasCombustibleUI : FormBaseObserver
    {
        // Mensajes del formulario (MessageBox y textos armados por código) en el idioma por defecto
        private static readonly Dictionary<string, string> MensajesFormulario = new()
        {
            ["msg_AnularCargaDeCombustible"] = "Anular carga de combustible",
            ["msg_ConfirmarAnulacion"] = "Confirmar anulación",
            ["msg_DebeIngresarUnMotivoDeAnulacion"] = "Debe ingresar un motivo de anulación.",
            ["msg_ErrorAlActualizarLaGrillaDeCargasDeCombustible"] = "Error al actualizar la grilla de cargas de combustible: ",
            ["msg_EstaSeguroDeQueDeseaAnularLaCargaSeleccionada"] = "¿Está seguro de que desea anular la carga seleccionada?",
            ["msg_IngreseElMotivoDeLaAnulacion"] = "Ingrese el motivo de la anulación:",
            ["msg_LaCargaSeleccionadaYaEstaAnulada"] = "La carga seleccionada ya está anulada.",
            ["msg_NoSeHaSeleccionadoNingunaCargaDeCombustible"] = "No se ha seleccionado ninguna carga de combustible.",
            ["msg_NoSeHaSeleccionadoUnInternoValido"] = "No se ha seleccionado un interno válido."
        };

        protected override IReadOnlyDictionary<string, string> Mensajes => MensajesFormulario;

        public GestionCargasCombustibleUI()
        {
            InitializeComponent();
            inicializarComponentes();
        }

        private void inicializarComponentes ()
        {
            Usuario? usuarioActual = SessionManager.getInstance.ObtenerUsuarioActivo();
            if (usuarioActual == null)
            {
                MessageBox.Show(T("msg_NoHayUnUsuarioActivoEnLaSesion"));
                this.Close();
                return;
            }

            buttonGestionCombustibleAnularCarga.Enabled = usuarioActual.Permisos.Any(p => p.ValidarPermiso("PERM-COMBUSTIBLE-ANULAR"));

            ActualizarGrillaCargasCombustible();

            comboBoxInterno.DataSource = GestorInterno.ObtenerInternos();
        }

        private void buttonGestionCombustibleConfirmarRegistro_Click(object sender, EventArgs e)
        {
            try
            {
                Interno internoSeleccionado = ObtenerInternoSeleccionado();
                DateTime fechaHoraCarga = dateTimePickerCargaFechaHora.Value;
                decimal litrosCargados = numericUpDownLitros.Value;
                decimal precioPorLitro = numericUpDownPrecioLitro.Value;
                decimal kilometrajeActual = numericUpDownKilometraje.Value;

                CargaCombustible nCarga = new CargaCombustible(-1, internoSeleccionado, fechaHoraCarga, litrosCargados, precioPorLitro, (int)kilometrajeActual, false, null);
                GestorCargaCombustible.RegistrarCarga(nCarga);
                ActualizarGrillaCargasCombustible();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonGestionCombustibleAnularCarga_Click(object sender, EventArgs e)
        {
            try
            {
                CargaCombustible cargaSeleccionada = ObtenerCargaSeleccionada();

                if (cargaSeleccionada.anulada)
                {
                    MessageBox.Show(T("msg_LaCargaSeleccionadaYaEstaAnulada"));
                    return;
                }

                if (MessageBox.Show(T("msg_EstaSeguroDeQueDeseaAnularLaCargaSeleccionada"), T("msg_ConfirmarAnulacion"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }

                string motivoAnulacion = Interaction.InputBox(T("msg_IngreseElMotivoDeLaAnulacion"), T("msg_AnularCargaDeCombustible"), "");
                if (string.IsNullOrWhiteSpace(motivoAnulacion))
                {
                    MessageBox.Show(T("msg_DebeIngresarUnMotivoDeAnulacion"));
                    return;
                }

                GestorCargaCombustible.AnularCarga(cargaSeleccionada.id, motivoAnulacion);
                ActualizarGrillaCargasCombustible();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private CargaCombustible ObtenerCargaSeleccionada ()
        {
            return dataGridViewGestionCombustibleCargas.CurrentRow?.DataBoundItem as CargaCombustible ?? throw new Exception(T("msg_NoSeHaSeleccionadoNingunaCargaDeCombustible"));
        }

        private Interno ObtenerInternoSeleccionado ()
        {
            if (comboBoxInterno.SelectedItem is Interno internoSeleccionado)
            {
                return internoSeleccionado;
            }
            else
            {
                throw new InvalidOperationException(T("msg_NoSeHaSeleccionadoUnInternoValido"));
            }
        }

        private void ActualizarGrillaCargasCombustible()
        {
            try
            {
                List<CargaCombustible> cargas = GestorCargaCombustible.ObtenerCargas();
                dataGridViewGestionCombustibleCargas.DataSource = cargas;
            }
            catch (Exception ex)
            {
                MessageBox.Show(T("msg_ErrorAlActualizarLaGrillaDeCargasDeCombustible") + ex.Message);
            }
        }
    }
}
