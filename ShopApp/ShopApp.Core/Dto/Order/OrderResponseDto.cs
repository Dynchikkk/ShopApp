namespace ShopApp.Core.Dto.Order
{
    /// <summary>
    /// Represents a full order as returned to the client.
    /// </summary>
    public class OrderResponseDto
    {
        /// <summary>
        /// Unique identifier of the order.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Address where the order will be delivered.
        /// </summary>
        public string DeliveryAddress { get; set; } = null!;

        /// <summary>
        /// Scheduled delivery date of the order.
        /// </summary>
        public DateTime DeliveryDate { get; set; }

        /// <summary>
        /// Timestamp when the order was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// List of items included in the order.
        /// </summary>
        public List<OrderItemResponseDto> Items { get; set; } = [];
    }
}
