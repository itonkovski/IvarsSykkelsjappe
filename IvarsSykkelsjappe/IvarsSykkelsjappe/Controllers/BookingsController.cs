using System.Security.Claims;
using IvarsSykkelsjappe.Infrastructure.Extensions;
using IvarsSykkelsjappe.Models.Bookings;
using IvarsSykkelsjappe.Services.Bookings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IvarsSykkelsjappe.Controllers
{
    public class BookingsController : Controller
    {
        private readonly IBookingService bookingService;

        public BookingsController(IBookingService bookingService)
        {
            this.bookingService = bookingService;
        }

        [Authorize]
        [Authorize(Roles = "Admin, User")]
        public IActionResult BookTime()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [Authorize(Roles = "Admin, User")]
        public IActionResult BookTime(BookingFormModel bookingForm)
        {
            //var clientId = this.User.GetId();
            var clientId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!ModelState.IsValid)
            {
                return View(bookingForm);
            }
            this.bookingService.Add(bookingForm, clientId);
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [Authorize(Roles = "Admin, Mechanic")]
        public IActionResult AllBookings()
        {
            var userName = User.FindFirstValue(ClaimTypes.Name);
            var bookings = this.bookingService.GetAllBookings(userName);
            return View(bookings);
        }

        [Authorize]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            this.bookingService.Delete(id);
            return RedirectToAction("Index", "Home");
        }
    }
}
