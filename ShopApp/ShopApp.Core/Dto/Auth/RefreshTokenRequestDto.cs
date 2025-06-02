namespace ShopApp.Core.Dto.Auth
{
    /// <summary>
    /// Data transfer object for requesting a new access token using a refresh token.
    /// </summary>
    public class RefreshTokenRequestDto
    {
        /// <summary>
        /// The identifier of the user associated with the refresh token.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// The refresh token to validate and exchange for a new access token.
        /// </summary>
        public string RefreshToken { get; set; } = null!;
    }
}
