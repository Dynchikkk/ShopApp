using Microsoft.EntityFrameworkCore;
using ShopApp.Core.Dto.Order;
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

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync(int userId)
        {
            return await _context.Orders
                .Include(o => o.Items)
                .Where(o => o.AuthUserId == userId)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();
        }

        public async Task<Order?> CreateOrderAsync(int userId, OrderCreateRequestDto dto)
        {
            List<Core.Models.Shop.CartItem> cartItems = await _context.CartItems
                .Include(ci => ci.Product)
                .Where(ci => ci.AuthUserId == userId)
                .ToListAsync();

            if (!cartItems.Any())
            {
                return null;
            }

            foreach (Core.Models.Shop.CartItem? item in cartItems)
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

            foreach (Core.Models.Shop.CartItem? item in cartItems)
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
