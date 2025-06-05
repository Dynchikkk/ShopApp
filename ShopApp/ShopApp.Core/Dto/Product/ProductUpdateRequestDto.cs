namespace ShopApp.Core.Dto.Product
{
    /// <summary>
    /// Request DTO for updating an existing product.
    /// </summary>
    public class ProductUpdateRequestDto
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string? Description { get; set; }
    }
}
