using System.ComponentModel.DataAnnotations;

namespace ShopApp.Core.Models.User
{
    /// <summary>
    /// Represents a refresh token used to renew access tokens without re-authenticating the user.
    /// </summary>
    public class RefreshToken
    {
        /// <summary>
        /// Primary key identifier for the refresh token.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Token string value (should be a secure random string).
        /// </summary>
        [Required]
        public required string Token { get; set; }

        /// <summary>
        /// Expiration date of the token.
        /// </summary>
        public DateTime ExpiresAt { get; set; }

        /// <summary>
        /// Indicates if the token is revoked and no longer valid.
        /// </summary>
        public bool IsRevoked { get; set; } = false;

        /// <summary>
        /// Foreign key linking to the authenticated user.
        /// </summary>
        [Required]
        public int AuthUserId { get; set; }
        /// <summary>
        /// Navigation property to the associated user.
        /// </summary>
        public AuthUser? AuthUser { get; set; }
    }
}
