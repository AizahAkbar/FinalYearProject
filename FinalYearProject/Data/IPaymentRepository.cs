using FinalYearProject.Models;

namespace FinalYearProject.Data
{
    public interface IPaymentRepository
    {
        Task<Payment> CreatePayment(Payment payment);
        Task<Payment> GetPaymentById(int id);
        Task<Payment> GetPaymentByOrderId(int orderId);
        Task<Payment> UpdatePayment(Payment payment);
        Task<IEnumerable<Payment>> GetPaymentsByUserId(int userId);
    }
}