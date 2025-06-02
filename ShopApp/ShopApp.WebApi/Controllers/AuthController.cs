using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Core.Dto.Auth;
using ShopApp.Core.Models.User;
using ShopApp.Core.Services.Auth;
using System.Security.Claims;

namespace ShopApp.WebApi.Controllers
{
    /// <summary>
    /// Controller for handling user authentication and token management.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="authService">Service responsible for authentication and token logic.</param>
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Registers a new user with the provided credentials.
        /// </summary>
        /// <param name="request">The user's registration data (username and password).</param>
        /// <returns>The newly created user or a bad request if the username already exists.</returns>
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<AuthUser>> Register(UserDto request)
        {
            AuthUser? user = await _authService.RegisterAsync(request);
            return user == null ?
                BadRequest("Username already exists.") :
                Ok(user);
        }

        /// <summary>
        /// Authenticates the user and returns a JWT access token along with a refresh token.
        /// </summary>
        /// <param name="request">The user's login credentials.</param>
        /// <returns>Token response DTO or a bad request if authentication fails.</returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<TokenResponseDto>> Login(UserDto request)
        {
            TokenResponseDto? result = await _authService.LoginAsync(request);
            return result == null ?
                BadRequest("Invalid username or password.") :
                Ok(result);
        }

        /// <summary>
        /// Refreshes the access token using a valid refresh token.
        /// </summary>
        /// <param name="request">The refresh token request containing user ID and refresh token.</param>
        /// <returns>A new token pair or unauthorized if the refresh token is invalid or doesn't match the user.</returns>
        [Authorize]
        [HttpPost("refresh-token")]
        public async Task<ActionResult<TokenResponseDto>> RefreshToken(RefreshTokenRequestDto request)
        {
            int currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            if (currentUserId != request.UserId)
            {
                return Unauthorized("Token does not match current user.");
            }

            TokenResponseDto? result = await _authService.RefreshTokensAsync(request);
            return result == null ?
                Unauthorized("Invalid refresh token.") :
                Ok(result);
        }
    }
}
