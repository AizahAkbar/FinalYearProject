using FinalYearProject.ViewModels;

namespace FinalYearProject.Services
{
    public static class BakesCache
    {
        private static IEnumerable<BakeFrontEnd> _bakes;

        public static void UpdateBakes(IEnumerable<BakeFrontEnd> bakes)
        {
            _bakes = bakes;
        }

        public static IEnumerable<BakeFrontEnd> GetBakes()
        {
            return _bakes ?? Enumerable.Empty<BakeFrontEnd>();
        }
    }
}

