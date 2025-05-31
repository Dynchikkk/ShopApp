using ShopApp.Core.Models.User;

namespace ShopApp.Core.Services.Auth
{
    public interface IJwtTokenService
    {
        string GenerateAccessToken(AuthUser user);
        RefreshToken GenerateRefreshToken(int userId);
    }
}
