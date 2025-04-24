using Microsoft.AspNetCore.Mvc;
using FinalYearProject.Models;
using FinalYearProject.Services;
using FinalYearProject.ViewModels;
using System.Threading.Tasks;
using System;
using AspNetCoreGeneratedDocument;
using Microsoft.VisualBasic;
using Stripe;

namespace FinalYearProject.Controllers
{
    public class OrderController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly IPaymentService _paymentService;
        private readonly IOrderService _orderService;

        public OrderController(IBasketService basketService, IPaymentService paymentService, IOrderService orderService)
        {
            _basketService = basketService;
            _paymentService = paymentService;
            _orderService = orderService;
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

            _orderService.AddOrder(model);

            return RedirectToAction("PaymentView");
        }

        public async Task<IActionResult> PaymentView()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
            {
                return RedirectToAction("Login", "User");
            }

            //var deliveryInfo = TempData.Peek("DeliveryInformation") as DeliveryInformation;
            //if (deliveryInfo == null)
            //{
            //    return RedirectToAction("DeliveryInformation");
            //}

            var viewModel = await _paymentService.PreparePaymentViewModel(userId.Value, new DeliveryInformation());
            return View("Payment", viewModel);
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

        public async Task<IActionResult> CreatePaymentIntent(string amount)
        {
            try
            {
                decimal decimalAmount = decimal.Parse(amount);
                long amountInCents = (long)(decimalAmount * 100);

                var userId = HttpContext.Session.GetInt32("UserId");
                if (!userId.HasValue)
                {
                    return RedirectToAction("Login", "User");
                }

                var paymentIntent = _paymentService.CreatePaymentIntent(amountInCents, userId.Value, "gbp");
                return Redirect(paymentIntent.Url);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        public async Task<IActionResult> Confirmation()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var order = await _orderService.GetOrderByUserId(userId.Value);
            var paymentModel = new PaymentViewModel
            {
                Basket = await _basketService.GetBasketByUserId(userId.Value),
                DeliveryInformation = order.DeliveryInformation
            };

            return View(paymentModel);
        }
    }
}