using Dto.Payment;
using EShop.Application.Convertors;
using EShop.Application.Extensions;
using EShop.Application.Services.Interfaces;
using EShop.Application.ViewModels.Order;
using EShop.Application.ViewModels.User.UserPanel;
using EShop.Application.ViewModels.Wallet;
using EShop.Domain.Models.Order;
using EShop.Domain.Models.Users;
using EShop.Domain.Models.Wallet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Security.Claims;
using ZarinPal.Class;

namespace EShop.Web.Controllers
{
    public class CheckoutValues
    {
        public int OrderId { get; set; }
        public double Amount { get; set; }
    }
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly Payment _payment;
        private readonly IUserService _userService;
        private readonly IWalletService _walletService;

        public OrderController(IOrderService orderService,
            Payment payment,
            IUserService userService,
            IWalletService walletService)
        {
            _orderService = orderService;
            _payment = payment;
            _userService = userService;
            _walletService = walletService;
        }
        [HttpGet("cart")]
        public async Task<IActionResult> Cart()
        {
            int userId = User.GetrUserId();
            OrderVM userOrderInfor = await _orderService.ShowUserOrderInforServiceAsync(userId);
            if (userOrderInfor != null)
            {
                ViewBag.IsEmpty = false;
                return View(userOrderInfor);
            }
            ViewBag.IsEmpty = true;
            return View();
        }


        [HttpGet("add-to-cart/{productId}/{colorId}")]
        public async Task<int> AddToCart(int productId, int colorId)
        {
            int usaerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            int orderId = await _orderService.AddToOrderServiceAsync(usaerId, productId, colorId);
            return orderId;


        }

        [HttpGet("checkout/cart")]
        public async Task<IActionResult> CheckoutCart(int productId, int colorId)
        {
            int usaerId = User.GetrUserId();

            OrderVM userOrderInfor = await _orderService.ShowUserOrderInforServiceAsync(usaerId);

            return View(userOrderInfor);

        }



        #region User Order Partial
        public async Task<IActionResult> ReloadUserCartInforTop()
        {
            int usaerId = User.GetrUserId();
            OrderVM userOrderInfor = await _orderService.ShowUserOrderInforServiceAsync(usaerId);
            return PartialView("_ReloadUserCartInforPartial", userOrderInfor);

        }
        #endregion


        #region Delete Order
        [HttpGet("order/deleteorder/{detailId}/{orderId}")]
        public async Task<IActionResult> DeleteOrder(int detailId, int orderId)
        {

            bool result = await _orderService.DeleteOrderServiceAsync(detailId, orderId);

            if (result)
                return Json(new { status = "success" });
            else
                return Json(new { status = "faild" });

        }
        [HttpGet("order/deleteorder-count/{detailId}/{orderId}")]
        public async Task<IActionResult> DeleteOrderCount(int detailId, int orderId)
        {

            bool result = await _orderService.DeleteOrderCountServiceAsync(detailId, orderId);

            if (result)
                return Json(new { status = "success" });
            else
                return Json(new { status = "faild" });

        }
        #endregion





        #region Payment
        [HttpGet("payment")]
        public async Task<IActionResult> Payment()
        {

            int userId = User.GetrUserId();
            OrderVM userOrderInfor = await _orderService.ShowUserOrderInforServiceAsync(userId);
            if (userOrderInfor != null)
            {
                ViewBag.IsEmpty = false;
                return View(userOrderInfor);
            }
            ViewBag.IsEmpty = true;
            return View();

        }

