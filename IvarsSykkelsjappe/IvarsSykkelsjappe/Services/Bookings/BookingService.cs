using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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

        public void Add(BookingFormModel booking, string userId)
        {

            var bookingData = new Booking
            {
                FullName = booking.FullName,
                Email = booking.Email,
                PhoneNumber = booking.PhoneNumber,
                TimeSlot = DateTime.ParseExact(booking.TimeSlot, "yyyy-mm-dd", CultureInfo.InvariantCulture),
                Details = booking.Details,
                UserId = userId,
            };

            this.dbContext.Bookings.Add(bookingData);
            this.dbContext.SaveChanges();
        }

        public IEnumerable<BookingViewModel> GetAllBookings()
        {
            var bookings = this.dbContext
                .Bookings
                .Select(x => new BookingViewModel
                {
                    Id = x.Id,
                    TimeSlot = x.TimeSlot.ToString(),
                    Details = x.Details,
                    UserId = x.UserId
                })
                .OrderByDescending(x => x.TimeSlot)
                .ToList();

            return bookings;
        }

        public void Delete(int id)
        {
            var booking = this.dbContext.Bookings.Find(id);
            this.dbContext.Bookings.Remove(booking);
            this.dbContext.SaveChanges();
        }
    }
}
