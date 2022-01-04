using Microsoft.AspNetCore.Mvc;

using System;

namespace DutchTreat.Controllers
{
    public class AppController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("contact")]
        public IActionResult Contact()
        {
            ViewBag.Title = "Contact Us";
            return View();
        }

        [HttpPost("contact")]
        public IActionResult Contact(object model)
        {
            return View();
        }
        public IActionResult About()
        {
            ViewBag.Title = "About";

            return View();
        }
    }
}
