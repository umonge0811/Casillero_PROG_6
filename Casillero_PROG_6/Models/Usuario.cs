using System.ComponentModel.DataAnnotations;

namespace Casillero_PROG_6.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El usuario es obligatorio")]
        [StringLength(50)]
        [Display(Name = "Usuario")]
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100)]
        [Display(Name = "Nombre completo")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress(ErrorMessage = "El formato del email no es válido")]
        [StringLength(100)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        [Display(Name = "Contraseña")]
        public string Clave { get; set; }

        [StringLength(20)]
        [RegularExpression(@"^\d{4}-\d{4}$", ErrorMessage = "El formato debe ser XXXX-XXXX")]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        [Required]
        [Display(Name = "Tipo de usuario")]
        public int Tipo { get; set; } = 1; // 1: Usuario normal, 2: Administrador
    }
}
