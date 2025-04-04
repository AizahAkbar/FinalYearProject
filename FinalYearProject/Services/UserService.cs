using FinalYearProject.Data;
using FinalYearProject.ViewModels;
using FinalYearProject.Models;

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
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password)){
                return null;
            }
            if (user == null)
            {
                return null;
            }
            var userFrontEnd = new UserFrontEnd
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = user.Role
            };

            return userFrontEnd;
        }

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt());
        }

        public async Task<RegisterFrontEnd> Register(RegisterFrontEnd registerFrontEnd)
        {
            // Check if user already exists
            var existingUser = await _userRepository.Login(registerFrontEnd.Email, registerFrontEnd.Password);
            if (existingUser != null)
            {
                return null; // User already exists
            }

            // Create new user with hashed password
            var user = new User
            {
                FirstName = registerFrontEnd.FirstName,
                LastName = registerFrontEnd.LastName,
                Email = registerFrontEnd.Email,
                Password = HashPassword(registerFrontEnd.Password),
                Role = registerFrontEnd.Role // Default role for new users
            };

            var createdUser = await _userRepository.Register(user);
            
            registerFrontEnd.Id = createdUser.Id;
            registerFrontEnd.Role = createdUser.Role;
            
            return registerFrontEnd;
        }
    }
}
