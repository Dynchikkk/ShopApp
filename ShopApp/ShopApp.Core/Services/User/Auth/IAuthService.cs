using ShopApp.Core.Dto.User.Auth;
using ShopApp.Core.Models.User;

namespace ShopApp.Core.Services.User.Auth
{
    /// <summary>
    /// Defines the authentication and authorization operations for users.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Registers a new user with the provided credentials.
        /// </summary>
        /// <param name="request">The user's registration details.</param>
        /// <returns>The newly created user, or null if the username is already taken.</returns>
        Task<AuthUser?> RegisterAsync(UserDto request);

        /// <summary>
        /// Authenticates a user and returns JWT access and refresh tokens.
        /// </summary>
        /// <param name="request">The user's login credentials.</param>
        /// <returns>Token pair if authentication is successful; otherwise, null.</returns>
        Task<TokenResponseDto?> LoginAsync(UserDto request);

        /// <summary>
        /// Validates and refreshes the user's JWT tokens.
        /// </summary>
        /// <param name="request">The refresh token request containing user ID and token.</param>
        /// <returns>A new pair of access and refresh tokens if the refresh is valid; otherwise, null.</returns>
        Task<TokenResponseDto?> RefreshTokensAsync(RefreshTokenRequestDto request);
    }
}
