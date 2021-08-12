using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IvarsSykkelsjappe.Models.Bikes
{
    public class BikeSearchQueryModel
    {
        public int Id { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public int Year { get; set; }

        public string BikeCategory { get; set; }
    }
}
