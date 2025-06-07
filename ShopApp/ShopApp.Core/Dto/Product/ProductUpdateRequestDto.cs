namespace ShopApp.Core.Dto.Product
{
    /// <summary>
    /// Represents the data required to update an existing product.
    /// </summary>
    public class ProductUpdateRequestDto
    {
        /// <summary>
        /// Updated name of the product.
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Updated price of the product.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Updated stock quantity of the product.
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// Updated description of the product.
        /// </summary>
        public string? Description { get; set; }
    }
}
