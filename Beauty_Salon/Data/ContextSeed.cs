using Beauty_Salon.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Beauty_Salon.Data
{
    public class ContextSeed
    {
        public static async Task SeedRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(Enums.Role.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.Role.Worker.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.Role.Client.ToString()));
        }
        public static async Task SeedAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                UserName = "admin",
                Email = "admin2@gmail.com",
                FirstName = "Radoslav",
                LastName = "Mirchev",
                MiddleName = "Konstantinov",
                PhoneNumber = "0896132783",
                Age = 18,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "parola123");
                    await userManager.AddToRoleAsync(defaultUser, Enums.Role.Client.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Enums.Role.Worker.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Enums.Role.Admin.ToString());
                }

            }
        }
    }
}
