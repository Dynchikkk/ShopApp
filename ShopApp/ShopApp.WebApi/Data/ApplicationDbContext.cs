using Microsoft.EntityFrameworkCore;
using ShopApp.Core.Models.Shop;
using ShopApp.Core.Models.Shop.Order;
using ShopApp.Core.Models.User;

namespace ShopApp.WebApi.Data
{
    /// <summary>
    /// Represents the database context for the ShopApp application.
    /// Provides access to all entity sets and configures their relationships.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
        /// </summary>
        /// <param name="options">Database context options.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        /// <summary>
        /// Gets or sets the users used for authentication.
        /// </summary>
        public DbSet<AuthUser> AuthUsers { get; set; }

        /// <summary>
        /// Gets or sets the refresh tokens for session renewal.
        /// </summary>
        public DbSet<RefreshToken> RefreshTokens { get; set; }

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
        /// Gets or sets the user orders.
        /// </summary>
        public DbSet<Order> Orders { get; set; }

        /// <summary>
        /// Gets or sets the items included in orders.
        /// </summary>
        public DbSet<OrderItem> OrderItems { get; set; }

        /// <summary>
        /// Configures relationships and database constraints for the entities.
        /// </summary>
        /// <param name="modelBuilder">The model builder used for configuration.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Order → OrderItems (One-to-Many)
            _ = modelBuilder.Entity<Order>()
                .HasMany(o => o.Items)
                .WithOne(i => i.Order)
                .HasForeignKey(i => i.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // CartItem → Product (Many-to-One)
            _ = modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Product)
                .WithMany()
                .HasForeignKey(ci => ci.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // CartItem → AuthUser (Many-to-One)
            _ = modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.AuthUser)
                .WithMany()
                .HasForeignKey(ci => ci.AuthUserId)
                .OnDelete(DeleteBehavior.Cascade);

            // UserProfile → AuthUser (One-to-One)
            _ = modelBuilder.Entity<UserProfile>()
                .HasOne(up => up.AuthUser)
                .WithOne()
                .HasForeignKey<UserProfile>(up => up.AuthUserId)
                .OnDelete(DeleteBehavior.Cascade);

            // RefreshToken → AuthUser (Many-to-One)
            _ = modelBuilder.Entity<RefreshToken>()
                .HasOne(rt => rt.AuthUser)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(rt => rt.AuthUserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Ensure uniqueness of refresh token strings
            _ = modelBuilder.Entity<RefreshToken>()
                .HasIndex(rt => rt.Token)
                .IsUnique();
        }
    }
}
