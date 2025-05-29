namespace ShopApp.Core.Services.Auth
{
    /// <summary>
    /// Defines authentication operations for login and registration.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Authenticates the user and returns a JWT token if successful.
        /// </summary>
        /// <param name="username">The user's username.</param>
        /// <param name="password">The user's password.</param>
        /// <returns>A JWT token if authentication is successful; otherwise, null.</returns>
        Task<string?> LoginAsync(string username, string password);

        /// <summary>
        /// Registers a new user with the specified credentials.
        /// </summary>
        /// <param name="username">The desired username.</param>
        /// <param name="password">The desired password.</param>
        /// <returns>True if registration was successful; otherwise, false.</returns>
        Task<bool> RegisterAsync(string username, string password);
    }
}
