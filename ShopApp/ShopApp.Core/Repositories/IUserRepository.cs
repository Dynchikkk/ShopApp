using ShopApp.Core.Models.User;

namespace ShopApp.Core.Repositories
{
    /// <summary>
    /// Defines the contract for user-related data operations.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Retrieves a user by their unique identifier.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>The user if found; otherwise, null.</returns>
        Task<AuthUser?> FindByIdAsync(int id);
        /// <summary>
        /// Retrieves a user by their username.
        /// </summary>
        /// <param name="username">The username to search for.</param>
        /// <returns>The user if found; otherwise, null.</returns>
        Task<AuthUser?> FindByUsernameAsync(string username);
        /// <summary>
        /// Retrieves all users in the system.
        /// </summary>
        /// <returns>A collection of all users.</returns>
        Task<IEnumerable<AuthUser>> GetAllAsync();
        /// <summary>
        /// Determines whether a user with the specified username already exists.
        /// </summary>
        /// <param name="username">The username to check.</param>
        /// <returns>True if the user exists; otherwise, false.</returns>
        Task<bool> UserExistsAsync(string username);

        /// <summary>
        /// Creates a new user in the data store.
        /// </summary>
        /// <param name="user">The user entity to create.</param>
        /// <returns>True if the operation succeeds; otherwise, false.</returns>
        Task<bool> CreateAsync(AuthUser user);

        /// <summary>
        /// Updates the specified user in the data store.
        /// </summary>
        /// <param name="user">The user entity to update.</param>
        /// <returns>True if the update succeeds; otherwise, false.</returns>
        Task<bool> UpdateAsync(AuthUser user);

        /// <summary>
        /// Deletes a user by their unique identifier.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>True if the deletion succeeds; otherwise, false.</returns>
        Task<bool> DeleteAsync(int id);
    }
}
