using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalYearProject.Data;
using FinalYearProject.Models;
using FinalYearProject.ViewModels;
using FinalYearProject.Services;
using Stripe;

namespace FinalYearProject.Controllers
{
    public class BakesController : Controller
    {
        private readonly IBakeService _bakeService;
        private readonly IReviewService _reviewService;

        public BakesController(IBakeService service, IReviewService reviewService)
        {
            _bakeService = service;
            _reviewService = reviewService;
        }

        // GET: Bakes
        public async Task<IActionResult> Index()
        {
            return View(_bakeService.GetAllBakes());
        }

        //// GET: Bakes/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var bake = _bakeService.GetBakeById(id);
            if (bake == null)
            {
                return NotFound();
            }

            var (averageRating, reviewCount) = await _reviewService.GetAverageRatingAsync(id);
            bake.AverageRating = averageRating;
            bake.ReviewCount = reviewCount;

            return View(bake);
        }
    }
}
