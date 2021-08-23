using System;
namespace IvarsSykkelsjappe.Models.Bookings
{
    public class AllBookingsListingModel
    {
        public string UserId { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public int PhoneNumber { get; set; }

        public string TimeSlot { get; set; }

        public string Details { get; set; }
    }
}
