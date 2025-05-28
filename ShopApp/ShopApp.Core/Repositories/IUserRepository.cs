using ShopApp.Core.Models.User;

namespace ShopApp.Core.Repositories
{
    /// <summary>
    /// Defines operations for managing authentication users in the data source.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Finds an authentication user by their username.
        /// </summary>
        /// <param name="username">The username to search for.</param>
        /// <returns>The matching <see cref="AuthUser"/> if found; otherwise, null.</returns>
        Task<AuthUser?> FindByUsernameAsync(string username);

        /// <summary>
        /// Creates a new authentication user in the data source.
        /// </summary>
        /// <param name="user">The user to create.</param>
        /// <returns>True if creation was successful; otherwise, false.</returns>
        Task<bool> CreateAsync(AuthUser user);

        /// <summary>
        /// Checks if a user with the specified username already exists.
        /// </summary>
        /// <param name="username">The username to check for existence.</param>
        /// <returns>True if the user exists; otherwise, false.</returns>
        Task<bool> UserExistsAsync(string username);
    }
}
