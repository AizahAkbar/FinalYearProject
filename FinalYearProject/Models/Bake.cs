using System.ComponentModel.DataAnnotations.Schema;

namespace FinalYearProject.Models
{
    public class Bake
    {
            public int Id { get; set; }

            public string Name { get; set; }

            public double Price { get; set; }

            public int CategoryId { get; set; }

            public string Description { get; set; }

            [ForeignKey("CategoryId")]
            public virtual BakeCategory BakeCategory { get; set; } = default!;
    }
}
