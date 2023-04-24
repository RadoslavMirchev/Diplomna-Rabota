using Beauty_Salon.Models;
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
        public static async Task SeedUsersAsync(UserManager<ApplicationUser> userManager)
        {
            //Seed Users
            var adminUser = new ApplicationUser
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
            if (userManager.Users.All(u => u.Id != adminUser.Id))
            {
                var user = await userManager.FindByEmailAsync(adminUser.Email);
                if (user == null)
                {
                    var result = userManager.CreateAsync(adminUser, "admin0000").Result;
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(adminUser, Enums.Role.Admin.ToString());
                    }
                }
            }
            var worker1User = new ApplicationUser
            {
                UserName = "worker1",
                Email = "worker1@gmail.com",
                FirstName = "Мария",
                LastName = "Магдалена",
                MiddleName = "Иванова",
                PhoneNumber = "0893198745",
                Age = 27,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != worker1User.Id))
            {
                var user = await userManager.FindByEmailAsync(worker1User.Email);
                if (user == null)
                {
                    var result = userManager.CreateAsync(worker1User, "worker10000").Result;
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(worker1User, Enums.Role.Worker.ToString());
                    }
                }
            }
            var member2User = new ApplicationUser
            {
                UserName = "worker2",
                Email = "worker2@gmail.com",
                FirstName = "Лазар",
                LastName = "Георгиев",
                MiddleName = "Иванов",
                PhoneNumber = "0894186597",
                Age = 23,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != member2User.Id))
            {
                var user = await userManager.FindByEmailAsync(member2User.Email);
                if (user == null)
                {
                    var result = userManager.CreateAsync(member2User, "worker20000").Result;
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(member2User, Enums.Role.Worker.ToString());
                    }
                }
            }
            var worker3User = new ApplicationUser
            {
                UserName = "worker3",
                Email = "worker3@gmail.com",
                FirstName = "Ерос",
                LastName = "Василев",
                MiddleName = "Иванов",
                PhoneNumber = "0893198745",
                Age = 27,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != worker3User.Id))
            {
                var user = await userManager.FindByEmailAsync(worker3User.Email);
                if (user == null)
                {
                    var result = userManager.CreateAsync(worker3User, "worker30000").Result;
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(worker3User, Enums.Role.Worker.ToString());
                    }
                }
            }
        }
    }
}
