using System.ComponentModel;

namespace UI
{
    /// <summary>
    /// Estilo visual centralizado. Se aplica sobre un formulario ya construido (despues de InitializeComponent)
    /// y recorre sus controles, sin agregar ni quitar ninguno.
    /// </summary>
    internal static class Tema
    {
        public static readonly Color Navy = Color.FromArgb(24, 39, 72);
        public static readonly Color NavyHover = Color.FromArgb(44, 66, 112);
        public static readonly Color Primario = Color.FromArgb(26, 82, 160);
        public static readonly Color PrimarioHover = Color.FromArgb(18, 62, 128);
        public static readonly Color PrimarioPresionado = Color.FromArgb(12, 44, 96);
        public static readonly Color Peligro = Color.FromArgb(185, 40, 40);
        public static readonly Color PeligroHover = Color.FromArgb(160, 42, 42);
        public static readonly Color PeligroPresionado = Color.FromArgb(130, 33, 33);
        public static readonly Color Fondo = Color.FromArgb(225, 230, 239);
        public static readonly Color Superficie = Color.White;
        public static readonly Color Texto = Color.FromArgb(33, 37, 41);
        public static readonly Color TextoSuave = Color.FromArgb(108, 117, 125);
        public static readonly Color Borde = Color.FromArgb(148, 163, 184);
        public static readonly Color Seleccion = Color.FromArgb(191, 219, 254);
        public static readonly Color FilaAlterna = Color.FromArgb(241, 245, 250);
        public static readonly Color Deshabilitado = Color.FromArgb(214, 219, 227);
        public static readonly Color TextoDeshabilitado = Color.FromArgb(105, 114, 128);
        public static readonly Color Campo = Color.FromArgb(244, 246, 250);
        public static readonly Color SecundarioFondo = Color.FromArgb(232, 238, 247);

        private static readonly Font FuenteBase = new("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
        private static readonly Font FuenteTitulo = new("Segoe UI Semibold", 9.5F, FontStyle.Regular, GraphicsUnit.Point);
        private static readonly Font FuenteEncabezado = new("Segoe UI Semibold", 9F, FontStyle.Regular, GraphicsUnit.Point);

        public static void Aplicar(Form form)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;

            form.BackColor = Fondo;
            form.ForeColor = Texto;
            form.Font = FuenteBase;

            foreach (Control c in form.Controls) AplicarControl(c, Fondo);
        }

        public static void AplicarMain(MainUI form, MenuStrip menu, Label etiquetaIdioma, ComboBox comboIdioma)
        {
            form.BackColor = Fondo;
            form.Font = FuenteBase;

            menu.Renderer = new MenuRenderer();
            menu.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point);
            menu.BackColor = Navy;
            menu.ForeColor = Color.White;
            menu.Padding = new Padding(8, 4, 8, 4);
            menu.GripStyle = ToolStripGripStyle.Hidden;
            foreach (ToolStripItem item in menu.Items) item.Padding = new Padding(10, 0, 10, 0);

            etiquetaIdioma.BackColor = Color.Transparent;
            etiquetaIdioma.ForeColor = Color.White;
            etiquetaIdioma.Font = FuenteBase;

            EstilizarCombo(comboIdioma);

            // El area MDI es un control interno del formulario: se colorea cuando se crea el handle.
            form.HandleCreated += (_, _) =>
            {
                foreach (Control c in form.Controls)
                {
                    if (c is MdiClient mdi) mdi.BackColor = Fondo;
                }
            };
        }

        private static void AplicarControl(Control c, Color fondoPadre)
        {
            Color fondoHijos = fondoPadre;

            switch (c)
            {
                case GroupBox gb:
                    // Los hijos heredan la fuente del GroupBox: se fija antes de cambiarla para que el titulo sea el unico en negrita.
                    foreach (Control hijo in gb.Controls) hijo.Font = new Font(hijo.Font, hijo.Font.Style);
                    gb.Font = FuenteTitulo;
                    gb.ForeColor = Primario;
                    gb.BackColor = Superficie;
                    fondoHijos = Superficie;
                    break;

                case Button b:
                    EstilizarBoton(b);
                    break;

                case DataGridView dgv:
                    EstilizarGrilla(dgv);
                    break;

                case ComboBox cb:
                    EstilizarCombo(cb);
                    break;

                case TextBox or MaskedTextBox or NumericUpDown or DateTimePicker or ListBox or TreeView or ListView or CheckedListBox:
                    c.ForeColor = Texto;
                    if (c is not DateTimePicker) c.BackColor = Campo;
                    if (c is ListBox lb) lb.BorderStyle = BorderStyle.FixedSingle;
                    if (c is TreeView tv) { tv.BorderStyle = BorderStyle.FixedSingle; tv.ItemHeight = 22; }
                    if (c is ListView lv) lv.BorderStyle = BorderStyle.FixedSingle;
                    break;

                case Label or CheckBox or RadioButton:
                    c.ForeColor = Texto;
                    c.BackColor = Color.Transparent;
                    break;

                case TabControl or Panel or SplitContainer:
                    c.BackColor = fondoPadre;
                    break;
            }

            foreach (Control hijo in c.Controls) AplicarControl(hijo, fondoHijos);
        }

