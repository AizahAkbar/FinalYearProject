using FinalYearProject.Models;
// look at srs message for rest
namespace FinalYearProject.Data
{
    public interface IBakeRepository
    {
        // Defines methods that BakeRepository class should implement - what other classes can access
        void AddBake(Bake bake);

        IEnumerable<Bake> GetAllBakes();

        Bake GetBakeById(int id);

        IEnumerable<Bake> GetBakesByCategory(string category);
    }
}
