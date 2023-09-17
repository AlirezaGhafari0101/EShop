using EShop.Application.Services.Interfaces;
using EShop.Application.ViewModels.User;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Areas.Admin.Controllers
{

    public class UserController : BaseController
    {
        #region Fields
        private IUserService _userService;
        #endregion

        #region Constructor

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        #endregion

        #region Actions

        #region Index

        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllUsersAsync();
            //bool userCreated = TempData["UserCreated"] as bool? ?? false;
            //if (userCreated)
            //    ViewBag.showModal = true;
            return View(users);
        }

        #endregion

        #region AddUser
        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUser(AddUserViewModel addUserViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(addUserViewModel);
            }
            if (await _userService.IsExistUserEmailService(addUserViewModel.Email))
            {
                ModelState.AddModelError("Email", "کاربری با این ایمیل قبلا در سایت ثبت نام کرده است.");
                return View(addUserViewModel);

            }

            await _userService.CreateUserAsync(addUserViewModel);
            TempData["UserCreated"] = true;
            return RedirectToAction("Index");
        }

        #endregion

        #region DeleteUser
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.DeleteUserByIdAsync(id); 
            return Json(new { status = "success" });
        }
        #endregion

        #region EditUser
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
            TempData["UserEdited"] = true;
            return RedirectToAction("Index");
        }
    }
    #endregion



    #endregion
}
