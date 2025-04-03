using Microsoft.AspNetCore.Mvc;
using Casillero_PROG_6.Data;
using Casillero_PROG_6.Services;

namespace Casillero_PROG_6.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargoServiceController : ControllerBase
    {
        private readonly CargoService _cargoService;

        public CargoServiceController(CargoService cargoService)
        {
            _cargoService = cargoService;
        }

        [HttpGet("CalcularFlete")]
        public ActionResult<decimal> CalcularFlete(int servicioId, decimal peso)
        {
            return _cargoService.CalcularFlete(servicioId, peso);
        }

        [HttpGet("CalcularImpuesto")]
        public ActionResult<decimal> CalcularImpuesto(int categoriaId, decimal valor)
        {
            return _cargoService.CalcularImpuesto(categoriaId, valor);
        }

        [HttpGet("CalcularTotal")]
        public ActionResult<decimal> CalcularTotal(decimal flete, decimal impuesto)
        {
            return _cargoService.CalcularTotal(flete, impuesto);
        }
    }
}