using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VehicalTracking.Service.Models;
using VehicalTracking.Service.User;

namespace VehicalTracking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] SignUpModel signUpModel)
        {
            var response = await _userService.SignUp(signUpModel);
            return Ok(response);
        }
    }
}
