namespace ShopApp.Core.Dto.Order
{
    /// <summary>
    /// Represents a request from the client to place a new order.
    /// </summary>
    public class OrderCreateRequestDto
    {
        /// <summary>
        /// Address where the order should be delivered.
        /// </summary>
        public string DeliveryAddress { get; set; } = null!;

        /// <summary>
        /// Desired delivery date for the order.
        /// </summary>
        public DateTime DeliveryDate { get; set; }
    }
}
