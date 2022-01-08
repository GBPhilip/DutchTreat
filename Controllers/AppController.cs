using DutchTreat.ViewModels;

using Microsoft.AspNetCore.Mvc;


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
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid) _mailService.SendMail("gbphilipsutton@gmail.com", model.Subject, $"From: {model.Name} - {model.Email}, Message: {model.Message}")
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
    }
}
