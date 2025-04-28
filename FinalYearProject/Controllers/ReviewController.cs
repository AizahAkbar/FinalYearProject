using FinalYearProject.Services;
using FinalYearProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace FinalYearProject.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        public async Task<IActionResult> GetReviews(int bakeId)
        {
            var (averageRating, reviewCount) = await _reviewService.GetAverageRatingAsync(bakeId);
            return Json(new
            {
                averageRating = averageRating,
                reviewCount = reviewCount
            });
        }

        public IActionResult Create(int bakeId)
        {
            var userIdStr = HttpContext.Session.GetInt32("UserId");
            if (!userIdStr.HasValue)
            {
                return RedirectToAction("Login", "User");
            }

            var model = new ReviewViewModel { BakeId = bakeId };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ReviewViewModel model)
        {
            var userIdStr = HttpContext.Session.GetInt32("UserId");
            if (!userIdStr.HasValue)
            {
                return RedirectToAction("Login", "User");
            }

            model.UserId = userIdStr.Value.ToString();

            await _reviewService.AddReviewAsync(model);

            return RedirectToAction("Details", "Bakes", new { id = model.BakeId });
        }
    }
}