        private static void EstilizarBoton(Button b)
        {
            string id = b.Name.ToLowerInvariant();
            bool peligro = id.Contains("eliminar") || id.Contains("borrar") || id.Contains("quitar") || id.Contains("bloquear");
            bool secundario = id.Contains("cancelar") || id.Contains("limpiar") || id.Contains("cerrar");

            Color baseColor = peligro ? Peligro : Primario;
            Color hover = peligro ? PeligroHover : PrimarioHover;
            Color presionado = peligro ? PeligroPresionado : PrimarioPresionado;
            Color fondo = secundario ? SecundarioFondo : baseColor;
            Color texto = secundario ? Primario : Color.White;

            b.UseVisualStyleBackColor = false;
            b.FlatStyle = FlatStyle.Flat;
            b.Cursor = Cursors.Hand;
            b.Font = FuenteEncabezado;
            b.FlatAppearance.BorderSize = 1;
            b.FlatAppearance.BorderColor = secundario ? Primario : presionado;
            b.FlatAppearance.MouseOverBackColor = secundario ? Seleccion : hover;
            b.FlatAppearance.MouseDownBackColor = secundario ? Borde : presionado;

            void aplicarEstado()
            {
                b.BackColor = b.Enabled ? fondo : Deshabilitado;
                b.ForeColor = b.Enabled ? texto : TextoDeshabilitado;
                b.FlatAppearance.BorderColor = b.Enabled ? (secundario ? Primario : presionado) : Borde;
                b.Cursor = b.Enabled ? Cursors.Hand : Cursors.Default;
            }

            b.EnabledChanged += (_, _) => aplicarEstado();
            aplicarEstado();
        }

        private static void EstilizarCombo(ComboBox cb)
        {
            cb.FlatStyle = FlatStyle.Flat;
            cb.BackColor = Campo;
            cb.ForeColor = Texto;
        }

        private static void EstilizarGrilla(DataGridView g)
        {
            g.EnableHeadersVisualStyles = false;
            g.BackgroundColor = Superficie;
            g.BorderStyle = BorderStyle.None;
            g.GridColor = Color.FromArgb(205, 212, 224);
            g.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            g.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            g.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            g.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Navy,
                ForeColor = Color.White,
                SelectionBackColor = Navy,
                SelectionForeColor = Color.White,
                Font = FuenteEncabezado,
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                Padding = new Padding(6, 0, 0, 0),
                WrapMode = DataGridViewTriState.False
            };
            g.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            g.ColumnHeadersHeight = 34;

            g.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Superficie,
                ForeColor = Texto,
                SelectionBackColor = Seleccion,
                SelectionForeColor = Texto,
                Padding = new Padding(6, 0, 0, 0)
            };
            g.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = FilaAlterna,
                SelectionBackColor = Seleccion,
                SelectionForeColor = Texto
            };
            g.RowTemplate.Height = 28;
            g.RowHeadersDefaultCellStyle.BackColor = Superficie;
        }

        private sealed class MenuRenderer : ToolStripProfessionalRenderer
        {
            public MenuRenderer() : base(new Colores()) { }

            protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
            {
                var item = e.Item;
                if (!item.Enabled || !(item.Selected || item.Pressed)) return;

                var rect = new Rectangle(Point.Empty, item.Size);
                if (item.IsOnDropDown)
                {
                    rect.Inflate(-3, -1);
                    using var brush = new SolidBrush(Seleccion);
                    e.Graphics.FillRectangle(brush, rect);
                }
                else
                {
                    using var brush = new SolidBrush(item.Pressed ? Primario : NavyHover);
                    e.Graphics.FillRectangle(brush, rect);
                }
            }

            protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
            {
                if (e.Item is ToolStripMenuItem item)
                {
                    e.TextColor = item.IsOnDropDown
                        ? (item.Enabled ? Texto : Deshabilitado)
                        : (item.Enabled ? Color.White : Color.FromArgb(120, 135, 165));
                }
                base.OnRenderItemText(e);
            }

            protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
            {
                if (e.ToolStrip is ToolStripDropDownMenu)
                {
                    using var pen = new Pen(Borde);
                    e.Graphics.DrawRectangle(pen, new Rectangle(0, 0, e.ToolStrip.Width - 1, e.ToolStrip.Height - 1));
                }
            }

            protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
            {
                // Sin franja de iconos: el menu desplegable queda de un solo color.
            }
        }

        private sealed class Colores : ProfessionalColorTable
        {
            public Colores() { UseSystemColors = false; }

            public override Color MenuStripGradientBegin => Navy;
            public override Color MenuStripGradientEnd => Navy;
            public override Color ToolStripDropDownBackground => Superficie;
            public override Color ImageMarginGradientBegin => Superficie;
            public override Color ImageMarginGradientMiddle => Superficie;
            public override Color ImageMarginGradientEnd => Superficie;
            public override Color MenuBorder => Borde;
            public override Color SeparatorDark => Borde;
        }
    }
}
