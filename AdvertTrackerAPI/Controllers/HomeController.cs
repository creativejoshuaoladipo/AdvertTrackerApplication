using AdvertTrackerAPI.DbContexts;
using AdvertTrackerAPI.Models;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AdvertTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db )
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var job=   BackgroundJob.Enqueue(() => UpdateCount("BellaNija"));

            var response = new ResponseModel
            {
                HttpStatus = HttpStatusCode.OK,
                Message = "The User Count was successfully updated",
                url = "https://mail.google.com/mail/u/0/#inbox"
            };
            
            return Ok(response);
        }

        [NonAction]
        public  void UpdateCount(string vendorName)
        {
            

           var vendor = _db.Vendors.FirstOrDefault(u => u.VendorName == vendorName);

            if(vendor == null)
            {
                var newVendor = new Vendor
                {
                    LastVisted = DateTime.UtcNow,
                    VendorName = vendorName,
                    VisitorCount = 1
                };

                _db.Add(newVendor);
                _db.SaveChanges();
            }
            else
            {
                vendor.VisitorCount += 1;
                vendor.LastVisted = DateTime.UtcNow;
                _db.SaveChanges();
            }
        }
    }
}
