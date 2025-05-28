using Microsoft.EntityFrameworkCore;
using ShopApp.Core.Models.User;
using ShopApp.Core.Services;
using ShopApp.WebApi.Data;

namespace ShopApp.WebApi.Services
{
    /// <summary>
    /// Service for managing user profiles.
    /// </summary>
    public class UserProfileService : IUserProfileService
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserProfileService"/> class.
        /// </summary>
        /// <param name="context">The application's database context.</param>
        public UserProfileService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves the profile of a specific user by their ID.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>The user profile if found; otherwise, null.</returns>
        public async Task<UserProfile?> GetProfileAsync(int userId)
        {
            return await _context.UserProfiles
                .Include(up => up.AuthUser)
                .FirstOrDefaultAsync(up => up.AuthUserId == userId);
        }

        /// <summary>
        /// Creates a new profile for a user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="profile">The profile information to create.</param>
        /// <returns>The created user profile.</returns>
        public async Task<UserProfile> CreateProfileAsync(int userId, UserProfile profile)
        {
            profile.AuthUserId = userId;
            _context.UserProfiles.Add(profile);
            await _context.SaveChangesAsync();
            return profile;
        }

        /// <summary>
        /// Updates an existing user profile.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="updated">The updated profile data.</param>
        /// <returns>The updated profile if found and modified; otherwise, null.</returns>
        public async Task<UserProfile?> UpdateProfileAsync(int userId, UserProfile updated)
        {
            UserProfile? existing = await _context.UserProfiles
                .FirstOrDefaultAsync(p => p.AuthUserId == userId);

            if (existing == null)
            {
                return null;
            }

            existing.FullName = updated.FullName;
            existing.Address = updated.Address;
            existing.Phone = updated.Phone;

            await _context.SaveChangesAsync();
            return existing;
        }
    }
}
