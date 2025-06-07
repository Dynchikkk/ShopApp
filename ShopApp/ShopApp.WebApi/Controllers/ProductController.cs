using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Core.Dto.Product;
using ShopApp.Core.Models.Shop;
using ShopApp.Core.Models.User;
using ShopApp.Core.Services;

namespace ShopApp.WebApi.Controllers
{
    /// <summary>
    /// Handles product operations including retrieval, creation, update and deletion.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductController"/> class.
        /// </summary>
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Retrieves all available products.
        /// </summary>
        /// <returns>A list of <see cref="ProductResponseDto"/>.</returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetAll()
        {
            IEnumerable<Product> products = await _productService.GetAllAsync();
            return Ok(products.Select(MapToResponseDto));
        }

        /// <summary>
        /// Retrieves a specific product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product.</param>
        /// <returns>A <see cref="ProductResponseDto"/> or 404 if not found.</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ProductResponseDto>> GetById(int id)
        {
            Product? product = await _productService.GetByIdAsync(id);
            return product == null ? NotFound("Product not found.") : Ok(MapToResponseDto(product));
        }

        /// <summary>
        /// Creates a new product. Accessible only to Admins.
        /// </summary>
        /// <param name="dto">The product creation request.</param>
        /// <returns>The created <see cref="ProductResponseDto"/> with 201 Created status.</returns>
        [HttpPost]
        [Authorize(Roles = nameof(UserRole.Admin))]
        public async Task<ActionResult<ProductResponseDto>> Create(ProductCreateRequestDto dto)
        {
            Product created = await _productService.CreateAsync(new Product
            {
                Name = dto.Name,
                Price = dto.Price,
                Stock = dto.Stock,
                Description = dto.Description
            });

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, MapToResponseDto(created));
        }

        /// <summary>
        /// Updates an existing product by ID. Accessible only to Admins.
        /// </summary>
        /// <param name="id">The ID of the product to update.</param>
        /// <param name="dto">The product update request.</param>
        /// <returns>The updated <see cref="ProductResponseDto"/> or 404 if not found.</returns>
        [HttpPut("{id}")]
        [Authorize(Roles = nameof(UserRole.Admin))]
        public async Task<ActionResult<ProductResponseDto>> Update(int id, ProductUpdateRequestDto dto)
        {
            Product? updated = await _productService.UpdateAsync(id, new Product
            {
                Name = dto.Name,
                Price = dto.Price,
                Stock = dto.Stock,
                Description = dto.Description
            });

            return updated == null ? NotFound("Product not found.") : Ok(MapToResponseDto(updated));
        }

        /// <summary>
        /// Deletes a product by ID. Accessible only to Admins.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        /// <returns>The deleted <see cref="ProductResponseDto"/> or 404 if not found.</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = nameof(UserRole.Admin))]
        public async Task<ActionResult<ProductResponseDto>> Delete(int id)
        {
            Product? deleted = await _productService.DeleteAsync(id);
            return deleted == null ? NotFound("Product not found.") : Ok(MapToResponseDto(deleted));
        }

        /// <summary>
        /// Maps a <see cref="Product"/> entity to its DTO representation.
        /// </summary>
        private static ProductResponseDto MapToResponseDto(Product product)
        {
            return new()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                Description = product.Description
            };
        }
    }
}
