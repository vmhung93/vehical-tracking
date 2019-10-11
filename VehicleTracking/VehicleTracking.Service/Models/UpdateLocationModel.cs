using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracking.Service.Models
{
    public class UpdateLocationModel
    {
        public string Code { get; set; }

        public decimal Longitude { get; set; }

        public decimal Latitude { get; set; }
    }
}
