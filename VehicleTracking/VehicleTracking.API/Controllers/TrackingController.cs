using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VehicleTracking.Common.Constants;
using VehicleTracking.Service.Models;
using VehicleTracking.Service.Tracking;

namespace VehicalTracking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrackingController : ControllerBase
    {
        private readonly ITrackingService _trackingService;

        public TrackingController(ITrackingService trackingService)
        {
            _trackingService = trackingService;
        }

        [HttpGet]
        [Route("start")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> StartTracking(string code)
        {
            // Start tracking
            await _trackingService.StartTracking(code);

            return Ok();
        }

        [HttpGet]
        [Route("stop")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> StopTracking(string code)
        {
            // Stop tracking
            await _trackingService.StopTracking(code);

            return Ok();
        }

        [HttpPost]
        [Route("update-location")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> UpdateLocation([FromBody] UpdateLocationModel model)
        {
            // Stop tracking
            await _trackingService.UpdateLocation(model);

            return Ok();
        }

        [HttpGet]
        [Route("get-current-location")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> GetCurrentLocation(string code)
        {
            // Get location
            var currentLocation = await _trackingService.GetCurrentLocation(code);

            return Ok(currentLocation);
        }

        [HttpPost]
        [Route("get-journey")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> GetJourney([FromBody] GetJourneyModel model)
        {
            // Get journey
            var locations = await _trackingService.GetJourney(model);

            return Ok(locations);
        }
    }
}
