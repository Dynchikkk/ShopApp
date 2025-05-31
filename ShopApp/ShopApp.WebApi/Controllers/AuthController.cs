using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Core.Dto.Auth;
using ShopApp.Core.Models.User;
using ShopApp.Core.Services.Auth;
using System.Security.Claims;

namespace ShopApp.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<AuthUser>> Register(UserDto request)
        {
            AuthUser? user = await _authService.RegisterAsync(request);
            return user == null ? 
                BadRequest("Username already exists.") : 
                Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<TokenResponseDto>> Login(UserDto request)
        {
            TokenResponseDto? result = await _authService.LoginAsync(request);
            return result == null ? 
                BadRequest("Invalid username or password.") : 
                Ok(result);
        }

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
