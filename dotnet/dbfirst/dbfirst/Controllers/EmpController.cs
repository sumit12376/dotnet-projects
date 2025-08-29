using dbfirst.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dbfirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpController : ControllerBase
    {
        private readonly DbfirstDotnetContext _context;

        public EmpController(DbfirstDotnetContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.Employees.Include(e => e.Department).ToList());
        }

        [HttpPost]
        public IActionResult Create([FromBody] Employee emp)
        {
            _context.Employees.Add(emp);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetAll), emp);
        }
    }
}
