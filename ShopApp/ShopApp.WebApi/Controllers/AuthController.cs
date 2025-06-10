using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Core.Dto.User.Auth;
using ShopApp.Core.Models.User;
using ShopApp.Core.Services.User.Auth;
using ShopApp.WebApi.Extensions;

namespace ShopApp.WebApi.Controllers
{
    /// <summary>
    /// Controller for handling user authentication, registration, and JWT token management.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="authService">The authentication service to handle user and token logic.</param>
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Registers a new user using provided username and password credentials.
        /// </summary>
        /// <param name="request">User registration request containing username and password.</param>
        /// <returns>
        /// A <see cref="UserResponseDto"/> representing the newly created user on success,
        /// or 400 BadRequest if the username already exists.
        /// </returns>
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserResponseDto>> Register(UserDto request)
        {
            AuthUser? user = await _authService.RegisterAsync(request);
            return user == null
                ? BadRequest("Username already exists.")
                : Ok(new UserResponseDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    Role = user.Role.ToString()
                });
        }

        /// <summary>
        /// Authenticates a user and returns an access token along with a refresh token if successful.
        /// </summary>
        /// <param name="request">Login credentials (username and password).</param>
        /// <returns>
        /// A <see cref="TokenResponseDto"/> containing the JWT access and refresh tokens,
        /// or 400 BadRequest if the credentials are invalid.
        /// </returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<TokenResponseDto>> Login(UserDto request)
        {
            TokenResponseDto? result = await _authService.LoginAsync(request);
            return result == null
                ? BadRequest("Invalid username or password.")
                : Ok(result);
        }

        /// <summary>
        /// Refreshes JWT tokens using a valid and unexpired refresh token.
        /// </summary>
        /// <param name="request">The refresh token request containing user ID and refresh token.</param>
        /// <returns>
        /// A <see cref="TokenResponseDto"/> with new access and refresh tokens if successful,
        /// or 401 Unauthorized if validation fails.
        /// </returns>
        [Authorize]
        [HttpPost("refresh-token")]
        public async Task<ActionResult<TokenResponseDto>> RefreshToken(RefreshTokenRequestDto request)
        {
            int currentUserId = User.GetUserId();
            if (currentUserId < 0 || currentUserId != request.UserId)
            {
                return Unauthorized("Token does not match current user.");
            }

            TokenResponseDto? result = await _authService.RefreshTokensAsync(request);
            return result == null
                ? Unauthorized("Invalid refresh token.")
                : Ok(result);
        }
    }
}
