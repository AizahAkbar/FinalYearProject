using System.ComponentModel.DataAnnotations.Schema;

namespace FinalYearProject.Models
{
    public class Basket
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Bakes { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; } = default!;
    }
}
