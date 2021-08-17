﻿using IvarsSykkelsjappe.Models.Bikes;
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
        [Authorize(Roles = "Admin")]
        public IActionResult AllAdmin()
        {
            var bikes = this.bikeService.GetAll();
            return View(bikes);
        }

        [Authorize]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var bike = this.bikeService.GetDetails(id);
            return View(bike);
        }

        [Authorize]
        public IActionResult AllCustomer(AllBikesQueryModel queryModel)
        {
            this.bikeService.AllSearch(queryModel);
            return View(queryModel);
        }

        [Authorize]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View(new BikeFormModel
            {
                Categories = this.bikeService.GetBikeCategories()
            });
        }


        [HttpPost]
        [Authorize]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(BikeFormModel bikeForm)
        {
            if (!ModelState.IsValid)
            {
                bikeForm.Categories = this.bikeService.GetBikeCategories();
                return View(bikeForm);
            }

            this.bikeService.Add(bikeForm);
            return RedirectToAction(nameof(AllCustomer));
        }

        [Authorize]
        public IActionResult Details(int id)
        {
            var bike = this.bikeService.GetDetails(id);
            return View(bike);
        }
    }
}
