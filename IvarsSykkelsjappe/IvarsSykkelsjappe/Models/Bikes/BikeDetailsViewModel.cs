using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace IvarsSykkelsjappe.Models.Bikes
{
    public class BikeDetailsViewModel : BikeViewModel
    {
        public string ImageUrl { get; set; }

        public string Description { get; set; }
    }
}
