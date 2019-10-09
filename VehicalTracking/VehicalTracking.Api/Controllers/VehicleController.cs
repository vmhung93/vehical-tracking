﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VehicalTracking.Common.Constants;
using VehicalTracking.Service.Vehicle;
using VehicleTracking.Domain.Vehicle.CommandHandlers.Vehicle;

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
    }
}
