using BE;
using System.Text.RegularExpressions;

namespace servicios
{
    public static class FormFieldValidationService
    {
        private static readonly Regex UsernameRegex = new Regex(@"^[a-zA-Z0-9_-]{3,16}$", RegexOptions.Compiled);
        private static readonly Regex EmailRegex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", RegexOptions.Compiled);
        private static readonly Regex PhoneRegex = new Regex(@"^\+?[0-9]{7,15}$", RegexOptions.Compiled);

        private static readonly Regex OnlyLettersRegex = new Regex(@"^[a-zA-ZñÑáéíóúÁÉÍÓÚüÜ ]+$", RegexOptions.Compiled);
        private static readonly Regex AlphaNumericStrictRegex = new Regex(@"^[a-zA-Z0-9]+$", RegexOptions.Compiled);
        private static readonly Regex AlphaNumericWithSpacesRegex = new Regex(@"^[a-zA-Z0-9ñÑáéíóúÁÉÍÓÚüÜ ]+$", RegexOptions.Compiled);
        private static readonly Regex ProfileNameRegex = new Regex(@"^PERF-[^\s]+$", RegexOptions.Compiled);

        public static ValidationResult ValidateUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return new ValidationResult(false, "err_UsernameVacio", "El nombre de usuario no puede estar vacío.");

            bool isValid = UsernameRegex.IsMatch(username.Trim());
            if (isValid) return new ValidationResult(true);

            return new ValidationResult(false, "err_UsernameFormato", "El nombre de usuario debe tener entre 3 y 16 caracteres (letras, números, guion o guion bajo).");
        }

        public static ValidationResult ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return new ValidationResult(false, "err_EmailVacio", "El email no puede estar vacío.");

            bool isValid = EmailRegex.IsMatch(email.Trim());
            if (isValid) return new ValidationResult(true);

            return new ValidationResult(false, "err_EmailFormato", "El formato del email no es válido.");
        }

        public static ValidationResult ValidatePhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return new ValidationResult(false, "err_PhoneVacio", "El teléfono no puede estar vacío.");

            bool isValid = PhoneRegex.IsMatch(phone.Trim());
            if (isValid) return new ValidationResult(true);

            return new ValidationResult(false, "err_PhoneFormato", "El teléfono debe tener entre 7 y 15 dígitos (puede comenzar con +).");
        }

        public static ValidationResult ValidateOnlyLetters(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return new ValidationResult(false, "err_OnlyLettersVacio", "El campo no puede estar vacío.");

            bool isValid = OnlyLettersRegex.IsMatch(text.Trim());
            if (isValid) return new ValidationResult(true);

            return new ValidationResult(false, "err_OnlyLettersFormato", "El campo solo puede contener letras y espacios.");
        }

        public static ValidationResult ValidateAlphaNumericStrict(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return new ValidationResult(false, "err_AlphaNumStrictVacio", "El campo no puede estar vacío.");

            bool isValid = AlphaNumericStrictRegex.IsMatch(text.Trim());
            if (isValid) return new ValidationResult(true);

            return new ValidationResult(false, "err_AlphaNumStrictFormato", "El campo solo puede contener letras y números, sin espacios.");
        }

        public static ValidationResult ValidateAlphaNumericWithSpaces(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return new ValidationResult(false, "err_AlphaNumSpacesVacio", "El campo no puede estar vacío.");

            bool isValid = AlphaNumericWithSpacesRegex.IsMatch(text.Trim());
            if (isValid) return new ValidationResult(true);

            return new ValidationResult(false, "err_AlphaNumSpacesFormato", "El campo solo puede contener letras, números y espacios.");
        }

        public static ValidationResult ValidateProfileName(string profileName)
        {
            if (string.IsNullOrWhiteSpace(profileName)) return new ValidationResult(false, "err_PerfilVacio", "El nombre del perfil no puede estar vacío.");

            bool isValid = ProfileNameRegex.IsMatch(profileName.Trim());
            if (isValid) return new ValidationResult(true);
            return new ValidationResult(false, "err_PerfilFormato", "El nombre del perfil debe comenzar con 'PERF-' seguido de caracteres (sin espacios).");
        }
    }
}