using System;
using System.Collections.Generic;
using IvarsSykkelsjappe.Models.Bikes;

namespace IvarsSykkelsjappe.Services.Bikes
{
    public interface IBikeService
    {
        void Add(BikeFormModel bike, string imagePath);

        void Edit(BikeFormModel bike, int id);

        void Delete(int id);

        IEnumerable<BikeCategoryViewModel> GetBikeCategories();

        IEnumerable<BikeViewModel> GetAll();

        BikeDetailsViewModel GetDetails(int id);

        BikeFormModel GetEdit(int id);

        IEnumerable<BikeLatestIndexCarousel> Latest();

        public void AllSearch(AllBikesQueryModel bikeModel);
    }
}
