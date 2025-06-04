using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Core.Dto.Order;
using ShopApp.Core.Models.Shop.Order;
using ShopApp.Core.Models.User;
using ShopApp.Core.Services;
using System.Security.Claims;

namespace ShopApp.WebApi.Controllers
{
    /// <summary>
    /// Handles customer order creation and history.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = nameof(UserRole.Client))]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Places a new order using the current user's cart.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<OrderResponseDto>> CreateOrder(OrderCreateRequestDto dto)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            Order? order = await _orderService.CreateOrderAsync(userId, dto);
            return order == null
                ? BadRequest("Cannot place order. Check cart and stock.")
                : Ok(MapToDto(order));
        }

        /// <summary>
        /// Returns the current user's order history.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderResponseDto>>> GetOrders()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            IEnumerable<Order> orders = await _orderService.GetOrdersAsync(userId);
            return Ok(orders.Select(MapToDto));
        }

        /// <summary>
        /// Maps Order entity to DTO.
        /// </summary>
        private static OrderResponseDto MapToDto(Order order)
        {
            return new OrderResponseDto
            {
                Id = order.Id,
                DeliveryAddress = order.DeliveryAddress,
                DeliveryDate = order.DeliveryDate,
                CreatedAt = order.CreatedAt,
                Items = order.Items.Select(i => new OrderItemResponseDto
                {
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    PriceAtPurchase = i.PriceAtPurchase,
                    Quantity = i.Quantity
                }).ToList()
            };
        }
    }
}
