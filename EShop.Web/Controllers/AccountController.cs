using EShop.Application.Convertors;
using EShop.Application.Senders;
using EShop.Application.Services.Interfaces;
using EShop.Application.ViewModels;
using EShop.Application.ViewModels.Account;
using EShop.Domain.Models.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost("register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid) {

               return View(register);
            
            }


            if (await _accountService.IsExistUserEmailServiceAsync(register.Email)==true)
            {
                ModelState.AddModelError("Email", "حساب کاربری با این ایمیل موجود می باشد");
                return View(register);
            };



            UserViewModel user = await _accountService.UserRegisterAsync(register);


            string body = _viewRender.RenderToStringAsync("_ActiveEmail", user);
            SendEmail.Send(user.Email, "فعالسازی", body);

            return View("SuccessRegister",user);


        }



        #endregion

        #region Active Account


        [Route("activeaccount/{id}")]
        public async Task<IActionResult> ActiveAccount(string id)
        {
            UserViewModel user = await _accountService.ActiveAccountServiceAsync(id);

            return RedirectToAction("login", user.IsActive);
        }


        #endregion


        #region SingIn
        [Route("login")]
        public IActionResult Login(bool IsActive=false)
        {
            ViewBag.IsActive = IsActive;
            return View();
        }


        [HttpPost("login")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (!ModelState.IsValid) { return View(); }

            UserViewModel user = await _accountService.LoginUserServiceAsync(login);
            if (user != null)
            {
                if (user.IsActive)
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                        new Claim(ClaimTypes.Name,user.Email),
                        new Claim("UserAvatar",user.Avatar),
                        new Claim("IsAdmin",user.IsAdmin.ToString())



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
        [Route("logout")]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }
        #endregion


        #region ForGotPassword
        [Route("forgotpassword")]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost("forgotpassword")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel forgotPassword)
        {
            if (!ModelState.IsValid) { return View(forgotPassword); };

            UserViewModel user = await _accountService.ForgotPasswordServiceAsync(forgotPassword.Email);

            if (user != null)
            {
                string body = _viewRender.RenderToStringAsync("_ForgotPasswordEmail", user);
                SendEmail.Send(user.Email, "فعالسازی", body);
                ViewBag.IsSended = true;
                return View();
            }
            ModelState.AddModelError("Email", "حساب کاربری یافت نشد");
            return View(forgotPassword);
        }





        #endregion

        #region ResetPassword
        public IActionResult ResetPassword(string id)
        {
            return View(new ResetPasswordViewModel()
            {
                ActiveCode = id
            });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {

            if (!ModelState.IsValid) { return View(resetPasswordViewModel); };

            await _accountService.ResetPasswordAsync(resetPasswordViewModel);

            return Redirect("/Login");


        }
        #endregion


    }
}
