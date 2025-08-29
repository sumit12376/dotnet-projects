using DAL;
using jwtwithLayer.JwtNoIdentity.Core.DTOs;
using jwtwithLayer.JwtNoIdentity.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace jwtwithLayer.JwtNoIdentity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;

        public AuthController(IAuthService authService) {
            _auth = authService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest register)
        {
            try
            {
                Class1 a = new Class1();
                var res = await _auth.RegisterAsync(register);
                return Ok(res);
            }

            catch (ApplicationException ex) {
                return BadRequest(new { message = ex.Message }
                );

            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest login)
        {
            try
            {
                var res = await _auth.LoginAsync(login);
                return Ok(res);
            }
            catch (ApplicationException ex) { return Unauthorized(new { message = ex.Message }); }
        }
    }
}
