using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ShopApp.Core.Models.User;
using ShopApp.Core.Services.User.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ShopApp.WebApi.Services.JwtAuth
{
    /// <summary>
    /// Provides functionality for generating JWT access and refresh tokens.
    /// </summary>
    public class JwtTokenService : IJwtTokenService
    {
        private readonly JwtSettings _settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="JwtTokenService"/> class.
        /// </summary>
        /// <param name="options">The JWT settings provided via dependency injection.</param>
        public JwtTokenService(IOptions<JwtSettings> options)
        {
            _settings = options.Value;
        }

        /// <summary>
        /// Generates a JWT access token for the specified user.
        /// </summary>
        /// <param name="user">The authenticated user for whom to generate the token.</param>
        /// <returns>A signed JWT access token string.</returns>
        public string GenerateAccessToken(AuthUser user)
        {
            List<Claim> claims =
            [
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            ];

            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_settings.Key));
            SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_settings.AccessTokenLifetimeMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Generates a refresh token for the specified user ID.
        /// </summary>
        /// <param name="userId">The ID of the user to associate with the refresh token.</param>
        /// <returns>A new <see cref="RefreshToken"/> instance.</returns>
        public RefreshToken GenerateRefreshToken(int userId)
        {
            return new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                ExpiresAt = DateTime.UtcNow.AddMinutes(_settings.RefreshTokenLifetimeMinutes),
                AuthUserId = userId
            };
        }
    }
}
