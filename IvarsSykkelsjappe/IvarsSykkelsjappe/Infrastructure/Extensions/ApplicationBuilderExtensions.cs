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

            SeedCategories(data);

            return app;
        }

        private static void SeedCategories(ApplicationDbContext data)
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
    }
}
