using System;

namespace VehicleTracking.Service.Models
{
    public class GetJourneyModel
    {
        public string Code { get; set; }

        public DateTimeOffset StartTime { get; set; }

        public DateTimeOffset EndTime { get; set; }
    }
}
