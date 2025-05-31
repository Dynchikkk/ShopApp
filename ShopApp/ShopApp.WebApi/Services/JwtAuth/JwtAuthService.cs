using Microsoft.AspNetCore.Identity;
using ShopApp.Core.Dto.Auth;
using ShopApp.Core.Models.User;
using ShopApp.Core.Repositories;
using ShopApp.Core.Services.Auth;

namespace ShopApp.WebApi.Services.JwtAuth
{
    public class JwtAuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly IPasswordHasher<AuthUser> _hasher;
        private readonly IJwtTokenService _tokenService;

        public JwtAuthService(IUserRepository userRepo, IPasswordHasher<AuthUser> hasher, IJwtTokenService tokenService)
        {
            _userRepo = userRepo;
            _hasher = hasher;
            _tokenService = tokenService;
        }

        public async Task<AuthUser?> RegisterAsync(UserDto request)
        {
            if (await _userRepo.UserExistsAsync(request.Username))
            {
                return null;
            }

            AuthUser user = new()
            {
                Username = request.Username,
                PasswordHash = _hasher.HashPassword(null!, request.Password),
                Role = UserRole.Client,
                RefreshTokens = []
            };

            bool created = await _userRepo.CreateAsync(user);
            return created ? user : null;
        }

        public async Task<TokenResponseDto?> LoginAsync(UserDto request)
        {
            AuthUser? user = await _userRepo.FindByUsernameAsync(request.Username);
            if (user == null)
            {
                return null;
            }

            PasswordVerificationResult result = _hasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                return null;
            }

            string access = _tokenService.GenerateAccessToken(user);
            RefreshToken refresh = _tokenService.GenerateRefreshToken(user.Id);

            user.RefreshTokens.Add(refresh);
            _ = await _userRepo.UpdateAsync(user);

            return new TokenResponseDto
            {
                AccessToken = access,
                RefreshToken = refresh.Token
            };
        }

        public async Task<TokenResponseDto?> RefreshTokensAsync(RefreshTokenRequestDto request)
        {
            AuthUser? user = await _userRepo.FindByIdAsync(request.UserId);
            if (user == null)
            {
                return null;
            }

            RefreshToken? token = user.RefreshTokens.FirstOrDefault(r =>
                r.Token == request.RefreshToken &&
                !r.IsRevoked &&
                r.ExpiresAt > DateTime.UtcNow);

            if (token == null)
            {
                return null;
            }

            string access = _tokenService.GenerateAccessToken(user);
            RefreshToken refresh = _tokenService.GenerateRefreshToken(user.Id);

            user.RefreshTokens.Add(refresh);
            token.IsRevoked = true;
            _ = await _userRepo.UpdateAsync(user);

            return new TokenResponseDto
            {
                AccessToken = access,
                RefreshToken = refresh.Token
            };
        }
    }
}
