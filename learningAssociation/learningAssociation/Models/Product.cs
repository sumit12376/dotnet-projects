using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DotnetOneToMany.Models
{
    public class Product
    {
        public int ProductId { get; set; } // Primary Key

        [Required]
        public string Name { get; set; }   // Product Name

        public decimal Price { get; set; } // Product Price

        // Foreign Key for Category
        public int CategoryId { get; set; }

        // Navigation Property (Each Product belongs to a Category)
        //[JsonIgnore]
        public Category? Category { get; set; }

    }
}
