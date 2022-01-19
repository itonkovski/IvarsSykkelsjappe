using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace IvarsSykkelsjappe.Models.Products
{
    using static Data.DataConstants.Product;

    public class ProductFormModel
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
        [RegularExpression(ProductNumberRegularExpression)]
        public string ProductNumber { get; set; }

        [Required]
        [StringLength(
            int.MaxValue,
            MinimumLength = DescriptionMinLength,
            ErrorMessage = "The Description is required and should be minimum {2} symbols.")]
        public string Description { get; set; }

        public int Quantity { get; set; }

        public bool IsAvailable { get; set; }

        [Required]
        [Url]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }

        //[Required]
        [Display(Name = "Upload image")]
        [MaxLength(10 * 1024 * 1024)]
        public IFormFile Image { get; set; }

        [Display(Name = "Product Category")]
        public int ProductCategoryId { get; set; }

        public IEnumerable<ProductCategoryViewModel> Categories { get; set; }
    }
}
