using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IvarsSykkelsjappe.Data.Models
{
    using static DataConstants.Category;
        
    public class ProductCategory
    {
        public ProductCategory()
        {
            this.Products = new HashSet<Product>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}