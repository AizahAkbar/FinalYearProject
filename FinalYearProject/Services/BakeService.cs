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
                Description = bakefrontend.Description,
                AltText = bakefrontend.AltText
            };

            _bakeRepository.AddBake(bake);
        }

        // Gets list of bakes from repository method
        // Maps to a list of type BakeFrontEnd
        // Returns
        public IEnumerable<BakeFrontEnd> GetAllBakes(string sortBy = null)
        {
            var bakes = _bakeRepository.GetAllBakes();
            var bakeFrontEnds = MapBakesToFrontEnd(bakes);
            var sortedBakes = SortBakes(bakeFrontEnds, sortBy);
            BakesCache.UpdateBakes(sortedBakes);
            return sortedBakes;
        }

        private IEnumerable<BakeFrontEnd> SortBakes(IEnumerable<BakeFrontEnd> bakes, string sortBy)
        {
            if (string.IsNullOrEmpty(sortBy))
                return bakes;

            return sortBy.ToLower() switch
            {
                "price_asc" => bakes.OrderBy(b => b.Price),
                "price_desc" => bakes.OrderByDescending(b => b.Price),
                "name" => bakes.OrderBy(b => b.Name),
                _ => bakes
            };
        }


        public IEnumerable<BakeFrontEnd> GetBakesByCategory(string category, string sortBy = null)
        {
            var bakes = _bakeRepository.GetBakesByCategory(category);
            var mappedBakes = MapBakesToFrontEnd(bakes);
            return SortBakes(mappedBakes, sortBy);
        }

        public IEnumerable<BakeFrontEnd> SearchBakes(string query, string sortBy = null)
        {
            if (string.IsNullOrEmpty(query))
                return GetAllBakes(sortBy);

            var allBakes = _bakeRepository.GetAllBakes();
            var searchResults = allBakes.Where(b =>
                b.Name.Contains(query, StringComparison.OrdinalIgnoreCase));

            var mappedResults = MapBakesToFrontEnd(searchResults);
            return SortBakes(mappedResults, sortBy);
        }

        private IEnumerable<BakeFrontEnd> MapBakesToFrontEnd(IEnumerable<Bake> bakes)
        {
            return bakes.Select(bake => new BakeFrontEnd
            {
                Id = bake.Id,
                Name = bake.Name,
                Price = bake.Price,
                Category = bake.Category,
                Description = bake.Description,
                AltText = bake.AltText
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
                Description = bake.Description,
                AltText = bake.AltText
            };
            return bakeFrontEnd;
        }

    }
}
