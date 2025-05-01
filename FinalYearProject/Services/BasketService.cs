using FinalYearProject.Models;
using FinalYearProject.ViewModels;
using FinalYearProject.Data;
using System.Text.Json;

namespace FinalYearProject.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IBakeRepository _bakeRepository;
        private readonly IUserRepository _userRepository;

        public async Task<BasketFrontEnd> GetBasketByUserId(int userId)
        {
            var basket = await _basketRepository.GetBasketByUserId(userId);
            if (basket == null)
            {
                return new BasketFrontEnd
                {
                    UserId = userId,
                    Bakes = new List<BakeFrontEnd>()
                };
            }

            return new BasketFrontEnd
            {
                Id = basket.Id,
                UserId = basket.UserId,
                Bakes = JsonSerializer.Deserialize<IEnumerable<BakeFrontEnd>>(basket.Bakes)
            };
        }

        public async Task<BasketFrontEnd> DeleteFromBasket(int userId, int bakeId)
        {
            var basket = await _basketRepository.GetBasketByUserId(userId);
            if (basket == null)
            {
                return null;
            }

            var bakes = JsonSerializer.Deserialize<List<BakeFrontEnd>>(basket.Bakes);
            var bakeToRemove = bakes.FirstOrDefault(b => b.Id == bakeId);

            if (bakeToRemove != null)
            {
                bakes.Remove(bakeToRemove);
                basket.Bakes = JsonSerializer.Serialize(bakes);
                await _basketRepository.UpdateBasket(basket);
            }

            return new BasketFrontEnd
            {
                Id = basket.Id,
                UserId = basket.UserId,
                Bakes = bakes
            };
        }

        public BasketService(IBasketRepository basketRepository, IBakeRepository bakeRepository, IUserRepository userRepository)
        {
            _basketRepository = basketRepository;
            _bakeRepository = bakeRepository;
            _userRepository = userRepository;
        }

        public async Task<BakeFrontEnd> AddToBasket(int userId, int bakeId, int quantity)
        {
            // Verify user exists
            var user = await _userRepository.GetUserById(userId);
            if (user == null)
            {
                return null;
            }

            // Get the bake to add
            var bake = _bakeRepository.GetBakeById(bakeId);
            if (bake == null)
            {
                return null;
            }

            // Get or create user's basket
            var basket = await _basketRepository.GetBasketByUserId(userId);
            if (basket == null)
            {
                basket = new Basket
                {
                    UserId = userId,
                    Bakes = JsonSerializer.Serialize(new List<BakeFrontEnd>
                    {
                        new BakeFrontEnd
                        {
                            Id = bake.Id,
                            Name = bake.Name,
                            Description = bake.Description,
                            Price = bake.Price,
                            Quantity = quantity,
                            AltText = bake.AltText
                        }
                    })
                };
                await _basketRepository.CreateBasket(basket);
            }
            else
            {
                var bakes = JsonSerializer.Deserialize<List<BakeFrontEnd>>(basket.Bakes) ?? new List<BakeFrontEnd>();
                var existingBake = bakes.FirstOrDefault(b => b.Id == bakeId);

                if (existingBake != null)
                {
                    existingBake.Quantity += quantity;
                }
                else
                {
                    bakes.Add(new BakeFrontEnd
                    {
                        Id = bake.Id,
                        Name = bake.Name,
                        Description = bake.Description,
                        Price = bake.Price,
                        Quantity = quantity,
                        AltText = bake.AltText
                    });
                }

                basket.Bakes = JsonSerializer.Serialize(bakes);
                await _basketRepository.UpdateBasket(basket);
            }

            return new BakeFrontEnd
            {
                Category = bake.Category,
                Description = bake.Description,
                Id = bake.Id,
                Name = bake.Name,
                Price = bake.Price,
                AltText = bake.AltText
            };
        }

        public async Task<BasketFrontEnd> UpdateToBasket(int userId, int bakeId, int quantity)
        {
            // Verify user exists
            var user = await _userRepository.GetUserById(userId);
            if (user == null)
            {
                return null;
            }

            // Get the bake to add
            var bake = _bakeRepository.GetBakeById(bakeId);
            if (bake == null)
            {
                return null;
            }

            // Get or create user's basket
            var basket = await _basketRepository.GetBasketByUserId(userId);

            var bakes = JsonSerializer.Deserialize<List<BakeFrontEnd>>(basket.Bakes) ?? new List<BakeFrontEnd>();
            var existingBake = bakes.FirstOrDefault(b => b.Id == bakeId);

            if (existingBake != null)
            {
                existingBake.Quantity = quantity;
            }

            basket.Bakes = JsonSerializer.Serialize(bakes);
            await _basketRepository.UpdateBasket(basket);

            return new BasketFrontEnd
            {
                Id = basket.Id,
                UserId = basket.UserId,
                Bakes = bakes
            };
        }

        public async Task EmptyBasket(int userId)
        {
            await _basketRepository.EmptyBasket(userId);
        }
    }
}
