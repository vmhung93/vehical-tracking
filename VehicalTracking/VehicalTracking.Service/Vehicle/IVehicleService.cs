using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Domain.Vehicle.CommandHandlers.Vehicle;

namespace VehicalTracking.Service.Vehicle
{
    public interface IVehicleService
    {
        Task RegisterVehicle(RegisterVehicleCommand comand);
    }
}
