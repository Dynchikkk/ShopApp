using System.ComponentModel.DataAnnotations;

namespace ShopApp.Core.Models.Shop.Order
{
    /// <summary>
    /// Represents a specific product included in an order.
    /// </summary>
    public class OrderItem
    {
        /// <summary>
        /// Primary key identifier of the order item.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Foreign key to the associated order.
        /// </summary>
        [Required]
        public int OrderId { get; set; }

        /// <summary>
        /// Navigation property to the parent order.
        /// </summary>
        public Order? Order { get; set; }

        /// <summary>
        /// Identifier of the product included in the order.
        /// </summary>
        [Required]
        public int ProductId { get; set; }

        /// <summary>
        /// Name of the product at the time of purchase.
        /// </summary>
        [Required]
        public string ProductName { get; set; } = null!;

        /// <summary>
        /// Price of the product at the time of purchase.
        /// </summary>
        [Required]
        public decimal PriceAtPurchase { get; set; }

        /// <summary>
        /// Quantity of the product included in the order.
        /// </summary>
        [Required]
        public int Quantity { get; set; }
    }
}
