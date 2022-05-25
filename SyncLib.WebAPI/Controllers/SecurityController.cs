using Microsoft.AspNetCore.Mvc;
using SyncLib.BusinessLogic.Interface;
using SyncLib.Model.Common;
using SyncLib.Model.Dto;

namespace SyncLib.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SecurityController : BaseController
    {
        private readonly IApplicationUserService _userService;
        public SecurityController(IApplicationUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Authenticate")]
        public async Task<ApiResponseModel> Authenticate([FromBody] LoginRequestModel login)
        {
            var user = await _userService.GetLogin(login);
            return GetResult(user);
        }

    }
}
