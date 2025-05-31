namespace ShopApp.Core.Dto.Auth
{
    public class RefreshTokenRequestDto
    {
        public int UserId { get; set; }
        public string RefreshToken { get; set; } = null!;
    }
}
