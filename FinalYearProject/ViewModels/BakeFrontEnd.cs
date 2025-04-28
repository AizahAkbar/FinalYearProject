
namespace FinalYearProject.ViewModels
{
    public class BakeFrontEnd
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public double Price { get; set; }

        public double TotalPrice => Price * Quantity;

        public string Category { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; } = 1;
        public double AverageRating { get; set; }
        public int ReviewCount { get; set; }
    }
}
