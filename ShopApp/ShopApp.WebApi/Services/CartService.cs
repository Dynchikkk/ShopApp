using Microsoft.EntityFrameworkCore;
using ShopApp.Core.Models.Shop;
using ShopApp.Core.Services;
using ShopApp.WebApi.Data;

namespace ShopApp.WebApi.Services
{
    /// <summary>
    /// Service for managing user's shopping cart operations.
    /// </summary>
    public class CartService : ICartService
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="CartService"/> class.
        /// </summary>
        /// <param name="context">The application's database context.</param>
        public CartService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all items in the user's cart.
        /// </summary>
        /// <param name="userId">The ID of the authenticated user.</param>
        /// <returns>A list of cart items including product information.</returns>
        public async Task<IEnumerable<CartItem>> GetCartAsync(int userId)
        {
            return await _context.CartItems
                .Include(ci => ci.Product)
                .Where(ci => ci.AuthUserId == userId)
                .ToListAsync();
        }

        /// <summary>
        /// Adds a new item to the user's cart.
        /// </summary>
        /// <param name="userId">The ID of the authenticated user.</param>
        /// <param name="productId">The ID of the product to add.</param>
        /// <param name="quantity">The quantity of the product to add.</param>
        /// <returns>The added cart item if successful; otherwise, null.</returns>
        public async Task<CartItem?> AddToCartAsync(int userId, int productId, int quantity)
        {
            CartItem cartItem = new()
            {
                AuthUserId = userId,
                ProductId = productId,
                Quantity = quantity
            };

            _ = _context.CartItems.Add(cartItem);
            _ = await _context.SaveChangesAsync();
            return cartItem;
        }

        /// <summary>
        /// Removes an item from the user's cart.
        /// </summary>
        /// <param name="userId">The ID of the authenticated user.</param>
        /// <param name="cartItemId">The ID of the cart item to remove.</param>
        /// <returns>Removed cart item if removal was successful; otherwise, null.</returns>
        public async Task<CartItem?> RemoveFromCartAsync(int userId, int cartItemId)
        {
            CartItem? item = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.Id == cartItemId && ci.AuthUserId == userId);

            if (item == null)
            {
                return null;
            }

            _ = _context.CartItems.Remove(item);
            _ = await _context.SaveChangesAsync();
            return item;
        }
    }
}
