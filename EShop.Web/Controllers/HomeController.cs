using EShop.Application.Services.Interfaces;
using EShop.Application.ViewModels.ContactUs;
using EShop.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IContactUsService _contactUsService;

        public HomeController(ILogger<HomeController> logger,IContactUsService contactUsService)
        {
            _logger = logger;
            _contactUsService = contactUsService;
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
        public async Task<IActionResult> ContactUs(CreateContactUsViewModel contactUsViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(contactUsViewModel);
            }

            await _contactUsService.CreateQuestionServiceAsync(contactUsViewModel);
            ViewBag.IsSended = true;
            return View();
        }


    }
}