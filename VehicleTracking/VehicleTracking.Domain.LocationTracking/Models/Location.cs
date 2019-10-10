using System;
using VehicleTracking.Common.Data;

namespace VehicleTracking.Domain.LocationTracking.Models
{
    public class Location : BaseModel
    {
        public Guid SessionId { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public DateTimeOffset TrackingTime { get; set; }
    }
}
