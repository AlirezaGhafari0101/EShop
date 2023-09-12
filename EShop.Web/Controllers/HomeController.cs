using EShop.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("contact-us")]
        public IActionResult ContactUs()
        {
            return View();
        }


        [HttpPost("contact-us")]
        public IActionResult ContactUs(ContactUsViewModel contactUsViewModel)
        {
            return View();
        }


    }
}