namespace ShopApp.Core.Dto.User
{
    /// <summary>
    /// Data transfer object for transferring user profile information.
    /// </summary>
    public class UserProfileDto
    {
        /// <summary>
        /// The full name of the user.
        /// </summary>
        public string? FullName { get; set; }

        /// <summary>
        /// The address associated with the user.
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// The contact phone number of the user.
        /// </summary>
        public string? Phone { get; set; }
    }
}
