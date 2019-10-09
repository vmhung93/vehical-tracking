using Microsoft.Extensions.DependencyInjection;
using VehicleTracking.Domain.Vehicle.CommandHandlers;
using VehicleTracking.Domain.Vehicle.Queries;
using VehicleTracking.Service.User;
using VehicleTracking.Service.Vehicle;

namespace VehicleTracking.Service
{
    public static class ServiceDependencies
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // Dependencies
            services.VehicleDependencies();

            // Register services
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IVehicleService, VehicleService>();
        }

        private static void VehicleDependencies(this IServiceCollection services)
        {
            // Command handlers
            services.AddTransient<RegisterVehicleCommandHandler, RegisterVehicleCommandHandler>();
            services.AddTransient<ResignVehicleCommandHandler, ResignVehicleCommandHandler>();

            // Queries
            services.AddTransient<GetVehicleByCodeQuery, GetVehicleByCodeQuery>();
        }
    }
}