        [HttpGet("payment/{orderId}/{PaymentMethod}")]
        public async Task<IActionResult> Payment(int orderId, int PaymentMethod)
        {

            int userId = User.GetrUserId();

            OrderVM userOrderInfor = await _orderService.ShowUserOrderInforServiceAsync(userId);
            double Amount = DiscountCalculator.TotalShopingCart(userOrderInfor.OrderDetails);

            double userBalance = await _userService.BalanceUserWalletAsyncService(userId);

            if (PaymentMethod == 1)
            {
                var result = await _payment.Request(new DtoRequest()
                {
                    Mobile = "09921865340",
                    CallbackUrl = $"https://localhost:44342/checkout/{orderId}/{Amount}",
                    Email = "hajizadesaeed.78@gmail.com",
                    Amount = (int)Amount,
                    Description = "خرید",
                    MerchantId = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX"
                }, ZarinPal.Class.Payment.Mode.sandbox);
                if (result.Status == 100)
                {


                    await _orderService.BillPaymentWithWalletServiceAsync(userId, Amount, "پرداخت صورت حساب", orderId, false, PaymentMethod);
                    return Redirect($"https://sandbox.zarinpal.com/pg/StartPay/{result.Authority}");
                }

            }
            else if (PaymentMethod == 0)
            {
                if (userBalance >= Amount)
                {
                    await _orderService.BillPaymentWithWalletServiceAsync(userId, Amount, "پرداخت صورت حساب", orderId, false, PaymentMethod);
                    

                    CheckoutValues checkouteValues = new CheckoutValues() { Amount = Amount, OrderId = orderId };
                    return RedirectToAction("CheckOuteWallet", checkouteValues);
                }
                else
                {
                    double amount = Amount - userBalance;

                    var result = await _payment.Request(new DtoRequest()
                    {
                        Mobile = "09921865340",
                        CallbackUrl = $"https://localhost:44342/ChargeWalletThenPayment/{orderId}/{amount}",
                        Email = "hajizadesaeed.78@gmail.com",
                        Amount = (int)amount,
                        Description = "شارژ کیف پول",
                        MerchantId = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX"
                    }, ZarinPal.Class.Payment.Mode.sandbox);
                    if (result.Status == 100)
                    {
                       
                        await _orderService.BillPaymentWithWalletServiceAsync(userId, Amount, "پرداخت صورت حساب", orderId, false, PaymentMethod);
                        return Redirect($"https://sandbox.zarinpal.com/pg/StartPay/{result.Authority}");
                    }
                }
            }



            return null;
        }

        #region CehckOute Actions
        [HttpGet("checkout/{orderId}/{amount}")]
        public async Task<IActionResult> Checkout(int orderId, double amount)
        {
            int userId = User.GetrUserId();
            WalletVM getWallet = await _walletService.GetWalletByOrderIdServiceAsync(orderId);
            if (HttpContext.Request.Query["Status"] != "" &&
               HttpContext.Request.Query["Status"].ToString().ToLower() == "ok"
               && HttpContext.Request.Query["Authority"] != "")
            {
                string authority = HttpContext.Request.Query["Authority"];
                var verification = await _payment.Verification(new DtoVerification
                {
                    Amount = (int)amount,
                    MerchantId = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX",
                    Authority = authority
                }, ZarinPal.Class.Payment.Mode.sandbox);

                if (verification.Status == 100)
                {
                    await _walletService.ChargeWalletAsyncService(userId, amount, "شارژ کیف پول", true);
                    UpdateWalletVM updateWalletVM = new UpdateWalletVM();
                    updateWalletVM.IsPay = true;
                    updateWalletVM.WalletId = getWallet.Id;

                    await _walletService.UpdateWalletServiceAsync(updateWalletVM);
                    await _orderService.IsFinallyOrder(orderId, amount);


                    ViewBag.code = verification.RefId;
                    ViewBag.IsFinally = true;

                    return View(getWallet);
                }

            }
            else
            {

                ViewBag.IsFinally = false;
                return View(getWallet);
            }


            if (getWallet.IsPay)
                ViewBag.IsFinally = true;
            else
                ViewBag.IsFinally = false;

            return View(getWallet);

        }

        public async Task<IActionResult> CheckOuteWallet(CheckoutValues checkoutValues)
        {
            int userId = User.GetrUserId();
            WalletVM getWallet = await _walletService.GetWalletByOrderIdServiceAsync(checkoutValues.OrderId);


            UpdateWalletVM updateWalletVM = new UpdateWalletVM();
            updateWalletVM.IsPay = true;
            updateWalletVM.WalletId = getWallet.Id;

            await _walletService.UpdateWalletServiceAsync(updateWalletVM);
            await _orderService.IsFinallyOrder((int)getWallet.OrderId, getWallet.Amount);

            return View(getWallet);
        }


