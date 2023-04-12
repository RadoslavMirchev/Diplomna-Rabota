using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Beauty_Salon.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder builder)
        {
            var pwd = "P@$$w0rd";
            var passwordHasher = new PasswordHasher<ApplicationUser>();

            // Seed Roles
            var adminRole = new IdentityRole("Admin");
            adminRole.NormalizedName = adminRole.Name.ToUpper();

            var memberRole = new IdentityRole("Member");
            memberRole.NormalizedName = memberRole.Name.ToUpper();

            List<IdentityRole> roles = new List<IdentityRole>() {
            adminRole,
            memberRole
            };

            builder.Entity<IdentityRole>().HasData(roles);


            // Seed Users
            var adminUser = new ApplicationUser
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
            adminUser.NormalizedUserName = adminUser.UserName.ToUpper();
            adminUser.NormalizedEmail = adminUser.Email.ToUpper();
            adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, pwd);

            var memberUser = new ApplicationUser
            {
                UserName = "member",
                Email = "member@gmail.com",
                FirstName = "Radoslav",
                LastName = "Mirchev",
                MiddleName = "Konstantinov",
                PhoneNumber = "0896132783",
                Age = 18,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            memberUser.NormalizedUserName = memberUser.UserName.ToUpper();
            memberUser.NormalizedEmail = memberUser.Email.ToUpper();
            memberUser.PasswordHash = passwordHasher.HashPassword(memberUser, pwd);

            List<ApplicationUser> users = new List<ApplicationUser>() {
                adminUser,
                memberUser,
            };

            builder.Entity<ApplicationUser>().HasData(users);

            // Seed UserRoles
            List<IdentityUserRole<string>> userRoles = new List<IdentityUserRole<string>>();

            userRoles.Add(new IdentityUserRole<string>
            {
                UserId = users[0].Id,
                RoleId = roles.First(q => q.Name == "Admin").Id
            });

            userRoles.Add(new IdentityUserRole<string>
            {
                UserId = users[1].Id,
                RoleId = roles.First(q => q.Name == "Member").Id
            });


            builder.Entity<IdentityUserRole<string>>().HasData(userRoles);
        }
    }
}
