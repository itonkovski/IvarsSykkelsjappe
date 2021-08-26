using AutoMapper;
using IvarsSykkelsjappe.Models.Bikes;
using IvarsSykkelsjappe.Services.Bikes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IvarsSykkelsjappe.Areas.Admin.Controllers
{
    public class BikesController : AdminController
    {
        private readonly IBikeService bikeService;
        private readonly IMapper mapper;

        public BikesController(IBikeService bikeService, IMapper mapper)
        {
            this.bikeService = bikeService;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
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

            this.bikeService.Add(bikeForm);
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
            return RedirectToAction(nameof(All));
        }
    }
}
