using FinalYearProject.Services;
using FinalYearProject.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FinalYearProject.Controllers
{
    public class SearchController : Controller
    {

        private readonly IBakeService _service;
        public SearchController(IBakeService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index(string category = null, string query = null)
        {
            if (!string.IsNullOrEmpty(category))
            {
                return View(_service.GetBakesByCategory(category));
            }
            if (!string.IsNullOrEmpty(query))
            {
                return View(_service.SearchBakes(query));
            }
            return View(_service.GetAllBakes());
        }
    }
}
