﻿using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using IvarsSykkelsjappe.Models.Bikes;
using IvarsSykkelsjappe.Services.Bikes;
using IvarsSykkelsjappe.Services.Messaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IvarsSykkelsjappe.Controllers
{
    using static WebConstants;

    public class BikesController : Controller
    {
        private readonly IBikeService bikeService;
        private readonly IMapper mapper;
        private readonly IEmailSender emailSender;

        public BikesController(IBikeService bikeService, IMapper mapper, IEmailSender emailSender)
        {
            this.bikeService = bikeService;
            this.mapper = mapper;
            this.emailSender = emailSender;
        }

        //[Authorize]
        //[Authorize(Roles = "Admin")]
        //public IActionResult AllAdmin()
        //{
        //    var bikes = this.bikeService.GetAll();
        //    return View(bikes);
        //}

        //[Authorize]
        //[Authorize(Roles = "Admin")]
        //public IActionResult Edit(int id)
        //{
        //    if (!User.IsInRole("Admin"))
        //    {
        //        return Unauthorized();
        //    }

        //    var bike = this.bikeService.GetEdit(id);

        //    var bikeForm = this.mapper.Map<BikeFormModel>(bike);

        //    bikeForm.Categories = this.bikeService.GetBikeCategories();

        //    return View(bikeForm);

        //    //without AutoMapper
        //    //return View(new BikeFormModel
        //    //{
        //    //    Brand = bike.Brand,
        //    //    Model = bike.Model,
        //    //    Description = bike.Description,
        //    //    Price = bike.Price,
        //    //    ImageUrl = bike.ImageUrl,
        //    //    Year = bike.Year,
        //    //    BikeCategoryId = bike.BikeCategoryId,
        //    //    Categories = this.bikeService.GetBikeCategories()
        //    //});
        //}

        //[Authorize]
        //[HttpPost]
        //[Authorize(Roles = "Admin")]
        //public IActionResult Edit(BikeFormModel bikeModel, int id)
        //{

        //    if (!ModelState.IsValid)
        //    {
        //        bikeModel.Categories = this.bikeService.GetBikeCategories();
        //        return View(bikeModel);
        //    }

        //    this.bikeService.Edit(bikeModel, id);
        //    return RedirectToAction(nameof(AllCustomer));
        //}

        [Authorize(Roles = "Admin, User, Mechanic")]
        public IActionResult AllCustomer(AllBikesQueryModel queryModel)
        {
            this.bikeService.AllSearch(queryModel);
            return View(queryModel);
        }

        //[Authorize]
        //[Authorize(Roles = "Admin")]
        //public IActionResult Create()
        //{
        //    return View(new BikeFormModel
        //    {
        //        Categories = this.bikeService.GetBikeCategories()
        //    });
        //}


        //[HttpPost]
        //[Authorize]
        //[Authorize(Roles = "Admin")]
        //public IActionResult Create(BikeFormModel bikeForm)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        bikeForm.Categories = this.bikeService.GetBikeCategories();
        //        return View(bikeForm);
        //    }

        //    this.bikeService.Add(bikeForm);
        //    return RedirectToAction(nameof(AllCustomer));
        //}

        [Authorize]
        public IActionResult Details(int id)
        {
            var bike = this.bikeService.GetDetails(id);
            return View(bike);
        }

        //[Authorize]
        //[Authorize(Roles = "Admin")]
        //public IActionResult Delete(int id)
        //{
        //    this.bikeService.Delete(id);
        //    return RedirectToAction(nameof(AllAdmin));
        //}

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> SendToEmail(int id)
        {
            var bike = this.bikeService.SendBikeByEmail(id);
            var html = new StringBuilder();
            html.AppendLine($"<h1>{bike.Brand}</h1>");
            html.AppendLine($"<h3>{bike.Model}</h3>");
            //html.AppendLine($"<img src=\"{bike.ImageUrl}\" />");
            await this.emailSender.SendEmailAsync("info@ivarssykkelsjappe.no", "IvarsSykkelsjappe", "namari6705@bubblybank.com", bike.Brand, html.ToString());
            TempData[GlobalMessageKey] = "The email was sent successfully.";
            return this.RedirectToAction(nameof(this.Details), new { id });
        }
    }
}