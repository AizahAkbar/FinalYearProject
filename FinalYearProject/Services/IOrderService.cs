using FinalYearProject.Models;
using FinalYearProject.ViewModels;

namespace FinalYearProject.Services
{
    public interface IOrderService
    {
        void AddOrder(DeliveryInformationViewModel deliveryInformationViewModel);
        Task<Order> GetOrder();
        Task<Order> GetOrderByUserId(int userId);
    }
}
