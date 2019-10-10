using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VehicleTracking.Service.Models;
using VehicleTracking.Service.User;

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
        [Route("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] SignInModel signInModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var response = await _userService.SignIn(signInModel);
            return Ok(new { token = response });
        }

        [HttpPost]
        [Route("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] SignUpModel signUpModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var response = await _userService.SignUp(signUpModel);
            return Ok(new { token = response });
        }
    }
}
