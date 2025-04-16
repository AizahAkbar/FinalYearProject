using FinalYearProject.Models;
using System.ComponentModel.DataAnnotations;

namespace FinalYearProject.ViewModels
{
    public class DeliveryInformationViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }
        public string StreetAddress { get; set; }
        public string DeliveryMethod { get; set; }
        public DateTime? PreferredDeliveryDate { get; set; }

        public BasketFrontEnd? Basket { get; set; }
    }
}