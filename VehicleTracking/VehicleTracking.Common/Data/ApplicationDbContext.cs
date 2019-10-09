using Microsoft.EntityFrameworkCore;

namespace VehicleTracking.Common.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
