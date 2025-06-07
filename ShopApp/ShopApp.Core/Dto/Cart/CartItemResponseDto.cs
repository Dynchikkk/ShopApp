namespace ShopApp.Core.Dto.Cart
{
    /// <summary>
    /// Represents a single item in the user's shopping cart, including product details and quantity.
    /// </summary>
    public class CartItemResponseDto
    {
        /// <summary>
        /// Unique identifier of the cart item.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Identifier of the product associated with the cart item.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Name of the product in the cart.
        /// </summary>
        public string ProductName { get; set; } = null!;

        /// <summary>
        /// Price of the product at the time it was added to the cart.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Quantity of the product in the cart.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Total price for this cart item (Price * Quantity).
        /// </summary>
        public decimal Total => Price * Quantity;
    }
}
