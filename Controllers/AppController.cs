using DutchTreat.Data;
using DutchTreat.Services;
using DutchTreat.ViewModels;

using Microsoft.AspNetCore.Mvc;

using System.Linq;

namespace DutchTreat.Controllers
{
    public class AppController : Controller
    {
        private readonly IMailService mailService;
        private readonly DutchContext dutchContext;

        public AppController(IMailService mailService, DutchContext dutchContext)
        {
            this.mailService = mailService;
            this.dutchContext = dutchContext;
        }

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
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                mailService.SendMessage("gbphilipsutton@gmail.com", model.Subject, $"From: {model.Name} - {model.Email}, Message: {model.Message}");
                ViewBag.UserMessage = "Mail Sent";
                ModelState.Clear();
            }
            return View();
        }
        public IActionResult About()
        {
            return View();
        }

        public IActionResult Shop()
        {
            var results = dutchContext.Products.ToList();
            return View();
        }
    }
}
