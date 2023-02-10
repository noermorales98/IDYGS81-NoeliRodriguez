using IDYGS81_NoeliRodriguez.Context;
using IDYGS81_NoeliRodriguez.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IDYGS81_NoeliRodriguez.Controllers
{
    public class ServicioController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ServicioController(ApplicationDbContext context) {
            _context= context;
        }
        public IActionResult Index()
        {
            var res = _context.Servicios.ToList();
            return View(res);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Servicio request)
        {
            try
            {
                Servicio servicio = new Servicio();
                servicio.Titulo = request.Titulo;
                servicio.Descripcion= request.Descripcion;

                _context.Servicios.Add(servicio);
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
                return View(_context.Servicios.Find(id));
            }
            catch (Exception ex)
            {

                throw new Exception("Surgio un error" + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Servicio request)
        {
            try
            {
                var servicio = await _context.Servicios.FindAsync(request.PkServicio);
                if (servicio == null) return NotFound();
                servicio.Titulo= request.Titulo;
                servicio.Descripcion = request.Descripcion;
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
                return View(_context.Servicios.Find(id));
            }
            catch (Exception ex)
            {

                throw new Exception("Surgio un error" + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(Servicio request)
        {
            var servicio = await _context.Servicios.FindAsync(request.PkServicio);
            if (servicio == null)
            {
                return NotFound();
            }

            _context.Servicios.Remove(servicio);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
