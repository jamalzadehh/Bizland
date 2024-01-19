using Microsoft.AspNetCore.Mvc;

namespace Bizland.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
