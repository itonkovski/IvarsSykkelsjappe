using System;
using AutoMapper;
using IvarsSykkelsjappe.Data.Models;
using IvarsSykkelsjappe.Models.Bikes;

namespace IvarsSykkelsjappe.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Bike, BikeLatestIndexCarousel>();

            this.CreateMap<Bike, BikeDetailsViewModel>();

            this.CreateMap<Bike, BikeFormModel>();

            this.CreateMap<Bike, BikeViewModel>();

            this.CreateMap<Bike, BikeListingViewModel>();

            this.CreateMap<BikeCategory, BikeCategoryViewModel>();
        }
    }
}
