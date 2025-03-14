using FinalYearProject.ViewModels;

namespace FinalYearProject.Services
{
    public interface IBakeService
    {
        void AddBake(BakeFrontEnd bake);
        IEnumerable<BakeFrontEnd> GetAllBakes();
        BakeFrontEnd GetBakeById(int id);
    }
}
