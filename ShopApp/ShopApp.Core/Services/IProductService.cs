using ShopApp.Core.Models.Shop;

namespace ShopApp.Core.Services
{
    /// <summary>
    /// Defines operations for managing products in the store.
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Retrieves all products.
        /// </summary>
        Task<IEnumerable<Product>> GetAllAsync();

        /// <summary>
        /// Retrieves a product by its ID.
        /// </summary>
        Task<Product?> GetByIdAsync(int id);

        /// <summary>
        /// Creates a new product.
        /// </summary>
        Task<Product> CreateAsync(Product product);

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        Task<Product?> UpdateAsync(int id, Product updated);

        /// <summary>
        /// Deletes a product by ID.
        /// </summary>
        Task<Product?> DeleteAsync(int id);
    }
}
