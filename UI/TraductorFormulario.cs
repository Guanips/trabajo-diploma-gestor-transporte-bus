using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace UI
{
    /// <summary>
    /// Encuentra los textos traducibles de un formulario (título, labels, botones, group boxes,
    /// pestañas, ítems de menú y encabezados de columnas) y les aplica las traducciones.
    ///
    /// Cada texto se identifica con la clave 'Formulario.Control' (o 'Formulario.Grilla.Columna'),
    /// así dos formularios con controles del mismo nombre no comparten traducción.
    /// Para excluir un control (por ejemplo un label cuyo texto se arma por código) se le asigna
    /// <c>Tag = "notranslate"</c>.
    /// </summary>
    internal sealed class TraductorFormulario
    {
        public const string TagNoTraducir = "notranslate";

        // Solo se registran textos con al menos una letra (se ignoran "0", "-", "...", etc.)
        private static readonly Regex TieneLetras = new Regex(@"\p{L}", RegexOptions.Compiled);

        private readonly Form _formulario;

        // Último texto que el traductor dejó en cada componente. Si al volver a traducir el texto
        // actual es distinto, es porque el código del formulario lo cambió y no se debe pisar.
        private readonly ConditionalWeakTable<object, string> _textosAplicados = new();

        public TraductorFormulario(Form formulario)
        {
            _formulario = formulario;
        }

        private string NombreFormulario => _formulario.GetType().Name;

        public List<(string Clave, string? Formulario, string Texto)> RecolectarEtiquetas()
        {
            return ObtenerElementos()
                .Select(e => (e.Clave, (string?)NombreFormulario, e.TextoParaRegistrar()))
                .Where(e => EsTextoRegistrable(e.Item3))
                .ToList();
        }

        public List<(string Clave, string? Formulario, string Texto)> RecolectarEtiquetas(DataGridViewColumn columna)
        {
            return ElementosDeColumna(columna)
                .Select(e => (e.Clave, (string?)NombreFormulario, e.TextoParaRegistrar()))
                .Where(e => EsTextoRegistrable(e.Item3))
                .ToList();
        }

        public void Aplicar(IReadOnlyDictionary<string, string> traducciones)
        {
            foreach (ElementoTraducible elemento in ObtenerElementos())
            {
                AplicarElemento(elemento, traducciones);
            }
        }

        public void Aplicar(DataGridViewColumn columna, IReadOnlyDictionary<string, string> traducciones)
        {
            foreach (ElementoTraducible elemento in ElementosDeColumna(columna))
            {
                AplicarElemento(elemento, traducciones);
            }
        }

        private void AplicarElemento(ElementoTraducible elemento, IReadOnlyDictionary<string, string> traducciones)
        {
            string textoActual = elemento.Obtener();

            if (_textosAplicados.TryGetValue(elemento.Componente, out string? ultimoAplicado) && textoActual != ultimoAplicado)
            {
                return;
            }

            if (traducciones.TryGetValue(elemento.Clave, out string? traduccion))
            {
                elemento.Asignar(traduccion);
                _textosAplicados.AddOrUpdate(elemento.Componente, traduccion);
            }
            else
            {
                _textosAplicados.AddOrUpdate(elemento.Componente, textoActual);
            }
        }

        private IEnumerable<ElementoTraducible> ObtenerElementos()
        {
            // Los formularios sin título propio tienen como Text el nombre de la clase: no hay nada que traducir
            if (_formulario.Text != NombreFormulario)
            {
                yield return new ElementoTraducible(_formulario, NombreFormulario, () => _formulario.Text, t => _formulario.Text = t);
            }

            foreach (ElementoTraducible elemento in ElementosDeControles(_formulario.Controls))
            {
                yield return elemento;
            }
        }

        private IEnumerable<ElementoTraducible> ElementosDeControles(Control.ControlCollection controles)
        {
            foreach (Control control in controles)
            {
                bool excluido = EsExcluido(control.Tag) || string.IsNullOrEmpty(control.Name);

                if (!excluido && control is Label or ButtonBase or GroupBox or TabPage)
                {
                    Control c = control;
                    yield return new ElementoTraducible(c, Clave(c.Name), () => c.Text, t => c.Text = t);
                }

                if (control is DataGridView grilla)
                {
                    foreach (DataGridViewColumn columna in grilla.Columns)
                    {
                        foreach (ElementoTraducible elemento in ElementosDeColumna(columna))
                            yield return elemento;
                    }
                }

                if (control is ToolStrip barra)
                {
                    foreach (ElementoTraducible elemento in ElementosDeItems(barra.Items))
                        yield return elemento;
                }

                if (control.ContextMenuStrip != null)
                {
                    foreach (ElementoTraducible elemento in ElementosDeItems(control.ContextMenuStrip.Items))
                        yield return elemento;
                }

                // Los controles compuestos (DataGridView, NumericUpDown, etc.) tienen hijos internos sin interés
                if (control is not DataGridView and not UpDownBase && control.HasChildren)
                {
                    foreach (ElementoTraducible elemento in ElementosDeControles(control.Controls))
                        yield return elemento;
                }
            }
        }

        private IEnumerable<ElementoTraducible> ElementosDeItems(ToolStripItemCollection items)
        {
            foreach (ToolStripItem item in items)
            {
                if (item is ToolStripSeparator or ToolStripControlHost) continue;

                if (!EsExcluido(item.Tag) && !string.IsNullOrEmpty(item.Name))
                {
                    ToolStripItem i = item;
                    yield return new ElementoTraducible(i, Clave(i.Name), () => i.Text ?? string.Empty, t => i.Text = t);
                }

                if (item is ToolStripDropDownItem desplegable)
                {
                    foreach (ElementoTraducible elemento in ElementosDeItems(desplegable.DropDownItems))
                        yield return elemento;
                }
            }
        }

        private IEnumerable<ElementoTraducible> ElementosDeColumna(DataGridViewColumn columna)
        {
            DataGridView? grilla = columna.DataGridView;
            if (grilla == null || EsExcluido(grilla.Tag) || EsExcluido(columna.Tag) || string.IsNullOrEmpty(grilla.Name)) yield break;

            // Las columnas creadas por código a veces no tienen Name: se usa la propiedad enlazada
            string nombreColumna = !string.IsNullOrEmpty(columna.Name) ? columna.Name : columna.DataPropertyName;
            if (string.IsNullOrEmpty(nombreColumna)) yield break;

            DataGridViewColumn col = columna;
            yield return new ElementoTraducible(col, Clave($"{grilla.Name}.{nombreColumna}"), () => col.HeaderText, t => col.HeaderText = t)
            {
                // Las columnas autogeneradas tienen como encabezado el nombre de la propiedad (Ej: EstaBloqueado)
                TextoParaRegistrar = () => col.HeaderText == col.DataPropertyName ? SepararPalabras(col.HeaderText) : col.HeaderText
            };
        }

        private string Clave(string nombreComponente) => $"{NombreFormulario}.{nombreComponente}";

        private static bool EsExcluido(object? tag) => tag is string s && s.Equals(TagNoTraducir, StringComparison.OrdinalIgnoreCase);

        private sealed class ElementoTraducible
        {
            private readonly Func<string> _obtener;
            private readonly Action<string> _asignar;

            public object Componente { get; }
            public string Clave { get; }

            /// <summary>Texto con el que se registra la etiqueta en el idioma por defecto.</summary>
            public Func<string> TextoParaRegistrar { get; init; }

            public ElementoTraducible(object componente, string clave, Func<string> obtener, Action<string> asignar)
            {
                Componente = componente;
                Clave = clave;
                _obtener = obtener;
                _asignar = asignar;
                TextoParaRegistrar = Obtener;
            }

            public string Obtener() => _obtener() ?? string.Empty;
            public void Asignar(string texto) => _asignar(texto);
        }

        /// <summary>"EstaBloqueado" -> "Esta bloqueado", "num_interno" -> "Num interno".</summary>
        private static string SepararPalabras(string nombre)
        {
            string separado = Regex.Replace(nombre.Replace('_', ' '), @"(?<=\p{Ll})(?=\p{Lu})", " ").Trim();
            return separado.Length == 0 ? nombre : char.ToUpper(separado[0]) + separado.Substring(1).ToLower();
        }

        private static bool EsTextoRegistrable(string texto) => !string.IsNullOrWhiteSpace(texto) && TieneLetras.IsMatch(texto);
    }
}
