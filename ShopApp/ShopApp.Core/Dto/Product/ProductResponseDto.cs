namespace ShopApp.Core.Dto.Product
{
    /// <summary>
    /// Data transfer object for creating or updating product information.
    /// </summary>
    public class ProductResponseDto
    {
        /// <summary>
        /// The name of the product.
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// The price of the product.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// The available quantity in stock.
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// Optional description of the product.
        /// </summary>
        public string? Description { get; set; }
    }
}
