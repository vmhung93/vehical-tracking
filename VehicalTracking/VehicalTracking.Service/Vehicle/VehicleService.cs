using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using VehicleTracking.Domain.Vehicle.CommandHandlers.Vehicle;

namespace VehicalTracking.Service.Vehicle
{
    public class VehicleService : IVehicleService
    {
        private readonly IServiceProvider _serviceProvider;

        public VehicleService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task RegisterVehicle(RegisterVehicleCommand command)
        {
            var registerVehicleCommandHandler = _serviceProvider.GetRequiredService<RegisterVehicleCommandHandler>();
            await registerVehicleCommandHandler.Handle(command);
        }
    }
}
