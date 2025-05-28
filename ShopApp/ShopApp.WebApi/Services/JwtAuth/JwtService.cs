using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ShopApp.Core.Models.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ShopApp.WebApi.Services.JwtAuth
{
    /// <summary>
    /// Service responsible for generating JSON Web Tokens (JWT) for authenticated users.
    /// </summary>
    public class JwtService
    {
        private readonly JwtSettings _settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="JwtService"/> class.
        /// </summary>
        /// <param name="options">Injected JWT settings.</param>
        public JwtService(IOptions<JwtSettings> options)
        {
            _settings = options.Value;
        }

        /// <summary>
        /// Generates a signed JWT token for the specified user.
        /// </summary>
        /// <param name="user">The authenticated user for whom the token is generated.</param>
        /// <returns>A signed JWT token as a string.</returns>
        public string GenerateToken(AuthUser user)
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
                expires: DateTime.UtcNow.AddDays(_settings.ExpireDays),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
