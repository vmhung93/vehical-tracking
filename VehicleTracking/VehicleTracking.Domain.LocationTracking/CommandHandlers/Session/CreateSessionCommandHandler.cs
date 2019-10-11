using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VehicleTracking.Common.Command;
using VehicleTracking.Domain.LocationTracking.Infrastructure;
using VehicleTracking.Domain.LocationTracking.Models;

namespace VehicleTracking.Domain.LocationTracking.CommandHandlers
{
    public class CreateSessionCommand : ICommand
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        [Required]
        public Guid VehicleId { get; set; }
    }

    public class CreateSessionCommandHandler : ICommandHandler<CreateSessionCommand, Task>
    {
        private readonly LocationTrackingContext _context;
        private readonly ILogger<CreateSessionCommandHandler> _logger;

        public CreateSessionCommandHandler(LocationTrackingContext context, ILogger<CreateSessionCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Handle(CreateSessionCommand command)
        {
            // Get uncompleted session of this vehicle
            // Just make sure there is only on session in progressing
            var uncompltedSession = await _context.Sessions.Where(s => s.VehicleId == command.VehicleId && !s.EndTime.HasValue).ToListAsync();

            if (uncompltedSession.Any())
            {
                foreach (var item in uncompltedSession)
                {
                    item.EndTime = DateTime.UtcNow;
                }
            }

            var session = new Session()
            {
                Id = command.Id,
                VehicleId = command.VehicleId,
                StartTime = DateTime.UtcNow
            };

            _context.Sessions.Add(session);
            _logger.LogInformation($"CreateSessionCommandHandler - Session Id: {command.Id}");

            await _context.SaveChangesAsync();
        }
    }
}
