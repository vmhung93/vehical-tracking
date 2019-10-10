using System;
using VehicleTracking.Common.Data;

namespace VehicleTracking.Domain.LocationTracking.Models
{
    public class Session : BaseModel
    {
        public Guid VehicleId { get; set; }

        public DateTimeOffset StartTime { get; set; }

        public DateTimeOffset? EndTime { get; set; }
    }
}
