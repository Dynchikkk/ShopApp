namespace ShopApp.Core.Dto.Product
{
    /// <summary>
    /// Represents product information returned to the client.
    /// </summary>
    public class ProductResponseDto
    {
        /// <summary>
        /// Unique identifier of the product.
        /// </summary>
        public int Id { get; set; }

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
