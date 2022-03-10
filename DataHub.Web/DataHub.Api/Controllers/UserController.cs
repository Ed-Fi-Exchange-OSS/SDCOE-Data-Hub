using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataHub.Api.Services;
using DataHub.Api.Data.Models;

namespace DataHub.Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IOrganizationContextProvider _organizationContextProvider;

        public UserController(IUserService userService, IOrganizationContextProvider organizationContextProvider)
        {
            _userService = userService;
            _organizationContextProvider = organizationContextProvider;
        }
        
        [Route("me")]
        public async Task<ActionResult<UserDto>> GetMe()
        {
            return Ok(await _organizationContextProvider.GetUser());
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<UserDto>> GetUser(int userId)
        {
            return Ok(await _userService.GetUser(userId));
        }
    }
}
