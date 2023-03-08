using IDYGS81_NoeliRodriguez.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace IDYGS81_NoeliRodriguez.Controllers
{
    public class PublicController : Controller
    {
        private readonly ApplicationDbContext _context;
        public PublicController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var response = _context.Usuarios.Include(x => x.Roles).ToList();
            return View(response);
   
        }
        public IActionResult Servicio()
        {
            var response = _context.Servicios.ToList();
            return View(response);
        }

        public IActionResult Roles()
        {
            var response = _context.Roles.ToList();
            return View(response);
        }
    }
}