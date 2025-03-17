using FinalYearProject.Data;
using FinalYearProject.ViewModels;

namespace FinalYearProject.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserFrontEnd> Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return null;
            }

            var user = await _userRepository.GetUserByEmail(email);
            if (user == null)
            {
                return null;
            }
            var userFrontEnd = new UserFrontEnd
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };

            bool validPassword = BCrypt.Net.BCrypt.Verify(password, user.Password);
            return validPassword ? userFrontEnd : null;
        }

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt());
        }
    }
}
