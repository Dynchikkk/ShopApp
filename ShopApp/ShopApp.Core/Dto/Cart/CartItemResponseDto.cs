namespace ShopApp.Core.Dto.Cart
{
    /// <summary>
    /// DTO for returning cart item information to the client.
    /// </summary>
    public class CartItemResponseDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Total => Price * Quantity;
    }
}
