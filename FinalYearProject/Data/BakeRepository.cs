using FinalYearProject.Migrations;
using FinalYearProject.Models;
// look at srs message for rest
namespace FinalYearProject.Data
{
    public class BakeRepository : IBakeRepository
    {
        private readonly FypContext _context;

        public BakeRepository(FypContext fypContext)
        {
            _context = fypContext;
        }

        // Adding bake object to database 
        // Add to _context which reflects database then save changes 
        public async void AddBake(Bake bake)
        {
            _context.Add(bake);
            await _context.SaveChangesAsync();
        }

        // Gets all bakes from database 
        public IEnumerable<Bake> GetAllBakes()
        {
            return _context.Bake.ToList();
        }

        public Bake GetBakeById(int id)
        {
            return _context.Bake.Find(id);
        }

        //public string GetCategoryById(int id)
        //{
        //    return _context.BakeCategory.Find(id);
        //}
        //public Bake GetAllBakesByCategory(string category)
        //{
        //    return _context.Bake.;
        //}
    }
}
