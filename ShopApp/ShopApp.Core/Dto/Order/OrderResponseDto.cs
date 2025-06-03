namespace ShopApp.Core.Dto.Order
{
    /// <summary>
    /// DTO representing an order to return to the client.
    /// </summary>
    public class OrderResponseDto
    {
        public int Id { get; set; }
        public string DeliveryAddress { get; set; } = null!;
        public DateTime DeliveryDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<OrderItemDto> Items { get; set; } = [];
    }
}
