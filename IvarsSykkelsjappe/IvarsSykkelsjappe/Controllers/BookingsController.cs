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
            //if userName is needed
            //var userName = User.FindFirstValue(ClaimTypes.Name);

            //var clientId = this.User.GetId();
            var clientId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!ModelState.IsValid)
            {
                return View(bookingForm);
            }
            this.bookingService.Add(bookingForm, clientId);

            if (User.IsInRole("Admin"))
            {
                return RedirectToAction(nameof(AllBookings));
            }
            else if (User.IsInRole("User"))
            {
                return RedirectToAction(nameof(MyBookings));
            }
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [Authorize(Roles = "Admin, Mechanic")]
        public IActionResult AllBookings()
        {
            var bookings = this.bookingService.GetAllBookings();
            return View(bookings);
        }

        [Authorize]
        [Authorize(Roles = "User")]
        public IActionResult MyBookings()
        {
            var clientId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var bookings = this.bookingService.MyBookings(clientId);
            return View(bookings);
        }

        [Authorize]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            this.bookingService.Delete(id);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [Authorize]
        [Authorize(Roles = "Mechanic")]
        public IActionResult TakeOrder(int id)
        {
            var mechanicId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            this.bookingService.TakeMechanic(id, mechanicId);
            return RedirectToAction(nameof(MyOrders));
        }

        [Authorize]
        [Authorize(Roles = "Mechanic")]
        public IActionResult MyOrders()
        {
            var mechanicId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var bookings = this.bookingService.MyOrders(mechanicId);
            return View(bookings);
        }
    }
}
