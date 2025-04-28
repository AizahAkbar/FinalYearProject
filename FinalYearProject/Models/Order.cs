using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalYearProject.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Order Number")]
        public string OrderNumber { get; set; }

        [Required]
        [Display(Name = "Order Date")]
        [DataType(DataType.DateTime)]
        public DateTime OrderDate { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        [Display(Name = "Order Items")]
        public string OrderItems { get; set; }

        [Required]
        [Display(Name = "Total Amount")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public virtual DeliveryInformation DeliveryInformation { get; set; }
    }
}