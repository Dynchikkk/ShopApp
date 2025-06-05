using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Core.Dto.Cart;
using ShopApp.Core.Models.User;
using ShopApp.Core.Services;
using System.Security.Claims;

namespace ShopApp.WebApi.Controllers
{
    /// <summary>
    /// Handles operations related to the user's shopping cart.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = nameof(UserRole.Client))]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        /// <summary>
        /// Retrieves all items currently in the user's cart.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartItemResponseDto>>> GetCart()
        {
            int userId = GetCurrentUserId();
            IEnumerable<Core.Models.Shop.CartItem> cart = await _cartService.GetCartAsync(userId);

            return Ok(cart.Select(ci => new CartItemResponseDto
            {
                Id = ci.Id,
                ProductId = ci.ProductId,
                ProductName = ci.Product?.Name ?? "(Unavailable)",
                Price = ci.Product?.Price ?? 0,
                Quantity = ci.Quantity
            }));
        }

        /// <summary>
        /// Adds a product to the user's cart.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<CartItemResponseDto>> AddToCart([FromBody] CartAddItemRequestDto request)
        {
            int userId = GetCurrentUserId();
            Core.Models.Shop.CartItem? item = await _cartService.AddToCartAsync(userId, request.ProductId, request.Quantity);
            return item == null
                ? (ActionResult<CartItemResponseDto>)BadRequest("Product not found or quantity invalid.")
                : (ActionResult<CartItemResponseDto>)Ok(new CartItemResponseDto
                {
                    Id = item.Id,
                    ProductId = item.ProductId,
                    ProductName = item.Product?.Name ?? "(Unavailable)",
                    Price = item.Product?.Price ?? 0,
                    Quantity = item.Quantity
                });
        }

        /// <summary>
        /// Removes an item from the user's cart.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<CartItemResponseDto>> RemoveFromCart(int id)
        {
            int userId = GetCurrentUserId();
            Core.Models.Shop.CartItem? removed = await _cartService.RemoveFromCartAsync(userId, id);
            return removed == null
                ? (ActionResult<CartItemResponseDto>)NotFound("Cart item not found.")
                : (ActionResult<CartItemResponseDto>)Ok(new CartItemResponseDto
                {
                    Id = removed.Id,
                    ProductId = removed.ProductId,
                    ProductName = removed.Product?.Name ?? "(Unavailable)",
                    Price = removed.Product?.Price ?? 0,
                    Quantity = removed.Quantity
                });
        }

        private int GetCurrentUserId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        }
    }
}
