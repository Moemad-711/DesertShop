using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DessertShop.Models;

namespace DessertShop
{
    public class SeedData
    {
        public static async Task InitializeAsync(
            IServiceProvider services)
        {
            var roleManager = services
                .GetRequiredService<RoleManager<IdentityRole>>();
            await EnsureRolesAsync(roleManager);

            var userManager = services
                .GetRequiredService<UserManager<IdentityUser>>();
            await EnsureAdminAsync(userManager);
        }

        private static async Task EnsureRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            var alreadyExists = await roleManager
                .RoleExistsAsync(Constants.AdministratorRole);

            if (alreadyExists) return;

            await roleManager.CreateAsync(
                new IdentityRole(Constants.AdministratorRole));
        }

        private static async Task EnsureAdminAsync(UserManager<IdentityUser> userManager)
        {
            var Admin = await userManager.Users
                .Where(x => x.UserName == "moemad.admin@admin.com")
                .SingleOrDefaultAsync();

            if (Admin != null) return;

            Admin = new IdentityUser
            {
                UserName = "moemad.admin@admin.com",
                Email = "moemad.admin@admin.com"
            };
            await userManager.CreateAsync(
                Admin, "Moemad@admin123");
            await userManager.AddToRoleAsync(
                Admin, Constants.AdministratorRole);
        }

    }
}
