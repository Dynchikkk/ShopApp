using Microsoft.AspNetCore.Identity;
using ShopApp.Core.Models.User;
using ShopApp.Core.Repositories;
using ShopApp.Core.Services.Auth;
using System.Text.Json;

namespace ShopApp.WebApi.Services.JwtAuth
{
    /// <summary>
    /// Service responsible for user authentication and registration using JWT.
    /// </summary>
    public class JwtAuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly IPasswordHasher<AuthUser> _hasher;
        private readonly IJwtTokenService _tokenService;

        /// <summary>
        /// Initializes a new instance of the <see cref="JwtAuthService"/> class.
        /// </summary>
        /// <param name="userRepo">Repository to interact with user data.</param>
        /// <param name="hasher">Password hasher for securely storing credentials.</param>
        /// <param name="tokenService">Service to generate JWT tokens.</param>
        public JwtAuthService(IUserRepository userRepo, IPasswordHasher<AuthUser> hasher, IJwtTokenService tokenService)
        {
            _userRepo = userRepo;
            _hasher = hasher;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Attempts to log in a user with the specified credentials.
        /// </summary>
        /// <param name="username">The user's username.</param>
        /// <param name="password">The user's password.</param>
        /// <returns>JWT token if credentials are valid; otherwise, null.</returns>
        public async Task<string?> LoginAsync(string username, string password)
        {
            AuthUser? user = await _userRepo.FindByUsernameAsync(username);
            if (user == null)
            {
                return null;
            }

            PasswordVerificationResult result = _hasher.VerifyHashedPassword(user, user.PasswordHash, password);
            if (result == PasswordVerificationResult.Failed)
            {
                return null;
            }

            string accessToken = _tokenService.GenerateAccessToken(user);
            RefreshToken refreshToken = _tokenService.GenerateRefreshToken(user.Id);

            user.RefreshTokens.Add(refreshToken);
            _ = await _userRepo.UpdateAsync(user);

            return JsonSerializer.Serialize(new
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token
            });
        }

        /// <summary>
        /// Registers a new user with the provided credentials.
        /// </summary>
        /// <param name="username">Desired username.</param>
        /// <param name="password">Desired password.</param>
        /// <returns>True if registration is successful; otherwise, false.</returns>
        public async Task<bool> RegisterAsync(string username, string password)
        {
            if (await _userRepo.UserExistsAsync(username))
            {
                return false;
            }

            AuthUser user = new()
            {
                Username = username,
                PasswordHash = _hasher.HashPassword(null!, password),
                Role = UserRole.Client
            };

            return await _userRepo.CreateAsync(user);
        }
    }
}
