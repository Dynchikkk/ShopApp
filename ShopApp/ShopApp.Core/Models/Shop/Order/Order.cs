using ShopApp.Core.Models.User;
using System.ComponentModel.DataAnnotations;

namespace ShopApp.Core.Models.Shop.Order
{
    /// <summary>
    /// Represents a confirmed order placed by a user.
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Primary key identifier of the order.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Foreign key to the user who placed the order.
        /// </summary>
        [Required]
        public int AuthUserId { get; set; }

        /// <summary>
        /// Navigation property to the user who placed the order.
        /// </summary>
        public AuthUser? AuthUser { get; set; }

        /// <summary>
        /// List of items included in the order.
        /// </summary>
        public List<OrderItem> Items { get; set; } = [];

        /// <summary>
        /// Address where the order will be delivered.
        /// </summary>
        [Required]
        public string DeliveryAddress { get; set; } = null!;

        /// <summary>
        /// Desired delivery date for the order.
        /// </summary>
        [Required]
        public DateTime DeliveryDate { get; set; }

        /// <summary>
        /// Date and time when the order was created.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
