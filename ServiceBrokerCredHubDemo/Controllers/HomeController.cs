using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CredHubDemoUI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Steeltoe.Extensions.Configuration.CloudFoundry;

namespace CredHubDemoUI.Controllers
{
    public class HomeController : Controller
    {
        private CloudFoundryServicesOptions CloudFoundryServices { get; set; }

        public HomeController(IOptions<CloudFoundryServicesOptions> servOptions)
        {
            if (servOptions != null)
                CloudFoundryServices = servOptions.Value;
        }

        public IActionResult Index([FromServices]CityContext context)
        {
            var cities = 
                context
                    .Cities
                    .Select(c => new CityViewModel
                        {
                            CityName = c.CityName, 
                            Id = c.Id
                        })
                    .OrderBy(c => c.Id).ToList();
            return View(cities);
        }

        public IActionResult Services()
        {
            return View(CloudFoundryServices ?? new CloudFoundryServicesOptions());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
