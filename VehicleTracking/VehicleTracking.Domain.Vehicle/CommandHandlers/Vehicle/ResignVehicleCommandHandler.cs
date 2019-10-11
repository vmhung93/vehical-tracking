using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VehicalTracking.Domain.ApplicationUser.Infrastructure;
using VehicleTracking.Common.Command;
using VehicleTracking.Common.Exceptions;

namespace VehicleTracking.Domain.Vehicle.CommandHandlers
{
    public class ResignVehicleCommand : ICommand
    {
        public Guid UserId { get; set; }

        [Required]
        public string Code { get; set; }
    }

    public class ResignVehicleCommandHandler : ICommandHandler<ResignVehicleCommand, Task>
    {
        private readonly VehicleContext _context;
        private readonly ILogger<ResignVehicleCommandHandler> _logger;

        public ResignVehicleCommandHandler(VehicleContext context, ILogger<ResignVehicleCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Handle(ResignVehicleCommand command)
        {
            var vehicle = await _context.Vehicles
                .Where(v => v.UserId == command.UserId && v.Code == command.Code)
                .FirstOrDefaultAsync();

            if (vehicle == null)
            {
                throw new CustomException(ErrorCodes.EC_Vehicle_001, command.Code);
            }

            // Update vehicle
            vehicle.IsActive = false;

            _logger.LogInformation($"ResignVehicleCommandHandler - Resign vehicle - Id: {vehicle.Id}");

            await _context.SaveChangesAsync();
        }
    }
}
