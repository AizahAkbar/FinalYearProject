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
        public async Task<IActionResult> Index()
        {
            return View(_service.GetAllBakes());
        }
    }
}
