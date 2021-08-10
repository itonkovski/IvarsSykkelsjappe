using System;
using IvarsSykkelsjappe.Models.Bikes;
using IvarsSykkelsjappe.Services.Bikes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IvarsSykkelsjappe.Controllers
{
    public class BikesController : Controller
    {
        private readonly IBikeService bikeService;

        public BikesController(IBikeService bikeService)
        {
            this.bikeService = bikeService;
        }

        [Authorize]
        public IActionResult OurBikes()
        {
            return this.View();
        }

        [Authorize]
        public IActionResult Create()
        {
            return View(new BikeFormModel
            {
                Categories = this.bikeService.GetBikeCategories()
            });
        }


        [HttpPost]
        [Authorize]
        public IActionResult Create(BikeFormModel bikeForm)
        {
            this.bikeService.Add(bikeForm);
            return RedirectToAction(nameof(OurBikes));
        }
    }
}
