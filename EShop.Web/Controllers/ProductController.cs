using EShop.Application.Services.Interfaces;
using EShop.Application.ViewModels.Product.Category;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Controllers
{
    public class ProductController : Controller
    {

        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [Route("/category/{id}")]
        public async Task<IActionResult> ShowProductsByCategory(int id)
        {
            var products = await _productService.GetAllProductsByCategoryIdServiceAsync(id);
            return View(products);
        }

        [Route("product/{id}")]
        public async Task<IActionResult> ShowProduct(int id)
        {
            var product = await _productService.GetProductByIdServiceAsync(id);
            return View(product);
        }

    }
}
