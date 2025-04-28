using FinalYearProject.Data;
using FinalYearProject.Models;
using FinalYearProject.ViewModels;
using Stripe;
using Stripe.Checkout;

namespace FinalYearProject.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IBasketService _basketService;
        private readonly IOrderService _orderService;

        public PaymentService(IBasketService basketService, IOrderService orderService)
        {
            _basketService = basketService;
            _orderService = orderService;
        }

        public async Task<PaymentViewModel> PreparePaymentViewModel(int userId, DeliveryInformation deliveryInfo)
        {
            var basket = await _basketService.GetBasketByUserId(userId);
            var subtotal = basket.Bakes.Sum(b => b.Price * b.Quantity);

            // Calculate delivery cost  
            var deliveryCost = GetDeliveryCost(deliveryInfo.DeliveryMethod);
            var totalAmount = (decimal)subtotal + deliveryCost;

            return new PaymentViewModel
            {
                Basket = basket,
                DeliveryInformation = deliveryInfo,
                SubTotal = (decimal)subtotal,
                DeliveryCost = deliveryCost,
                TotalAmount = totalAmount
            };
        }

        private decimal GetDeliveryCost(string deliveryMethod)
        {
            if (Enum.TryParse<DeliveryMethodEnum>(deliveryMethod, true, out var method))
            {
                return (int)method / 100m; // Convert from pence to pounds
            }
            return 3.99m; // Default to standard delivery if method not recognized
        }

        public Session CreatePaymentIntent(long amount, int userId, string currency = "gbp")
        {
            Stripe.StripeConfiguration.ApiKey = "";

            var basket = _basketService.GetBasketByUserId(userId).Result;

            var order = _orderService.GetOrderByUserId(userId).Result;

            var successUrl = "https://localhost:7044/Order/Confirmation";
            var cancelUrl = "https://localhost:7044/";

            var lineItems = basket.Bakes.Select(x => new SessionLineItemOptions
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
            }).ToList();

            // Add delivery cost as a separate line item
            lineItems.Add(new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    Currency = currency,
                    UnitAmount = (long)(GetDeliveryCost(order.DeliveryInformation.DeliveryMethod) * 100), // Convert to cents
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = $"Delivery ({order.DeliveryInformation.DeliveryMethod})",
                    },
                },
                Quantity = 1
            });

            var option1 = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = lineItems,
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
