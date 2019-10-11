using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleTracking.Common.Constants;
using VehicleTracking.Common.Exceptions;
using VehicleTracking.Common.Helpers;
using VehicleTracking.Domain.LocationTracking.CommandHandlers;
using VehicleTracking.Domain.LocationTracking.Queries;
using VehicleTracking.Domain.Vehicle.Queries;
using VehicleTracking.Service.Cache;
using VehicleTracking.Service.Models;

namespace VehicleTracking.Service.Tracking
{
    public class TrackingService : ITrackingService
    {
        private readonly ICacheService _cacheService;
        private readonly IServiceProvider _serviceProvider;

        public TrackingService(ICacheService cacheProvider, IServiceProvider serviceProvider)
        {
            _cacheService = cacheProvider;
            _serviceProvider = serviceProvider;
        }

        public async Task<LocationViewModel> GetCurrentLocation(string code)
        {
            // Get vehicle by code
            var getVehicleByCodeQuery = _serviceProvider.GetRequiredService<GetVehicleByCodeQuery>();
            var vehicle = await getVehicleByCodeQuery.Execute(code);

            // Get current location
            var getCurrentLocationQuery = _serviceProvider.GetRequiredService<GetCurrentLocationQuery>();
            return await getCurrentLocationQuery.Execute(vehicle.Id);
        }

        public async Task<List<LocationViewModel>> GetJourney(GetJourneyModel model)
        {
            // Get vehicle by code
            var getVehicleByCodeQuery = _serviceProvider.GetRequiredService<GetVehicleByCodeQuery>();
            var vehicle = await getVehicleByCodeQuery.Execute(model.Code);

            // Get current location
            var getJourneyQuery = _serviceProvider.GetRequiredService<GetJourneyQuery>();
            return await getJourneyQuery.Execute(new GetJourneyQueryViewMode()
            {
                VehicleId = vehicle.Id,
                StartTime = model.StartTime,
                EndTime = model.EndTime
            });
        }

        public async Task StartTracking(string code)
        {
            // Get current user id
            var userId = HttpContextHelper.GetCurrentUserId();

            // Get vehicle by code
            var getUserVehicleQuery = _serviceProvider.GetRequiredService<GetUserVehicleQuery>();
            var vehicle = await getUserVehicleQuery.Execute(new GetUserVehicleViewModel()
            {
                UserId = userId,
                Code = code
            });

            var command = new CreateSessionCommand()
            {
                Id = Guid.NewGuid(),
                UserId = vehicle.UserId,
                VehicleId = vehicle.Id
            };

            // Create new session
            var createSessionCommandHandler = _serviceProvider.GetRequiredService<CreateSessionCommandHandler>();
            await createSessionCommandHandler.Handle(command);

            // Store this session to cache
            await _cacheService.Store<TrackingSession>(Schema.SESSION_SCHEMA, $"{vehicle.UserId.ToString()}_{vehicle.Code}",
                new TrackingSession()
                {
                    SessionId = command.Id,
                    VehicleId = command.VehicleId
                });
        }

        public async Task StopTracking(string code)
        {
            Guid sessionId;

            // Get current user id
            var userId = HttpContextHelper.GetCurrentUserId();

            // Remove this session from Redis cache
            var trackingSession = await _cacheService.GetAndRemove<TrackingSession>(Schema.SESSION_SCHEMA, $"{userId.ToString()}_{code}");

            if (trackingSession != null)
            {
                // Session id
                sessionId = trackingSession.SessionId;
            }
            else
            {
                // Get vehicle by code
                var getUserVehicleQuery = _serviceProvider.GetRequiredService<GetUserVehicleQuery>();
                var vehicle = await getUserVehicleQuery.Execute(new GetUserVehicleViewModel()
                {
                    UserId = userId,
                    Code = code
                });

                // Get session by vehicle id
                var getSesssionByVehicleIdQuery = _serviceProvider.GetRequiredService<GetSesssionByVehicleIdQuery>();
                var session = await getSesssionByVehicleIdQuery.Execute(vehicle.Id);

                // Session id
                sessionId = session.Id;
            }

            if (sessionId != Guid.Empty)
            {
                // Stop session 
                var stopSessionCommandHandler = _serviceProvider.GetRequiredService<StopSessionCommandHandler>();
                await stopSessionCommandHandler.Handle(sessionId);
            }
        }

        public async Task UpdateLocation(UpdateLocationModel model)
        {
            // Get current user id
            var userId = HttpContextHelper.GetCurrentUserId();

            // Get tracking session from Redis cache
            var session = await _cacheService.Get<TrackingSession>(Schema.SESSION_SCHEMA, $"{userId.ToString()}_{model.Code}");

            if (session == null)
            {
                throw new CustomException(ErrorCodes.EC_Session_002, model.Code);
            }

            // Update location
            var updateLocationCommandHandler = _serviceProvider.GetRequiredService<UpdateLocationCommandHandler>();
            await updateLocationCommandHandler.Handle(new UpdateLocationCommand()
            {
                SessionId = session.SessionId,
                Longitude = model.Longitude,
                Latitude = model.Latitude
            });
        }
    }
}
