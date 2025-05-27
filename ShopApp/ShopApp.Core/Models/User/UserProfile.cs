using System.ComponentModel.DataAnnotations;

namespace ShopApp.Core.Models.User
{
    /// <summary>
    /// Represents a user profile containing personal information.
    /// </summary>
    public class UserProfile
    {
        /// <summary>
        /// Primary key identifier for the profile.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Foreign key linking to the authenticated user.
        /// </summary>
        [Required]
        public int AuthUserId { get; set; }
        /// <summary>
        /// Navigation property to the authenticated user.
        /// </summary>
        public AuthUser? AuthUser { get; set; }

        /// <summary>
        /// Full name of the user.
        /// </summary>
        public string? FullName { get; set; }
        /// <summary>
        /// Shipping or billing address of the user.
        /// </summary>
        public string? Address { get; set; }
        /// <summary>
        /// Contact phone number.
        /// </summary>
        public string? Phone { get; set; }
    }
}
