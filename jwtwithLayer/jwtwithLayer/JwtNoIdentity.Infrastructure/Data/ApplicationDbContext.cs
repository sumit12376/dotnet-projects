using jwtwithLayer.JwtNoIdentity.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace jwtwithLayer.JwtNoIdentity.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) { }
        public DbSet<User>Users =>Set<User>();

    }
}
