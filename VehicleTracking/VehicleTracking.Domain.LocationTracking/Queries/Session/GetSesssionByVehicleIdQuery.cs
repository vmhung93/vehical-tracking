using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using VehicleTracking.Common.Exceptions;
using VehicleTracking.Common.Query;
using VehicleTracking.Domain.LocationTracking.Infrastructure;

namespace VehicleTracking.Domain.LocationTracking.Queries
{
    public class GetSesssionByVehicleIdQuery : IQuery<Guid, Task<SessionViewModel>>
    {
        private readonly LocationTrackingContext _context;
        private readonly ILogger<GetSesssionByVehicleIdQuery> _logger;

        public GetSesssionByVehicleIdQuery(LocationTrackingContext context, ILogger<GetSesssionByVehicleIdQuery> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<SessionViewModel> Execute(Guid vehicleId)
        {
            var session = await _context.Sessions
                .Where(s => s.VehicleId == vehicleId && !s.EndTime.HasValue)
                .FirstOrDefaultAsync();

            if (session == null)
            {
                throw new CustomException(ErrorCodes.EC_Session_002, vehicleId);
            }

            return new SessionViewModel()
            {
                Id = session.Id,
                VehicleId = session.VehicleId,
                StartTime = session.StartTime,
                EndTime = session.EndTime
            };
        }
    }
}
