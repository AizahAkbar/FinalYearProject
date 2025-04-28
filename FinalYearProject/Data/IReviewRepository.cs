using FinalYearProject.Models;

namespace FinalYearProject.Data
{
    public interface IReviewRepository
    {
        Task AddAsync(Review review);
        Task<IEnumerable<Review>> GetByBakeIdAsync(int bakeId);
    }
}