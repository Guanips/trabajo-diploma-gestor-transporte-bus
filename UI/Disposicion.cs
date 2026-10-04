using System.ComponentModel;

namespace UI
{
    /// <summary>
    /// Fila de un formulario apilado: etiqueta arriba y campo abajo. Con Etiqueta2/Campo2 la fila se parte en dos columnas.
    /// Peso &gt; 0 hace que el campo absorba el alto sobrante (por ejemplo, un TextBox multilinea).
    /// </summary>
    internal readonly record struct Fila(Control? Etiqueta, Control Campo, Control? Etiqueta2 = null, Control? Campo2 = null, float Peso = 0);

    /// <summary>
    /// Distribucion proporcional de los controles de un formulario. Solo cambia posicion y tamano de controles
    /// existentes: se recalcula cada vez que el formulario cambia de tamano, de modo que grillas y listas aprovechan el espacio.
    /// </summary>
    internal sealed class Disposicion
    {
        private readonly float escala;

        private Disposicion(Form form)
        {
            escala = form.DeviceDpi / 96f;
        }

        public static void Montar(Form form, Action<Disposicion, Rectangle> aplicar)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;

            var d = new Disposicion(form);

            void ejecutar(object? sender, EventArgs e)
            {
                if (form.WindowState == FormWindowState.Minimized) return;

                var area = new Rectangle(d.E(16), d.E(16), form.ClientSize.Width - d.E(32), form.ClientSize.Height - d.E(32));
                if (area.Width < d.E(300) || area.Height < d.E(200)) return;

                form.SuspendLayout();
                aplicar(d, area);
                form.ResumeLayout(true);
            }

            form.Load += ejecutar;
            form.Shown += ejecutar;
            form.Resize += ejecutar;
        }

        /// <summary>Escala un valor en pixeles a la densidad de pantalla actual.</summary>
        public int E(int px) => (int)Math.Round(px * escala);

        public int Espacio => E(12);

        /// <summary>Coloca el control en el rectangulo dado, anclado arriba-izquierda (la posicion la manda este metodo, no el Anchor).</summary>
        public void Poner(Control c, Rectangle r)
        {
            c.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            if (c is ListBox lb) lb.IntegralHeight = false;
            c.Bounds = new Rectangle(r.X, r.Y, Math.Max(r.Width, 1), Math.Max(r.Height, 1));
        }

        /// <summary>Area util dentro de un GroupBox, por debajo de su titulo.</summary>
        public Rectangle Interior(GroupBox gb)
        {
            int izq = E(14), arriba = E(26), abajo = E(14);
            return new Rectangle(izq, arriba, Math.Max(gb.ClientSize.Width - izq * 2, 1), Math.Max(gb.ClientSize.Height - arriba - abajo, 1));
        }

        public Rectangle[] Columnas(Rectangle area, params float[] pesos) => Repartir(area, pesos, horizontal: true);

        public Rectangle[] Filas(Rectangle area, params float[] pesos) => Repartir(area, pesos, horizontal: false);

        // Peso negativo = tamano fijo en pixeles (por ejemplo -300); positivo = proporcion del espacio restante.
        private Rectangle[] Repartir(Rectangle area, float[] pesos, bool horizontal)
        {
            int total = horizontal ? area.Width : area.Height;
            int gap = Espacio;
            float fijo = pesos.Where(p => p < 0).Sum(p => E((int)-p));
            float flex = pesos.Where(p => p > 0).Sum();
            float resto = Math.Max(total - fijo - gap * (pesos.Length - 1), 0);

            var rects = new Rectangle[pesos.Length];
            int pos = horizontal ? area.X : area.Y;
            for (int i = 0; i < pesos.Length; i++)
            {
                int tam = pesos[i] < 0 ? E((int)-pesos[i]) : (flex > 0 ? (int)(resto * pesos[i] / flex) : 0);
                if (i == pesos.Length - 1 && pesos[i] > 0)
                    tam = (horizontal ? area.Right : area.Bottom) - pos;
                rects[i] = horizontal
                    ? new Rectangle(pos, area.Y, Math.Max(tam, 1), area.Height)
                    : new Rectangle(area.X, pos, area.Width, Math.Max(tam, 1));
                pos += tam + gap;
            }
            return rects;
        }

        /// <summary>Separa una franja de alto fijo en la parte inferior del area y devuelve esa franja.</summary>
        public Rectangle CortarAbajo(ref Rectangle area, int alto)
        {
            int h = Math.Min(alto, area.Height);
            var franja = new Rectangle(area.X, area.Bottom - h, area.Width, h);
            area = new Rectangle(area.X, area.Y, area.Width, Math.Max(area.Height - h - E(10), 1));
            return franja;
        }

        public Rectangle CortarArriba(ref Rectangle area, int alto)
        {
            int h = Math.Min(alto, area.Height);
            var franja = new Rectangle(area.X, area.Y, area.Width, h);
            area = new Rectangle(area.X, area.Y + h + E(10), area.Width, Math.Max(area.Height - h - E(10), 1));
            return franja;
        }

