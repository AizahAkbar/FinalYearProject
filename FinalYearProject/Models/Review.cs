using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FinalYearProject.Data;

namespace FinalYearProject.Models
{
    public class Review
    {
        private ReviewsContext context;
        public int Id { get; set; }

        public int BakeId { get; set; }
        [ForeignKey("BakeId")]
        public virtual Bake Bake { get; set; }

        public int UserId { get; set; }

        public double Rating { get; set; }

        public string? Comment { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReviewDate { get; set; }
    }
}
