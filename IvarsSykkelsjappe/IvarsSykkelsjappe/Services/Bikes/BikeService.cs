﻿using System.Collections.Generic;
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

        public IEnumerable<BikeSearchQueryModel> AllBikes()
        {
            var bikes = this.dbContext
                .Bikes
                .OrderByDescending(x => x.Id)
                .Select(x => new BikeSearchQueryModel
                {
                    Id = x.Id,
                    Brand = x.Brand,
                    Model = x.Model,
                    Price = x.Price,
                    ImageUrl = x.ImageUrl,
                    Year = x.Year,
                    BikeCategory = x.BikeCategory.Name
                })
                .ToList();

            return bikes;
        }

        public IEnumerable<BikeViewModel> GetAll()
        {
            var bikes = this.dbContext
                .Bikes
                .Select(x => new BikeViewModel
                {
                    Id = x.Id,
                    Brand = x.Brand,
                    Model = x.Model,
                    Price = x.Price,
                    Year = x.Year,
                    BikeCategory = x.BikeCategory.Name
                })
                .OrderBy(x => x.Brand)
                .ThenBy(x => x.Model)
                .ToList();

            return bikes;
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

        public BikeDetailsViewModel GetDetails(int id)
        {
            var bike = this.dbContext.Bikes
                .Where(x => x.Id == id)
                .Select(x => new BikeDetailsViewModel
                {
                    Id = x.Id,
                    Model = x.Model,
                    Brand = x.Brand,
                    Price = x.Price,
                    Year = x.Year,
                    Description = x.Description,
                    BikeCategory = x.BikeCategory.Name,
                    ImageUrl = x.ImageUrl,
                })
                .FirstOrDefault();
            return bike;
        }
    }
}