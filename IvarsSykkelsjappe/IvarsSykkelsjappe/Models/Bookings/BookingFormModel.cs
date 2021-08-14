﻿using System;
using System.ComponentModel.DataAnnotations;

namespace IvarsSykkelsjappe.Models.Bookings
{
    using static Data.DataConstants.Booking;

    public class BookingFormModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(
            FullNameMaxLength,
            MinimumLength = MinNameMaxLength,
            ErrorMessage = "The field should contain from {2} to {1} symbols.")]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        [StringLength(PhoneNumberMaxLength)]
        public int PhoneNumber { get; set; }

        [Required]
        public DateTime TimeSlot { get; set; }

        [Required]
        [StringLength(
            int.MaxValue,
            MinimumLength = DetailsMinLength,
            ErrorMessage = "The Description is required and should be minimum {2} symbols.")]
        public string Details { get; set; }

        [Required]
        [Url]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }

        public bool Agreements { get; set; }
    }
}