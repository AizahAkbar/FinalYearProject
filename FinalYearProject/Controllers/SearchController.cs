using Microsoft.AspNetCore.Mvc;

namespace FinalYearProject.Controllers
{
    public class SearchController : Controller
    {
        public IActionResult Index()
        {
            return View("SearchPage");
        }
    }
}
