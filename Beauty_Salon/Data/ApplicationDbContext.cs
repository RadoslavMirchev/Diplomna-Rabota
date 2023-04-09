using Beauty_Salon.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Beauty_Salon.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Procedure> Procedures { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}