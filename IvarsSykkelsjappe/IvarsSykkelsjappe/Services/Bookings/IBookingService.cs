using System.Collections.Generic;
using IvarsSykkelsjappe.Models.Bookings;

namespace IvarsSykkelsjappe.Services.Bookings
{
    public interface IBookingService
    {
        void Add(BookingFormModel bike, string userId);

        IEnumerable<BookingViewModel> GetAllBookings();

        void Delete(int id);

        IEnumerable<BookingViewModel> MyBookings(string userId);

        void TakeMechanic(int id, string mechanicId);

        IEnumerable<BookingViewModel> MyOrders(string mechanicId);
    }
}
