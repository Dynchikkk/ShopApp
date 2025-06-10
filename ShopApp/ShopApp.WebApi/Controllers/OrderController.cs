using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Core.Dto.Order;
using ShopApp.Core.Models.Shop.Order;
using ShopApp.Core.Models.User;
using ShopApp.Core.Services;
using ShopApp.WebApi.Extensions;

namespace ShopApp.WebApi.Controllers
{
    /// <summary>
    /// Handles customer order creation and order history retrieval.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = nameof(UserRole.Client))]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderController"/> class.
        /// </summary>
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Places a new order using the current user's cart.
        /// </summary>
        /// <param name="dto">The delivery details for the order.</param>
        /// <returns>The created order or error if validation fails.</returns>
        [HttpPost]
        public async Task<ActionResult<OrderResponseDto>> CreateOrder(OrderCreateRequestDto dto)
        {
            int userId = User.GetUserId();
            if (userId < 0)
            {
                return Unauthorized("Token does not match current user.");
            }
            Order? order = await _orderService.CreateOrderAsync(userId, dto);

            return order == null
                ? BadRequest("Cannot place order. Check cart and stock.")
                : Ok(MapToDto(order));
        }

        /// <summary>
        /// Retrieves a list of all orders placed by the current user.
        /// </summary>
        /// <returns>A list of order DTOs.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderResponseDto>>> GetOrders()
        {
            int userId = User.GetUserId();
            if (userId < 0)
            {
                return Unauthorized("Token does not match current user.");
            }
            IEnumerable<Order> orders = await _orderService.GetOrdersAsync(userId);

            return Ok(orders.Select(MapToDto));
        }

        /// <summary>
        /// Maps an <see cref="Order"/> entity to its corresponding DTO.
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
