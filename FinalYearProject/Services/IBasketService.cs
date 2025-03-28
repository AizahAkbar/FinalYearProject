using FinalYearProject.ViewModels;

namespace FinalYearProject.Services
{
    public interface IBasketService
    {
        Task<BasketFrontEnd> AddToBasket(int userId, int bakeId);
        Task<BasketFrontEnd> GetBasketByUserId(int userId);
        Task<BasketFrontEnd> DeleteFromBasket(int userId, int bakeId);
    }
}
