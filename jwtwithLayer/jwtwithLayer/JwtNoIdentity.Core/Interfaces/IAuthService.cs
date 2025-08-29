using jwtwithLayer.JwtNoIdentity.Core.DTOs;
using Microsoft.AspNetCore.Identity.Data;
using LoginRequest = jwtwithLayer.JwtNoIdentity.Core.DTOs.LoginRequest;
using RegisterRequest = jwtwithLayer.JwtNoIdentity.Core.DTOs.RegisterRequest;

namespace jwtwithLayer.JwtNoIdentity.Core.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterAsync(RegisterRequest request);
        Task<AuthResponse> LoginAsync(LoginRequest request);
    }
}
