namespace ShopApp.Core.Dto.Order
{
    /// <summary>
    /// Represents the request to place a new order.
    /// </summary>
    public class OrderCreateRequestDto
    {
        public string DeliveryAddress { get; set; } = null!;
        public DateTime DeliveryDate { get; set; }
    }
}
