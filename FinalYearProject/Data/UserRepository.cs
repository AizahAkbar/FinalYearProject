using FinalYearProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalYearProject.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly FypContext _context;

        public UserRepository(FypContext fypContext)
        {
            _context = fypContext;
        }

        public async Task<User> Login(string email, string password)
        {
            return await _context.User
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.User
                .FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
