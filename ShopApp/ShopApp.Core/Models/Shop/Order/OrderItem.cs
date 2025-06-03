using System.ComponentModel.DataAnnotations;

namespace ShopApp.Core.Models.Shop.Order
{
    /// <summary>
    /// Represents an item included in a specific order.
    /// </summary>
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }

        public Order? Order { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public string ProductName { get; set; } = null!;

        [Required]
        public decimal PriceAtPurchase { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
