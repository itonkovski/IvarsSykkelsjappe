using IvarsSykkelsjappe.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IvarsSykkelsjappe.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Bike> Bikes { get; set; }

        public DbSet<BikeCategory> BikesCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Bike>()
                .HasOne(x => x.BikeCategory)
                .WithMany(x => x.Bikes)
                .HasForeignKey(x => x.BikeCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