        /// <summary>Coloca una etiqueta en el borde superior del area y descuenta su alto.</summary>
        public void Titulo(ref Rectangle area, Label etiqueta)
        {
            int h = etiqueta.PreferredSize.Height;
            etiqueta.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            etiqueta.Location = new Point(area.X, area.Y);
            area = new Rectangle(area.X, area.Y + h + E(6), area.Width, Math.Max(area.Height - h - E(6), 1));
        }

        public int AltoBoton => E(38);

        /// <summary>Reparte una fila de botones a lo ancho del rectangulo (con un ancho maximo por boton).</summary>
        public void Botones(Rectangle franja, params Button[] botones)
        {
            if (botones.Length == 0) return;
            int gap = E(8);
            int ancho = Math.Clamp((franja.Width - gap * (botones.Length - 1)) / botones.Length, E(100), E(230));
            int x = franja.X;
            foreach (var b in botones)
            {
                Poner(b, new Rectangle(x, franja.Y, ancho, franja.Height));
                x += ancho + gap;
            }
        }

        /// <summary>Coloca controles en una sola linea, centrados en vertical. Peso 0 = ancho natural, negativo = ancho fijo, positivo = proporcional.</summary>
        public void Linea(Rectangle area, params (Control c, float peso)[] items)
        {
            int gap = E(10);
            float fijo = 0, flex = 0;
            foreach (var (c, p) in items)
            {
                if (p == 0) fijo += c.PreferredSize.Width;
                else if (p < 0) fijo += E((int)-p);
                else flex += p;
            }
            float resto = Math.Max(area.Width - fijo - gap * (items.Length - 1), 0);

            int x = area.X;
            foreach (var (c, p) in items)
            {
                int w = p == 0 ? c.PreferredSize.Width : p < 0 ? E((int)-p) : (int)(resto * p / flex);
                int h = c is Label ? c.PreferredSize.Height : c.Height;
                Poner(c, new Rectangle(x, area.Y + (area.Height - h) / 2, w, h));
                x += w + gap;
            }
        }

        /// <summary>Apila filas "etiqueta + campo" de arriba hacia abajo. Devuelve la coordenada Y donde termina la ultima.</summary>
        public int Pila(Rectangle area, params Fila[] filas)
        {
            int gapEtiqueta = E(3), gapFila = E(10);

            int AltoCampo(Control c) => c switch
            {
                Button => E(36),
                TextBox { Multiline: true } => E(60),
                CheckBox => c.PreferredSize.Height + E(4),
                _ => c.Height
            };
            int AltoEtiqueta(Control? l) => l == null ? 0 : l.PreferredSize.Height + gapEtiqueta;

            int basico = 0;
            float pesoTotal = 0;
            foreach (var f in filas)
            {
                basico += AltoEtiqueta(f.Etiqueta) + AltoCampo(f.Campo);
                pesoTotal += f.Peso;
            }
            basico += gapFila * (filas.Length - 1);
            int sobrante = Math.Max(area.Height - basico, 0);

            int y = area.Y;
            foreach (var f in filas)
            {
                int extra = f.Peso > 0 && pesoTotal > 0 ? (int)(sobrante * f.Peso / pesoTotal) : 0;
                int altoEtq = AltoEtiqueta(f.Etiqueta);
                int altoCampo = AltoCampo(f.Campo) + extra;
                bool doble = f.Campo2 != null;
                int mitad = (area.Width - E(10)) / 2;
                int ancho1 = doble ? mitad : area.Width;

                if (f.Etiqueta != null) Poner(f.Etiqueta, new Rectangle(area.X, y, ancho1, f.Etiqueta.PreferredSize.Height));
                Poner(f.Campo, new Rectangle(area.X, y + altoEtq, ancho1, altoCampo));

                if (doble)
                {
                    int x2 = area.X + mitad + E(10);
                    if (f.Etiqueta2 != null) Poner(f.Etiqueta2, new Rectangle(x2, y, mitad, f.Etiqueta2.PreferredSize.Height));
                    Poner(f.Campo2!, new Rectangle(x2, y + altoEtq, mitad, altoCampo));
                }
                y += altoEtq + altoCampo + gapFila;
            }
            return y - gapFila;
        }

        /// <summary>Tarjeta tipica: etiqueta opcional arriba, botones abajo y el control principal (grilla, lista, arbol) ocupando el resto.</summary>
        public void Tarjeta(GroupBox gb, Rectangle r, Control principal, Label? etiqueta = null, params Button[] botones)
        {
            Poner(gb, r);
            var area = Interior(gb);
            if (etiqueta != null) Titulo(ref area, etiqueta);
            if (botones.Length > 0) Botones(CortarAbajo(ref area, AltoBoton), botones);
            Poner(principal, area);
        }

        /// <summary>Ajusta el alto de un GroupBox para que termine justo debajo del contenido apilado (y es relativo al GroupBox).</summary>
        public void AjustarAlto(GroupBox gb, int yFinContenido)
        {
            gb.Height = yFinContenido + E(16);
        }
    }
}
