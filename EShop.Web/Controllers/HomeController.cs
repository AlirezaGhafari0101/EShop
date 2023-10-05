using Dto.Payment;
using EShop.Application.Services.Interfaces;
using EShop.Application.ViewModels.ContactUs;
using EShop.Application.ViewModels.User.UserPanel;
using Microsoft.AspNetCore.Mvc;
using ZarinPal.Class;

namespace EShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IContactUsService _contactUsService;
        private readonly IUserService _userService;
        private readonly Payment _payment;

        public HomeController(ILogger<HomeController> logger,
            IContactUsService contactUsService,
            IUserService userService, Payment payment
           )
        {
           

            _logger = logger;
            _contactUsService = contactUsService;
            _userService = userService;
            //_payment = expose.CreatePayment();
            _payment = payment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("contact-us")]
        public IActionResult ContactUs()
        {
            return View();
        }


        [HttpPost("contact-us")]
        public async Task<IActionResult> ContactUs(CreateContactUsViewModel contactUsViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(contactUsViewModel);
            }

            await _contactUsService.CreateQuestionServiceAsync(contactUsViewModel);
            ModelState.Clear();
            ViewBag.IsSended = true;
            return View();
        }


        [HttpGet("onlinepayment/{id}")]
        public async Task<IActionResult> OnlinePayment(int id)
        {
            WalletVM wallet = await _userService.GetWalletByIdAsyncService(id);


            if (HttpContext.Request.Query["Status"] != "" &&
               HttpContext.Request.Query["Status"].ToString().ToLower() == "ok"
               && HttpContext.Request.Query["Authority"] != "")
            {
                ViewBag.IsPay = true;

                string authority = HttpContext.Request.Query["Authority"];
                var verification = await _payment.Verification(new DtoVerification
                {
                    Amount = wallet.Amount,
                    MerchantId = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX",
                    Authority = authority
                }, Payment.Mode.sandbox);

                if (verification.Status == 100)
                {
                    await _userService.IsPayWallet(id);
                    ViewBag.code = verification.RefId;
                    wallet.IsPay = true;



                }

                return View(wallet);





            }
            ViewBag.IsPay = false;
            return View(wallet);



        }


    }
}