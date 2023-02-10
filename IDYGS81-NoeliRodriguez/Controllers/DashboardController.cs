using Microsoft.AspNetCore.Mvc;

namespace IDYGS81_NoeliRodriguez.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
