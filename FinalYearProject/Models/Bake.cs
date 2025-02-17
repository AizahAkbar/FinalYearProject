using FinalYearProject.Data;

namespace FinalYearProject.Models
{
    public class Bake
    {
            private BakesContext context;

            public int Id { get; set; }

            public string Name { get; set; }

            public double Price { get; set; }

            public string Category { get; set; }

            public string Description { get; set; }
    }
}
