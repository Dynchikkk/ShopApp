namespace ShopApp.Core.Dto.Order
{
    /// <summary>
    /// DTO representing an item within an order.
    /// </summary>
    public class OrderItemResponseDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public decimal PriceAtPurchase { get; set; }
        public int Quantity { get; set; }
    }
}
