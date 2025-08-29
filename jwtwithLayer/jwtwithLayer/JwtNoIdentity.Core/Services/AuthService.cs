using jwtwithLayer.JwtNoIdentity.Core.DTOs;
using jwtwithLayer.JwtNoIdentity.Core.Interfaces;
using jwtwithLayer.JwtNoIdentity.Infrastructure.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using jwtwithLayer.JwtNoIdentity.Infrastructure.Security;
namespace JwtNoIdentity.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _users;
        private readonly IConfiguration _config;

        public AuthService(IUserRepository users, IConfiguration config)
        {
            _users = users;
            _config = config;
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest req)
        {
            var existing = await _users.GetByEmailAsync(req.Email);
            if (existing != null)
                throw new ApplicationException("Email already registered");

            var (hash, salt) = PasswordHasher.Hash(req.Password);

            var user = new User
            {
                FullName = req.FullName,
                Email = req.Email,
                PasswordHash = hash,
                PasswordSalt = salt,
                Role = string.IsNullOrWhiteSpace(req.Role) ? "User" : req.Role
            };

            user = await _users.CreateAsync(user);

            return BuildTokenResponse(user);
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest req)
        {
            var user = await _users.GetByEmailAsync(req.Email);
            if (user == null || !PasswordHasher.varify(req.Password, user.PasswordHash, user.PasswordSalt))
                throw new ApplicationException("Invalid credentials");

            return BuildTokenResponse(user);
        }

        private AuthResponse BuildTokenResponse(User user)
        {
            var token = GenerateJwtToken(user);
            return new AuthResponse
            {
                Token = token,
                Email = user.Email,
                FullName = user.FullName,
                Role = user.Role
            };
        }

        private string GenerateJwtToken(User user)
        {
            var key = _config["Jwt:Key"] ?? throw new ApplicationException("Jwt:Key missing");
            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("fullName", user.FullName),
                new Claim(ClaimTypes.Role, user.Role ?? "User")
            };

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var creds = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(3),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
