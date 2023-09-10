using EShop.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            return View(await _userService.GetUserInforServiceAsync(User.Identity.Name));
        }

        
        public async Task<IActionResult> EditUser()
        {
            return View(await _userService.GetUserInforServiceAsync(User.Identity.Name));
        }
    }
}
