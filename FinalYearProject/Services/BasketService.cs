using FinalYearProject.Models;
using FinalYearProject.ViewModels;
using System.Text.Json;

namespace FinalYearProject.Services
{
    public class BasketService
    {
        public BasketService() { }

        //public void UpdateBasket(Basket basket, Bake bake) {
        //    basket.Bakes.Append(bake);
        //    //call basket repo
        //}

        public void test1Update(BasketFrontEnd basket)
        {
            var basket1 = new Basket
            {
                Id = basket.Id,
                Bakes = JsonSerializer.Serialize(basket.Bakes),
                UserId = basket.UserId
            };
        }

        public void test2GET()
        {
            Basket basket = new Basket();

            var basket1 = new BasketFrontEnd
            {
                Id = basket.Id,
                Bakes = JsonSerializer.Deserialize<IEnumerable<BakeFrontEnd>>(basket.Bakes),
                UserId = basket.UserId
            };
        }
    }
}
