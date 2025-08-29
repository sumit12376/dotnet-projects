using DotnetOneToMany.Data;
using DotnetOneToMany.Models;
using learningAssociation.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotnetOneToMany.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/product
        // Get all products with their category info
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var products = await _context.Products.Include(p => p.Category).ToListAsync();

            var productDtos = products.Select(p => new ProductDto
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Price = p.Price,
                CategoryId = p.CategoryId,
                Category = new CategorySimpleDto
                {
                    CategoryId = p.Category.CategoryId,
                    Name = p.Category.Name
                }
            }).ToList();

            return Ok(productDtos);
        }

        // GET: api/product/1
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await _context.Products.Include(p => p.Category)
                                                 .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null) return NotFound();

            var productDto = new ProductDto
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Price = product.Price,
                CategoryId = product.CategoryId,
                Category = new CategorySimpleDto
                {
                    CategoryId = product.Category.CategoryId,
                    Name = product.Category.Name
                }
            };

            return Ok(productDto);
        }

        // POST: api/product
        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // Map to DTO
            var productDto = new ProductDto
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Price = product.Price,
                CategoryId = product.CategoryId,
                Category = new CategorySimpleDto
                {
                    CategoryId = product.Category.CategoryId,
                    Name = product.Category.Name
                }
            };

            return CreatedAtAction(nameof(GetProduct), new { id = product.ProductId }, productDto);
        }

        //PUT: api/product/1
        //Send JSON:
        // { "productId": 1, "name": "Gaming Laptop", "price": 60000, "categoryId": 1 }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if (id != product.ProductId) return BadRequest();

            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //DELETE: api/product/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
