using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IvarsSykkelsjappe.Data.Models
{
    using static DataConstants.Bike;

    public class Bike
    {
        public Bike()
        {
            this.Images = new HashSet<Image>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(BrandMaxLength)]
        public string Brand { get; set; }

        [Required]
        [MaxLength(ModelMaxLength)]
        public string Model { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        [Required]
        public int Year { get; set; }

        public int BikeCategoryId { get; set; }

        public BikeCategory BikeCategory { get; set; }
    }
}
