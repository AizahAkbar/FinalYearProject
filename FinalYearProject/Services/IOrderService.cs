using FinalYearProject.Models;
using FinalYearProject.ViewModels;

namespace FinalYearProject.Services
{
    public interface IOrderService
    {
        Task AddDeliveryInformation(DeliveryInformationViewModel deliveryInformationViewModel);
        Task AddOrder(DeliveryInformationViewModel deliveryInformationViewModel, int userId);
        Task<DeliveryInformation> GetDeliveryInformation(string firstname);
        Task<Order> GetOrderByUserId(int userId);
    }
}