using System.Security.Claims;
using IvarsSykkelsjappe.Models.Bookings;
using IvarsSykkelsjappe.Services.Bookings;
using IvarsSykkelsjappe.Services.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IvarsSykkelsjappe.Controllers
{
    using static WebConstants;

    public class BookingsController : Controller
    {
        private readonly IBookingService bookingService;
        private readonly IProductService productService;

        public BookingsController(IBookingService bookingService, IProductService productService)
        {
            this.bookingService = bookingService;
            this.productService = productService;
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
            TempData[GlobalMessageKey] = "The booking was added successfully.";

            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("AllBookings", "Bookings", new { area = "Admin" });

            }
            else if (User.IsInRole("User"))
            {
                return RedirectToAction(nameof(MyBookings));
            }
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [Authorize(Roles = "Mechanic")]
        public IActionResult AllBookingsForToday()
        {
            var bookings = this.bookingService.GetAllBookingsForToday();
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

        

        [HttpPost]
        [Authorize]
        [Authorize(Roles = "Mechanic")]
        public IActionResult TakeOrder(int id)
        {
            var mechanicId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var mechanicName = User.FindFirstValue(ClaimTypes.Name);
            this.bookingService.TakeMechanic(id, mechanicId, mechanicName);
            TempData[GlobalMessageKey] = "The order is yours now.";
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


        public IActionResult OrderEdit(int id)
        {
            var order = this.bookingService.GetByMechanic(id);

            return View(new OrderDetailsViewModel
            {
                FullName = order.FullName,
                Email = order.Email,
                PhoneNumber = order.PhoneNumber,
                Details = order.Details,
                TimeSlot = order.TimeSlot,
                AssistanceId = order.AssistanceId,
                Assistances = this.bookingService.GetAssistances(),
                Products = this.productService.GetAll(),
                MechanicDetails = order.MechanicDetails,
                PickUpTime = order.PickUpTime
            });
        }
    }
}
