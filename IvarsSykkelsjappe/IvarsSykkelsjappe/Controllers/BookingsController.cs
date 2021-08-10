using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IvarsSykkelsjappe.Controllers
{
    public class BookingsController : Controller
    {
        public BookingsController()
        {

        }

        [Authorize]
        public IActionResult BookTime()
        {
            return View();
        }
    }
}
