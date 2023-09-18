using EShop.Application.Services.Interfaces;
using EShop.Application.ViewModels.Product.Category;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductCategory : Controller
    {
        private readonly IProductService _productService;
        public ProductCategory(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IActionResult> Index(int? id)
        {
            ViewBag.CategoryId = id;
            return View(await _productService.GetAllCategoriesServiceAsync(id));
        }

        [HttpGet("admin/addcategory/{id?}")]
        public async Task<IActionResult> Add(int? id)
        {
            ViewBag.CategoryId = id;

            return View();
        }

        [HttpPost("admin/addcategory/{id?}")]
        public async Task<IActionResult> Add(CreateCategoryViewModel createViewModel, int? categoryId)
        {
            if (!ModelState.IsValid)
            {
                return View(createViewModel);
            }
            await _productService.AddCategory(createViewModel, categoryId);
            return Redirect($"/Admin/ProductCategory/Index/{categoryId}");
        }
    }
}
