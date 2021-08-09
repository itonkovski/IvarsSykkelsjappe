using System;
using System.ComponentModel.DataAnnotations;

namespace IvarsSykkelsjappe.Data.Models
{
    public class PopUp
    {
        public int Id { get; set; }

        [Required]
        public string Location { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime DueTime { get; set; }

        public int AvailableSpots { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
