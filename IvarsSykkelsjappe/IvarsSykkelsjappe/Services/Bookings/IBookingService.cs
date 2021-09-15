using System.Collections.Generic;
using System.Threading.Tasks;
using IvarsSykkelsjappe.Models.Bookings;

namespace IvarsSykkelsjappe.Services.Bookings
{
    public interface IBookingService
    {
        void Add(BookingFormModel bike, string userId);

        IEnumerable<BookingViewModel> GetAllBookings();

        IEnumerable<BookingViewModel> GetAllBookingsForToday();

        void Delete(int id);

        IEnumerable<BookingViewModel> MyBookings(string userId);

        void TakeMechanic(int id, string mechanicId, string mechanicName);

        IEnumerable<BookingViewModel> MyOrders(string mechanicId);

        OrderDetailsViewModel GetByMechanic(int id);

        IEnumerable<OrderAssistanceViewModel> GetAssistances();
    }
}
