using FinalYearProject.Services;
using FinalYearProject.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FinalYearProject.Controllers
{
    public class SearchController : Controller
    {
        [HttpGet]
        public IActionResult LiveSearch(string query)
        {
            if (string.IsNullOrEmpty(query))
                return Json(new BakeFrontEnd[0]);

            var results = _service.SearchBakes(query);
            return Json(results);
        }


        private readonly IBakeService _service;
        public SearchController(IBakeService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index(string category = null, string query = null, string sortBy = null)
        {
            if (!string.IsNullOrEmpty(category))
            {
                return View(_service.GetBakesByCategory(category, sortBy));
            }
            if (!string.IsNullOrEmpty(query))
            {
                return View(_service.SearchBakes(query, sortBy));
            }
            return View(_service.GetAllBakes(sortBy));
        }
    }
}
