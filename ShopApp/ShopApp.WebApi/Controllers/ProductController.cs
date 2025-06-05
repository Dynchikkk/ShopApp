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

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetAll()
        {
            IEnumerable<Product> products = await _productService.GetAllAsync();
            return Ok(products.Select(MapToResponseDto));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ProductResponseDto>> GetById(int id)
        {
            Product? product = await _productService.GetByIdAsync(id);
            return product == null ? NotFound("Product not found.") : Ok(MapToResponseDto(product));
        }

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

        [HttpDelete("{id}")]
        [Authorize(Roles = nameof(UserRole.Admin))]
        public async Task<ActionResult<ProductResponseDto>> Delete(int id)
        {
            Product? deleted = await _productService.DeleteAsync(id);
            return deleted == null ? NotFound("Product not found.") : Ok(MapToResponseDto(deleted));
        }

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
