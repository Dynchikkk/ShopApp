using ShopApp.Core.Models.User;

namespace ShopApp.Core.Services.Auth
{
    /// <summary>
    /// Defines operations for generating and managing JWT tokens.
    /// </summary>
    public interface IJwtTokenService
    {
        /// <summary>
        /// Generates a JWT access token for the specified user.
        /// </summary>
        /// <param name="user">The authenticated user.</param>
        /// <returns>A signed JWT access token string.</returns>
        string GenerateAccessToken(AuthUser user);

        /// <summary>
        /// Generates a new refresh token tied to the given user ID.
        /// </summary>
        /// <param name="userId">The ID of the user the token is associated with.</param>
        /// <returns>A new refresh token object.</returns>
        RefreshToken GenerateRefreshToken(int userId);
    }
}
