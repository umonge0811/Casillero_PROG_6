using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Casillero_PROG_6.Models;
using Casillero_PROG_6.Data;

namespace Casillero_PROG_6.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.Version = "1.0";
            ViewBag.Developers = "Tu Nombre";
            return View();
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult Servicios()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View(new Mensaje());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(Mensaje mensaje)
        {
            if (ModelState.IsValid)
            {
                mensaje.Fecha = DateTime.Now;
                _context.Mensajes.Add(mensaje);
                await _context.SaveChangesAsync();

                ViewBag.Success = "Mensaje enviado correctamente.";
                return View(new Mensaje());
            }

            return View(mensaje);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}