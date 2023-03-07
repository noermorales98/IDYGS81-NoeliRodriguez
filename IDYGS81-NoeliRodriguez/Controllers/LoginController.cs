using IDYGS81_NoeliRodriguez.Context;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace IDYGS81_NoeliRodriguez.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoginController(ApplicationDbContext context)
        {
            _context = context; 
        }


        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public JsonResult Login(string user, string pass)
        {
            var response = _context.Usuarios.Where(x=> x.Nombre == user && x.Password == pass).ToList();
            Console.WriteLine(response);

            if (response.Count > 0)
            {
                return Json(new {Succes =  true, admin =true});
            }
            return Json(new { Succes = true, admin = true });
        }
    }
}
