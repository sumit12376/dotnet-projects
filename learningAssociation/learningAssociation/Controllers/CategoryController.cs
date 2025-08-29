using DotnetOneToMany.Data;
using DotnetOneToMany.Models;
using learningAssociation.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotnetOneToMany.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        //ET: api/category
        //Get all categories with products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            var categories = await _context.Categories
                                  .Include(c => c.Products)
                                  .ToListAsync();
            var categoryDtos = categories.Select(c=>new CategoryDto
            {
                CategoryId = c.CategoryId,
                Name = c.Name,
                Products = c.Products.Select(p => new ProductSimpleDto
                {
                    ProductId = p.ProductId,
                    Name = p.Name,
                    Price = p.Price
                }).ToList()
            }).ToList();
            return Ok(categoryDtos);
        }

        //GET: api/category/1
        //Get category by ID with products
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Category>> GetCategory(int id)
        //{
        //    var category = await _context.Categories.Include(c => c.Products)
        //                                            .FirstOrDefaultAsync(c => c.CategoryId == id);

        //    if (category == null) return NotFound();

        //    return category;
        //}


        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategory(int id)
        {
            var category = await _context.Categories
                                         .Include(c => c.Products)
                                         .FirstOrDefaultAsync(c => c.CategoryId == id);

            if (category == null) return NotFound();

            var categoryDto = new CategoryDto
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                Products = category.Products.Select(p => new ProductSimpleDto
                {
                    ProductId = p.ProductId,
                    Name = p.Name,
                    Price = p.Price
                }).ToList()
            };

            return Ok(categoryDto);
        }



        //POST: api/category
        //Send JSON: { "name": "Electronics" }
        [HttpPost]
        public async Task<ActionResult<Category>> CreateCategory(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCategory), new { id = category.CategoryId }, category);
        }

        // PUT: api/category/1
        //Send JSON: { "categoryId": 1, "name": "Updated Name" }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, Category category)
        {
            if (id != category.CategoryId) return BadRequest();

            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //DELETE: api/category/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return NotFound();

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
