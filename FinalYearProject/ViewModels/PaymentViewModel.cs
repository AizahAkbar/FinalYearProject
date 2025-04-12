using FinalYearProject.Models;

namespace FinalYearProject.ViewModels
{
    public class PaymentViewModel
    {
        public PaymentViewModel()
        {
            Payment = new Payment();
            Basket = new BasketFrontEnd
            {
                Bakes = new List<BakeFrontEnd>()
            };
        }

        public Payment Payment { get; set; }
        public BasketFrontEnd Basket { get; set; }
        public DeliveryInformation DeliveryInformation { get; set; }
        public decimal TotalAmount { get; set; }
    }
}