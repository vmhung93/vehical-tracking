using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace VehicleTracking.Domain.ApplicationUser.Infrastructure
{
    public class ApplicationUserContext : IdentityDbContext<Models.ApplicationUser,
        IdentityRole<Guid>, Guid,
        IdentityUserClaim<Guid>, IdentityUserRole<Guid>,
        IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>,
        IdentityUserToken<Guid>>
    {
        public ApplicationUserContext(DbContextOptions<ApplicationUserContext> options)
             : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Customize identity model
            // Reference link: https://docs.microsoft.com/en-us/aspnet/core/security/authentication/customize-identity-model
            builder.Entity<Models.ApplicationUser>(b => { b.ToTable("Users"); });

            builder.Entity<IdentityUserClaim<Guid>>(b => { b.ToTable("UserClaims"); });

            builder.Entity<IdentityUserLogin<Guid>>(b => { b.ToTable("UserLogins"); });

            builder.Entity<IdentityUserToken<Guid>>(b => { b.ToTable("UserTokens"); });

            builder.Entity<IdentityRole<Guid>>(b => { b.ToTable("Roles"); });

            builder.Entity<IdentityRoleClaim<Guid>>(b => { b.ToTable("RoleClaims"); });

            builder.Entity<IdentityUserRole<Guid>>(b => { b.ToTable("UserRoles"); });
        }
    }
}