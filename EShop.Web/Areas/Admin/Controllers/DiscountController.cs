using EShop.Application.Services.Interfaces;
using EShop.Application.ViewModels.Discount;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Areas.Admin.Controllers
{
    public class DiscountController : BaseController
    {

        #region Fields
        private readonly IDiscountService _discountService;
        #endregion

        #region Constructor 
        public DiscountController(IDiscountService discountService)
        {
            _discountService = discountService;
        }
        #endregion

        #region Index
        public async Task<IActionResult> Index()
        {
            var discounts = await _discountService.GetAllDiscountsServiceAsync();
            return View(discounts);
        }
        #endregion

        #region Add Discount
        [Route("/AddDiscount")]
        public IActionResult AddDiscount()
        {
            return View();
        }

        [Route("/AddDiscount")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddDiscount(AddDiscountViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            await _discountService.CreateDiscountServiceAsync(model);
            TempData["DiscountCreated"] = true;
            return RedirectToAction("Index");
        }
        #endregion

        #region DeleteDiscount

        public async Task<IActionResult> DeleteDiscount(int id)
        {
            await _discountService.DeleteDiscountServiceAsync(id);
            return Json(new { status = "success" });
        }

        #endregion
    }
}



