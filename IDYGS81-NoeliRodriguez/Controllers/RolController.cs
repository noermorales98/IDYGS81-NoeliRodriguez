using IDYGS81_NoeliRodriguez.Context;
using IDYGS81_NoeliRodriguez.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IDYGS81_NoeliRodriguez.Controllers
{
    public class RolController : Controller
    {
        private readonly ApplicationDbContext _context;
        public RolController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var res = _context.Roles.ToList();
            return View(res);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Rol request)
        {
            try
            {
                Rol rol = new Rol();
                rol.Nombre = request.Nombre;
                _context.   Roles.Add(rol);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                throw new Exception("Surgio un error " + ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            try
            {
                return View(_context.Roles.Find(id));
            }
            catch (Exception ex)
            {

                throw new Exception("Surgio un error" + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Rol request)
        {
            try
            {
                var rol = await _context.Roles.FindAsync(request.PkRol);
                if (rol == null) return NotFound();
                rol.Nombre = request.Nombre;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                throw new Exception("Surgio un error " + ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Eliminar(int id)
        {
            try
            {
                return View(_context.Roles.Find(id));
            }
            catch (Exception ex)
            {

                throw new Exception("Surgio un error" + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(Rol request)
        {
            var rol = await _context.Roles.FindAsync(request.PkRol);
            if (rol == null)
            {
                return NotFound();
            }

            _context.Roles.Remove(rol);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
