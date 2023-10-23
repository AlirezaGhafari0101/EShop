using Dto.Payment;
using EShop.Application.Services.Interfaces;
using EShop.Application.ViewModels.User.UserPanel;
using EShop.Application.ViewModels.Wallet;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ZarinPal.Class;

namespace EShop.Web.Areas.UserPanel.Controllers
{
    public class WalletController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IWalletService _walletService;
        private readonly Payment _payment;
        public WalletController(IUserService userService,Payment payment,IWalletService walletService)
        {
            var expose = new Expose();
            _userService = userService;
            _payment = payment;
            _walletService = walletService;
        }

        [Route("userpanel/wallet")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("userpanel/chargewallet")]
        public async Task<IActionResult> ChargeWallet()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            ChargeWalletVM chargeWalletVM = new ChargeWalletVM();
   

            ViewBag.userWalletList = await _userService.GetAllUserWalletsAsyncService(userId);
            return View();

        }
        [HttpPost("userpanel/chargewallet")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChargeWallet(ChargeWalletVM charge)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            ViewBag.userWalletList = await _userService.GetAllUserWalletsAsyncService(userId);

            if (ModelState.IsValid)
            {

                int walletId = await _userService.ChargeWalletAsyncService(userId, charge.Amount, "شارژ کیف پول");
              

                var result = await _payment.Request(new DtoRequest()
                {
                    Mobile = "09921865340",
                    CallbackUrl = $"https://localhost:44342/OnlinePayment/{walletId}",
                    Email = "hajizadesaeed.78@gmail.com",
                    Amount = (int)charge.Amount,
                    Description = "شارژ کیف پول",
                    MerchantId = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX"
                }, ZarinPal.Class.Payment.Mode.sandbox);
                if (result.Status == 100)
                {
                    return Redirect($"https://sandbox.zarinpal.com/pg/StartPay/{result.Authority}");
                }


                return null;


            }

            return View(charge);
        }

        [HttpGet("userpanel/chargeFaildwallet/{walletId}")]
        public async Task<IActionResult> ChargeFaildWallet(int walletId)
        {

            var wallet=await _walletService.GetWalletByIdServiceAsync(walletId);

                var result = await _payment.Request(new DtoRequest()
                {
                    Mobile = "09921865340",
                    CallbackUrl = $"https://localhost:44342/OnlinePayment/{walletId}",
                    Email = "hajizadesaeed.78@gmail.com",
                    Amount = (int)wallet.Amount,
                    Description = "شارژ کیف پول",
                    MerchantId = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX"
                }, ZarinPal.Class.Payment.Mode.sandbox);
                if (result.Status == 100)
                {
                    return Redirect($"https://sandbox.zarinpal.com/pg/StartPay/{result.Authority}");
                }
                return null;
        }
    }
}
