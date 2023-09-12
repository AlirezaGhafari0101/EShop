﻿using EShop.Application.Services.Interfaces;
using EShop.Application.ViewModels.User.UserPanel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EShop.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class HomeController : Controller
    {
        private IUserService _userService;
        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            int usaerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return View(await _userService.GetUserInforServiceAsync(usaerId));
        }

        [HttpGet("UserPanel/EditProfile")]
        public async Task<IActionResult> EditProfile()
        {
            int usaerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            return View(await _userService.GetDataForEditProfileUserAsync(usaerId));
        }

        [HttpPost("UserPanel/EditProfile")]
        public async Task<IActionResult> EditProfile(EditProfileViewModel editViewModel, int Id)
        {
            if (!ModelState.IsValid)
                return View(editViewModel);

            await _userService.EditUserProfileAsync(editViewModel, Id);
            //Log Out User
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Redirect("/Login");

        }


        [HttpGet("UserPanel/ChangePassword")]
        public async Task<IActionResult> ChangePassword()
        {


            return View();
        }

        [HttpPost("UserPanel/ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel changePasswordViewModel)
        {
            if (!ModelState.IsValid)
                return View(changePasswordViewModel);
            int usaerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            bool result = await _userService.ChangePasswordAsync(changePasswordViewModel, usaerId);

            if (!result)
            {
                ModelState.AddModelError("CurrentPassword", "رمز عبور فعلی اشتباه وارد شده");
                return View(changePasswordViewModel);
            }

            return Redirect("/Login");
        }
    }
}
