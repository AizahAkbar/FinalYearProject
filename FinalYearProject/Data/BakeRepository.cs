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
    }
}
