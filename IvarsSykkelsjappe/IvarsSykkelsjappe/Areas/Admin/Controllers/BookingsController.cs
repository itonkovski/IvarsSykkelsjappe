using System;
using System.Threading.Tasks;
using IvarsSykkelsjappe.Services.Bookings;
using Microsoft.AspNetCore.Mvc;

namespace IvarsSykkelsjappe.Areas.Admin.Controllers
{
    using static WebConstants;

    public class BookingsController : AdminController
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

        [HttpPost]
        public async Task<IActionResult>  Delete(int id)
        {
            await this.bookingService.DeleteAsync(id);
            TempData[GlobalMessageKey] = "The booking was deleted successfully.";
            return RedirectToAction(nameof(AllBookings));
        }
    }
}
