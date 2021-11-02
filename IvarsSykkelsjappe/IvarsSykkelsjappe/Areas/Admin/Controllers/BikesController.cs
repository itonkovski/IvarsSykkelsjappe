using AutoMapper;
using IvarsSykkelsjappe.Models.Bikes;
using IvarsSykkelsjappe.Services.Bikes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace IvarsSykkelsjappe.Areas.Admin.Controllers
{
    using static WebConstants;

    public class BikesController : AdminController
    {
        private readonly IBikeService bikeService;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment environment;

        public BikesController(IBikeService bikeService, IMapper mapper, IWebHostEnvironment environment)
        {
            this.bikeService = bikeService;
            this.mapper = mapper;
            this.environment = environment;
        }

        public IActionResult All()
        {
            var bikes = this.bikeService.GetAll();
            return View(bikes);
        }

        public IActionResult Edit(int id)
        {
            if (!User.IsInRole("Admin"))
            {
                return Unauthorized();
            }

            var bike = this.bikeService.GetEdit(id);

            var bikeForm = this.mapper.Map<BikeFormModel>(bike);

            bikeForm.Categories = this.bikeService.GetBikeCategories();

            return View(bikeForm);

            //without AutoMapper
            //return View(new BikeFormModel
            //{
            //    Brand = bike.Brand,
            //    Model = bike.Model,
            //    Description = bike.Description,
            //    Price = bike.Price,
            //    ImageUrl = bike.ImageUrl,
            //    Year = bike.Year,
            //    BikeCategoryId = bike.BikeCategoryId,
            //    Categories = this.bikeService.GetBikeCategories()
            //});
        }

        [HttpPost]
        public IActionResult Edit(BikeFormModel bikeModel, int id)
        {

            if (!ModelState.IsValid)
            {
                bikeModel.Categories = this.bikeService.GetBikeCategories();
                return View(bikeModel);
            }

            this.bikeService.Edit(bikeModel, id);
            TempData[GlobalMessageKey] = "The bike was edited successfully.";
            return RedirectToAction(nameof(All));
        }

        public IActionResult Create()
        {
            return View(new BikeFormModel
            {
                Categories = this.bikeService.GetBikeCategories()
            });
        }

        [HttpPost]
        public IActionResult Create(BikeFormModel bikeForm)
        {
            if (!ModelState.IsValid)
            {
                bikeForm.Categories = this.bikeService.GetBikeCategories();
                return View(bikeForm);
            }

            this.bikeService.Add(bikeForm, $"{this.environment.WebRootPath}/images");
            TempData[GlobalMessageKey] = "The bike was added successfully.";
            return RedirectToAction(nameof(All));
        }

        public IActionResult Details(int id)
        {
            var bike = this.bikeService.GetDetails(id);
            return View(bike);
        }

        public IActionResult Delete(int id)
        {
            this.bikeService.Delete(id);
            TempData[GlobalMessageKey] = "The bike was deleted successfully.";
            return RedirectToAction(nameof(All));
        }
    }
}
