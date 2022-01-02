﻿using Microsoft.AspNetCore.Mvc;

using System;

namespace DutchTreat.Controllers
{
    public class AppController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contact()
        {
            ViewBag.Title = "Contact Us";

            return View();
        }

        public IActionResult About()
        {
            ViewBag.Title = "About";

            return View();
        }
    }
}
