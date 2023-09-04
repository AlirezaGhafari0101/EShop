﻿using EShop.Web.Models;
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

     
    }
}