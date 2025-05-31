namespace ShopApp.Core.Dto
{
    public class ProductDto
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string? Description { get; set; }
    }
}
