using System;
using VehicleTracking.Common.Data;

namespace VehicleTracking.Domain.LocationTracking.Models
{
    public class SessionTracking : BaseModel
    {
        public Guid UserId { get; set; }

        public Guid VehicleId { get; set; }
    }
}
