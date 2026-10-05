using BE.interno_entities;
using BE.taller_entities;
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

namespace UI.Modules.gestion_taller
{
    public partial class GestionRevisionesTallerAgregarUI : FormBaseObserver
    {
        public RevisionTaller? NuevaRevision { get; private set; }

        public GestionRevisionesTallerAgregarUI()
        {
            InitializeComponent();

            List<Interno> internos = GestorInterno.ObtenerInternos();
            if (internos == null)
            {
                MessageBox.Show(T("msg_NoHayInternosDisponibles", "No hay internos disponibles"));
                this.DialogResult = DialogResult.Abort;
                this.Close();
            }
            comboBoxInternos.DataSource = internos;
        }

        private void buttonGestionRevisionAgregarConfirmar_Click(object sender, EventArgs e)
        {
            Interno? selectedInterno = comboBoxInternos.SelectedItem as Interno;

            if (selectedInterno == null)
            {
                MessageBox.Show(T("msg_SeleccioneUnInternoValido", "Seleccione un interno válido."));
                return;
            }

            string descripcion = textBoxDescripcion.Text.Trim();
            if (string.IsNullOrEmpty(descripcion))
            {
                MessageBox.Show(T("msg_IngreseUnaDescripcionParaLaRevision", "Ingrese una descripción para la revisión."));
                return;
            }

            DateOnly nFecha = new DateOnly(dateTimePickerFecha.Value.Year, dateTimePickerFecha.Value.Month, dateTimePickerFecha.Value.Day);

            RevisionTaller nRevision = new RevisionTaller(-1, selectedInterno, nFecha, descripcion, checkBoxGestionRevisionAgregarRequiereReparacion.Checked);

            if (checkBoxGestionRevisionAgregarRequiereReparacion.Checked)
            {
                using (GestionRevisionesTallerOrdenReparacionAgregarDetalle dialog = new GestionRevisionesTallerOrdenReparacionAgregarDetalle(nRevision))
                {
                    dialog.ShowDialog();

                    if (dialog.DialogResult != DialogResult.OK)
                    {
                        MessageBox.Show(T("msg_DebeAgregarAlMenosUnaOrdenDeReparacionSiLaRevisionRequiereRe", "Debe agregar al menos una orden de reparación si la revisión requiere reparación."));
                        return;
                    }

                    // Las órdenes ya vienen con sus detalles y se enganchan a la revisión que se está creando
                    foreach (OrdenReparacion orden in dialog.OrdenesReparacion)
                    {
                        nRevision.AgregarOrdenReparacion(orden);
                    }
                }
            }

            NuevaRevision = nRevision;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
