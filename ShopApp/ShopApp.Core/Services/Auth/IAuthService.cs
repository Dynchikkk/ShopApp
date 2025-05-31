using ShopApp.Core.Dto.Auth;
using ShopApp.Core.Models.User;

namespace ShopApp.Core.Services.Auth
{
    public interface IAuthService
    {
        Task<AuthUser?> RegisterAsync(UserDto request);
        Task<TokenResponseDto?> LoginAsync(UserDto request);
        Task<TokenResponseDto?> RefreshTokensAsync(RefreshTokenRequestDto request);
    }
}
