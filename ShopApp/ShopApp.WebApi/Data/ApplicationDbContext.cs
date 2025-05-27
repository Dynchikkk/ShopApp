using Microsoft.EntityFrameworkCore;
using ShopApp.Core.Models.Shop;
using ShopApp.Core.Models.User;

namespace ShopApp.WebApi.Data
{
    /// <summary>
    /// Represents the database context for the ShopApp application.
    /// Provides access to user, profile, product, and cart item entities.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class using the specified options.
    /// </remarks>
    /// <param name="options">The options to configure the context.</param>
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        /// <summary>
        /// Gets or sets the users used for authentication.
        /// </summary>
        public DbSet<AuthUser> AuthUsers { get; set; }
        /// <summary>
        /// Gets or sets the user profile data.
        /// </summary>
        public DbSet<UserProfile> UserProfiles { get; set; }
        /// <summary>
        /// Gets or sets the product catalog.
        /// </summary>
        public DbSet<Product> Products { get; set; }
        /// <summary>
        /// Gets or sets the items in user shopping carts.
        /// </summary>
        public DbSet<CartItem> CartItems { get; set; }

        /// <summary>
        /// Configures the relationships and model settings.
        /// </summary>
        /// <param name="modelBuilder">The model builder used to configure entities.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            _ = modelBuilder.Entity<UserProfile>()
                .HasOne(up => up.AuthUser)
                .WithOne()
                .HasForeignKey<UserProfile>(up => up.AuthUserId)
                .OnDelete(DeleteBehavior.Cascade);

            _ = modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Product)
                .WithMany()
                .HasForeignKey(ci => ci.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
            _ = modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.AuthUser)
                .WithMany()
                .HasForeignKey(ci => ci.AuthUserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
