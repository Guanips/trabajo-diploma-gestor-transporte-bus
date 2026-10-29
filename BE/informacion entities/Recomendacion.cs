namespace BE.informacion_entities
{
    public class Recomendacion
    {
        public SeveridadRecomendacion severidad { get; private set; }
        public string titulo { get; private set; }
        public string evidencia { get; private set; }
        public string accionSugerida { get; private set; }

        public Recomendacion(SeveridadRecomendacion severidad, string titulo, string evidencia, string accionSugerida)
        {
            this.severidad = severidad;
            this.titulo = titulo;
            this.evidencia = evidencia;
            this.accionSugerida = accionSugerida;
        }
    }
}
