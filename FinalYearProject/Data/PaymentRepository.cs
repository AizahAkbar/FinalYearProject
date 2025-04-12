using FinalYearProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalYearProject.Data
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly FypContext _context;

        public PaymentRepository(FypContext context)
        {
            _context = context;
        }

        public async Task<Payment> CreatePayment(Payment payment)
        {
            _context.Payment.Add(payment);
            await _context.SaveChangesAsync();
            return payment;
        }

        public async Task<Payment> GetPaymentById(int id)
        {
            return await _context.Payment
                .Include(p => p.Order)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Payment> GetPaymentByOrderId(int orderId)
        {
            return await _context.Payment
                .Include(p => p.Order)
                .FirstOrDefaultAsync(p => p.OrderId == orderId);
        }

        public async Task<Payment> UpdatePayment(Payment payment)
        {
            _context.Entry(payment).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return payment;
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByUserId(int userId)
        {
            return await _context.Payment
                .Include(p => p.Order)
                .Where(p => p.Order.UserId == userId)
                .ToListAsync();
        }
    }
}