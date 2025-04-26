using FinalYearProject.Data;
using FinalYearProject.Models;
using FinalYearProject.ViewModels;
using System.Text.Json;

namespace FinalYearProject.Services
{
    public class OrderService : IOrderService
    {
        public readonly IOrderRepository _orderRepository;
        public readonly IUserRepository _userRepository;
        public readonly IBasketRepository _basketRepository;

        public OrderService(IOrderRepository orderRepository, IUserRepository userRepository, IBasketRepository basketRepository)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _basketRepository = basketRepository;
        }

        public async Task AddOrder(DeliveryInformationViewModel deliveryInformationViewModel, int userId)
        {
            var user = await _userRepository.GetUserById(userId);

            var basket = await _basketRepository.GetBasketByUserId(userId);

            var bakes = JsonSerializer.Deserialize<List<BakeFrontEnd>>(basket.Bakes);

            var deliveryInformation = await _orderRepository.GetDeliveryInformation(user.FirstName);

            var order = new Order
            {
                UserId = userId,
                User = user,
                OrderDate = DateTime.Now,
                OrderItems = basket.Bakes,
                DeliveryInformation = deliveryInformation,
                OrderNumber = Guid.NewGuid().ToString(),
                Status = "Pending",
                TotalAmount = (decimal)bakes.Sum(b => b.Price * b.Quantity),
            };

            await _orderRepository.AddOrder(order);
        }

        public async Task AddDeliveryInformation(DeliveryInformationViewModel deliveryInformationViewModel)
        {
            var deliveryInformation = new DeliveryInformation
            {
                FirstName = deliveryInformationViewModel.FirstName,
                LastName = deliveryInformationViewModel.LastName,
                StreetAddress = deliveryInformationViewModel.StreetAddress,
                City = deliveryInformationViewModel.City,
                Country = deliveryInformationViewModel.Country,
                PostCode = deliveryInformationViewModel.PostCode,
                PhoneNumber = deliveryInformationViewModel.PhoneNumber,
                DeliveryMethod = deliveryInformationViewModel.DeliveryMethod,
                PreferredDeliveryDate = deliveryInformationViewModel.PreferredDeliveryDate
            };

            await _orderRepository.AddDeliveryInformation(deliveryInformation);
        }

        public async Task<DeliveryInformation> GetDeliveryInformation(string firstname)
        {
            var deliveryInformation = await _orderRepository.GetDeliveryInformation(firstname);
            if (deliveryInformation == null)
            {
                throw new Exception("Delivery information not found");
            }
            return deliveryInformation;
        }

        public async Task<Order> GetOrderByUserId(int userId)
        {
            var order = await _orderRepository.GetOrderByUserId(userId);
            if (order == null)
            {
                throw new Exception("Order not found");
            }
            return order;
        }
    }
}
