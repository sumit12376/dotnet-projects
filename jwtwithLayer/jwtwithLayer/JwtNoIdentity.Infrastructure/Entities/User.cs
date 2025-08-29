using System;

namespace jwtwithLayer.JwtNoIdentity.Infrastructure.Entities
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string FullName { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public string PasswordSalt { get; set; } = default!;
        public string? Role { get; set; } = "user";
        public string? Email { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
