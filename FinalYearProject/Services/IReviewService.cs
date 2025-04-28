using FinalYearProject.ViewModels;

namespace FinalYearProject.Services
{
    public interface IReviewService
    {
        Task AddReviewAsync(ReviewViewModel model);
        Task<(double AverageRating, int ReviewCount)> GetAverageRatingAsync(int bakeId);
    }
}