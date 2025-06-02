using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShopApp.WebApi.Controllers
{
    /// <summary>
    /// Basic controller for health check and base routing.
    /// </summary>
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        /// <summary>
        /// Base endpoint for verifying API is online.
        /// </summary>
        /// <returns>API welcome message.</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            return Ok(new
            {
                message = "Welcome to ShopApp API",
                version = "v1"
            });
        }
    }
}
