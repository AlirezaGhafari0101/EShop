using EShop.Application.Services.Interfaces;
using EShop.Application.ViewModels.Product.Category;
using EShop.Domain.Models.Rating;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EShop.Web.Controllers
{
    public class ProductController : Controller
    {

        private readonly IProductService _productService;
        private readonly IDiscountService _discountService;

        public ProductController(IProductService productService, IDiscountService discountService)
        {
            _productService = productService;
            _discountService = discountService;
        }
        [Route("/category/{id}")]
        public async Task<IActionResult> ShowProductsByCategory(int id)
        {
            var products = await _productService.GetAllProductsByCategoryIdServiceAsync(id);
            ViewBag.globalDiscount = await _discountService.GetGlobalDiscountServiceAsync();
            return View(products);
        }

        [Route("product/{id}")]
        public async Task<IActionResult> ShowProduct(int id)
        {
            var product = await _productService.GetProductByIdServiceAsync(id);
            ViewBag.globalDiscount = await _discountService.GetGlobalDiscountServiceAsync();
            return View(product);
        }

        [HttpPost("rate/{productId}")]
        public async Task<IActionResult> AddRate(int productId, RatingScores scoreRatingElem)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _productService.CreateRateServiceAsync(productId,userId, scoreRatingElem);
            var avgScore = await _productService.CalculateAverageRateForProductAsync(productId);
            return Json(new {isSuccess=true, averageScore=avgScore});
        }

       

    }
}
