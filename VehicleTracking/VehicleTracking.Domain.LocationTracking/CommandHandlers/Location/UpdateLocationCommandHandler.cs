using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using VehicleTracking.Common.Command;
using VehicleTracking.Domain.LocationTracking.Infrastructure;
using VehicleTracking.Domain.LocationTracking.Models;

namespace VehicleTracking.Domain.LocationTracking.CommandHandlers
{
    public class UpdateLocationCommand : ICommand
    {
        public Guid SessionId { get; set; }

        public decimal Longitude { get; set; }

        public decimal Latitude { get; set; }
    }

    public class UpdateLocationCommandHandler : ICommandHandler<UpdateLocationCommand, Task>
    {
        private readonly LocationTrackingContext _context;
        private readonly ILogger<UpdateLocationCommandHandler> _logger;

        public UpdateLocationCommandHandler(LocationTrackingContext context, ILogger<UpdateLocationCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Handle(UpdateLocationCommand command)
        {
            var location = new Location()
            {
                SessionId = command.SessionId,
                Longitude = command.Longitude,
                Latitude = command.Latitude,
                TrackingTime = DateTime.UtcNow
            };

            _context.Locations.Add(location);
            _logger.LogInformation($"UpdateLocationCommandHandler - Session Id: {command.SessionId}");

            await _context.SaveChangesAsync();
        }
    }
}
