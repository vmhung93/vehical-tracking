using Microsoft.EntityFrameworkCore;
using VehicleTracking.Common.Data;
using VehicleTracking.Domain.LocationTracking.Models;

namespace VehicleTracking.Domain.LocationTracking.Infrastructure
{
    public class LocationTrackingContext : ApplicationDbContext
    {
        public LocationTrackingContext(DbContextOptions<LocationTrackingContext> options)
           : base(options)
        {
        }

        public DbSet<Models.LocationTracking> LocationTrackings { get; set; }

        public DbSet<SessionTracking> SessionTrackings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
