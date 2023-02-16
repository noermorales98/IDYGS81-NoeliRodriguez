using IDYGS81_NoeliRodriguez.Context;
using IDYGS81_NoeliRodriguez.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace IDYGS81_NoeliRodriguez.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var response = _context.Usuarios.Include(x => x.Roles).ToList();
            return View(response);
        }
        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Usuario request)
        {
            try
            {
                Usuario user = new Usuario();
                user.Nombre = request.Nombre;
                user.Apellido = request.Apellido;
                user.FkRol = request.FkRol;
                string hash = GetHash(request.Password);
                user.Correo = request.Correo;
                user.Password = hash;

                _context.Usuarios.Add(user);
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
                return View(_context.Usuarios.Find(id));
            }
            catch (Exception ex)
            {

                throw new Exception("Surgio un error" + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Usuario request)
        {
            try
            {
                var user = await _context.Usuarios.FindAsync(request.PkUsuario);
                if (user == null) return NotFound();
                user.Nombre = request.Nombre;
                user.FkRol = request.FkRol;
                user.Apellido = request.Apellido;
                user.Correo = request.Correo;
                string hash = GetHash(request.Password);
                user.Password = hash;
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
                return View(_context.Usuarios.Find(id));
            }
            catch (Exception ex)
            {

                throw new Exception("Surgio un error" + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(Usuario request)
        {
            var user = await _context.Usuarios.FindAsync(request.PkUsuario);
            if (user == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(user);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public static string GetHash(string input)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Convierte la entrada en bytes y genera el hash
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Convierte el hash en una cadena
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}