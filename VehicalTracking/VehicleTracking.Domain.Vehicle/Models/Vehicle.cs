using System;
using VehicalTracking.Common.Data;

namespace VehicleTracking.Domain.Vehicle.Models
{
    public class Vehicle : BaseModel
    {
        public Guid UserId { get; set; }

        public string Code { get; set; }

        public bool IsActive { get; set; }
    }
}
