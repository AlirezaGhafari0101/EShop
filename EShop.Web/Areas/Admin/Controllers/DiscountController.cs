using EShop.Application.Services.Interfaces;
using EShop.Application.ViewModels.Discount;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Areas.Admin.Controllers
{
    public class DiscountController : BaseController
    {

        private readonly IDiscountService _discountService;
        public DiscountController(IDiscountService discountService)
        {
            _discountService = discountService;
        }
        public async Task<IActionResult> Index()
        {
            var discounts = await _discountService.GetAllDiscountsServiceAsync();
            return View(discounts);
        }
        [Route("/AddDiscount")]
        public async Task<IActionResult> AddDiscount()
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
    }
}



