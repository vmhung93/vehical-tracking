using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VehicleTracking.Common.Constants;
using VehicleTracking.Common.Helpers;
using VehicleTracking.Domain.Vehicle.CommandHandlers;
using VehicleTracking.Service.Vehicle;

namespace VehicalTracking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoles.User)]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return BadRequest();
            }

            // Get vehicle by code
            var vehicle = await _vehicleService.GetVehicleByCode(code);

            return Ok(vehicle);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterVehicleCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            // Register vehicle
            await _vehicleService.RegisterVehicle(command);

            return Ok();
        }

        [HttpPost]
        [Route("resign")]
        public async Task<IActionResult> Resign([FromBody] ResignVehicleCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            // Delete vehicle
            await _vehicleService.ResignVehicle(command);

            return Ok();
        }
    }
}
