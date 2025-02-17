using Microsoft.AspNetCore.Mvc;

namespace FinalYearProject.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View("OrderSummary");
        }

        public IActionResult Form()
        {
            return View("OrderForm");
        }

        public IActionResult Payment()
        {
            return View("Payment");
        }
    }
}
