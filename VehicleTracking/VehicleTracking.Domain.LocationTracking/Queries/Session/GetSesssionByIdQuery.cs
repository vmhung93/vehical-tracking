using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using VehicleTracking.Common.Exceptions;
using VehicleTracking.Common.Query;
using VehicleTracking.Common.ViewModels;
using VehicleTracking.Domain.LocationTracking.Infrastructure;

namespace VehicleTracking.Domain.LocationTracking.Queries
{
    public class SessionViewModel : BaseViewModel
    {
        public Guid VehicleId { get; set; }

        public DateTimeOffset StartTime { get; set; }

        public DateTimeOffset? EndTime { get; set; }
    }

    public class GetSesssionByIdQuery : IQuery<Guid, Task<SessionViewModel>>
    {
        private readonly LocationTrackingContext _context;
        private readonly ILogger<GetSesssionByIdQuery> _logger;

        public GetSesssionByIdQuery(LocationTrackingContext context, ILogger<GetSesssionByIdQuery> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<SessionViewModel> Execute(Guid sessionId)
        {
            var session = await _context.Sessions.FindAsync(sessionId);

            if (session == null)
            {
                throw new CustomException(ErrorCodes.EC_Session_001, sessionId);
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
