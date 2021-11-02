using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using IvarsSykkelsjappe.Data;
using IvarsSykkelsjappe.Data.Models;
using IvarsSykkelsjappe.Models.Bikes;

namespace IvarsSykkelsjappe.Services.Bikes
{
    public class BikeService : IBikeService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "jpeg" };
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public BikeService(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public void Add(BikeFormModel bike, string imagePath)
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

            Directory.CreateDirectory($"{imagePath}/bikes/");
            foreach (var image in bike.Images)
            {
                var extension = Path.GetExtension(image.FileName).TrimStart('.');
                if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
                {
                    throw new Exception($"Invalid image extension {extension}");
                }

                var dbImage = new Image
                {
                    Extension = extension,
                };
                bikeData.Images.Add(dbImage);

                var physicalPath = $"{imagePath}/bikes/{dbImage.Id}.{extension}";
                using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
                image.CopyTo(fileStream);
            }

            this.dbContext.Bikes.Add(bikeData);
            this.dbContext.SaveChanges();
        }

        public void AllSearch(AllBikesQueryModel queryModel)
        {
            var bikesQuery = this.dbContext
                .Bikes
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryModel.Brand))
            {
                bikesQuery = bikesQuery.Where(x => x.Brand == queryModel.Brand);
            }

            if (!string.IsNullOrWhiteSpace(queryModel.SearchTerm))
            {
                bikesQuery = bikesQuery.Where(x =>
                    (x.Brand + " " + x.Model).ToLower().Contains(queryModel.SearchTerm.ToLower()) ||
                    x.Description.ToLower().Contains(queryModel.SearchTerm.ToLower()));
            }

            bikesQuery = queryModel.Sorting switch
            {
                BikeSorting.Year => bikesQuery.OrderByDescending(x => x.Year),
                BikeSorting.BrandAndModel => bikesQuery.OrderBy(x => x.Brand).ThenBy(x => x.Model),
                _ => bikesQuery.OrderByDescending(x => x.Id)
            };

            var totalBikes = bikesQuery
                .Count();

            var bikes = bikesQuery
                .Skip((queryModel.CurrentPage - 1) * AllBikesQueryModel.BikesPerPage)
                .Take(AllBikesQueryModel.BikesPerPage)
                //.ProjectTo<BikeListingViewModel>(this.mapper.ConfigurationProvider)
                .Select(x => new BikeListingViewModel
                {
                    Id = x.Id,
                    Brand = x.Brand,
                    Model = x.Model,
                    Year = x.Year,
                    ImageUrl = "/images/bikes/" + x.Images.FirstOrDefault().Id + "." + x.Images.FirstOrDefault().Extension,
                    BikeCategory = x.BikeCategory.Name
                })
                .ToList();

            var bikeBrands = this.dbContext
                .Bikes
                .Select(x => x.Brand)
                .Distinct()
                .OrderBy(x => x)
                .ToList();

            queryModel.TotalBikes = totalBikes;
            queryModel.Brands = bikeBrands;
            queryModel.Bikes = bikes;
        }

        public IEnumerable<BikeViewModel> GetAll()
        {
            var bikes = this.dbContext
                .Bikes
                //.ProjectTo<BikeViewModel>(this.mapper.ConfigurationProvider)
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
                //.ProjectTo<BikeCategoryViewModel>(this.mapper.ConfigurationProvider)
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
                //.ProjectTo<BikeDetailsViewModel>(this.mapper.ConfigurationProvider)
                .Select(x => new BikeDetailsViewModel
                {
                    Id = x.Id,
                    Model = x.Model,
                    Brand = x.Brand,
                    Price = x.Price,
                    Year = x.Year,
                    Description = x.Description,
                    BikeCategory = x.BikeCategory.Name,
                    ImageUrl = "/images/bikes/" + x.Images.FirstOrDefault().Id + "." + x.Images.FirstOrDefault().Extension
                })
                .FirstOrDefault();
            return bike;
        }

        public BikeFormModel GetEdit(int id)
        {
            var bike = this.dbContext.Bikes
                .Where(x => x.Id == id)
                //.ProjectTo<BikeFormModel>(this.mapper.ConfigurationProvider)
                .Select(x => new BikeFormModel
                {
                    Model = x.Model,
                    Brand = x.Brand,
                    Price = x.Price,
                    Year = x.Year,
                    Description = x.Description,
                    BikeCategoryId = x.BikeCategoryId,
                    ImageUrl = x.ImageUrl
                })
                .FirstOrDefault();
            return bike;
        }

        public void Edit(BikeFormModel bike, int id)
        {
            var bikeData = this.dbContext.Bikes.Find(id);

            bikeData.Model = bike.Model;
            bikeData.Brand = bike.Brand;
            bikeData.Price = bike.Price;
            bikeData.Year = bike.Year;
            bikeData.Description = bike.Description;
            bikeData.BikeCategoryId = bike.BikeCategoryId;
            bikeData.ImageUrl = bike.ImageUrl;

            this.dbContext.SaveChanges();

        }

        public void Delete(int id)
        {
            var bike = this.dbContext.Bikes.Find(id);
            this.dbContext.Bikes.Remove(bike);
            this.dbContext.SaveChanges();
        }

        public IEnumerable<BikeLatestIndexCarousel> Latest()
        {
            var bikes = this.dbContext
                .Bikes
                .OrderByDescending(c => c.Id)
                //.ProjectTo<BikeLatestIndexCarousel>(this.mapper.ConfigurationProvider)
                .Select(x => new BikeLatestIndexCarousel
                {
                    Id = x.Id,
                    Brand = x.Brand,
                    Model = x.Model,
                    ImageUrl = "/images/bikes/" + x.Images.FirstOrDefault().Id + "." + x.Images.FirstOrDefault().Extension,
                    Year = x.Year
                })
                .Take(3)
                .ToList();

            return bikes;
        }
    }
}
