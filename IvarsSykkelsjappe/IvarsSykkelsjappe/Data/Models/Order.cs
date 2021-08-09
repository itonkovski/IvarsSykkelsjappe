using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IvarsSykkelsjappe.Data.Models
{
    public class Order
    {
        public Order()
        {
            this.ProductOrders = new HashSet<ProductOrder>();
            this.ServiceOrders = new HashSet<ServiceOrder>();
        }

        public int Id { get; set; }

        public int BookingId { get; set; }

        public Booking Booking { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,4)")]
        public decimal TotalPrice { get; set; }

        public bool IsReadyToPickUp { get; set; }

        public DateTime TimeToPickUp { get; set; }

        public ICollection<ProductOrder> ProductOrders { get; set; }

        public ICollection<ServiceOrder> ServiceOrders { get; set; }


    }
}
