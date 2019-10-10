using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using VehicleTracking.Common.Exceptions;
using VehicleTracking.Common.Query;
using VehicleTracking.Common.ViewModels;
using VehicleTracking.Domain.LocationTracking.Infrastructure;

namespace VehicleTracking.Domain.LocationTracking.Queries
{
    public class LocationViewModel : BaseViewModel
    {
        public Guid SessionId { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public DateTimeOffset TrackingTime { get; set; }
    }

    public class GetCurrentLocationQuery : IQuery<Guid, Task<LocationViewModel>>
    {
        private readonly LocationTrackingContext _context;
        private readonly ILogger<GetCurrentLocationQuery> _logger;

        public GetCurrentLocationQuery(LocationTrackingContext context, ILogger<GetCurrentLocationQuery> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<LocationViewModel> Execute(Guid vehicleId)
        {
            var location = await (from session in _context.Sessions
                                  join _location in _context.Locations on session.Id equals _location.SessionId
                                  where session.VehicleId == vehicleId
                                  orderby _location.TrackingTime descending
                                  select _location).FirstOrDefaultAsync();

            if (location == null)
            {
                throw new CustomException(ErrorCodes.EC_Location_001, vehicleId);
            }

            return new LocationViewModel()
            {
                Id = location.Id,
                SessionId = location.SessionId,
                Latitude = location.Latitude,
                Longitude = location.Longitude,
                TrackingTime = location.TrackingTime
            };
        }
    }
}
