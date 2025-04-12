using FinalYearProject.Data;
using FinalYearProject.Models;
using FinalYearProject.ViewModels;

namespace FinalYearProject.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IBasketService _basketService;

        public PaymentService(IPaymentRepository paymentRepository, IBasketService basketService)
        {
            _paymentRepository = paymentRepository;
            _basketService = basketService;
        }

        public async Task<Payment> ProcessPayment(Payment payment)
        {
            payment.PaymentDate = DateTime.UtcNow;
            payment.PaymentStatus = "Processed";
            return await _paymentRepository.CreatePayment(payment);
        }

        public async Task<Payment> GetPaymentById(int id)
        {
            return await _paymentRepository.GetPaymentById(id);
        }

        public async Task<Payment> GetPaymentByOrderId(int orderId)
        {
            return await _paymentRepository.GetPaymentByOrderId(orderId);
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByUserId(int userId)
        {
            return await _paymentRepository.GetPaymentsByUserId(userId);
        }

        public async Task<PaymentViewModel> PreparePaymentViewModel(int userId, DeliveryInformation deliveryInfo)
        {
            var basket = await _basketService.GetBasketByUserId(userId);
            var totalAmount = basket.Bakes.Sum(b => b.Price * b.Quantity);

            return new PaymentViewModel
            {
                Basket = basket,
                DeliveryInformation = deliveryInfo,
                TotalAmount = (decimal)totalAmount,
                Payment = new Payment
                {
                    Amount = (decimal)totalAmount
                }
            };
        }
    }
}