using FinalYearProject.Data;
using FinalYearProject.Models;
using FinalYearProject.ViewModels;

namespace FinalYearProject.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task AddReviewAsync(ReviewViewModel model)
        {
            var review = new Review
            {
                BakeId = model.BakeId,
                Description = model.Description,
                Rating = model.Rating,
                User = model.UserId,
                CreatedDate = DateTime.UtcNow
            };

            await _reviewRepository.AddAsync(review);
        }

        public async Task<(double AverageRating, int ReviewCount)> GetAverageRatingAsync(int bakeId)
        {
            var reviews = await _reviewRepository.GetByBakeIdAsync(bakeId);
            if (!reviews.Any()) return (0, 0);

            var average = reviews.Average(r => r.Rating);
            return (average, reviews.Count());
        }
    }
}