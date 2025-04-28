using FinalYearProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalYearProject.Data
{
    public interface IOrderRepository
    {
        Task AddDeliveryInformation(DeliveryInformation deliveryInformation);
        Task AddOrder(Order order);
        Task<DeliveryInformation> GetDeliveryInformation(string id);
        Task<Order> GetOrderByUserId(int userId);
    }
}