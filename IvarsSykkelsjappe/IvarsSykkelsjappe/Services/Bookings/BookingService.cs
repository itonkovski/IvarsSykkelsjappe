using System;
using IvarsSykkelsjappe.Data;
using IvarsSykkelsjappe.Data.Models;
using IvarsSykkelsjappe.Models.Bookings;

namespace IvarsSykkelsjappe.Services.Bookings
{
    public class BookingService : IBookingService
    {
        private readonly ApplicationDbContext dbContext;

        public BookingService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(BookingFormModel booking)
        {
            var bookingData = new Booking
            {
                FullName = booking.FullName,
                Email = booking.Email,
                PhoneNumber = booking.PhoneNumber,
                TimeSlot = booking.TimeSlot,
                ImageUrl = booking.ImageUrl,
                Details = booking.Details
            };

            this.dbContext.Bookings.Add(bookingData);
            this.dbContext.SaveChanges();
        }
    }
}
