using EShop.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {

        private IProductService _productService;

        public ProductController(IProductService userService) 
        {
            _productService = userService;   
        }
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllProductsServiceAsync();
            return View(products);
        }

        public async Task<IActionResult> AddProduct()
        {
            return View();
        }
    }
}
