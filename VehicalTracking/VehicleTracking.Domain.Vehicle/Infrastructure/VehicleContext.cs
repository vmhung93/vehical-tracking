using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using VehicalTracking.Common.Data;
using VehicleTracking.Domain.Vehicle.Models;

namespace VehicalTracking.Domain.ApplicationUser.Infrastructure
{
    public class VehicleContext : ApplicationDbContext
    {
        public VehicleContext(DbContextOptions<VehicleContext> options)
             : base(options)
        {
        }

        public DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }

    public class VehicleContextFactory : IDesignTimeDbContextFactory<VehicleContext>
    {
        public VehicleContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<VehicleContext>();
            optionsBuilder.UseSqlServer("Server=.;Database=vehical-tracking;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new VehicleContext(optionsBuilder.Options);
        }
    }
}