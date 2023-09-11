using EShop.Application.Services.Interfaces;
using EShop.Application.ViewModels.User.UserPanel;
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
            int usaerId =int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return View(await _userService.GetUserInforServiceAsync(usaerId));
        }

        [HttpGet("UserPanel/EditProfile")]
        public async Task<IActionResult> EditProfile()
        {
            int usaerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            return View(await _userService.GetDataForEditProfileUserAsync(usaerId));
        }

        [HttpPost("UserPanel/EditProfile")]
        public async Task<IActionResult> EditProfile(EditProfileViewModel editViewModel,int Id)
        {
            if (!ModelState.IsValid)
                return View(editViewModel);

            _userService.EditUserProfileAsync(editViewModel, Id);
            return Redirect("/UserPanel/Home/Index");
        }
    }
}
