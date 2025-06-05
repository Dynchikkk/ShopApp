using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Core.Dto.User.Auth;
using ShopApp.Core.Models.User;
using ShopApp.Core.Services.User.Auth;
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

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Registers a new user with the provided credentials.
        /// </summary>
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
        /// Authenticates the user and returns a JWT access token along with a refresh token.
        /// </summary>
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
        /// Refreshes the access token using a valid refresh token.
        /// </summary>
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
            return result == null
                ? Unauthorized("Invalid refresh token.")
                : Ok(result);
        }
    }
}
