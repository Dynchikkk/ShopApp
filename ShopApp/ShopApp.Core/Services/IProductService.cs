using ShopApp.Core.Models.Shop;

namespace ShopApp.Core.Services
{
    /// <summary>
    /// Defines operations for managing products in the store.
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Retrieves all products available in the store.
        /// </summary>
        /// <returns>A collection of <see cref="Product"/> representing all available products.</returns>
        Task<IEnumerable<Product>> GetAllAsync();

        /// <summary>
        /// Retrieves a specific product by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the product.</param>
        /// <returns>The <see cref="Product"/> if found; otherwise, null.</returns>
        Task<Product?> GetByIdAsync(int id);

        /// <summary>
        /// Creates and persists a new product in the store.
        /// </summary>
        /// <param name="product">The product details to create.</param>
        /// <returns>The created <see cref="Product"/> with its assigned identifier.</returns>
        Task<Product> CreateAsync(Product product);

        /// <summary>
        /// Updates an existing product with new details.
        /// </summary>
        /// <param name="id">The ID of the product to update.</param>
        /// <param name="updated">The updated product information.</param>
        /// <returns>The updated <see cref="Product"/> if found and modified; otherwise, null.</returns>
        Task<Product?> UpdateAsync(int id, Product updated);

        /// <summary>
        /// Deletes a product by its unique identifier.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        /// <returns>The deleted <see cref="Product"/> if it existed; otherwise, null.</returns>
        Task<Product?> DeleteAsync(int id);
    }
}
