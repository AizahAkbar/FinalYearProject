using FinalYearProject.ViewModels;

namespace FinalYearProject.Services
{
    public interface IBakeService
    {
        void AddBake(BakeFrontEnd bake);
        IEnumerable<BakeFrontEnd> GetAllBakes(string sortBy = null);
        BakeFrontEnd GetBakeById(int id);
        IEnumerable<BakeFrontEnd> GetBakesByCategory(string category, string sortBy = null);
        IEnumerable<BakeFrontEnd> SearchBakes(string query, string sortBy = null);
    }
}
