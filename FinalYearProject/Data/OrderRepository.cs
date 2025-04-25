using FinalYearProject.Models;
using Microsoft.EntityFrameworkCore;
using ZstdSharp.Unsafe;

namespace FinalYearProject.Data
{
    public class OrderRepository : IOrderRepository
    {
        private readonly FypContext _context;

        public OrderRepository(FypContext fypContext)
        {
            _context = fypContext;
        }
        public async Task AddOrder(Order order)
        {
            _context.Order.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task AddDeliveryInformation(DeliveryInformation deliveryInformation)
        {
            _context.DeliveryInformation.Add(deliveryInformation);
            await _context.SaveChangesAsync();
        }

        public async Task<Order> GetOrder()
        {
            return _context.Order.FirstOrDefault();
        }

        public async Task<Order> GetOrderByUserId(int userId)
        {
            return await _context.Order
                .Include(x => x.User)
                .Include(x => x.DeliveryInformation)
                .OrderByDescending(x => x.OrderDate)
                .LastOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task<DeliveryInformation> GetDeliveryInformation(string firstname)
        {
            return await _context.DeliveryInformation.FirstOrDefaultAsync(x => x.FirstName == firstname);
        }
    }
}