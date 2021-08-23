using System;
namespace IvarsSykkelsjappe.Models.Bikes
{
    public class BikeListingViewModel
    {
        public int Id { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public string ImageUrl { get; set; }

        public int Year { get; set; }

        public string BikeCategory { get; set; }
    }
}
