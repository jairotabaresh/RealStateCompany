using Microsoft.AspNetCore.Mvc;
using RealStateCompanyApplication.Data;
using RealStateCompanyApplication.Services.Contracts;

namespace RealStateCompanyApplication.Controllers
{
    /// <summary>
    /// Controller or entry point that handles the security of other Endpoints
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class SecurityController : Controller
    {
        private readonly ISecurityService _securityService;

        public SecurityController(ISecurityService securityService)
        {
            _securityService = securityService;
        }

        /// <summary>
        /// Determines whether the token can be obtained for the respective user
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetJwtToken")]
        public async Task<IActionResult> GetJwtToken(UserDto userDto)
        {
            var response = await _securityService.GetJwtToken(userDto);
            return response != null ? Ok(response) : NotFound("User not found");
        }
        
        
    }
}