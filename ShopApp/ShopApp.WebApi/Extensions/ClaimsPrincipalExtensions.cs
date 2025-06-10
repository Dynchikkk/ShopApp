using System.Security.Claims;

namespace ShopApp.WebApi.Extensions
{
    /// <summary>
    /// <see cref="ClaimsPrincipal"/> Extensions
    /// </summary>
    public static class ClaimsPrincipalExtensions
    {
        /// <summary>
        /// Get user id form claims
        /// </summary>
        /// <param name="user"><see cref="ClaimsPrincipal"/> self object</param>
        /// <returns>User id if has, else - -1</returns>
        public static int GetUserId(this ClaimsPrincipal user)
        {
            string? idClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.TryParse(idClaim, out int userId) ? userId : -1;
        }
    }
}
