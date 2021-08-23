using IvarsSykkelsjappe.Data.Models;
using Microsoft.AspNetCore.Identity;
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

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductCategory> ProductsCategories { get; set; }

        public DbSet<Service> Services { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<PopUp> PopUps { get; set; }

        public DbSet<Mechanic> Mechanics { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<ProductOrder> ProductOrders { get; set; }

        public DbSet<ServiceOrder> ServiceOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Bike>()
                .HasOne(x => x.BikeCategory)
                .WithMany(x => x.Bikes)
                .HasForeignKey(x => x.BikeCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Booking>()
                .HasOne(x => x.Client)
                .WithMany(x => x.Bookings)
                .HasForeignKey(x => x.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Client>()
                .HasOne<IdentityUser>()
                .WithOne()
                .HasForeignKey<Client>(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Product>()
                .HasOne(x => x.ProductCategory)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.ProductCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<ProductOrder>()
                .HasKey(po => new { po.ProductId, po.OrderId });

            builder
                .Entity<ServiceOrder>()
                .HasKey(so => new { so.ServiceId, so.OrderId });

            builder
                .Entity<Mechanic>()
                .HasOne<IdentityUser>()
                .WithOne()
                .HasForeignKey<Mechanic>(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
