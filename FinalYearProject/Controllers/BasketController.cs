﻿using Microsoft.AspNetCore.Mvc;
using FinalYearProject.Services;
using FinalYearProject.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace FinalYearProject.Controllers
{
    [Authorize]
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<IActionResult> Index()
        {
            var userIdStr = HttpContext.Session.GetInt32("UserId");
            if (!userIdStr.HasValue)
            {
                return RedirectToAction("Login", "User");
            }

            var basket = await _basketService.GetBasketByUserId(userIdStr.Value);
            return View(basket);
        }

        [HttpPost]
        public async Task<IActionResult> AddToBasket(int bakeId)
        {
            var userIdStr = HttpContext.Session.GetInt32("UserId");
            if (!userIdStr.HasValue)
            {
                return Json(new { success = false, message = "User not logged in" });
            }

            var result = await _basketService.AddToBasket(userIdStr.Value, bakeId);
            if (result == null)
            {
                return Json(new { success = false, message = "Failed to add item to basket" });
            }

            return Json(new { success = true, message = "Item added to basket", basket = result });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteFromBasket()
        {
            int bakeId = 1;
            var userIdStr = HttpContext.Session.GetInt32("UserId");
            if (!userIdStr.HasValue)
            {
                return Json(new { success = false, message = "User not logged in" });
            }

            var result = await _basketService.DeleteFromBasket(userIdStr.Value, bakeId);
            if (result == null)
            {
                return Json(new { success = false, message = "Failed to delete item from basket" });
            }

            return Json(new { success = true, message = "Item deleted from basket", basket = result });
        }
    }
}
