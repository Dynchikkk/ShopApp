namespace ShopApp.Core.Dto.Product
{
    /// <summary>
    /// Represents the data required to create a new product in the catalog.
    /// </summary>
    public class ProductCreateRequestDto
    {
        /// <summary>
        /// Name of the new product.
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Price of the new product.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Initial stock quantity of the product.
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// Optional description of the product.
        /// </summary>
        public string? Description { get; set; }
    }
}
