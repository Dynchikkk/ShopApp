using ShopApp.Core.Dto.Order;
using ShopApp.Core.Models.Shop.Order;

namespace ShopApp.Core.Services
{
    /// <summary>
    /// Defines operations for managing user orders.
    /// </summary>
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetOrdersAsync(int userId);
        Task<Order?> CreateOrderAsync(int userId, CreateOrderRequestDto dto);
    }
}
