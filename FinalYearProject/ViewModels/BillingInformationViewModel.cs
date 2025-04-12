using FinalYearProject.Models;

namespace FinalYearProject.ViewModels
{
    public class BillingInformationViewModel
    {
        public BillingInformationViewModel()
        {
            BillingInformation = new BillingInformation();
            DeliveryInformation = new DeliveryInformation();
            Basket = new BasketFrontEnd
            {
                Bakes = new List<BakeFrontEnd>()
            };
        }

        public BillingInformation BillingInformation { get; set; }
        public DeliveryInformation DeliveryInformation { get; set; }
        public BasketFrontEnd Basket { get; set; }
    }
}