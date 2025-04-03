using System.ComponentModel.DataAnnotations;

namespace Casillero_PROG_6.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50)]
        [Display(Name = "Nombre de categoría")]
        public string nombre { get; set; }

        [Required(ErrorMessage = "El porcentaje es obligatorio")]
        [Range(0, 100, ErrorMessage = "El porcentaje debe estar entre 0 y 100")]
        [Display(Name = "Porcentaje de impuesto")]
        public decimal porcentaje { get; set; }
    }
}
