using System;
using Microsoft.AspNetCore.Mvc;

namespace IvarsSykkelsjappe.Controllers
{
    public class BikesController : Controller
    {
        public BikesController()
        {
        }

        public IActionResult OurBikes()
        {
            return View();
        }
    }
}
