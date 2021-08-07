using System.Linq;
using IvarsSykkelsjappe.Data;
using IvarsSykkelsjappe.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IvarsSykkelsjappe.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var data = scopedServices.ServiceProvider.GetService<ApplicationDbContext>();

            data.Database.Migrate();

            SeedBikeCategories(data);
            SeedProductCategories(data);

            return app;
        }

        private static void SeedBikeCategories(ApplicationDbContext data)
        {
            if (data.BikesCategories.Any())
            {
                return;
            }

            data.BikesCategories.AddRange(new[]
            {
                new BikeCategory { Name = "Road" },
                new BikeCategory { Name = "Mountain" },
                new BikeCategory { Name = "Hybrid" },
                new BikeCategory { Name = "City" },
                new BikeCategory { Name = "Electric" },
                new BikeCategory { Name = "Fat tire" },
                new BikeCategory { Name = "Folding" },
                new BikeCategory { Name = "BMX" },
                new BikeCategory { Name = "Kids" },
                new BikeCategory { Name = "Women`s" },
                new BikeCategory { Name = "Cruiser" },
                new BikeCategory { Name = "Touring" },
                new BikeCategory { Name = "Fixie/Single speed" },
                new BikeCategory { Name = "Gravel" },
                new BikeCategory { Name = "Tandem" }
            });

            data.SaveChanges();
        }

        private static void SeedProductCategories(ApplicationDbContext data)
        {
            if (data.ProductsCategories.Any())
            {
                return;
            }

            data.ProductsCategories.AddRange(new[]
            {
                new ProductCategory { Name = "Brakes" },
                new ProductCategory { Name = "Chainrings" },
                new ProductCategory { Name = "Chains" },
                new ProductCategory { Name = "Chain tensioners" },
                new ProductCategory { Name = "Seats" },
                new ProductCategory { Name = "Bar ends" },
                new ProductCategory { Name = "Grips" },
                new ProductCategory { Name = "Rigid forks" },
                new ProductCategory { Name = "Cassettes" },
                new ProductCategory { Name = "Crank Arms" },
                new ProductCategory { Name = "Gear kits" },
                new ProductCategory { Name = "Group sets" },
                new ProductCategory { Name = "Rear deraileurs" },
                new ProductCategory { Name = "Shifters" },
                new ProductCategory { Name = "Twist shifters" },
                new ProductCategory { Name = "Triggers" },
                new ProductCategory { Name = "Lights" },
                new ProductCategory { Name = "Locks" },
                new ProductCategory { Name = "Mudguards" },
                new ProductCategory { Name = "Pedals" },
                new ProductCategory { Name = "Tires" },
                new ProductCategory { Name = "Wheels" },
            });

            data.SaveChanges();
        }
    }
}
