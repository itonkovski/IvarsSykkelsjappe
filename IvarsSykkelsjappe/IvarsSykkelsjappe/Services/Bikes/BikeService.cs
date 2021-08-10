using System.Collections.Generic;
using System.Linq;
using IvarsSykkelsjappe.Data;
using IvarsSykkelsjappe.Data.Models;
using IvarsSykkelsjappe.Models.Bikes;

namespace IvarsSykkelsjappe.Services.Bikes
{
    public class BikeService : IBikeService
    {
        private readonly ApplicationDbContext dbContext;

        public BikeService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(BikeFormModel bike)
        {
            var bikeData = new Bike
            {
                Brand = bike.Brand,
                Model = bike.Model,
                Price = bike.Price,
                Description = bike.Description,
                ImageUrl = bike.ImageUrl,
                Year = bike.Year,
                BikeCategoryId = bike.BikeCategoryId
            };

            this.dbContext.Bikes.Add(bikeData);
            this.dbContext.SaveChanges();
        }

        public IEnumerable<BikeCategoryViewModel> GetBikeCategories()
            => this.dbContext
                .BikesCategories
                .Select(x => new BikeCategoryViewModel
                {
                    BikeCategoryId = x.Id,
                    Name = x.Name
                })
                .ToList();
    }
}
