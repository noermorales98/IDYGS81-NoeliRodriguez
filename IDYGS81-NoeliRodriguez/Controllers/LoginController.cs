using IDYGS81_NoeliRodriguez.Context;
using IDYGS81_NoeliRodriguez.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IDYGS81_NoeliRodriguez.Controllers
{
    public class LoginController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public LoginController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Verificar(Usuario request)
        {
            try
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand("VerificarUsuario", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@correo", request.Correo);
                command.Parameters.AddWithValue("@contrasena", GetHash(request.Password));

                var resultado = new SqlParameter("@resultado", SqlDbType.Int);
                resultado.Direction = ParameterDirection.Output;
                command.Parameters.Add(resultado);
                connection.Open();
                command.ExecuteNonQuery();
                var esValido = (int)resultado.Value == 1;
                connection.Close();
                if(esValido)
                {
                    using var command2 = new SqlCommand("TipoRol", connection);
                    command2.CommandType = CommandType.StoredProcedure;
                    command2.Parameters.AddWithValue("@correo", request.Correo);
                    command2.Parameters.AddWithValue("@contrasena", GetHash(request.Password));
                    var tipoRol = new SqlParameter("@resultado", SqlDbType.Int);
                    tipoRol.Direction = ParameterDirection.Output;
                    command2.Parameters.Add(tipoRol);
                    connection.Open();
                    command2.ExecuteNonQuery();
                    int userRole = Convert.ToInt32(command2.Parameters["@resultado"].Value);
                    connection.Close();
                    if (userRole == 1)
                    {
                        return RedirectToAction("Index", "User");
                    }
                    else
                    {
                        return RedirectToAction("Index","Public");
                    }
                }
                return RedirectToAction("Index");


            }
            catch (Exception)
            {

                throw;
            }


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
