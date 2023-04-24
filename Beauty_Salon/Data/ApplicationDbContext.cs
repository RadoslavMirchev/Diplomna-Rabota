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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Appointment>().HasOne(m => m.Client).WithMany(m => m.Appointments).HasForeignKey(m => m.ClientId).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Procedure>().HasOne(m => m.Worker).WithMany(m => m.Procedures).HasForeignKey(m => m.WorkerId).OnDelete(DeleteBehavior.NoAction);
            base.OnModelCreating(builder);
        }

        public DbSet<Procedure> Procedures { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}