namespace learningAssociation.DTOs
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }



        // Embed category details
        public CategorySimpleDto Category { get; set; }
    }

    // Simple Category info (without Products to avoid cycle)
    public class CategorySimpleDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
    }
}
