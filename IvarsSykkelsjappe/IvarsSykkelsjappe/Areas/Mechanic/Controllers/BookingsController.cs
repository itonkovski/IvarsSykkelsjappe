using IvarsSykkelsjappe.Services.Bookings;
using Microsoft.AspNetCore.Mvc;

namespace IvarsSykkelsjappe.Areas.Mechanic.Controllers
{
    public class BookingsController : MechanicController
    {
        private readonly IBookingService bookingService;

        public BookingsController(IBookingService bookingService)
        {
            this.bookingService = bookingService;
        }

        public IActionResult AllBookings()
        {
            var bookings = this.bookingService.GetAllBookings();
            return View(bookings);
        }
    }
}
