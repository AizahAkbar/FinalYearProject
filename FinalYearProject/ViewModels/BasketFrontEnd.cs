namespace FinalYearProject.ViewModels
{
    public class BasketFrontEnd
    {
        public int Id { get; set; }
        public IEnumerable<BakeFrontEnd> Bakes { get; set; }
        public int UserId { get; set; }
    }
}