        [HttpGet("ChargeWalletThenPayment/{orderId}/{amount}")]
        public async Task<IActionResult> ChargeWalletThenPayment(int orderId, double amount)
        {

            int userId = User.GetrUserId();
            WalletVM getWallet = await _walletService.GetWalletByOrderIdServiceAsync(orderId);


            if (HttpContext.Request.Query["Status"] != "" &&
               HttpContext.Request.Query["Status"].ToString().ToLower() == "ok"
               && HttpContext.Request.Query["Authority"] != "")
            {

                string authority = HttpContext.Request.Query["Authority"];
                var verification = await _payment.Verification(new DtoVerification
                {
                    Amount = (int)amount,
                    MerchantId = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX",
                    Authority = authority
                }, ZarinPal.Class.Payment.Mode.sandbox);
                if (verification.Status == 100)
                {
                    await _walletService.ChargeWalletAsyncService(userId, amount, "شارژ کیف پول", true);
                    UpdateWalletVM updateWalletVM = new UpdateWalletVM();
                    updateWalletVM.IsPay = true;
                    updateWalletVM.WalletId = getWallet.Id;

                    await _walletService.UpdateWalletServiceAsync(updateWalletVM);
                    await _orderService.IsFinallyOrder(orderId, amount);
                    ViewBag.code = verification.RefId;
             
                    ViewBag.IsFinally = true;
                    return View(getWallet);
                }
            }
            else
            {

                ViewBag.IsFinally = false;
                return View(getWallet);
            }


            if (getWallet.IsPay)
                ViewBag.IsFinally = true;
            else
                ViewBag.IsFinally = false;

            return View(getWallet);

        }
        #endregion


        #endregion

        #region RePayment
        [HttpGet("repayment/{walletId}")]
        public async Task<IActionResult> RePayment(int walletId)
        {
            int userId = User.GetrUserId();
            double userBalance = await _userService.BalanceUserWalletAsyncService(userId);

            WalletVM walletVM=await _walletService.GetWalletByIdServiceAsync(walletId);


            if (walletVM.PaymentType.GetHashCode() == 1)
            {
                var result = await _payment.Request(new DtoRequest()
                {
                    Mobile = "09921865340",
                    CallbackUrl = $"https://localhost:44342/checkout/{walletVM.OrderId}/{walletVM.Amount}",
                    Email = "hajizadesaeed.78@gmail.com",
                    Amount = (int)walletVM.Amount,
                    Description = "خرید",
                    MerchantId = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX"
                }, ZarinPal.Class.Payment.Mode.sandbox);
                if (result.Status == 100)
                {
                    return Redirect($"https://sandbox.zarinpal.com/pg/StartPay/{result.Authority}");
                }
            }else if(walletVM.PaymentType.GetHashCode() ==0)
            {
                if (userBalance >= walletVM.Amount)
                {
                    CheckoutValues checkouteValues = new CheckoutValues() { Amount = walletVM.Amount, OrderId = (int)walletVM.OrderId };
                    return RedirectToAction("CheckOuteWallet", checkouteValues);
                }
                else
                {
                    double amount = walletVM.Amount - userBalance;

                    var result = await _payment.Request(new DtoRequest()
                    {
                        Mobile = "09921865340",
                        CallbackUrl = $"https://localhost:44342/ChargeWalletThenPayment/{walletVM.OrderId}/{amount}",
                        Email = "hajizadesaeed.78@gmail.com",
                        Amount = (int)amount,
                        Description = "شارژ کیف پول",
                        MerchantId = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX"
                    }, ZarinPal.Class.Payment.Mode.sandbox);
                    if (result.Status == 100)
                    {

                        return Redirect($"https://sandbox.zarinpal.com/pg/StartPay/{result.Authority}");
                    }
                }
            }













            return Redirect("https://localhost:44342/");
        }
        #endregion








    }
}
