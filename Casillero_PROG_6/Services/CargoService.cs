using Casillero_PROG_6.Data;
using Casillero_PROG_6.Models;

namespace Casillero_PROG_6.Services
{
    public class CargoService
    {
        private readonly ApplicationDbContext _context;

        public CargoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public decimal CalcularFlete(int servicioId, decimal peso)
        {
            var servicio = _context.Tarifas.Find(servicioId);
            if (servicio == null)
                return 0;

            return servicio.Costo * peso;
        }

        public decimal CalcularImpuesto(int categoriaId, decimal valor)
        {
            var categoria = _context.Categorias.Find(categoriaId);
            if (categoria == null)
                return 0;

            return valor * (categoria.porcentaje / 100);
        }

        public decimal CalcularTotal(decimal flete, decimal impuesto)
        {
            return flete + impuesto;
        }
    }
}