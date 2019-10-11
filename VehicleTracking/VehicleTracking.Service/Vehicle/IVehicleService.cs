using System.Threading.Tasks;
using VehicleTracking.Domain.Vehicle.CommandHandlers;
using VehicleTracking.Domain.Vehicle.Queries;

namespace VehicleTracking.Service.Vehicle
{
    public interface IVehicleService
    {
        Task<VehicleViewModel> GetVehicleByCode(string code);

        Task RegisterVehicle(RegisterVehicleCommand command);

        Task ResignVehicle(ResignVehicleCommand command);
    }
}
