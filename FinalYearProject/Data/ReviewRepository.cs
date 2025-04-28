using FinalYearProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalYearProject.Data
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly FypContext _context;

        public ReviewRepository(FypContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Review review)
        {
            await _context.Review.AddAsync(review);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Review>> GetByBakeIdAsync(int bakeId)
        {
            return await _context.Review
                .Where(r => r.BakeId == bakeId)
                .ToListAsync();
        }
    }
}