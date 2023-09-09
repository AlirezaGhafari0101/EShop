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
        [Route("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid) { View(register); }


            if (await _accountService.IsExistUserEmailServiceAsync(register.Email))
            {
                ModelState.AddModelError("Email", "ایمیل وارد معتبرنمی باشد");
                return View(register);
            };



            UserViewModel user = await _accountService.UserRegisterAsync(register);


            string body = _viewRender.RenderToStringAsync("_ActiveEmail", user);
            SendEmail.Send(user.Email, "فعالسازی", body);

            ViewBag.IsSuccess = true;



            //return View("SuccessRegister", user);
            return View();

        }

        #region Active Account


        [Route("activeaccount")]
        public IActionResult ActiveAccount()
        {

            return View();
        }

        [HttpPost("activeaccount")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActiveAccount(ActiveAccountViewModel activeAccount)
        {
            if (!ModelState.IsValid) { View(activeAccount); }

            UserViewModel user = await _accountService.ActiveAccountServiceAsync(activeAccount.ActiveCode);
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
        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost("login")]
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
        [Route("logout")]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Login");
        }
        #endregion


        #region ForGotPassword
        [Route("forgotpassword")]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost("forgotpassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel forgotPassword)
        {
            if (!ModelState.IsValid) { return View(forgotPassword); };

            User user = await _accountService.ForgotPasswordServiceAsync(forgotPassword.Email);

            if (user != null)
            {
                string body = _viewRender.RenderToStringAsync("_ForgotPasswordEmail", user);
                SendEmail.Send(user.Email, "فعالسازی", body);

                return Redirect($"/CheckForgotPassword");
            }
            ModelState.AddModelError("Email", "حساب کاربری یافت نشد");
            return View(forgotPassword);
        }


        [Route("checkforgotpassword")]
        public IActionResult CheckForgotPassword()
        {

            return View();
        }
        [HttpPost("checkforgotpassword")]
        public async Task<IActionResult> CheckForgotPassword(ActiveAccountViewModel viewModel)
        {
            if (!ModelState.IsValid) { return View(viewModel); };

            UserViewModel user = await _accountService.CheckForgotPasswordAsync(viewModel.ActiveCode);

            if (user != null)
            {
                return RedirectToAction("changepassword", user.Email);
            }
            ModelState.AddModelError("ActiveCode", "کد وارد شده صحیح نمی باشد");
            return View(viewModel);
        }

        [HttpGet("changepassword")]
        public IActionResult ChangePassword(string Email)
        {

            return View(new ChangePasswordViewModel() { Email = Email });
        }


        [HttpPost("changepassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel viewModel)
        {

            if (!ModelState.IsValid) { return View(viewModel); };

            await _accountService.ChangeUserPasswordAsync(viewModel);
            ViewBag.IsSuccess = true;
            return View();
        }
        #endregion


    }
}
