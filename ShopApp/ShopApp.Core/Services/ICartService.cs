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
        /// <param name="userId">The ID of the user whose cart is being retrieved.</param>
        /// <returns>A collection of <see cref="CartItem"/> representing the user's cart contents.</returns>
        Task<IEnumerable<CartItem>> GetCartAsync(int userId);

        /// <summary>
        /// Adds an item to the user's cart.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="productId">The ID of the product to add.</param>
        /// <param name="quantity">The quantity of the product to add.</param>
        /// <returns>The created <see cref="CartItem"/>, or null if the operation failed.</returns>
        Task<CartItem?> AddToCartAsync(int userId, int productId, int quantity);

        /// <summary>
        /// Removes an item from the user's cart.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="cartItemId">The ID of the cart item to remove.</param>
        /// <returns>The removed <see cref="CartItem"/>, or null if it was not found or not removed.</returns>
        Task<CartItem?> RemoveFromCartAsync(int userId, int cartItemId);
    }
}
