using FinalYearProject.Data;
using FinalYearProject.Models;
using FinalYearProject.ViewModels;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace FinalYearProject.Services
{
    public class BakeService : IBakeService
    {
        // Dependency injection
        public readonly IBakeRepository _bakeRepository;

        public BakeService(IBakeRepository bakeRepository)
        {
            _bakeRepository = bakeRepository;
        }

        public void AddBake(BakeFrontEnd bakefrontend)
        {
            // Sevice layer

            var bake = new Bake
            {
                Name = bakefrontend.Name,
                Price = bakefrontend.Price,
                //Category = bakefrontend.Category,
                Description = bakefrontend.Description
            };

            _bakeRepository.AddBake(bake);
        }

        // Gets list of bakes from repository method
        // Maps to a list of type BakeFrontEnd
        // Returns
        public IEnumerable<BakeFrontEnd> GetAllBakes()
        {
            var bakes = _bakeRepository.GetAllBakes();
            var bakeFrontEnds = MapBakesToFrontEnd(bakes);
            BakesCache.UpdateBakes(bakeFrontEnds);
            return bakeFrontEnds;
        }


        public IEnumerable<BakeFrontEnd> GetBakesByCategory(string category)
        {
            var bakes = _bakeRepository.GetBakesByCategory(category);
            return MapBakesToFrontEnd(bakes);
        }

        public IEnumerable<BakeFrontEnd> SearchBakes(string query)
        {
            if (string.IsNullOrEmpty(query))
                return GetAllBakes();

            var allBakes = _bakeRepository.GetAllBakes();
            var searchResults = allBakes.Where(b =>
                b.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                b.Description.Contains(query, StringComparison.OrdinalIgnoreCase));

            return MapBakesToFrontEnd(searchResults);
        }

        private IEnumerable<BakeFrontEnd> MapBakesToFrontEnd(IEnumerable<Bake> bakes)
        {
            return bakes.Select(bake => new BakeFrontEnd
            {
                Id = bake.Id,
                Name = bake.Name,
                Price = bake.Price,
                Category = bake.Category,
                Description = bake.Description
            });
        }

        public BakeFrontEnd GetBakeById(int id)
        {
            var bake = _bakeRepository.GetBakeById(id);
            var bakeFrontEnd = new BakeFrontEnd
            {
                Id = bake.Id,
                Name = bake.Name,
                Price = bake.Price,
                Category = bake.Category,
                Description = bake.Description
            };
            return bakeFrontEnd;
        }

    }
}
