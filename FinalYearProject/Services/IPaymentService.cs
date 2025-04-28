using FinalYearProject.Models;
using FinalYearProject.ViewModels;
using Stripe.Checkout;

namespace FinalYearProject.Services
{
    public interface IPaymentService
    {
        Task<PaymentViewModel> PreparePaymentViewModel(int userId, DeliveryInformation deliveryInfo);
        public Session CreatePaymentIntent(long amount, int userId, string currency = "gbp");
    }
}