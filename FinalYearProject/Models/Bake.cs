using System.ComponentModel.DataAnnotations.Schema;

namespace FinalYearProject.Models
{
    public class Bake
    {
            public int Id { get; set; }

            public string Name { get; set; }

            public double Price { get; set; }

            public string Category { get; set; }

            public string Description { get; set; }
            public string AltText { get; set; }
    }
}
