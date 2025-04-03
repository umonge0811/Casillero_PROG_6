using System.ComponentModel.DataAnnotations;

namespace Casillero_PROG_6.Models
{
    public class Mensaje
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El mensaje es obligatorio")]
        [StringLength(1000)]
        [Display(Name = "Mensaje")]
        public string mensaje { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress(ErrorMessage = "El formato del email no es válido")]
        [StringLength(50)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100)]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        [StringLength(10)]
        [RegularExpression(@"^\d{4}-\d{4}$", ErrorMessage = "El formato debe ser XXXX-XXXX")]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }
    }
}
