using Microsoft.AspNetCore.Mvc;

using System;

namespace DutchTreat.Controllers
{
    public class AppController : Controller
    {
        public IActionResult Index()
        {
            throw new InvalidProgramException("Bad thinmgs happens to good developers");
            return View();
        }
    }
}
