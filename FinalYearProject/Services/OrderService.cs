using FinalYearProject.Data;
using FinalYearProject.Models;
using FinalYearProject.ViewModels;

namespace FinalYearProject.Services
{
    public class OrderService : IOrderService
    {
        public readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public void AddOrder(DeliveryInformationViewModel deliveryInformationViewModel)
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
            };

            var order = new Order
            {
                DeliveryInformation = deliveryInformation
            };

            _orderRepository.AddOrder(order);
        }
        public async Task<Order> GetOrder()
        {
            var order = await _orderRepository.GetOrder();
            if (order == null)
            {
                throw new Exception("Order not found");
            }
            return order;
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
