﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IvarsSykkelsjappe.Data.Models
{
    using static DataConstants.Category;

    public class BikeCategory
    {
        public BikeCategory()
        {
            this.Bikes = new HashSet<Bike>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public ICollection<Bike> Bikes { get; set; }
    }
}