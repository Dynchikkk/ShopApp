using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Core.Dto;
using ShopApp.Core.Models.User;
using ShopApp.Core.Services;
using System.Security.Claims;

namespace ShopApp.WebApi.Controllers
{
    /// <summary>
    /// API controller for managing the authenticated user's profile.
    /// Accessible only to users with the Client role.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = nameof(UserRole.Client))]
    public class ProfileController : ControllerBase
    {
        private readonly IUserProfileService _profileService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileController"/> class.
        /// </summary>
        /// <param name="profileService">Service for accessing and modifying user profiles.</param>
        public ProfileController(IUserProfileService profileService)
        {
            _profileService = profileService;
        }

        /// <summary>
        /// Retrieves the profile of the currently authenticated user.
        /// </summary>
        /// <returns>The user's profile data, or 404 if not found.</returns>
        [HttpGet]
        public async Task<ActionResult<UserProfileDto>> GetProfile()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            UserProfile? profile = await _profileService.GetProfileAsync(userId);
            return profile == null
                ? (ActionResult<UserProfileDto>)NotFound("Profile not found.")
                : (ActionResult<UserProfileDto>)Ok(new UserProfileDto
                {
                    FullName = profile.FullName,
                    Address = profile.Address,
                    Phone = profile.Phone
                });
        }

        /// <summary>
        /// Updates the profile information of the currently authenticated user.
        /// </summary>
        /// <param name="dto">The updated profile data.</param>
        /// <returns>The updated profile, or 404 if not found.</returns>
        [HttpPut]
        public async Task<ActionResult<UserProfileDto>> UpdateProfile([FromBody] UserProfileDto dto)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            UserProfile? updated = await _profileService.UpdateProfileAsync(userId, new()
            {
                FullName = dto.FullName,
                Address = dto.Address,
                Phone = dto.Phone
            });

            return updated == null
                ? (ActionResult<UserProfileDto>)NotFound("Profile not found or could not be updated.")
                : (ActionResult<UserProfileDto>)Ok(new UserProfileDto
                {
                    FullName = updated.FullName,
                    Address = updated.Address,
                    Phone = updated.Phone
                });
        }
    }
}
