using Microsoft.AspNetCore.Mvc;

namespace FinalYearProject.Controllers
{
    public class BasketController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
