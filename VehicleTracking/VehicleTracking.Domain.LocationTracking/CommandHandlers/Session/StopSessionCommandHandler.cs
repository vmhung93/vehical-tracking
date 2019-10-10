using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using VehicleTracking.Common.Command;
using VehicleTracking.Common.Exceptions;
using VehicleTracking.Domain.LocationTracking.Infrastructure;

namespace VehicleTracking.Domain.LocationTracking.CommandHandlers
{
    public class StopSessionCommandHandler : ICommandHandler<Guid, Task>
    {
        private readonly LocationTrackingContext _context;
        private readonly ILogger<StopSessionCommandHandler> _logger;

        public StopSessionCommandHandler(LocationTrackingContext context, ILogger<StopSessionCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Handle(Guid sessionId)
        {
            var session = await _context.Sessions.FindAsync(sessionId);

            if (session == null)
            {
                throw new CustomException(ErrorCodes.EC_Session_001, sessionId);
            }

            // Update end time for this session
            session.EndTime = DateTime.UtcNow;

            _logger.LogInformation($"StopSessionCommandHandler - Session Id: {sessionId}");

            await _context.SaveChangesAsync();
        }
    }
}
