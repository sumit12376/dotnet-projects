using System;
using System.Threading.Tasks;
using jwtwithLayer.JwtNoIdentity.Infrastructure.Entities;

namespace jwtwithLayer.JwtNoIdentity.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User> CreateAsync(User user);
        Task<User?> GetByIdAsync(Guid id);
    }
}
