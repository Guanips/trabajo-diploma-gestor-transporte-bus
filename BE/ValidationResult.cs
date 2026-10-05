namespace BE
{
    public class ValidationResult
    {
        public bool IsValid { get; set; }

        /// <summary>Clave de traducción del error (Ej: 'err_EmailFormato').</summary>
        public string ErrorMessage { get; set; }

        /// <summary>Texto del error en el idioma por defecto; se registra al autodetectar la etiqueta.</summary>
        public string MensajePorDefecto { get; set; }

        public ValidationResult(bool isValid, string errorMessage = "", string mensajePorDefecto = "")
        {
            IsValid = isValid;
            ErrorMessage = errorMessage;
            MensajePorDefecto = string.IsNullOrEmpty(mensajePorDefecto) ? errorMessage : mensajePorDefecto;
        }
    }
}
