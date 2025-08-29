namespace jwtwithLayer.JwtNoIdentity.Core.DTOs
{
    public class AuthResponse
    {
        public string Token { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public string? Role { get; set; }
    }
}
