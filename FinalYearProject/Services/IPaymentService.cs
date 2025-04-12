using FinalYearProject.Models;
using FinalYearProject.ViewModels;

namespace FinalYearProject.Services
{
    public interface IPaymentService
    {
        Task<Payment> ProcessPayment(Payment payment);
        Task<Payment> GetPaymentById(int id);
        Task<Payment> GetPaymentByOrderId(int orderId);
        Task<IEnumerable<Payment>> GetPaymentsByUserId(int userId);
        Task<PaymentViewModel> PreparePaymentViewModel(int userId, DeliveryInformation deliveryInfo);
    }
}