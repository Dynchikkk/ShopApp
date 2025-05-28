using Microsoft.EntityFrameworkCore;
using ShopApp.Core.Models.Shop;
using ShopApp.Core.Services;
using ShopApp.WebApi.Data;

namespace ShopApp.WebApi.Services
{
    /// <summary>
    /// Service for managing product-related operations in the store.
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all products from the database.
        /// </summary>
        /// <returns>A collection of all products.</returns>
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        /// <summary>
        /// Retrieves a product by its identifier.
        /// </summary>
        /// <param name="id">The product ID.</param>
        /// <returns>The product if found; otherwise, null.</returns>
        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        /// <summary>
        /// Creates a new product in the database.
        /// </summary>
        /// <param name="product">The product to create.</param>
        /// <returns>The created product.</returns>
        public async Task<Product> CreateAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="id">The ID of the product to update.</param>
        /// <param name="updated">The updated product data.</param>
        /// <returns>The updated product if successful; otherwise, null.</returns>
        public async Task<Product?> UpdateAsync(int id, Product updated)
        {
            Product? product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return null;
            }

            product.Name = updated.Name;
            product.Price = updated.Price;
            product.Stock = updated.Stock;
            product.Description = updated.Description;

            await _context.SaveChangesAsync();
            return product;
        }

        /// <summary>
        /// Deletes a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        /// <returns>The deleted product if found; otherwise, null.</returns>
        public async Task<Product?> DeleteAsync(int id)
        {
            Product? product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return null;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return product;
        }
    }
}
