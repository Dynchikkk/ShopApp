namespace ShopApp.WebApi.Services.JwtAuth
{
    /// <summary>
    /// Represents the JWT configuration settings used for token generation and validation.
    /// </summary>
    public class JwtSettings
    {
        /// <summary>
        /// The secret key used to sign JWT tokens.
        /// </summary>
        public string Key { get; set; } = null!;

        /// <summary>
        /// The issuer of the JWT token, typically your application's URL or name.
        /// </summary>
        public string Issuer { get; set; } = null!;

        /// <summary>
        /// The audience that the JWT token is intended for.
        /// </summary>
        public string Audience { get; set; } = null!;

        /// <summary>
        /// The number of days after which the JWT token will expire.
        /// </summary>
        public int ExpireDays { get; set; }
    }
}
