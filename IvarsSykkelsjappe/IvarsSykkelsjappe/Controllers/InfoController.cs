using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace IvarsSykkelsjappe.Controllers
{
    public class InfoController : Controller
    {
        private readonly IDistributedCache cacheService;
        private readonly ILogger<InfoController> logger;

        public InfoController(IDistributedCache cacheService, ILogger<InfoController> logger)
        {
            this.cacheService = cacheService;
            this.logger = logger;
        }

        private DateTime GetData()
        {
            Thread.Sleep(5000);
            return DateTime.Now;
        }

        //There is a way to cache with attribites
        //[ResponseCache(Duration = 24 * 60 * 60, Location = ResponseCacheLocation.Client)]
        //Or directly into the view
        //<cache expires-after="TimeSpan.FromMinutes(10)">
        //    <vc:registered-users title = "Registered users" ></ vc:registered-users>
        //</cache>
        public async Task<IActionResult> Date()
        {
            this.logger.LogCritical(12345, "User asked for the date.");

            var dataAsString = await this.cacheService.GetStringAsync("DateTimeInfo");
            DateTime data;
            if (dataAsString == null)
            {
                data = this.GetData();
                await this.cacheService.SetStringAsync("DateTimeInfo",
                    JsonConvert.SerializeObject(data),
                    new DistributedCacheEntryOptions
                    {
                        SlidingExpiration = new TimeSpan(0, 0, 10)
                    });
            }
            else
            {
                data = JsonConvert.DeserializeObject<DateTime>(dataAsString);
            }

            return this.Content(data.ToString());
        }
    }
}
