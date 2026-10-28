using System.Runtime.ExceptionServices;

namespace UI
{
    /// <summary>Ventana minima con barra indeterminada para tareas de arranque (se usa antes de que exista la base, por eso no se traduce).</summary>
    internal sealed class FormEspera : Form
    {
        private FormEspera(string mensaje)
        {
            Text = "Gestor Transporte";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterScreen;
            ControlBox = false;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(380, 90);

            Controls.Add(new Label
            {
                Text = mensaje,
                AutoSize = false,
                Location = new Point(15, 15),
                Size = new Size(350, 30),
                TextAlign = ContentAlignment.MiddleLeft
            });
            Controls.Add(new ProgressBar
            {
                Style = ProgressBarStyle.Marquee,
                Location = new Point(15, 50),
                Size = new Size(350, 20)
            });
        }

        /// <summary>Muestra la ventana mientras <paramref name="trabajo"/> corre en un hilo de fondo y devuelve su resultado.</summary>
        public static T Ejecutar<T>(string mensaje, Func<T> trabajo)
        {
            T resultado = default!;
            Exception? error = null;

            using FormEspera form = new(mensaje);
            form.Shown += async (_, _) =>
            {
                try { resultado = await Task.Run(trabajo); }
                catch (Exception ex) { error = ex; }
                form.Close();
            };
            form.ShowDialog();

            if (error != null) ExceptionDispatchInfo.Capture(error).Throw();
            return resultado;
        }
    }
}
