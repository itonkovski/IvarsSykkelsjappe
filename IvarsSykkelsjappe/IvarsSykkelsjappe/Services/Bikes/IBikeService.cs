using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IvarsSykkelsjappe.Models.Bikes;

namespace IvarsSykkelsjappe.Services.Bikes
{
    public interface IBikeService
    {
        Task AddAsync(BikeFormModel bike, string imagePath);

        Task EditAsync(BikeFormModel bike, int id);

        Task DeleteAsync(int id);

        IEnumerable<BikeCategoryViewModel> GetBikeCategories();

        IEnumerable<BikeViewModel> GetAll();

        BikeDetailsViewModel GetDetails(int id);

        BikeFormModel GetEdit(int id);

        IEnumerable<BikeLatestIndexCarousel> Latest();

        public void AllSearch(AllBikesQueryModel bikeModel);

        BikeDetailsViewModel SendBikeByEmail(int id);
    }
}
