﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleTracking.Common.Constants;

namespace VehicleTracking.Domain.ApplicationUser.Infrastructure
{
    public static class IdentityDataInitializer
    {
        public static async Task SeedData(UserManager<Models.ApplicationUser> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            // Add default roles
            var roles = new List<string> { UserRoles.User, UserRoles.Admin };

            foreach (var role in roles)
            {
                var isExisted = await roleManager.RoleExistsAsync(role);

                if (!isExisted)
                {
                    await roleManager.CreateAsync(new IdentityRole<Guid>(role));
                }
            }

            // Add Admin
            var adminEmail = "admin@mail.com";
            var admin = await userManager.FindByEmailAsync(adminEmail);

            if (admin == null)
            {
                admin = new Models.ApplicationUser()
                {
                    UserName = "admin",
                    Email = adminEmail
                };

                await userManager.CreateAsync(admin, "1234x@X");
                await userManager.AddToRoleAsync(admin, UserRoles.Admin);
            }

            // Add default user
            var userEmail = "user@mail.com";
            var user = await userManager.FindByEmailAsync(userEmail);

            if (user == null)
            {
                user = new Models.ApplicationUser()
                {
                    UserName = "user_1",
                    Email = userEmail
                };

                await userManager.CreateAsync(user, "1234x@X");
                await userManager.AddToRoleAsync(user, UserRoles.User);
            }
        }
    }
}
