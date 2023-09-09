using EShop.Application.Services.Interfaces;
using EShop.Application.ViewModels.User;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {

        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUser(AddUserViewModel addUserViewModel)
        {
            if(!ModelState.IsValid)
            {
                return View(addUserViewModel);
            }
            if (await _userService.IsExistUserEmailService(addUserViewModel.Email))
            {
                return View(addUserViewModel);
            }

            await _userService.CreateUserAsync(addUserViewModel);
            ViewBag.IsSuccess = true;
            return View( addUserViewModel);
        }
    }
}
