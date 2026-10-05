namespace BE
{
    /// <summary>
    /// Fila de la vista de edición de traducciones: una etiqueta con su texto en el idioma por
    /// defecto (referencia) y su texto en el idioma que se está traduciendo.
    /// </summary>
    public class EtiquetaTraduccion
    {
        public string Clave { get; set; }
        public string? Formulario { get; set; }
        public string TextoReferencia { get; set; }
        public string? TextoTraducido { get; set; }

        public bool EstaTraducida => !string.IsNullOrWhiteSpace(TextoTraducido);

        public EtiquetaTraduccion(string clave, string? formulario, string textoReferencia, string? textoTraducido)
        {
            Clave = clave;
            Formulario = formulario;
            TextoReferencia = textoReferencia;
            TextoTraducido = textoTraducido;
        }
    }
}
