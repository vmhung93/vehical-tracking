using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using VehicalTracking.Domain.ApplicationUser.Infrastructure;
using VehicleTracking.Common.Command;

namespace VehicleTracking.Domain.Vehicle.CommandHandlers.Vehicle
{
    public class RegisterVehicleCommand : ICommand
    {
        public Guid UserId { get; set; }

        [Required]
        public string Code { get; set; }
    }

    public class RegisterVehicleCommandHandler : ICommandHandler<RegisterVehicleCommand, Task>
    {
        private readonly VehicleContext _context;
        private readonly ILogger<RegisterVehicleCommandHandler> _logger;

        public RegisterVehicleCommandHandler(VehicleContext context, ILogger<RegisterVehicleCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Handle(RegisterVehicleCommand command)
        {
            var vehicle = new Models.Vehicle()
            {
                UserId = command.UserId,
                Code = command.Code,
                IsActive = true
            };

            _context.Vehicles.Add(vehicle);
            _logger.LogInformation($"RegisterVehicleCommandHandler - Register vehicle - Id: {vehicle.Id}");

            await _context.SaveChangesAsync();
        }
    }
}
