namespace ShopApp.Core.Models.User
{
    /// <summary>
    /// Defines the possible roles for a user.
    /// </summary>
    public enum UserRole
    {
        /// <summary>
        /// Administrator role with elevated permissions.
        /// </summary>
        Admin = 0,
        /// <summary>
        /// Standard client role.
        /// </summary>
        Client = 1
    }
}
