using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Casillero_PROG_6.Models
{
    public class Paquete
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "El servicio es obligatorio")]
        [Display(Name = "Servicio")]
        public int servicio { get; set; }

        [Required(ErrorMessage = "La categoría es obligatoria")]
        [Display(Name = "Categoría")]
        public int Categoria { get; set; }

        [Required(ErrorMessage = "El usuario es obligatorio")]
        [StringLength(50)]
        [Display(Name = "Usuario")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio")]
        [StringLength(50)]
        [EmailAddress(ErrorMessage = "El formato del email no es válido")]
        [Display(Name = "Correo")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        [StringLength(10)]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha")]
        public DateTime fecha { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria")]
        [StringLength(1000)]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El número de tracking es obligatorio")]
        [StringLength(50)]
        [Display(Name = "Tracking")]
        public string Tracking { get; set; }

        [Required(ErrorMessage = "El peso es obligatorio")]
        [Range(1, 100, ErrorMessage = "El peso debe estar entre 1 y 100 kg")]
        [Display(Name = "Peso (kg)")]
        public decimal Peso { get; set; }

        [Required(ErrorMessage = "El valor es obligatorio")]
        [Range(0, 10000, ErrorMessage = "El valor debe estar entre 0 y 10,000")]
        [Display(Name = "Valor ($)")]
        public decimal Valor { get; set; }

        [Required]
        [Display(Name = "Tarifa")]
        public decimal Tarifa { get; set; }

        [Required]
        [Display(Name = "Impuestos")]
        public decimal Impuestos { get; set; }

        [Required]
        [Display(Name = "Flete")]
        public decimal Flete { get; set; }

        [Required]
        [Display(Name = "Total")]
        public decimal Total { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Estado")]
        public string Estado { get; set; } = "Registrado";

        [ForeignKey("servicio")]
        public virtual Tarifa TarifaNavigation { get; set; }

        [ForeignKey("Categoria")]
        public virtual Categoria CategoriaNavigation { get; set; }
    }
}
