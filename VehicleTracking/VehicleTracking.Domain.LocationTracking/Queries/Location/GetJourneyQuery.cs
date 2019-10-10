using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleTracking.Common.Exceptions;
using VehicleTracking.Common.Query;
using VehicleTracking.Domain.LocationTracking.Infrastructure;

namespace VehicleTracking.Domain.LocationTracking.Queries
{
    public class GetJourneyQueryViewMode
    {
        public Guid VehicleId { get; set; }

        public DateTimeOffset StartTime { get; set; }

        public DateTimeOffset EndTime { get; set; }
    }

    public class GetJourneyQuery : IQuery<GetJourneyQueryViewMode, Task<List<LocationViewModel>>>
    {
        private readonly LocationTrackingContext _context;
        private readonly ILogger<GetJourneyQuery> _logger;

        public GetJourneyQuery(LocationTrackingContext context, ILogger<GetJourneyQuery> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<LocationViewModel>> Execute(GetJourneyQueryViewMode parameters)
        {
            var locations = await (from session in _context.Sessions
                                   join _location in _context.Locations on session.Id equals _location.SessionId
                                   where session.VehicleId == parameters.VehicleId
                                     && _location.TrackingTime >= parameters.StartTime
                                     && _location.TrackingTime <= parameters.EndTime
                                   orderby _location.TrackingTime
                                   select new LocationViewModel()
                                   {
                                       Id = _location.Id,
                                       Latitude = _location.Latitude,
                                       Longitude = _location.Longitude,
                                       SessionId = _location.SessionId,
                                       TrackingTime = _location.TrackingTime
                                   }).ToListAsync();

            if (!locations.Any())
            {
                throw new CustomException(ErrorCodes.EC_Location_002, parameters.VehicleId);
            }

            return locations;
        }
    }
}
