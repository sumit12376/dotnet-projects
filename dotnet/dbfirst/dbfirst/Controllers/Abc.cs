//using dbfirst.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace dbfirst.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]


//    public class employeeController : ControllerBase
//    {
//        private readonly DbfirstDotnetContext _context;

//        public employeeController(DbfirstDotnetContext context) {
//            _context = context;
//        }
//        [HttpPost]

//        public async Task<IActionResult> addcourse([FromBody] Course course)
//        {
//            if (course == null)
//                return BadRequest("Student data is required");
//            await _context.AddAsync(course);
//            await _context.SaveChangesAsync();
//            return Ok(course);
//        }
//    }
//}
