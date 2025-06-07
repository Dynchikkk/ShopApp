using Microsoft.EntityFrameworkCore;
using ShopApp.Core.Dto.Order;
using ShopApp.Core.Models.Shop;
using ShopApp.Core.Models.Shop.Order;
using ShopApp.Core.Services;
using ShopApp.WebApi.Data;

namespace ShopApp.WebApi.Services
{
    /// <summary>
    /// Service for managing order creation and retrieval.
    /// </summary>
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderService"/> class.
        /// </summary>
        /// <param name="context">Database context used to access order and cart data.</param>
        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all orders placed by the specified user.
        /// </summary>
        /// <param name="userId">ID of the user whose orders to retrieve.</param>
        /// <returns>A list of <see cref="Order"/> entities sorted by creation date descending.</returns>
        public async Task<IEnumerable<Order>> GetOrdersAsync(int userId)
        {
            return await _context.Orders
                .Include(o => o.Items)
                .Where(o => o.AuthUserId == userId)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();
        }

        /// <summary>
        /// Creates a new order based on the current contents of the user's cart.
        /// </summary>
        /// <param name="userId">The ID of the user placing the order.</param>
        /// <param name="dto">DTO containing delivery address and date.</param>
        /// <returns>The newly created <see cref="Order"/>, or null if the cart is empty or invalid.</returns>
        public async Task<Order?> CreateOrderAsync(int userId, OrderCreateRequestDto dto)
        {
            List<CartItem> cartItems = await _context.CartItems
                .Include(ci => ci.Product)
                .Where(ci => ci.AuthUserId == userId)
                .ToListAsync();

            if (cartItems.Count == 0)
            {
                return null;
            }

            foreach (CartItem? item in cartItems)
            {
                if (item.Product == null || item.Product.Stock < item.Quantity)
                {
                    return null;
                }
            }

            Order order = new()
            {
                AuthUserId = userId,
                DeliveryAddress = dto.DeliveryAddress,
                DeliveryDate = dto.DeliveryDate,
                Items = cartItems.Select(ci => new OrderItem
                {
                    ProductId = ci.ProductId,
                    ProductName = ci.Product!.Name,
                    Quantity = ci.Quantity,
                    PriceAtPurchase = ci.Product.Price
                }).ToList()
            };

            foreach (CartItem? item in cartItems)
            {
                item.Product!.Stock -= item.Quantity;
            }

            _ = _context.Orders.Add(order);
            _context.CartItems.RemoveRange(cartItems);
            _ = await _context.SaveChangesAsync();

            return order;
        }
    }
}
