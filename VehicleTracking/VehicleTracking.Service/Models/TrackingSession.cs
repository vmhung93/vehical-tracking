using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracking.Service.Models
{
    public class TrackingSession
    {
        public Guid SessionId { get; set; }

        public Guid VehicleId { get; set; }
    }
}
