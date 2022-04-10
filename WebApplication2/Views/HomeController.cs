using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Views
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
