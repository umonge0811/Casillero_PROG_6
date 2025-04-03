using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Casillero_PROG_6.Models;
using Casillero_PROG_6.Data;
using Casillero_PROG_6.Filters;
using Casillero_PROG_6.Services;

namespace Casillero_PROG_6.Controllers
{
    [AdminAuthorize]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly CargoService _cargoService;

        public AdminController(ApplicationDbContext context, CargoService cargoService)
        {
            _context = context;
            _cargoService = cargoService;
        }

        #region Tarifas/Servicios
        public async Task<IActionResult> Tarifas()
        {
            return View(await _context.Tarifas.ToListAsync());
        }

        public IActionResult CreateTarifa()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTarifa(Tarifa tarifa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tarifa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Tarifas));
            }
            return View(tarifa);
        }

        public async Task<IActionResult> EditTarifa(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarifa = await _context.Tarifas.FindAsync(id);
            if (tarifa == null)
            {
                return NotFound();
            }
            return View(tarifa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTarifa(int id, Tarifa tarifa)
        {
            if (id != tarifa.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tarifa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TarifaExists(tarifa.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Tarifas));
            }
            return View(tarifa);
        }

        public async Task<IActionResult> DeleteTarifa(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarifa = await _context.Tarifas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tarifa == null)
            {
                return NotFound();
            }

            return View(tarifa);
        }

        [HttpPost, ActionName("DeleteTarifa")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteTarifaConfirmed(int id)
        {
            var tarifa = await _context.Tarifas.FindAsync(id);
            if (tarifa != null)
            {
                _context.Tarifas.Remove(tarifa);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Tarifas));
        }

        private bool TarifaExists(int id)
        {
            return _context.Tarifas.Any(e => e.Id == id);
        }
        #endregion

        #region Categorias
        public async Task<IActionResult> Categorias()
        {
            return View(await _context.Categorias.ToListAsync());
        }

        public IActionResult CreateCategoria()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategoria(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Categorias));
            }
            return View(categoria);
        }

        public async Task<IActionResult> EditCategoria(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCategoria(int id, Categoria categoria)
        {
            if (id != categoria.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaExists(categoria.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Categorias));
            }
            return View(categoria);
        }

        public async Task<IActionResult> DeleteCategoria(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categorias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        [HttpPost, ActionName("DeleteCategoria")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCategoriaConfirmed(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria != null)
            {
                _context.Categorias.Remove(categoria);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Categorias));
        }

        private bool CategoriaExists(int id)
        {
            return _context.Categorias.Any(e => e.Id == id);
        }
        #endregion

        #region Usuarios
        public async Task<IActionResult> Usuarios()
        {
            return View(await _context.Usuarios.ToListAsync());
        }

        public IActionResult CreateUsuario()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUsuario(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Usuarios));
            }
            return View(usuario);
        }

        public async Task<IActionResult> EditUsuario(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUsuario(int id, Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Usuarios));
            }
            return View(usuario);
        }

        public async Task<IActionResult> DeleteUsuario(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        [HttpPost, ActionName("DeleteUsuario")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUsuarioConfirmed(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Usuarios));
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }
        #endregion

        #region Paquetes
        public async Task<IActionResult> Paquetes()
        {
            var paquetes = await _context.Paquetes
                .Include(p => p.TarifaNavigation)
                .Include(p => p.CategoriaNavigation)
                .ToListAsync();
            return View(paquetes);
        }

        public async Task<IActionResult> EditPaquete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paquete = await _context.Paquetes.FindAsync(id);
            if (paquete == null)
            {
                return NotFound();
            }

            ViewBag.Servicios = new SelectList(_context.Tarifas, "Id", "nombre", paquete.servicio);
            ViewBag.Categorias = new SelectList(_context.Categorias, "Id", "nombre", paquete.Categoria);
            ViewBag.Estados = new SelectList(new[]
            {
               "Registrado",
               "Recibido Bodega",
               "En Transito CR",
               "Recibido CR",
               "Para entrega",
               "Entregado"
           }, paquete.Estado);

            return View(paquete);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPaquete(int id, Paquete paquete)
        {
            if (id != paquete.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Recalcular costos si cambian los valores
                    if (paquete.servicio != 0 || paquete.Categoria != 0 || paquete.Peso != 0 || paquete.Valor != 0)
                    {
                        // Obtener tarifa y categoría
                        var servicio = await _context.Tarifas.FindAsync(paquete.servicio);
                        var categoria = await _context.Categorias.FindAsync(paquete.Categoria);

                        if (servicio != null && categoria != null)
                        {
                            // Calcular costos
                            paquete.Tarifa = servicio.Costo;
                            paquete.Flete = _cargoService.CalcularFlete(paquete.servicio, paquete.Peso);
                            paquete.Impuestos = _cargoService.CalcularImpuesto(paquete.Categoria, paquete.Valor);
                            paquete.Total = _cargoService.CalcularTotal(paquete.Flete, paquete.Impuestos);
                        }
                    }

                    _context.Update(paquete);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaqueteExists(paquete.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Paquetes));
            }

            ViewBag.Servicios = new SelectList(_context.Tarifas, "Id", "nombre", paquete.servicio);
            ViewBag.Categorias = new SelectList(_context.Categorias, "Id", "nombre", paquete.Categoria);
            ViewBag.Estados = new SelectList(new[]
            {
               "Registrado",
               "Recibido Bodega",
               "En Transito CR",
               "Recibido CR",
               "Para entrega",
               "Entregado"
           }, paquete.Estado);

            return View(paquete);
        }

        public async Task<IActionResult> DeletePaquete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paquete = await _context.Paquetes
                .Include(p => p.TarifaNavigation)
                .Include(p => p.CategoriaNavigation)
                .FirstOrDefaultAsync(m => m.id == id);
            if (paquete == null)
            {
                return NotFound();
            }

            return View(paquete);
        }

        [HttpPost, ActionName("DeletePaquete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePaqueteConfirmed(int id)
        {
            var paquete = await _context.Paquetes.FindAsync(id);
            if (paquete != null)
            {
                _context.Paquetes.Remove(paquete);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Paquetes));
        }

        private bool PaqueteExists(int id)
        {
            return _context.Paquetes.Any(e => e.id == id);
        }
        #endregion
    }
}