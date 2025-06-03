using ShopApp.Core.Models.User;
using System.ComponentModel.DataAnnotations;

namespace ShopApp.Core.Models.Shop.Order
{
    /// <summary>
    /// Represents a user's confirmed order.
    /// </summary>
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int AuthUserId { get; set; }

        public AuthUser? AuthUser { get; set; }

        public List<OrderItem> Items { get; set; } = [];

        [Required]
        public string DeliveryAddress { get; set; } = null!;

        [Required]
        public DateTime DeliveryDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
