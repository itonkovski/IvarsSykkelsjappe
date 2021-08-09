﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IvarsSykkelsjappe.Data.Models.Enums;

namespace IvarsSykkelsjappe.Data.Models
{
    using static DataConstants.Service;

    public class Service
    {
        public Service()
        {
            this.ServiceOrders = new HashSet<ServiceOrder>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }

        public ServiceCategory ServiceCategory { get; set; }

        public ICollection<ServiceOrder> ServiceOrders { get; set; }
    }
}
