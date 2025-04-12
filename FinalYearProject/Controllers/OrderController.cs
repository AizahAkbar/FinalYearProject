using Microsoft.AspNetCore.Mvc;
using FinalYearProject.Models;
using FinalYearProject.Services;
using FinalYearProject.ViewModels;
using System.Threading.Tasks;
using System;

namespace FinalYearProject.Controllers
{
    public class OrderController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly IPaymentService _paymentService;

        public OrderController(IBasketService basketService, IPaymentService paymentService)
        {
            _basketService = basketService;
            _paymentService = paymentService;
        }

        public async Task<IActionResult> DeliveryInformation()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
            {
                return RedirectToAction("Login", "User");
            }

            var basket = await _basketService.GetBasketByUserId(userId.Value);
            if (basket == null)
            {
                basket = new BasketFrontEnd
                {
                    UserId = userId.Value,
                    Bakes = new List<BakeFrontEnd>()
                };
            }

            // Ensure Bakes is not null before checking if it's empty
            if (basket.Bakes == null)
            {
                basket.Bakes = new List<BakeFrontEnd>();
            }

            if (!basket.Bakes.Any())
            {
                return RedirectToAction("Index", "Basket");
            }

            var viewModel = new DeliveryInformationViewModel();
            viewModel.Basket = basket;

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ProcessDeliveryInformation(DeliveryInformationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("DeliveryInformation", model);
            }

            // Get delivery method and preferred date from form
            var deliveryOption = Request.Form["deliveryOption"].ToString();
            var preferredDate = Request.Form["preferredDate"].ToString();

            // Update delivery information
            model.DeliveryInformation.DeliveryMethod = deliveryOption;
            if (!string.IsNullOrEmpty(preferredDate))
            {
                model.DeliveryInformation.PreferredDeliveryDate = DateTime.Parse(preferredDate);
            }

            // Store delivery information in TempData
            TempData["DeliveryInformation"] = model.DeliveryInformation;
            
            return RedirectToAction("Payment");
        }

        public async Task<IActionResult> Payment()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
            {
                return RedirectToAction("Login", "User");
            }

            var deliveryInfo = TempData.Peek("DeliveryInformation") as DeliveryInformation;
            if (deliveryInfo == null)
            {
                return RedirectToAction("DeliveryInformation");
            }

            var viewModel = await _paymentService.PreparePaymentViewModel(userId.Value, deliveryInfo);
            return View(viewModel);
        }

        public async Task<IActionResult> BillingInformation()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
            {
                return RedirectToAction("Login", "User");
            }

            var deliveryInfo = TempData.Peek("DeliveryInformation") as DeliveryInformation;
            if (deliveryInfo == null)
            {
                return RedirectToAction("DeliveryInformation");
            }

            var basket = await _basketService.GetBasketByUserId(userId.Value);
            if (basket == null || basket.Bakes == null || !basket.Bakes.Any())
            {
                return RedirectToAction("Index", "Basket");
            }

            var viewModel = new BillingInformationViewModel
            {
                DeliveryInformation = deliveryInfo,
                Basket = basket,
                BillingInformation = new BillingInformation()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> BillingInformation(BillingInformationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Store billing information in TempData
            TempData["BillingInformation"] = model.BillingInformation;
            
            return RedirectToAction("Payment");
        }

        public async Task<IActionResult> ProcessPayment(PaymentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Payment", model);
            }

            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
            {
                return RedirectToAction("Login", "User");
            }

            try
            {
                var payment = await _paymentService.ProcessPayment(model.Payment);
                // Clear the basket after successful payment
                // Redirect to order confirmation page
                return RedirectToAction("Confirmation", new { orderId = payment.OrderId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while processing your payment. Please try again.");
                return View("Payment", model);
            }
        }
    }
}
