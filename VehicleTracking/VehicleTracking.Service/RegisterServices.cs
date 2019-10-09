using Microsoft.Extensions.DependencyInjection;
using VehicleTracking.Service.User;
using VehicleTracking.Service.Vehicle;
using VehicleTracking.Domain.Vehicle.CommandHandlers.Vehicle;

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

            // Queries
        }
    }
}
