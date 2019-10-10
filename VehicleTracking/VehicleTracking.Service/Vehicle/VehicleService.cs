using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using VehicleTracking.Common.Helpers;
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
            // Get current user id
            var userId = HttpContextHelper.GetCurrentUserId();

            // Get vehicle by code
            var getVehicleByCodeQuery = _serviceProvider.GetRequiredService<GetUserVehicleQuery>();
            return await getVehicleByCodeQuery.Execute(new GetUserVehicleViewModel()
            {
                UserId = userId,
                Code = code
            });
        }

        public async Task RegisterVehicle(RegisterVehicleCommand command)
        {
            // Get current user id
            var userId = HttpContextHelper.GetCurrentUserId();
            
            // Set user id
            command.UserId = userId;

            var registerVehicleCommandHandler = _serviceProvider.GetRequiredService<RegisterVehicleCommandHandler>();
            await registerVehicleCommandHandler.Handle(command);
        }

        public async Task ResignVehicle(ResignVehicleCommand command)
        {
            // Get current user id
            var userId = HttpContextHelper.GetCurrentUserId();
            
            // Set user id
            command.UserId = userId;

            var resignVehicleCommandHandler = _serviceProvider.GetRequiredService<ResignVehicleCommandHandler>();
            await resignVehicleCommandHandler.Handle(command);
        }
    }
}
