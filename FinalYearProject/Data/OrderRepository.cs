using FinalYearProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalYearProject.Data
{
    public class OrderRepository : IOrderRepository
    {
        private readonly FypContext _context;

        public OrderRepository(FypContext fypContext)
        {
            _context = fypContext;
        }
        public async void AddOrder(Order order)
        {
            _context.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task<Order> GetOrder()
        {
            return _context.Order.FirstOrDefault();
        }

        public async Task<Order> GetOrderByUserId(int userId)
        {
            return await _context.Order
                .LastOrDefaultAsync(x => x.UserId == userId);
        }
    }
}
