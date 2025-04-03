using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Casillero_PROG_6.Models;
using Casillero_PROG_6.Data;

namespace Casillero_PROG_6.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AccountController> _logger;

        public AccountController(ApplicationDbContext context, ILogger<AccountController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var usuario = _context.Usuarios.FirstOrDefault(u =>
                    u.NombreUsuario == model.UserName && u.Clave == model.Password);

                if (usuario != null)
                {
                    // Crear claims para la autenticación
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, usuario.NombreUsuario),
                        new Claim(ClaimTypes.Role, usuario.Tipo == 2 ? "Admin" : "User"),
                        new Claim("FullName", usuario.Nombre)
                    };

                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = model.RememberMe
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    // Guardar datos adicionales en sesión
                    HttpContext.Session.SetInt32("UserType", usuario.Tipo);
                    HttpContext.Session.SetString("UserName", usuario.NombreUsuario);
                    HttpContext.Session.SetString("FullName", usuario.Nombre);

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Usuario o contraseña incorrectos.");
            }

            return View(model);
        }

        public async Task<IActionResult> LogOff()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_context.Usuarios.Any(u => u.NombreUsuario == model.UserName || u.Email == model.Email))
                {
                    ModelState.AddModelError("", "El usuario o email ya existe.");
                    return View(model);
                }

                var usuario = new Usuario
                {
                    NombreUsuario = model.UserName,
                    Nombre = model.FullName,
                    Email = model.Email,
                    Clave = model.Password,
                    Telefono = model.Phone,
                    Tipo = 1 // Usuario normal
                };

                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();

                // Autenticar al usuario automáticamente
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usuario.NombreUsuario),
                    new Claim(ClaimTypes.Role, "User"),
                    new Claim("FullName", usuario.Nombre)
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));

                HttpContext.Session.SetInt32("UserType", 1);
                HttpContext.Session.SetString("UserName", usuario.NombreUsuario);
                HttpContext.Session.SetString("FullName", usuario.Nombre);

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}