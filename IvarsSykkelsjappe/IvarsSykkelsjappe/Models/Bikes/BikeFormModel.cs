using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace IvarsSykkelsjappe.Models.Bikes
{
    using static Data.DataConstants.Bike;

    public class BikeFormModel
    {
        [Required]
        [StringLength(
            BrandMaxLength,
            MinimumLength = BrandMinLength,
            ErrorMessage = "The field should contain from {2} to {1} symbols.")]
        public string Brand { get; set; }

        [Required]
        [StringLength(
            ModelMaxLength,
            MinimumLength = ModelMinLength,
            ErrorMessage = "The field should contain from {2} to {1} symbols.")]
        public string Model { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        [StringLength(
            int.MaxValue,
            MinimumLength = DescriptionMinLength,
            ErrorMessage = "The Description is required and should be minimum {2} symbols.")]
        public string Description { get; set; }

        //[Required]
        [Display(Name = "Upload image")]
        //[MaxLength(10 * 1024 * 1024)]
        public IEnumerable<IFormFile> Images { get; set; }

        [Range(YearMinValue, YearMaxValue)]
        public int Year { get; set; }

        [Display(Name = "Category")]
        public int BikeCategoryId { get; set; }

        public bool Agreements { get; set; }

        public IEnumerable<BikeCategoryViewModel> Categories { get; set; }
    }
}
