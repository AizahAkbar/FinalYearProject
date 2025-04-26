using FinalYearProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalYearProject.Data
{
    public class BasketRepository : IBasketRepository
    {
        private readonly FypContext _context;

        public BasketRepository(FypContext context)
        {
            _context = context;
        }

        public async Task<Basket> GetBasketByUserId(int userId)
        {
            return await _context.Basket
                .FirstOrDefaultAsync(b => b.UserId == userId);
        }

        public async Task<Basket> CreateBasket(Basket basket)
        {
            _context.Basket.Add(basket);
            await _context.SaveChangesAsync();
            return basket;
        }

        public async Task<Basket> UpdateBasket(Basket basket)
        {
            _context.Entry(basket).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return basket;
        }

        public async Task EmptyBasket(int userId)
        {
            var basket = await GetBasketByUserId(userId);
            if (basket != null)
            {
                basket.Bakes = "[]"; // or set to an empty list
                _context.Basket.Update(basket);
                await _context.SaveChangesAsync();
            }
        }
    }
}
