using FinalYearProject.Data;
using FinalYearProject.Models;
using FinalYearProject.ViewModels;
using Stripe;
using Stripe.Checkout;

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

        public Session CreatePaymentIntent(long amount, int userId, string currency = "gbp")
        {
            Stripe.StripeConfiguration.ApiKey = "";

            var basket = _basketService.GetBasketByUserId(userId).Result;

            var successUrl = "https://localhost:7044/";
            var cancelUrl = "https://localhost:7044/";

            var option1 = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                    "card"
                },
                LineItems = basket.Bakes.Select(x => new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = currency,
                        UnitAmount = (long)(x.Price * 100),
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = x.Name,
                        },
                    },
                    Quantity = x.Quantity
                }).ToList(),
                Mode = "payment",
                SuccessUrl = successUrl,
                CancelUrl = cancelUrl
            };

            var service = new SessionService();
            var paymentIntent = service.Create(option1);

            return paymentIntent;
        }

    }
}