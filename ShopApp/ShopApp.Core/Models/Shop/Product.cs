using System.ComponentModel.DataAnnotations;

namespace ShopApp.Core.Models.Shop
{
    /// <summary>
    /// Represents a product available in the shop.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Primary key identifier for the product.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Name of the product.
        /// </summary>
        [Required]
        public required string Name { get; set; }
        /// <summary>
        /// Price of the product.
        /// </summary>
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Price should be non-negative")]
        public decimal Price { get; set; }
        /// <summary>
        /// Quantity of the product in stock.
        /// </summary>
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Stock should be non-negative")]
        public int Stock { get; set; }

        /// <summary>
        /// Optional description of the product.
        /// </summary>
        public string? Description { get; set; }
    }
}
