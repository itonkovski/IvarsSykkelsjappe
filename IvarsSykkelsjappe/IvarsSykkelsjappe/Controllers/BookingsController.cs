using System;
using Microsoft.AspNetCore.Mvc;

namespace IvarsSykkelsjappe.Controllers
{
    public class BookingsController : Controller
    {
        public BookingsController()
        {

        }

        public IActionResult BookTime()
        {
            return View();
        }
    }
}
