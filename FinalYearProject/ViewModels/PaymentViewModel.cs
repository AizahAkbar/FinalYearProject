using FinalYearProject.Models;

namespace FinalYearProject.ViewModels
{
    public class PaymentViewModel
    {
        public BasketFrontEnd Basket { get; set; }
        public DeliveryInformation DeliveryInformation { get; set; }
        public decimal SubTotal { get; set; }
        public decimal DeliveryCost { get; set; }
        public decimal TotalAmount { get; set; }
        public int OrderId { get; set; }
    }
}