using System;
using System.ComponentModel.DataAnnotations;

namespace FinalYearProject.Models
{
    public class BillingInformation
    {
        [Required]
        [Display(Name = "Card Holder Name")]
        public string CardHolderName { get; set; }

        [Required]
        [Display(Name = "Card Number")]
        [RegularExpression(@"^[0-9]{16}$", ErrorMessage = "Please enter a valid 16-digit card number")]
        public string CardNumber { get; set; }

        [Required]
        [Display(Name = "Expiry Date")]
        [RegularExpression(@"^(0[1-9]|1[0-2])\/([0-9]{2})$", ErrorMessage = "Please enter a valid expiry date (MM/YY)")]
        public string ExpiryDate { get; set; }

        [Required]
        [Display(Name = "CVV")]
        [RegularExpression(@"^[0-9]{3,4}$", ErrorMessage = "Please enter a valid CVV")]
        public string CVV { get; set; }

        [Required]
        [Display(Name = "Country")]
        public string BillingCountry { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string BillingAddress { get; set; }

        [Required]
        [Display(Name = "City")]
        public string BillingCity { get; set; }

        [Required]
        [Display(Name = "Post Code")]
        public string BillingPostCode { get; set; }
    }
}