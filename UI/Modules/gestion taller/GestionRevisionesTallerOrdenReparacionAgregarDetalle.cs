using BE.taller_entities;
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
    public partial class GestionRevisionesTallerOrdenReparacionAgregarDetalle : Form
    {
        private readonly BindingList<OrdenReparacion> ordenes = new BindingList<OrdenReparacion>();
        private OrdenReparacion? ordenSeleccionada;

        // Órdenes generadas por el diálogo, cada una con sus detalles, para que la UI que lo abre las agregue a la revisión
        public List<OrdenReparacion> OrdenesReparacion => ordenes.ToList();

        public GestionRevisionesTallerOrdenReparacionAgregarDetalle(RevisionTaller nRevision)
        {
            InitializeComponent();

            listBoxGestionRevisionesOrdenesReparacion.DataSource = ordenes;
            listBoxGestionRevisionesOrdenesReparacion.DisplayMember = nameof(OrdenReparacion.motivoReparacion);

            // La grilla de insumos muestra solo insumo y cantidad: el costo se asigna más adelante
            dataGridViewGestionRevisionesOrdenesInsumos.AutoGenerateColumns = false;
            dataGridViewGestionRevisionesOrdenesInsumos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "insumo",
                HeaderText = "Insumo",
                DataPropertyName = nameof(DetalleOrdenReparacion.insumo),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            dataGridViewGestionRevisionesOrdenesInsumos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "cantidad",
                HeaderText = "Cantidad",
                DataPropertyName = nameof(DetalleOrdenReparacion.cantidad)
            });

            // Los insumos recién se pueden cargar cuando hay una orden seleccionada
            groupBoxGestionRevisionesOrdenesInsumos.Enabled = false;
        }

        private void buttonGestionRevisionesAltaOrdenAgregarOrden_Click(object sender, EventArgs e)
        {
            string motivoReparacion = textBoxMotivoReparacion.Text.Trim();
            if (string.IsNullOrEmpty(motivoReparacion))
            {
                MessageBox.Show("Ingrese el motivo de la reparación.");
                return;
            }

            OrdenReparacion nuevaOrden = new OrdenReparacion(-1, motivoReparacion, new List<DetalleOrdenReparacion>());
            ordenes.Add(nuevaOrden);
            textBoxMotivoReparacion.Clear();

            // Se selecciona la orden recién creada para poder cargarle insumos
            listBoxGestionRevisionesOrdenesReparacion.SelectedItem = nuevaOrden;
        }

        private void buttonGestionRevisionesAltaOrdenAgregarInsumo_Click(object sender, EventArgs e)
        {
            if (ordenSeleccionada == null)
            {
                MessageBox.Show("Seleccione una orden de reparación antes de agregar insumos.");
                return;
            }

            string insumo = textBoxNombreInsumo.Text.Trim();
            if (string.IsNullOrEmpty(insumo))
            {
                MessageBox.Show("Ingrese el nombre del insumo.");
                return;
            }

            int cantidad = (int)numericUpDownCantidadInsumo.Value;
            if (cantidad <= 0)
            {
                MessageBox.Show("Ingrese una cantidad mayor a cero.");
                return;
            }

            ordenSeleccionada.AgregarDetalle(new DetalleOrdenReparacion(-1, insumo, cantidad, 0));

            textBoxNombreInsumo.Clear();
            numericUpDownCantidadInsumo.Value = 0;

            RefrescarDetallesSeleccionados();
        }

        private void buttonGestionRevisionesAltaOrdenesReparacionConfirmar_Click(object sender, EventArgs e)
        {
            if (ordenes.Count == 0)
            {
                MessageBox.Show("Agregue al menos una orden de reparación.");
                return;
            }

            // Cada orden debe tener como mínimo un detalle
            OrdenReparacion? ordenSinDetalles = ordenes.FirstOrDefault(orden => orden.detalles.Count == 0);
            if (ordenSinDetalles != null)
            {
                MessageBox.Show($"La orden \"{ordenSinDetalles.motivoReparacion}\" debe tener al menos un insumo.");
                listBoxGestionRevisionesOrdenesReparacion.SelectedItem = ordenSinDetalles;
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void listBoxGestionRevisionesOrdenesReparacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            ordenSeleccionada = listBoxGestionRevisionesOrdenesReparacion.SelectedItem as OrdenReparacion;

            groupBoxGestionRevisionesOrdenesInsumos.Enabled = ordenSeleccionada != null;
            RefrescarDetallesSeleccionados();
        }

        private void RefrescarDetallesSeleccionados()
        {
            dataGridViewGestionRevisionesOrdenesInsumos.DataSource = null;
            dataGridViewGestionRevisionesOrdenesInsumos.DataSource = ordenSeleccionada?.detalles;
        }
    }
}
