using IDYGS81_NoeliRodriguez.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace IDYGS81_NoeliRodriguez.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        public UserController(ApplicationDbContext context) {
            _context = context;
        }
        public IActionResult Index()
        {
            var response = _context.Usuarios.Include(x => x.Roles).ToList();
            return View(response);
        }
    }
}
