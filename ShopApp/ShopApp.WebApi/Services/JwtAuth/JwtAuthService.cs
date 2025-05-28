using Microsoft.AspNetCore.Identity;
using ShopApp.Core.Models.User;
using ShopApp.Core.Repositories;
using ShopApp.Core.Services;

namespace ShopApp.WebApi.Services.JwtAuth
{
    /// <summary>
    /// Service responsible for user authentication and registration using JWT.
    /// </summary>
    public class JwtAuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly IPasswordHasher<AuthUser> _hasher;
        private readonly JwtService _jwt;

        /// <summary>
        /// Initializes a new instance of the <see cref="JwtAuthService"/> class.
        /// </summary>
        /// <param name="userRepo">Repository to interact with user data.</param>
        /// <param name="hasher">Password hasher for securely storing credentials.</param>
        /// <param name="jwt">Service to generate JWT tokens.</param>
        public JwtAuthService(IUserRepository userRepo, IPasswordHasher<AuthUser> hasher, JwtService jwt)
        {
            _userRepo = userRepo;
            _hasher = hasher;
            _jwt = jwt;
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
            return result == PasswordVerificationResult.Failed ? null : _jwt.GenerateToken(user);
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
