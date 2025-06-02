namespace ShopApp.Core.Dto
{
    /// <summary>
    /// Data transfer object for adding a product to the user's shopping cart.
    /// </summary>
    public class AddToCartDto
    {
        /// <summary>
        /// The identifier of the product to add to the cart.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// The quantity of the product to add.
        /// </summary>
        public int Quantity { get; set; }
    }
}
