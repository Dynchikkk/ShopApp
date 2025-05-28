using ShopApp.Core.Models.Shop;

namespace ShopApp.Core.Services
{
    /// <summary>
    /// Defines operations for managing the user's shopping cart.
    /// </summary>
    public interface ICartService
    {
        /// <summary>
        /// Retrieves all items in the user's cart.
        /// </summary>
        Task<IEnumerable<CartItem>> GetCartAsync(int userId);

        /// <summary>
        /// Adds an item to the user's cart.
        /// </summary>
        Task<CartItem?> AddToCartAsync(int userId, int productId, int quantity);

        /// <summary>
        /// Removes an item from the user's cart.
        /// </summary>
        Task<bool> RemoveFromCartAsync(int userId, int cartItemId);
    }
}
