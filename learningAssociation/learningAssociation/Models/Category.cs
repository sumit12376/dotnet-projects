
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DotnetOneToMany.Models
{
    public class Category
    {
        public int CategoryId { get; set; } // Primary Key

        [Required]
        public string Name { get; set; }    // Category Name

        // One-to-Many relationship: One Category has many Products
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
