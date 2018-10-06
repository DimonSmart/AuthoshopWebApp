using AutoshopWebApp.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoshopWebApp.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider provider, string userTestPw)
        {
            using (var context = new ApplicationDbContext(
                provider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                var adminID = await EnsureUser(provider, userTestPw, "admin@default.com");
                await EnsureRole(provider, adminID, Constants.AdministratorRole);
            }
        }

        public static async Task<string> EnsureUser(IServiceProvider serviceProvider, string userPw, string userName)
        {
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByNameAsync(userName);

            if(user == null)
            {
                user = new IdentityUser(userName);
                await userManager.CreateAsync(user, userPw);
            }

            return user.Id;
        }

        public static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider, string uid, string role)
        {
            IdentityResult identityResult  = null;

            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if(!await roleManager.RoleExistsAsync(role))
            {
                identityResult = await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByIdAsync(uid);

            identityResult = await userManager.AddToRoleAsync(user, role);

            return identityResult;
        }
    }
}
