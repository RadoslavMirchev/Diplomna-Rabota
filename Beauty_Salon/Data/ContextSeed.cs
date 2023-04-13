﻿using Beauty_Salon.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Beauty_Salon.Data
{
    public class ContextSeed
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
                await roleManager.CreateAsync(new IdentityRole(Enums.Role.Admin.ToString()));
                await roleManager.CreateAsync(new IdentityRole(Enums.Role.Worker.ToString()));
                await roleManager.CreateAsync(new IdentityRole(Enums.Role.Client.ToString()));
        }
        public static async Task SeedAdminAsync(UserManager<ApplicationUser> userManager)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                UserName = "admin",
                Email = "admin@gmail.com",
                FirstName = "Радослав",
                LastName = "Мирчев",
                MiddleName = "Константинов",
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
                    var result = userManager.CreateAsync(defaultUser, "admin0000").Result;
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(defaultUser, Enums.Role.Client.ToString());
                        await userManager.AddToRoleAsync(defaultUser, Enums.Role.Worker.ToString());
                        await userManager.AddToRoleAsync(defaultUser, Enums.Role.Admin.ToString());
                    }
                }
            }
        }
    }
}
