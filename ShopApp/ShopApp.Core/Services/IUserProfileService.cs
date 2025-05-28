using ShopApp.Core.Models.User;

namespace ShopApp.Core.Services
{
    /// <summary>
    /// Defines operations for managing user profile data.
    /// </summary>
    public interface IUserProfileService
    {
        /// <summary>
        /// Retrieves the profile of a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        Task<UserProfile?> GetProfileAsync(int userId);

        /// <summary>
        /// Creates a new profile for a user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="profile">The profile data to create.</param>
        Task<UserProfile> CreateProfileAsync(int userId, UserProfile profile);

        /// <summary>
        /// Updates an existing user profile.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="updated">The updated profile data.</param>
        Task<UserProfile?> UpdateProfileAsync(int userId, UserProfile updated);
    }
}
