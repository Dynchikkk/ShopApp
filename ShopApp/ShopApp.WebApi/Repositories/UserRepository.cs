using Microsoft.EntityFrameworkCore;
using ShopApp.Core.Models.User;
using ShopApp.Core.Repositories;
using ShopApp.WebApi.Data;

namespace ShopApp.WebApi.Repositories
{
    /// <summary>
    /// Repository for performing CRUD operations related to authentication users.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="context">The application's database context.</param>
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a user by their username.
        /// </summary>
        /// <param name="username">The username of the user to find.</param>
        /// <returns>The user if found; otherwise, null.</returns>
        public async Task<AuthUser?> FindByUsernameAsync(string username)
        {
            return await _context.AuthUsers.FirstOrDefaultAsync(u => u.Username == username);
        }

        /// <summary>
        /// Creates a new authentication user in the database.
        /// </summary>
        /// <param name="user">The user entity to create.</param>
        /// <returns>True if the user was successfully created; otherwise, false.</returns>
        public async Task<bool> CreateAsync(AuthUser user)
        {
            _ = _context.AuthUsers.Add(user);
            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Checks if a user with the specified username exists in the database.
        /// </summary>
        /// <param name="username">The username to check.</param>
        /// <returns>True if the user exists; otherwise, false.</returns>
        public async Task<bool> UserExistsAsync(string username)
        {
            return await _context.AuthUsers.AnyAsync(u => u.Username == username);
        }
    }
}
