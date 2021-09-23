using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IdentityMongo.Services.UserServices;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityMongo.Services.AuthServices;
using IdentityMongo.Dtos.AuthDtos;

namespace IdentityMongo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IAuthService authService;

        public AuthController(IUserService userService, IAuthService authService)
        {
            this.userService = userService;
            this.authService = authService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(Dtos.UserDto userDto)
        {
            var user = await userService.CreateUserAsync(userDto);
            return Ok(user);
        }

        [HttpPost("Login")]

        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var result = await authService.Login(loginDto);
            return Ok(result);
        }

        [HttpPost("create/roll")]
        public async Task<IActionResult> CreateRoll(RoleDto roleDto)
        {
            var role = await authService.CreateRole(roleDto);
            return Ok(role);
        }
    }
}
