namespace ShopApp.Core.Dto.Auth
{
    /// <summary>
    /// Data transfer object for user authentication and registration.
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// The username provided by the user.
        /// </summary>
        public string Username { get; set; } = null!;

        /// <summary>
        /// The plain-text password provided by the user.
        /// </summary>
        public string Password { get; set; } = null!;
    }
}
