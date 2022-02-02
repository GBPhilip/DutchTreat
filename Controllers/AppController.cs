using DutchTreat.Data;
using DutchTreat.Services;
using DutchTreat.ViewModels;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Linq;

namespace DutchTreat.Controllers
{
    public class AppController : Controller
    {
        private readonly IMailService mailService;
        private readonly IDutchRepository dutchRepository;

        public AppController(IMailService mailService, IDutchRepository dutchRepository)
        {
            this.mailService = mailService;
            this.dutchRepository = dutchRepository;
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

        [Authorize]
        public IActionResult Shop()
        {
            var results = dutchRepository.GetAllProducts()
                .OrderBy(p => p.Category);
            return View(results);
        }
    }
}
