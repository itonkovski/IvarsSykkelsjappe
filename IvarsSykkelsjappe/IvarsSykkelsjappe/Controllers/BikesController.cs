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
        public IActionResult AllAdmin()
        {
            var bikes = this.bikeService.GetAll();
            return View(bikes);
        }

        [Authorize]
        public IActionResult AllCustomer()
        {
            var bikes = this.bikeService.AllBikes();
            return View(bikes);
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
