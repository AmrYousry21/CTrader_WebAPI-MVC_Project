using Microsoft.AspNetCore.Mvc;

namespace CTraderMVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
