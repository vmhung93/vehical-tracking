using Microsoft.Extensions.DependencyInjection;
using VehicalTracking.Service.User;
using VehicalTracking.Service.Vehicle;
using VehicleTracking.Domain.Vehicle.CommandHandlers.Vehicle;

namespace VehicalTracking.Service
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
