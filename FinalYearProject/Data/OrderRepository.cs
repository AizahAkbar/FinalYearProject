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
            var existingInformation = await GetDeliveryInformation(deliveryInformation.FirstName);

            if (existingInformation != null)
            {
                // Copy new values to existing entity
                existingInformation.LastName = deliveryInformation.LastName;
                existingInformation.PhoneNumber = deliveryInformation.PhoneNumber;
                existingInformation.Country = deliveryInformation.Country;
                existingInformation.PostCode = deliveryInformation.PostCode;
                existingInformation.StreetAddress = deliveryInformation.StreetAddress;
                existingInformation.DeliveryMethod = deliveryInformation.DeliveryMethod;
                existingInformation.PreferredDeliveryDate = deliveryInformation.PreferredDeliveryDate;

                _context.DeliveryInformation.Update(existingInformation);
            }
            else
            {
                _context.DeliveryInformation.Add(deliveryInformation);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Order> GetOrderByUserId(int userId)
        {
            return await _context.Order
                .Include(x => x.User)
                .Include(x => x.DeliveryInformation)
                .OrderBy(x => x.OrderDate)
                .LastOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task<DeliveryInformation> GetDeliveryInformation(string firstname)
        {
            return await _context.DeliveryInformation.FirstOrDefaultAsync(x => x.FirstName == firstname);
        }
    }
}
