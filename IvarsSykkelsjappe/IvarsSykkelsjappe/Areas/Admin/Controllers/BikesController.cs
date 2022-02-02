using System.Threading.Tasks;
using AutoMapper;
using IvarsSykkelsjappe.Models.Bikes;
using IvarsSykkelsjappe.Services.Bikes;
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

            //var bikeForm = this.mapper.Map<BikeFormModel>(bike);

            //bikeForm.Categories = this.bikeService.GetBikeCategories();

            //return View(bikeForm);

            //without AutoMapper
            return View(new BikeFormModel
            {
                Brand = bike.Brand,
                Model = bike.Model,
                Description = bike.Description,
                Price = bike.Price,
                Year = bike.Year,
                //ImageUrl = bike.ImageUrl, 
                BikeCategoryId = bike.BikeCategoryId,
                Categories = this.bikeService.GetBikeCategories()
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BikeFormModel bikeModel, int id)
        {
            if (!ModelState.IsValid)
            {
                bikeModel.Categories = this.bikeService.GetBikeCategories();
                return View(bikeModel);
            }

            await this.bikeService.EditAsync(bikeModel, id);
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
        public async Task<IActionResult> Create(BikeFormModel bikeForm)
        {
            if (!ModelState.IsValid)
            {
                bikeForm.Categories = this.bikeService.GetBikeCategories();
                return View(bikeForm);
            }

            await this.bikeService.AddAsync(bikeForm, $"{this.environment.WebRootPath}/images");
            TempData[GlobalMessageKey] = "The bike was added successfully.";
            return RedirectToAction(nameof(All));
        }

        public IActionResult Details(int id)
        {
            var bike = this.bikeService.GetDetails(id);
            return View(bike);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await this.bikeService.DeleteAsync(id);
            TempData[GlobalMessageKey] = "The bike was deleted successfully.";
            return RedirectToAction(nameof(All));
        }
    }
}
