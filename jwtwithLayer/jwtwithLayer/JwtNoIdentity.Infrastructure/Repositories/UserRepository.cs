using jwtwithLayer.JwtNoIdentity.Core.Interfaces;
using jwtwithLayer.JwtNoIdentity.Infrastructure.Data;
using jwtwithLayer.JwtNoIdentity.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace jwtwithLayer.JwtNoIdentity.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task<User?> GetByEmailAsync(string email)
        {
            return _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task<User> CreateAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            var result = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            return result;
        }
    }
}
