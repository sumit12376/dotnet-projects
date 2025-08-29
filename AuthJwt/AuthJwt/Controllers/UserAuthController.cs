using AuthJwt.data;
using AuthJwt.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthJwt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signinManager;
        private readonly string? _jwtkey;
        private readonly string? _Issuer;
        private readonly string? _Audience;
        private readonly int _JwtExpiry;

        public UserAuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signinManager = signInManager;
            _jwtkey = configuration["Jwt:Key"];
            _Issuer = configuration["Jwt:Issuer"];
            _Audience = configuration["Jwt:Audience"];
            _JwtExpiry = int.Parse(configuration["Jwt:ExpiryMinuts"]);
        }

        // POST: baseurl/api/userAuth/register
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel register)
        {
            if (register == null
                || string.IsNullOrEmpty(register.Email)
                || string.IsNullOrEmpty(register.Name)
                || string.IsNullOrEmpty(register.Password))
            {
                return BadRequest("Invalid form");
            }

            var existingUser = await _userManager.FindByEmailAsync(register.Email);
            if (existingUser != null)
            {
                return Conflict("Email already existss");
            }

            var user = new ApplicationUser
            {
                UserName = register.Email,
                Email = register.Email,
                Name = register.Name,
            };

            var result = await _userManager.CreateAsync(user, register.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok("User created successfully");
        }

        [HttpPost("login")]

        public async Task<IActionResult> login([FromBody] LoginModel loginModel)
        {
            var user = await _userManager.FindByEmailAsync(loginModel.Email);
            if(user == null)
            {
                return Unauthorized(new { success = false, message = "invalid username or password" });
                
            }
           var result =  await _signinManager.CheckPasswordSignInAsync(user, loginModel.Password, false);
            if (!result.Succeeded)
            {
                return Unauthorized(new { success = false, message = "invalid username or password" });
            }
            var token = GenerateJWTToken(user);
            return Ok(new { success = true, token });
        }
        private string GenerateJWTToken(ApplicationUser user)
        {
            // 1. Define claims
            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, user.Id),
        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim("Name", user.Name)
    };

            //Create the symmetric security key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtkey));

            //Create signing credentials
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //Create the token
            var token = new JwtSecurityToken(
                issuer: _Issuer,      // usually your app or domain
                audience: _Audience,  // usually your app or domain
                claims: claims,
                expires: DateTime.Now.AddMinutes(_JwtExpiry), // token expiration
                signingCredentials: credentials
            );

            //Return the token as a string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }



    }
}
