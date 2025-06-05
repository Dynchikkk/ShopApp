using Microsoft.AspNetCore.Identity;
using ShopApp.Core.Dto.User.Auth;
using ShopApp.Core.Models.User;
using ShopApp.Core.Repositories;
using ShopApp.Core.Services.User.Auth;

namespace ShopApp.WebApi.Services.JwtAuth
{
    /// <summary>
    /// Service responsible for user authentication and registration using JWT tokens.
    /// </summary>
    public class JwtAuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly IPasswordHasher<AuthUser> _hasher;
        private readonly IJwtTokenService _tokenService;

        /// <summary>
        /// Initializes a new instance of the <see cref="JwtAuthService"/> class.
        /// </summary>
        public JwtAuthService(IUserRepository userRepo, IPasswordHasher<AuthUser> hasher, IJwtTokenService tokenService)
        {
            _userRepo = userRepo;
            _hasher = hasher;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Registers a new user with hashed password and default role.
        /// </summary>
        /// <param name="request">The user registration details.</param>
        /// <returns>The created user or null if the username is already taken.</returns>
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

        /// <summary>
        /// Validates user credentials and issues JWT access and refresh tokens.
        /// </summary>
        /// <param name="request">The user's login credentials.</param>
        /// <returns>Token response if login is successful, otherwise null.</returns>
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

        /// <summary>
        /// Refreshes access and refresh tokens for a valid, non-revoked refresh token.
        /// </summary>
        /// <param name="request">The refresh token request including user ID and token.</param>
        /// <returns>New token pair or null if token is invalid or expired.</returns>
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
