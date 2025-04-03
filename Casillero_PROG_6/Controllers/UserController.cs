using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Casillero_PROG_6.Models;
using Casillero_PROG_6.Data;
using Casillero_PROG_6.Services;
using Microsoft.EntityFrameworkCore;

namespace Casillero_PROG_6.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly CargoService _cargoService;

        public UserController(ApplicationDbContext context, CargoService cargoService)
        {
            _context = context;
            _cargoService = cargoService;
        }

        public IActionResult Paquetes()
        {
            ViewBag.Servicios = new SelectList(_context.Tarifas, "Id", "nombre");
            ViewBag.Categorias = new SelectList(_context.Categorias, "Id", "nombre");

            string username = User.Identity?.Name;
            var paquetes = _context.Paquetes.Where(p => p.Usuario == username).ToList();

            ViewBag.PaquetesList = paquetes;
            return View(new Paquete());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Paquetes(Paquete paquete)
        {
            if (ModelState.IsValid)
            {
                string username = User.Identity?.Name;
                var usuario = _context.Usuarios.FirstOrDefault(u => u.NombreUsuario == username);

                if (usuario == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                paquete.Usuario = username;
                paquete.Correo = usuario.Email;
                paquete.Telefono = usuario.Telefono;
                paquete.fecha = DateTime.Now;
                paquete.Estado = "Registrado";

                var servicio = await _context.Tarifas.FindAsync(paquete.servicio);
                var categoria = await _context.Categorias.FindAsync(paquete.Categoria);

                if (servicio == null || categoria == null)
                {
                    ModelState.AddModelError("", "Servicio o categoría no encontrados.");

                    ViewBag.Servicios = new SelectList(_context.Tarifas, "Id", "nombre", paquete.servicio);
                    ViewBag.Categorias = new SelectList(_context.Categorias, "Id", "nombre", paquete.Categoria);

                    var paquetes = _context.Paquetes.Where(p => p.Usuario == username).ToList();
                    ViewBag.PaquetesList = paquetes;

                    return View(paquete);
                }

                // Calcular costos
                paquete.Tarifa = servicio.Costo;
                paquete.Flete = _cargoService.CalcularFlete(paquete.servicio, paquete.Peso);
                paquete.Impuestos = _cargoService.CalcularImpuesto(paquete.Categoria, paquete.Valor);
                paquete.Total = _cargoService.CalcularTotal(paquete.Flete, paquete.Impuestos);

                _context.Paquetes.Add(paquete);
                await _context.SaveChangesAsync();

                // Guardar ID en TempData para la página de resumen
                TempData["PaqueteId"] = paquete.id;

                return RedirectToAction(nameof(Resumen));
            }

            ViewBag.Servicios = new SelectList(_context.Tarifas, "Id", "nombre", paquete.servicio);
            ViewBag.Categorias = new SelectList(_context.Categorias, "Id", "nombre", paquete.Categoria);

            string username2 = User.Identity?.Name;
            var paquetes2 = _context.Paquetes.Where(p => p.Usuario == username2).ToList();

            ViewBag.PaquetesList = paquetes2;
            return View(paquete);
        }

        public async Task<IActionResult> Resumen()
        {
            if (TempData["PaqueteId"] == null)
            {
                return RedirectToAction(nameof(Paquetes));
            }

            int paqueteId = (int)TempData["PaqueteId"];
            var paquete = await _context.Paquetes.FindAsync(paqueteId);

            if (paquete == null)
            {
                return RedirectToAction(nameof(Paquetes));
            }

            ViewBag.Servicio = (await _context.Tarifas.FindAsync(paquete.servicio))?.nombre;
            ViewBag.Categoria = (await _context.Categorias.FindAsync(paquete.Categoria))?.nombre;
            ViewBag.Usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.NombreUsuario == paquete.Usuario);

            return View(paquete);
        }
    }
}