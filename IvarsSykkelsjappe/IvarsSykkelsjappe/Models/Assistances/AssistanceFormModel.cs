using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IvarsSykkelsjappe.Models.Assistances
{
    using static Data.DataConstants.Assistance;

    public class AssistanceFormModel
    {
        [Required]
        [StringLength(
            NameMaxLength,
            MinimumLength = NameMinLength,
            ErrorMessage = "The field should contain from {2} to {1} symbols.")]
        public string Name { get; set; }

        [Required]
        [StringLength(
            int.MaxValue,
            MinimumLength = DescriptionMinLength,
            ErrorMessage = "The Description is required and should be minimum {2} symbols.")]
        public string Description { get; set; }

        [Required]
        //If I need to put a price validation
        //[Range(typeof(decimal), DataValidation.ProductMinPrice, DataValidation.ProductMaxPrice)]
        public decimal Price { get; set; }
    }
}
