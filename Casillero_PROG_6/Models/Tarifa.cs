using System.ComponentModel.DataAnnotations;

namespace Casillero_PROG_6.Models
{
    public class Tarifa
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50)]
        [Display(Name = "Nombre del servicio")]
        public string nombre { get; set; }

        [Required(ErrorMessage = "El costo es obligatorio")]
        [Range(5, 100, ErrorMessage = "El costo debe estar entre 5 y 100")]
        [Display(Name = "Costo por kilo")]
        public decimal Costo { get; set; }
    }
}
