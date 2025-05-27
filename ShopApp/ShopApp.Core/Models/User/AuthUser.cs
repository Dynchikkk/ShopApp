using System.ComponentModel.DataAnnotations;

namespace ShopApp.Core.Models.User
{
    /// <summary>
    /// Represents a user account used for authentication and authorization.
    /// </summary>
    public class AuthUser
    {
        /// <summary>
        /// Primary key identifier for the user.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Unique username used for login.
        /// </summary>
        [Required]
        public required string Username { get; set; }
        /// <summary>
        /// Hashed password for user authentication.
        /// </summary>
        [Required]
        public required string PasswordHash { get; set; }
        /// <summary>
        /// Role assigned to the user.
        /// </summary>
        [Required]
        public UserRole Role { get; set; } = UserRole.Client;
    }
}
