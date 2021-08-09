using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IvarsSykkelsjappe.Data.Models
{
    using static DataConstants.Product;

    public class Product
    {
        public Product()
        {
            this.ProductOrders = new HashSet<ProductOrder>();
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
        [RegularExpression(ProductNumberRegularExpression)]
        public string ProductNumber { get; set; }

        [Required]
        public string Description { get; set; }

        public int Quantity { get; set; }

        public bool IsAvailable{ get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public int ProductCategoryId { get; set; }

        public ProductCategory ProductCategory { get; set; }

        public ICollection<ProductOrder> ProductOrders { get; set; }
    }
}
