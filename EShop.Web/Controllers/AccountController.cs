using EShop.Application.Convertors;
using EShop.Application.Senders;
using EShop.Application.Services.Interfaces;
using EShop.Application.ViewModels.Account;
using EShop.Domain.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Controllers
{
    public class AccountController : Controller
    {
        private IAccountService _accountService;
        private IViewRenderService _viewRender;
        public AccountController(IAccountService accountService, IViewRenderService viewRenderService)
        {
            _accountService = accountService;
            _viewRender = viewRenderService;
        }
        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid) { View(register); }


            if (await _accountService.IsExistUserEmailService(register.Email))
            {
                ModelState.AddModelError("Email", "ایمیل وارد معتبرنمی باشد");
                return View(register);
            };



            User user = await _accountService.UserRegister(register);
            //region Send Activation Email

            //string body = _viewRender.RenderToStringAsync("_ActiveEmail", user);
            //SendEmail.Send(user.Email, "فعالسازی", body);





            return View("SuccessRegister");
        }

        #region Active Account
        [Route("ActiveAccount")]
        public IActionResult ActiveAccount(string id)
        {
            ViewBag.IsActive = _accountService.ActiveAccountService(id);
            return View();
        }
        #endregion


    }
}
