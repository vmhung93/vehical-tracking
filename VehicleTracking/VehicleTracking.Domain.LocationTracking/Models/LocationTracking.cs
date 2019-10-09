using System;
using VehicleTracking.Common.Data;

namespace VehicleTracking.Domain.LocationTracking.Models
{
    public class LocationTracking : BaseModel
    {
        public Guid VehicleId { get; set; }

        public int Latitude { get; set; }
        
        public int Longitude { get; set; }

        public DateTimeOffset TrackingTime{ get; set; }
    }
}
