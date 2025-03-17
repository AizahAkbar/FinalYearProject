using FinalYearProject.Models;

namespace FinalYearProject.Data
{
    public interface IUserRepository
    {
        Task<User> Login(string email, string password);
        Task<User> GetUserByEmail(string email);
    }
}
