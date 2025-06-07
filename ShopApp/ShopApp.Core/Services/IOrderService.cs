using ShopApp.Core.Dto.Order;
using ShopApp.Core.Models.Shop.Order;

namespace ShopApp.Core.Services
{
    /// <summary>
    /// Defines operations related to processing and retrieving user orders.
    /// </summary>
    public interface IOrderService
    {
        /// <summary>
        /// Retrieves all orders placed by a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user whose orders to retrieve.</param>
        Task<IEnumerable<Order>> GetOrdersAsync(int userId);

        /// <summary>
        /// Creates a new order for the specified user based on their cart and request data.
        /// </summary>
        /// <param name="userId">The ID of the user placing the order.</param>
        /// <param name="dto">Order creation request containing delivery details.</param>
        Task<Order?> CreateOrderAsync(int userId, OrderCreateRequestDto dto);
    }
}
