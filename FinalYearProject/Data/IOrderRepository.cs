using FinalYearProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalYearProject.Data
{
    public interface IOrderRepository
    {
        void AddOrder(Order order);
        Task<Order> GetOrder();
        Task<Order> GetOrderByUserId(int userId);
    }
}
