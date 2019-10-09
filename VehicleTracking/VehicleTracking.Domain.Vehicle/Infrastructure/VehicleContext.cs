using Microsoft.EntityFrameworkCore;
using VehicleTracking.Common.Data;
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
}