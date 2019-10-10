using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleTracking.Domain.LocationTracking.Queries;
using VehicleTracking.Service.Models;

namespace VehicleTracking.Service.Tracking
{
    public interface ITrackingService
    {
        Task StartTracking(string code);

        Task StopTracking(string sessionId);

        Task UpdateLocation(UpdateLocationModel model);

        Task<LocationViewModel> GetCurrentLocation(string code);

        Task<List<LocationViewModel>> GetJourney(GetJourneyModel model);
    }
}