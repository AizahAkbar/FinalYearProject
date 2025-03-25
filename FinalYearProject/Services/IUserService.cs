using FinalYearProject.ViewModels;

namespace FinalYearProject.Services
{
    public interface IUserService
    {
        Task<UserFrontEnd> Login(string email, string password);
        string HashPassword(string password);
        Task<RegisterFrontEnd> Register(RegisterFrontEnd registerFrontEnd);
    }
}
