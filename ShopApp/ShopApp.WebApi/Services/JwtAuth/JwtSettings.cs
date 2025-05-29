namespace ShopApp.WebApi.Services.JwtAuth
{
    /// <summary>
    /// Settings for configuring JWT tokens.
    /// </summary>
    public class JwtSettings
    {
        /// <summary>
        /// Secret key used for signing JWT tokens.
        /// </summary>
        public required string Key { get; set; }

        /// <summary>
        /// Token issuer (who issued the token).
        /// </summary>
        public required string Issuer { get; set; }
        /// <summary>
        /// Token audience (who the token is intended for).
        /// </summary>
        public required string Audience { get; set; }

        /// <summary>
        /// Lifetime of the access token in minutes.
        /// </summary>
        public int AccessTokenLifetimeMinutes { get; set; }
        /// <summary>
        /// Lifetime of the refresh token in minutes.
        /// </summary>
        public int RefreshTokenLifetimeMinutes { get; set; }
    }
}
