using EShop.Application.Convertors;
using EShop.Application.Senders;
using EShop.Application.Services.Interfaces;
using EShop.Application.ViewModels.Account;
using EShop.Domain.Models.Users;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        #region SignUp
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


            string body = _viewRender.RenderToStringAsync("_ActiveEmail", user);
            SendEmail.Send(user.Email, "فعالسازی", body);

            ViewBag.IsSuccess = true;



            //return View("SuccessRegister", user);
            return View();

        }

        #region Active Account
        //[Route("ActiveAccount/{id}")]
        //public async Task<IActionResult> ActiveAccount(string id)
        //{
        //    ViewBag.IsActive = await _accountService.ActiveAccountService(id);
        //    return View();
        //}

        [Route("ActiveAccount")]
        public async Task<IActionResult> ActiveAccount()
        {

            return View();
        }

        [Route("ActiveAccount")]
        [HttpPost]
        public async Task<IActionResult> ActiveAccount(ActiveAccountViewModel activeAccount)
        {
            if (!ModelState.IsValid) { View(activeAccount); }

            User user = await _accountService.ActiveAccountService(activeAccount.ActiveCode);
            if (user == null)
            {
                ViewBag.IsSuccess = false;
                return View();
            }

            return View("SuccessRegister", user);
        }
        #endregion

        #endregion


        #region SingIn
        [Route("Login")]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (!ModelState.IsValid) { return View(); }

            User user = await _accountService.LoginUserService(login);
            if (user != null)
            {
                if (user.IsActive)
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                        new Claim(ClaimTypes.Name,user.Email)
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    var properties = new AuthenticationProperties
                    {
                        IsPersistent = true
                    };
                    await HttpContext.SignInAsync(principal, properties);
                    ViewBag.IsSuccess = true;
                    return View();

                }
                else
                {
                    ModelState.AddModelError("Email", "حساب کاربری شما فعال نمی باشد");

                }

            }
            ModelState.AddModelError("Email", "کاربری با مشخصات وارد شده یافت نشد");
            return View(login);
        }
        #endregion


        #region LogOut
        [Route("LogOut")]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Login");
        }
        #endregion


    }
}
