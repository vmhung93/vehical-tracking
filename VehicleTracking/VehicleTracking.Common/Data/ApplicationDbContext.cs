using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VehicleTracking.Common.Helpers;

namespace VehicleTracking.Common.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Generate tracking columns
            OnBeforeSaving();

            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            // Generate tracking columns
            OnBeforeSaving();

            return base.SaveChanges();
        }

        private void OnBeforeSaving()
        {
            // Get user id and user name
            Guid changedById = GetCurrentUserId();

            // Get entities that are either added or modified
            var changedEntities = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified).ToList();

            // Run through entities and look for a property named
            changedEntities.ForEach(entity =>
            {
                // If entity is inherited BaseModel then set value for tracking columns
                if (entity.Entity is BaseModel baseModel)
                {
                    switch (entity.State)
                    {
                        case EntityState.Modified:
                            baseModel.UpdatedById = changedById;
                            baseModel.UpdatedDate = DateTime.UtcNow;

                            break;
                        case EntityState.Added:
                            baseModel.CreatedById = changedById;
                            baseModel.CreatedDate = DateTime.UtcNow;
                            baseModel.UpdatedById = changedById;
                            baseModel.UpdatedDate = DateTime.UtcNow;

                            break;
                        default:
                            break;
                    }
                }
            });
        }

        /// <summary>
        /// Try to retrieve user id from httpcontext
        /// </summary>
        /// <returns></returns>
        private Guid GetCurrentUserId()
        {
            try
            {
                return HttpContextHelper.GetCurrentUserId();
            }
            catch
            {
                return Guid.Empty;
            }
        }
    }
}
