using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
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

        public DbSet<Location> Locations { get; set; }

        public DbSet<Session> Sessions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }

    //public class LocationTrackingContextFactory : IDesignTimeDbContextFactory<LocationTrackingContext>
    //{
    //    public LocationTrackingContext CreateDbContext(string[] args)
    //    {
    //        var optionsBuilder = new DbContextOptionsBuilder<LocationTrackingContext>();
    //        optionsBuilder.UseSqlServer("Server=.;Database=VehicleTracking;Trusted_Connection=True;MultipleActiveResultSets=true");

    //        return new LocationTrackingContext(optionsBuilder.Options);
    //    }
    //}
}
