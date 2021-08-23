﻿using IvarsSykkelsjappe.Models.Bookings;
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
            if (!ModelState.IsValid)
            {
                return View(bookingForm);
            }
            this.bookingService.Add(bookingForm);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AllBookings(BookingViewModel bookingModel)
        {
            var bookings = this.bookingService.GetAllBookings();
            return View(bookings);
        }

        public IActionResult Delete(int id)
        {
            this.bookingService.Delete(id);
            return RedirectToAction("Index", "Home");
        }
    }
}
