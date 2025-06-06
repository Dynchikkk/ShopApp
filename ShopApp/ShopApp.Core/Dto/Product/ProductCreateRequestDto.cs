﻿namespace ShopApp.Core.Dto.Product
{
    /// <summary>
    /// Request DTO for creating a new product.
    /// </summary>
    public class ProductCreateRequestDto
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string? Description { get; set; }
    }
}
