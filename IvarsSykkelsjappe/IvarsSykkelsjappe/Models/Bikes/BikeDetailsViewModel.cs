using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IvarsSykkelsjappe.Models.Bikes
{
    public class BikeDetailsViewModel : BikeViewModel
    {
        public string ImageUrl { get; set; }

        public string Description { get; set; }
    }
}
