using System.Threading.Tasks;
using VehicleTracking.Domain.Vehicle.CommandHandlers.Vehicle;

namespace VehicleTracking.Service.Vehicle
{
    public interface IVehicleService
    {
        Task RegisterVehicle(RegisterVehicleCommand comand);
    }
}
