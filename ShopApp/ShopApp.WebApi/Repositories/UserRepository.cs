using Microsoft.EntityFrameworkCore;
using ShopApp.Core.Models.User;
using ShopApp.Core.Repositories;
using ShopApp.WebApi.Data;

namespace ShopApp.WebApi.Repositories
{
    /// <summary>
    /// Provides data access methods for <see cref="AuthUser"/> entities.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="context">The database context used for user operations.</param>
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Finds a user by their unique identifier.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <returns>The matching <see cref="AuthUser"/> or null if not found.</returns>
        public async Task<AuthUser?> FindByIdAsync(int id)
        {
            return await _context.AuthUsers
                .Include(u => u.RefreshTokens)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        /// <summary>
        /// Finds a user by their username.
        /// </summary>
        /// <param name="username">The username to search for.</param>
        /// <returns>The matching <see cref="AuthUser"/> or null if not found.</returns>
        public async Task<AuthUser?> FindByUsernameAsync(string username)
        {
            return await _context.AuthUsers
                .Include(u => u.RefreshTokens)
                .FirstOrDefaultAsync(u => u.Username == username);
        }

        /// <summary>
        /// Retrieves all users from the database.
        /// </summary>
        /// <returns>A list of all <see cref="AuthUser"/> entities.</returns>
        public async Task<IEnumerable<AuthUser>> GetAllAsync()
        {
            return await _context.AuthUsers
                .Include(u => u.RefreshTokens)
                .ToListAsync();
        }

        /// <summary>
        /// Checks whether a user with the given username already exists.
        /// </summary>
        /// <param name="username">The username to check.</param>
        /// <returns>True if the user exists; otherwise, false.</returns>
        public async Task<bool> UserExistsAsync(string username)
        {
            return await _context.AuthUsers.AnyAsync(u => u.Username == username);
        }

        /// <summary>
        /// Adds a new user to the database.
        /// </summary>
        /// <param name="user">The <see cref="AuthUser"/> to create.</param>
        /// <returns>True if the operation succeeded; otherwise, false.</returns>
        public async Task<bool> CreateAsync(AuthUser user)
        {
            _ = _context.AuthUsers.Add(user);
            _ = _context.UserProfiles.Add(new UserProfile
            {
                AuthUser = user,
                FullName = string.Empty,
                Address = string.Empty,
                Phone = string.Empty,
            });

            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Updates an existing user in the database.
        /// </summary>
        /// <param name="user">The <see cref="AuthUser"/> with updated information.</param>
        /// <returns>True if the update was successful; otherwise, false.</returns>
        public async Task<bool> UpdateAsync(AuthUser user)
        {
            _ = _context.AuthUsers.Update(user);
            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Deletes a user with the specified ID from the database.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>True if the deletion was successful; otherwise, false.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            AuthUser? user = await _context.AuthUsers.FindAsync(id);
            if (user == null)
            {
                return false;
            }

            _ = _context.AuthUsers.Remove(user);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
