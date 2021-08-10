using System;
using Microsoft.AspNetCore.Mvc;

namespace IvarsSykkelsjappe.Controllers
{
    public class PopUpsController : Controller
    {
        public PopUpsController()
        {

        }

        public IActionResult Location()
        {
            return View();
        }
    }
}
