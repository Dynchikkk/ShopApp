namespace ShopApp.Core.Dto.Auth
{
    /// <summary>
    /// Data transfer object representing a pair of JWT access and refresh tokens.
    /// </summary>
    public class TokenResponseDto
    {
        /// <summary>
        /// The generated JWT access token used for authenticated requests.
        /// </summary>
        public string AccessToken { get; set; } = null!;

        /// <summary>
        /// The generated refresh token used to obtain a new access token after expiration.
        /// </summary>
        public string RefreshToken { get; set; } = null!;
    }
}
