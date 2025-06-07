namespace ShopApp.Core.Dto.Order
{
    /// <summary>
    /// Represents an individual product item within an order.
    /// </summary>
    public class OrderItemResponseDto
    {
        /// <summary>
        /// Identifier of the ordered product.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Name of the product at the time of purchase.
        /// </summary>
        public string ProductName { get; set; } = null!;

        /// <summary>
        /// Price of the product at the time of purchase.
        /// </summary>
        public decimal PriceAtPurchase { get; set; }

        /// <summary>
        /// Quantity of the product ordered.
        /// </summary>
        public int Quantity { get; set; }
    }
}
