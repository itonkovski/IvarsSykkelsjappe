using IvarsSykkelsjappe.Models.Bikes;
using IvarsSykkelsjappe.Services.Bikes;
using Microsoft.AspNetCore.Mvc;

namespace IvarsSykkelsjappe.Areas.Admin.Controllers
{
    public class BikesController : AdminController
    {
        private readonly IBikeService bikeService;

        public BikesController(IBikeService bikeService)
        {
            this.bikeService = bikeService;
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
    }
}
