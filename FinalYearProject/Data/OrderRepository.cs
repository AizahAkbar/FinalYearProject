using FinalYearProject.Models;

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
    }

}
