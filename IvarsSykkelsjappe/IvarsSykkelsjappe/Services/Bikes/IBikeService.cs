using System;
using System.Collections.Generic;
using IvarsSykkelsjappe.Models.Bikes;

namespace IvarsSykkelsjappe.Services.Bikes
{
    public interface IBikeService
    {
        void Add(BikeFormModel bike);

        IEnumerable<BikeCategoryViewModel> GetBikeCategories();

        IEnumerable<BikeViewModel> GetAll();

        IEnumerable<BikeListingViewModel> AllBikes();

        BikeDetailsViewModel GetDetails(int id);

        IEnumerable<BikeLatestIndexCarousel> Latest();

        public void AllSearch(AllBikesQueryModel bikeModel);
    }
}
