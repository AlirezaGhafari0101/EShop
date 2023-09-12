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
        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllUsersAsync();
            return View(users);
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
                ModelState.AddModelError("Email", "کاربری با این ایمیل قبلا در سایت ثبت نام کرده است.");
                return View(addUserViewModel);

            }

            await _userService.CreateUserAsync(addUserViewModel);
            ViewBag.IsSuccess = true;
            return RedirectToAction("Index");
        }


       
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.DeleteUserByIdAsync(id);
            return Json(new {status="success"});
        }

        public async Task<ActionResult> EditUser(int id)
        {
            var user = await _userService.GetUserByIdForEditAsync(id);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(EditUserViewModel userViewModel, int id)
        {
            if (!ModelState.IsValid)
            {
                return View(userViewModel);
            }
            
            await _userService.EditUserFromAdminAsync(userViewModel, id);
            return RedirectToAction("Index");
        }
    }
}
