using ShopApp.Core.Models.User;

namespace ShopApp.Core.Services.Auth
{
    /// <summary>
    /// Provides functionality for generating JWT access tokens and secure refresh tokens.
    /// </summary>
    public interface IJwtTokenService
    {
        /// <summary>
        /// Generates a JWT access token based on the provided user data.
        /// </summary>
        /// <param name="user">The authenticated user for whom the token is generated.</param>
        /// <returns>A signed JWT access token string.</returns>
        string GenerateAccessToken(AuthUser user);

        /// <summary>
        /// Generates a secure refresh token tied to the specified user ID.
        /// </summary>
        /// <param name="userId">The ID of the user to associate with the refresh token.</param>
        /// <returns>A new <see cref="RefreshToken"/> instance.</returns>
        RefreshToken GenerateRefreshToken(int userId);
    }
}
