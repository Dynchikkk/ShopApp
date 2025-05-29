using ShopApp.Core.Models.User;

namespace ShopApp.Core.Services
{
    /// <summary>
    /// Defines operations for managing user profile data.
    /// </summary>
    public interface IUserProfileService
    {
        /// <summary>
        /// Retrieves the profile of a specific user by their user ID.
        /// </summary>
        /// <param name="userId">The ID of the user whose profile is being retrieved.</param>
        /// <returns>The user's profile if found; otherwise, null.</returns>
        Task<UserProfile?> GetProfileAsync(int userId);

        /// <summary>
        /// Creates a new profile for the specified user.
        /// </summary>
        /// <param name="userId">The ID of the user for whom the profile is being created.</param>
        /// <param name="profile">The profile data to associate with the user.</param>
        /// <returns>The created <see cref="UserProfile"/> instance.</returns>
        Task<UserProfile> CreateProfileAsync(int userId, UserProfile profile);

        /// <summary>
        /// Updates the profile information of an existing user.
        /// </summary>
        /// <param name="userId">The ID of the user whose profile is being updated.</param>
        /// <param name="updated">The updated profile data.</param>
        /// <returns>The updated <see cref="UserProfile"/> if successful; otherwise, null if the profile does not exist.</returns>
        Task<UserProfile?> UpdateProfileAsync(int userId, UserProfile updated);
    }
}
