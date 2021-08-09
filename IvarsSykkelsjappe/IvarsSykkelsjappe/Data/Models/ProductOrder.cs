using System.ComponentModel.DataAnnotations;

namespace IvarsSykkelsjappe.Data.Models
{
    public class ProductOrder
    {
        [Required]
        public int ProductId { get; set; }

        public Product Product { get; set; }

        [Required]
        public int OrderId { get; set; }

        public Order Order { get; set; }
    }
}