using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using VehicalTracking.Domain.ApplicationUser.Infrastructure;
using VehicleTracking.Common.Exceptions;
using VehicleTracking.Common.Query;
using VehicleTracking.Common.ViewModels;

namespace VehicleTracking.Domain.Vehicle.Queries
{
    public class VehicleViewModel : BaseViewModel
    {
        public Guid UserId { get; set; }

        public string Code { get; set; }
    }

    public class GetVehicleByCodeQuery : IQuery<string, Task<VehicleViewModel>>
    {
        private readonly VehicleContext _context;
        private readonly ILogger<GetVehicleByCodeQuery> _logger;

        public GetVehicleByCodeQuery(VehicleContext context, ILogger<GetVehicleByCodeQuery> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<VehicleViewModel> Execute(string code)
        {
            var vehicle = await _context.Vehicles.Where(v => v.Code == code && v.IsActive).FirstOrDefaultAsync();

            if (vehicle == null)
            {
                throw new CustomException(ErrorCodes.EC_Vehicle_001, code);
            }

            return new VehicleViewModel()
            {
                Id = vehicle.Id,
                UserId = vehicle.UserId,
                Code = vehicle.Code
            };
        }
    }
}
