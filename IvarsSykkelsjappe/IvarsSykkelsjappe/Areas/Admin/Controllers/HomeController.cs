using System.Diagnostics;
using System.Linq;
using IvarsSykkelsjappe.Models;
using IvarsSykkelsjappe.Services.Bikes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IvarsSykkelsjappe.Areas.Admin.Controllers
{
    public class HomeController : AdminController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBikeService bikeService;

        public HomeController(ILogger<HomeController> logger, IBikeService bikeService)
        {
            _logger = logger;
            this.bikeService = bikeService;
        }

        public IActionResult Index()
        {
            var bikes = this.bikeService
                .Latest()
                .ToList();
            return View(bikes);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
