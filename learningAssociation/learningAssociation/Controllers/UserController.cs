using DotnetOneToMany.Data;
using learningAssociation.Models;
using learningAssociation.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
namespace learningAssociation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;
        public UserController(AppDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult createUser([FromBody] User user)
        {
            var validator = new UserValidator();
            var results = validator.Validate(user);
            if (!results.IsValid)
            {
                return BadRequest(results.Errors.Select(e => new
                {
                    Property = e.PropertyName,
                    Error = e.ErrorMessage
                })); 
            }
            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok(new { Message = "User is valid!" });
        }
    }
}
