using BE;
using BLL;

namespace UI.Modules
{
    public partial class GestionIdiomasUI : FormBaseObserver
    {
        private static readonly Color ColorSinTraducir = Color.FromArgb(255, 244, 214);

        private List<Idioma> _idiomas = new List<Idioma>();
        private Idioma? _idiomaSeleccionado;
        private bool _modoNuevoIdioma;

        // Todas las etiquetas del idioma seleccionado (la grilla muestra las que pasan los filtros)
        private List<EtiquetaTraduccion> _filas = new List<EtiquetaTraduccion>();

        // Ediciones pendientes de guardar: clave -> texto nuevo (vacío = quitar traducción)
        private readonly Dictionary<string, string?> _cambiosPendientes = new Dictionary<string, string?>();

        private bool _ignorarCambioSeleccion;

        public GestionIdiomasUI()
        {
            InitializeComponent();
        }

        private void GestionIdiomasUI_Load(object? sender, EventArgs e)
        {
            CargarIdiomas(GestorIdioma.GetInstance.IdiomaActual);
        }

        // ---------------------------------------------------------
        // Idiomas
        // ---------------------------------------------------------

        private void CargarIdiomas(string? codigoASeleccionar)
        {
            try
            {
                _idiomas = GestorIdioma.GetInstance.ObtenerIdiomasDisponibles();

                _ignorarCambioSeleccion = true;
                dataGridViewIdiomas.Rows.Clear();
                foreach (Idioma idioma in _idiomas)
                {
                    int indice = dataGridViewIdiomas.Rows.Add(idioma.Codigo, idioma.Nombre, $"{idioma.PorcentajeAvance}%");
                    dataGridViewIdiomas.Rows[indice].Tag = idioma;
                    if (idioma.EsDefault)
                        dataGridViewIdiomas.Rows[indice].DefaultCellStyle.Font = new Font(dataGridViewIdiomas.Font, FontStyle.Bold);
                }
                _ignorarCambioSeleccion = false;

                DataGridViewRow? fila = dataGridViewIdiomas.Rows.Cast<DataGridViewRow>()
                    .FirstOrDefault(r => ((Idioma)r.Tag!).Codigo.Equals(codigoASeleccionar, StringComparison.OrdinalIgnoreCase))
                    ?? dataGridViewIdiomas.Rows.Cast<DataGridViewRow>().FirstOrDefault();

                if (fila != null)
                {
                    dataGridViewIdiomas.ClearSelection();
                    fila.Selected = true;
                    dataGridViewIdiomas.CurrentCell = fila.Cells[0];
                    SeleccionarIdioma((Idioma)fila.Tag!);
                }
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
            finally
            {
                _ignorarCambioSeleccion = false;
            }
        }

        private void dataGridViewIdiomas_SelectionChanged(object? sender, EventArgs e)
        {
            if (_ignorarCambioSeleccion || dataGridViewIdiomas.SelectedRows.Count == 0) return;

            Idioma idioma = (Idioma)dataGridViewIdiomas.SelectedRows[0].Tag!;
            if (idioma == _idiomaSeleccionado && !_modoNuevoIdioma) return;

            if (!ConfirmarDescartarCambios())
            {
                // Se vuelve a marcar el idioma anterior sin disparar de nuevo el evento
                _ignorarCambioSeleccion = true;
                DataGridViewRow? anterior = dataGridViewIdiomas.Rows.Cast<DataGridViewRow>().FirstOrDefault(r => r.Tag == _idiomaSeleccionado);
                if (anterior != null)
                {
                    dataGridViewIdiomas.ClearSelection();
                    anterior.Selected = true;
                    dataGridViewIdiomas.CurrentCell = anterior.Cells[0];
                }
                _ignorarCambioSeleccion = false;
                return;
            }

            SeleccionarIdioma(idioma);
        }

        private void SeleccionarIdioma(Idioma idioma)
        {
            _idiomaSeleccionado = idioma;
            _modoNuevoIdioma = false;

            textBoxCodigo.Text = idioma.Codigo;
            textBoxCodigo.ReadOnly = true;
            textBoxNombre.Text = idioma.Nombre;

            CargarTraducciones();
        }

        private void btnNuevoIdioma_Click(object? sender, EventArgs e)
        {
            if (!ConfirmarDescartarCambios()) return;

            _modoNuevoIdioma = true;
            _cambiosPendientes.Clear();

            textBoxCodigo.ReadOnly = false;
            textBoxCodigo.Clear();
            textBoxNombre.Clear();
            textBoxCodigo.Focus();
        }

        private void btnGuardarIdioma_Click(object? sender, EventArgs e)
        {
            try
            {
                string codigo = textBoxCodigo.Text.Trim().ToUpper();

                if (_modoNuevoIdioma)
                {
                    GestorIdioma.GetInstance.RegistrarNuevoIdioma(codigo, textBoxNombre.Text);
                    MessageBox.Show(
                        T("msg_IdiomaCreadoExito", "El idioma se creó correctamente. Mientras no tenga traducciones se mostrarán los textos del idioma por defecto."),
                        T("msg_TituloExito", "Éxito"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (_idiomaSeleccionado != null)
                {
                    GestorIdioma.GetInstance.ModificarIdioma(_idiomaSeleccionado.Codigo, textBoxNombre.Text);
                    MessageBox.Show(T("msg_IdiomaModificadoExito", "El idioma se modificó correctamente."),
                        T("msg_TituloExito", "Éxito"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                CargarIdiomas(codigo);
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        // ---------------------------------------------------------
        // Traducciones
        // ---------------------------------------------------------

        private void CargarTraducciones()
        {
            if (_idiomaSeleccionado == null) return;

            try
            {
                _filas = GestorIdioma.GetInstance.ObtenerMatrizTraduccion(_idiomaSeleccionado.Codigo);
                _cambiosPendientes.Clear();

                CargarFiltroFormularios();
                MostrarFilasFiltradas();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private void CargarFiltroFormularios()
        {
            object? seleccionAnterior = comboBoxFormulario.SelectedItem;

            comboBoxFormulario.SelectedIndexChanged -= filtros_Changed;
            comboBoxFormulario.Items.Clear();
            comboBoxFormulario.Items.Add(new OpcionFiltro(null, T("GestionIdiomasUI_FiltroTodos", "(Todos)")));
            comboBoxFormulario.Items.Add(new OpcionFiltro(string.Empty, T("GestionIdiomasUI_FiltroMensajes", "(Mensajes)")));
            foreach (string formulario in _filas.Where(f => f.Formulario != null).Select(f => f.Formulario!).Distinct().OrderBy(f => f))
            {
                comboBoxFormulario.Items.Add(new OpcionFiltro(formulario, formulario));
            }

            comboBoxFormulario.SelectedItem = comboBoxFormulario.Items.Cast<OpcionFiltro>()
                .FirstOrDefault(o => seleccionAnterior is OpcionFiltro anterior && o.Formulario == anterior.Formulario)
                ?? comboBoxFormulario.Items[0];
            comboBoxFormulario.SelectedIndexChanged += filtros_Changed;
        }

        private void filtros_Changed(object? sender, EventArgs e)
        {
            MostrarFilasFiltradas();
        }

        private void MostrarFilasFiltradas()
        {
            string busqueda = textBoxBuscar.Text.Trim();
            string? formulario = (comboBoxFormulario.SelectedItem as OpcionFiltro)?.Formulario;

            IEnumerable<EtiquetaTraduccion> filtradas = _filas;

            if (formulario == string.Empty)
                filtradas = filtradas.Where(f => f.Formulario == null);
            else if (formulario != null)
                filtradas = filtradas.Where(f => f.Formulario == formulario);

            if (checkBoxSoloFaltantes.Checked)
                filtradas = filtradas.Where(f => !f.EstaTraducida);

            if (busqueda.Length > 0)
            {
                filtradas = filtradas.Where(f =>
                    f.Clave.Contains(busqueda, StringComparison.OrdinalIgnoreCase) ||
                    f.TextoReferencia.Contains(busqueda, StringComparison.OrdinalIgnoreCase) ||
                    (f.TextoTraducido?.Contains(busqueda, StringComparison.OrdinalIgnoreCase) ?? false));
            }

            dataGridViewTraducciones.SuspendLayout();
            dataGridViewTraducciones.Rows.Clear();
            foreach (EtiquetaTraduccion fila in filtradas)
            {
                int indice = dataGridViewTraducciones.Rows.Add(fila.Clave, fila.TextoReferencia, fila.TextoTraducido);
                dataGridViewTraducciones.Rows[indice].Tag = fila;
                PintarFila(dataGridViewTraducciones.Rows[indice]);
            }
            dataGridViewTraducciones.ResumeLayout();

            ActualizarProgreso();
        }

        private void dataGridViewTraducciones_CellEndEdit(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != colTraduccion.Index) return;

            DataGridViewRow filaGrilla = dataGridViewTraducciones.Rows[e.RowIndex];
            EtiquetaTraduccion fila = (EtiquetaTraduccion)filaGrilla.Tag!;
            string? texto = filaGrilla.Cells[colTraduccion.Index].Value?.ToString();

            if ((texto ?? string.Empty) == (fila.TextoTraducido ?? string.Empty)) return;

            fila.TextoTraducido = string.IsNullOrWhiteSpace(texto) ? null : texto;
            _cambiosPendientes[fila.Clave] = texto;

            PintarFila(filaGrilla);
            ActualizarProgreso();
        }

        private void btnGuardarTraducciones_Click(object? sender, EventArgs e)
        {
            if (_idiomaSeleccionado == null || _cambiosPendientes.Count == 0) return;

            try
            {
                dataGridViewTraducciones.EndEdit();
                GestorIdioma.GetInstance.GuardarTraducciones(_idiomaSeleccionado.Codigo, new Dictionary<string, string?>(_cambiosPendientes));
                _cambiosPendientes.Clear();

                MessageBox.Show(T("msg_TraduccionesGuardadasExito", "Las traducciones se guardaron correctamente."),
                    T("msg_TituloExito", "Éxito"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarIdiomas(_idiomaSeleccionado.Codigo);
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private void btnDescartarCambios_Click(object? sender, EventArgs e)
        {
            if (_cambiosPendientes.Count == 0) return;
            if (ConfirmarDescartarCambios()) CargarTraducciones();
        }

        // ---------------------------------------------------------
        // Auxiliares
        // ---------------------------------------------------------

        private bool ConfirmarDescartarCambios()
        {
            if (_cambiosPendientes.Count == 0) return true;

            DialogResult respuesta = MessageBox.Show(
                T("msg_DescartarTraducciones", "Hay traducciones sin guardar. ¿Desea descartarlas?"),
                T("msg_TituloConfirmacion", "Confirmación"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta != DialogResult.Yes) return false;

            _cambiosPendientes.Clear();
            return true;
        }

        private static void PintarFila(DataGridViewRow fila)
        {
            bool traducida = ((EtiquetaTraduccion)fila.Tag!).EstaTraducida;
            fila.Cells[2].Style.BackColor = traducida ? Color.Empty : ColorSinTraducir;
        }

        private void ActualizarProgreso()
        {
            int traducidas = _filas.Count(f => f.EstaTraducida);
            string pendientes = _cambiosPendientes.Count > 0
                ? " " + string.Format(T("GestionIdiomasUI_CambiosPendientes", "({0} cambios sin guardar)"), _cambiosPendientes.Count)
                : string.Empty;

            labelProgreso.Text = string.Format(T("GestionIdiomasUI_Progreso", "Traducidas: {0} de {1}"), traducidas, _filas.Count) + pendientes;
        }

        private void MostrarError(Exception ex)
        {
            MessageBox.Show(ex.Message, T("msg_TituloError", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        protected override void TraducirElementosParticulares(string codigoIdioma)
        {
            // Textos armados por código: se regeneran en el idioma nuevo
            if (_idiomaSeleccionado != null)
            {
                CargarFiltroFormularios();
                ActualizarProgreso();
            }
        }

        private sealed class OpcionFiltro
        {
            // null = todos, "" = mensajes (sin formulario), otro = nombre del formulario
            public string? Formulario { get; }
            public string Texto { get; }

            public OpcionFiltro(string? formulario, string texto)
            {
                Formulario = formulario;
                Texto = texto;
            }

            public override string ToString() => Texto;
        }
    }
}
