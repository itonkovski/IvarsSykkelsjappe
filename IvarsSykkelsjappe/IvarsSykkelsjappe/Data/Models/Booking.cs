using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IvarsSykkelsjappe.Data.Models
{
    using static DataConstants.Booking;

    public class Booking
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(FullNameMaxLength)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; }

        public DateTime TimeSlot { get; set; }

        [Required]
        public string Details { get; set; }

        public string UserId { get; set; }

        public string MechanicId { get; set; }

        public string MechanicName { get; set; }

        public bool IsTaken { get; set; }

        public ICollection<Assistance> Assistances { get; set; }
    }
}
