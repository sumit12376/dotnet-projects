using jwtwithLayer.JwtNoIdentity.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace jwtwithLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // JWT protected
    public class GetUserController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public GetUserController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/getuser
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _dbContext.Users
                .AsNoTracking()
                .Select(u => new
                {
                    u.Id,
                    u.FullName,
                    u.Email,
                    u.Role,
                    u.CreatedAt
                })
                .ToListAsync();

            return Ok(users);
        }
    }
}
