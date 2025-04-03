using System.ComponentModel.DataAnnotations;

namespace Casillero_PROG_6.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "El usuario es obligatorio")]
        [Display(Name = "Usuario")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "El nombre completo es obligatorio")]
        [Display(Name = "Nombre Completo")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress(ErrorMessage = "El formato del email no es válido")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La confirmación del email es obligatoria")]
        [EmailAddress(ErrorMessage = "El formato del email no es válido")]
        [Compare("Email", ErrorMessage = "Los emails no coinciden")]
        [Display(Name = "Confirmar Email")]
        public string ConfirmEmail { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Required(ErrorMessage = "La confirmación de contraseña es obligatoria")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
        [Display(Name = "Confirmar Contraseña")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        [RegularExpression(@"^\d{4}-\d{4}$", ErrorMessage = "El formato debe ser XXXX-XXXX")]
        [Display(Name = "Teléfono")]
        public string Phone { get; set; }
    }
}
