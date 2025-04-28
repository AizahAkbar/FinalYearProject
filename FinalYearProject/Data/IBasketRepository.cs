using FinalYearProject.Models;

namespace FinalYearProject.Data
{
    public interface IBasketRepository
    {
        Task<Basket> GetBasketByUserId(int userId);
        Task<Basket> CreateBasket(Basket basket);
        Task<Basket> UpdateBasket(Basket basket);
        Task EmptyBasket(int userId);
    }
}