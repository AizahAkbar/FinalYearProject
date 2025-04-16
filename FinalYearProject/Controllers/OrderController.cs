﻿using Microsoft.AspNetCore.Mvc;

namespace FinalYearProject.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
            var deliveryOption = Request.Form["deliveryMethod"].ToString();
            model.DeliveryMethod = deliveryOption;
                model.PreferredDeliveryDate = DateTime.Parse(preferredDate);
            //TempData["DeliveryInformation"] = model;
            return RedirectToAction("PaymentView");
        public async Task<IActionResult> PaymentView()
            //var deliveryInfo = TempData.Peek("DeliveryInformation") as DeliveryInformation;
            //if (deliveryInfo == null)
            //{
            //    return RedirectToAction("DeliveryInformation");
            //}

            var viewModel = await _paymentService.PreparePaymentViewModel(userId.Value, new DeliveryInformation());
            return View("Payment", viewModel);
        public async Task<IActionResult> CreatePaymentIntent(string amount)
        {
            try
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
                return BadRequest(new { error = ex.Message });
            }
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
