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
    public partial class GestionCargasCombustibleUI : Form
    {
        public GestionCargasCombustibleUI()
        {
            InitializeComponent();
            Tema.Aplicar(this);
            Disposicion.Montar(this, Disponer);
            inicializarComponentes();
        }

        private void inicializarComponentes ()
        {
            Usuario? usuarioActual = SessionManager.getInstance.ObtenerUsuarioActivo();
            if (usuarioActual == null)
            {
                MessageBox.Show("No hay un usuario activo en la sesión.");
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
                    MessageBox.Show("La carga seleccionada ya está anulada.");
                    return;
                }

                if (MessageBox.Show("¿Está seguro de que desea anular la carga seleccionada?", "Confirmar anulación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }

                string motivoAnulacion = Interaction.InputBox("Ingrese el motivo de la anulación:", "Anular carga de combustible", "");
                if (string.IsNullOrWhiteSpace(motivoAnulacion))
                {
                    MessageBox.Show("Debe ingresar un motivo de anulación.");
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
            return dataGridViewGestionCombustibleCargas.CurrentRow?.DataBoundItem as CargaCombustible ?? throw new Exception("No se ha seleccionado ninguna carga de combustible.");
        }

        private Interno ObtenerInternoSeleccionado ()
        {
            if (comboBoxInterno.SelectedItem is Interno internoSeleccionado)
            {
                return internoSeleccionado;
            }
            else
            {
                throw new InvalidOperationException("No se ha seleccionado un interno válido.");
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
                MessageBox.Show($"Error al actualizar la grilla de cargas de combustible: {ex.Message}");
            }
        }
    }
}
