using ShopApp.Core.Models.User;
using System.ComponentModel.DataAnnotations;

namespace ShopApp.Core.Models.Shop
{
    /// <summary>
    /// Represents an item in the user's shopping cart.
    /// </summary>
    public class CartItem
    {
        /// <summary>
        /// Primary key identifier for the cart item.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Foreign key to the authenticated user who owns the cart item.
        /// </summary>
        [Required]
        public int AuthUserId { get; set; }
        /// <summary>
        /// Navigation property to the authenticated user.
        /// </summary>
        public AuthUser? AuthUser { get; set; }

        /// <summary>
        /// Foreign key to the associated product.
        /// </summary>
        [Required]
        public int ProductId { get; set; }
        /// <summary>
        /// Navigation property to the product.
        /// </summary>
        public Product? Product { get; set; }

        /// <summary>
        /// Quantity of the product in the cart.
        /// </summary>
        [Required]
        public int Quantity { get; set; }
    }
}
