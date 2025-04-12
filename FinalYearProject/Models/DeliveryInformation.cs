using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalYearProject.Models
{
    public class DeliveryInformation
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string Country { get; set; }

        [Required]
        [Display(Name = "Post Code")]
        [StringLength(10)]
        public string PostCode { get; set; }

        [Required]
        [Display(Name = "City")]
        [StringLength(20)]
        public string City { get; set; }

        [Required]
        [Display(Name = "Street Address")]
        [StringLength(200)]
        public string StreetAddress { get; set; }

        [Required]
        [Display(Name = "Delivery Method")]
        public string DeliveryMethod { get; set; }

        [Display(Name = "Preferred Delivery Date")]
        [DataType(DataType.Date)]
        public DateTime? PreferredDeliveryDate { get; set; }

        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
    }
}