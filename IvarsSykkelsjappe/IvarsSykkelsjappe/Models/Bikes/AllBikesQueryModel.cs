using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IvarsSykkelsjappe.Models.Bikes
{
    public class AllBikesQueryModel
    {
        public const int BikesPerPage = 6;

        public int CurrentPage { get; set; } = 1;

        public int TotalBikes { get; set; }

        public string Brand { get; set; }

        public IEnumerable<string> Brands { get; set; }

        [Display(Name = "Search by text")]
        public string SearchTerm { get; set; }

        public BikeSorting Sorting { get; set; }

        public IEnumerable<BikeListingViewModel> Bikes { get; set; }
    }
}
