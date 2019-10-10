using Microsoft.Extensions.DependencyInjection;
using VehicleTracking.Domain.LocationTracking.CommandHandlers;
using VehicleTracking.Domain.LocationTracking.Queries;
using VehicleTracking.Domain.Vehicle.CommandHandlers;
using VehicleTracking.Domain.Vehicle.Queries;
using VehicleTracking.Service.Cache;
using VehicleTracking.Service.Tracking;
using VehicleTracking.Service.User;
using VehicleTracking.Service.Vehicle;

namespace VehicleTracking.Service
{
    public static class ServiceDependencies
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // Dependencies
            services.TrackingDependencies();
            services.VehicleDependencies();

            // Register services
            services.AddTransient<ICacheService, CacheService>();

            services.AddTransient<ITrackingService, TrackingService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IVehicleService, VehicleService>();
        }

        private static void VehicleDependencies(this IServiceCollection services)
        {
            // Command handlers
            services.AddTransient<RegisterVehicleCommandHandler, RegisterVehicleCommandHandler>();
            services.AddTransient<ResignVehicleCommandHandler, ResignVehicleCommandHandler>();

            // Queries
            services.AddTransient<GetUserVehicleQuery, GetUserVehicleQuery>();
            services.AddTransient<GetVehicleByCodeQuery, GetVehicleByCodeQuery>();
        }

        private static void TrackingDependencies(this IServiceCollection services)
        {
            // Command handlers
            services.AddTransient<UpdateLocationCommandHandler, UpdateLocationCommandHandler>();

            services.AddTransient<CreateSessionCommandHandler, CreateSessionCommandHandler>();
            services.AddTransient<StopSessionCommandHandler, StopSessionCommandHandler>();

            // Queries
            services.AddTransient<GetCurrentLocationQuery, GetCurrentLocationQuery>();
            services.AddTransient<GetJourneyQuery, GetJourneyQuery>();

            services.AddTransient<GetSesssionByIdQuery, GetSesssionByIdQuery>();
            services.AddTransient<GetSesssionByVehicleIdQuery, GetSesssionByVehicleIdQuery>();
        }
    }
}
