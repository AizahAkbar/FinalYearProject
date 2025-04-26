using FinalYearProject.ViewModels;

namespace FinalYearProject.Services
{
    public interface IBasketService
    {
        Task<BakeFrontEnd> AddToBasket(int userId, int bakeId, int quantity);
        Task<BasketFrontEnd> GetBasketByUserId(int userId);
        Task<BasketFrontEnd> DeleteFromBasket(int userId, int bakeId);
        Task<BasketFrontEnd> UpdateToBasket(int userId, int bakeId, int quantity);
        Task EmptyBasket(int userId);
    }
}