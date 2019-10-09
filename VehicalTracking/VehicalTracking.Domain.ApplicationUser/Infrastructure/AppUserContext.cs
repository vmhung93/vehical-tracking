using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using VehicalTracking.Domain.ApplicationUser.Models;

namespace VehicalTracking.Domain.ApplicationUser.Infrastructure
{
    public class AppUserContext : IdentityDbContext<AppUser,
        IdentityRole<Guid>, 
        Guid, 
        IdentityUserClaim<Guid>,
        IdentityUserRole<Guid>,
        IdentityUserLogin<Guid>,
        IdentityRoleClaim<Guid>,
        IdentityUserToken<Guid>>
    {
        public AppUserContext(DbContextOptions<AppUserContext> options)
             : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Customize identity model
            // Reference link: https://docs.microsoft.com/en-us/aspnet/core/security/authentication/customize-identity-model
            builder.Entity<AppUser>(b => { b.ToTable("Users"); });

            builder.Entity<IdentityUserClaim<Guid>>(b => { b.ToTable("UserClaims"); });

            builder.Entity<IdentityUserLogin<Guid>>(b => { b.ToTable("UserLogins"); });

            builder.Entity<IdentityUserToken<Guid>>(b => { b.ToTable("UserTokens"); });

            builder.Entity<IdentityRole<Guid>>(b => { b.ToTable("Roles"); });

            builder.Entity<IdentityRoleClaim<Guid>>(b => { b.ToTable("RoleClaims"); });

            builder.Entity<IdentityUserRole<Guid>>(b => { b.ToTable("UserRoles"); });
        }
    }

    public class UserContextFactory : IDesignTimeDbContextFactory<AppUserContext>
    {
        public AppUserContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppUserContext>();
            optionsBuilder.UseSqlServer("Server=.;Database=vehical-tracking;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new AppUserContext(optionsBuilder.Options);
        }
    }
}