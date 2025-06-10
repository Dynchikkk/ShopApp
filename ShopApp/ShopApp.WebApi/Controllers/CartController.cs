using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Core.Dto.Cart;
using ShopApp.Core.Models.Shop;
using ShopApp.Core.Models.User;
using ShopApp.Core.Services;
using ShopApp.WebApi.Extensions;

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

        /// <summary>
        /// Initializes a new instance of the <see cref="CartController"/> class.
        /// </summary>
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        /// <summary>
        /// Retrieves all items currently in the user's cart.
        /// </summary>
        /// <returns>A list of cart items.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartItemResponseDto>>> GetCart()
        {
            int userId = User.GetUserId();
            if (userId < 0)
            {
                return Unauthorized("Token does not match current user.");
            }
            IEnumerable<CartItem> cart = await _cartService.GetCartAsync(userId);

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
        /// <param name="request">The product ID and quantity to add.</param>
        /// <returns>The added cart item or an error.</returns>
        [HttpPost]
        public async Task<ActionResult<CartItemResponseDto>> AddToCart([FromBody] CartAddItemRequestDto request)
        {
            int userId = User.GetUserId();
            if (userId < 0)
            {
                return Unauthorized("Token does not match current user.");
            }
            CartItem? item = await _cartService.AddToCartAsync(userId, request.ProductId, request.Quantity);

            return item == null
                ? BadRequest("Product not found or quantity invalid.")
                : Ok(new CartItemResponseDto
                {
                    Id = item.Id,
                    ProductId = item.ProductId,
                    ProductName = item.Product?.Name ?? "(Unavailable)",
                    Price = item.Product?.Price ?? 0,
                    Quantity = item.Quantity
                });
        }

        /// <summary>
        /// Removes an item from the user's cart by its ID.
        /// </summary>
        /// <param name="id">The ID of the cart item to remove.</param>
        /// <returns>The removed cart item or not found.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<CartItemResponseDto>> RemoveFromCart(int id)
        {
            int userId = User.GetUserId();
            if (userId < 0)
            {
                return Unauthorized("Token does not match current user.");
            }
            CartItem? removed = await _cartService.RemoveFromCartAsync(userId, id);

            return removed == null
                ? NotFound("Cart item not found.")
                : Ok(new CartItemResponseDto
                {
                    Id = removed.Id,
                    ProductId = removed.ProductId,
                    ProductName = removed.Product?.Name ?? "(Unavailable)",
                    Price = removed.Product?.Price ?? 0,
                    Quantity = removed.Quantity
                });
        }
    }
}
