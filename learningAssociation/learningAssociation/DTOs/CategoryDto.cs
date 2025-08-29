namespace learningAssociation.DTOs
{
    public class CategoryDto
    {
        public int CategoryId { get; set; }   // Name should match model
        public string Name { get; set; }

        // Category ke products ko bhi dikhana hai
        public List<ProductSimpleDto> Products { get; set; }
    }

    // Lightweight Product DTO (without Category to avoid loop)
    public class ProductSimpleDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}


