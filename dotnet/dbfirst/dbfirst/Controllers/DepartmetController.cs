using dbfirst.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dbfirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmetController : ControllerBase
    {
        private readonly DbfirstDotnetContext _context;

        public DepartmetController(DbfirstDotnetContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.Departments.Include(d => d.Employees).ToList());
        }
    }
}
