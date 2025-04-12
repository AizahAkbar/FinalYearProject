using FinalYearProject.Models;

namespace FinalYearProject.ViewModels
{
    public class DeliveryInformationViewModel
    {
        public DeliveryInformationViewModel()
        {
            DeliveryInformation = new DeliveryInformation();
            Basket = new BasketFrontEnd
            {
                Bakes = new List<BakeFrontEnd>()
            };
        }

        public DeliveryInformation DeliveryInformation { get; set; }
        public BasketFrontEnd Basket { get; set; }
    }
}