using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using VehicleTracking.Domain.Vehicle.CommandHandlers;
using VehicleTracking.Domain.Vehicle.Queries;

namespace VehicleTracking.Service.Vehicle
{
    public class VehicleService : IVehicleService
    {
        private readonly IServiceProvider _serviceProvider;

        public VehicleService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<VehicleViewModel> GetVehicleByCode(string code)
        {
            var getVehicleByCodeQuery = _serviceProvider.GetRequiredService<GetVehicleByCodeQuery>();
            return await getVehicleByCodeQuery.Execute(code);
        }

        public async Task RegisterVehicle(RegisterVehicleCommand command)
        {
            var registerVehicleCommandHandler = _serviceProvider.GetRequiredService<RegisterVehicleCommandHandler>();
            await registerVehicleCommandHandler.Handle(command);
        }

        public async Task ResignVehicle(ResignVehicleCommand command)
        {
            var resignVehicleCommandHandler = _serviceProvider.GetRequiredService<ResignVehicleCommandHandler>();
            await resignVehicleCommandHandler.Handle(command);
        }
    }
}
