namespace ShopApp.Core.Dto.User.Auth
{
    /// <summary>
    /// Represents user information returned to the client after registration or login.
    /// </summary>
    public class UserResponseDto
    {
        /// <summary>
        /// Unique identifier of the user.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Username of the user.
        /// </summary>
        public string Username { get; set; } = null!;

        /// <summary>
        /// Role assigned to the user (e.g., Admin or Client).
        /// </summary>
        public string Role { get; set; } = null!;
    }
}
