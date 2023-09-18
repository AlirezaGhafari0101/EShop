using EShop.Application.Services.Interfaces;
using EShop.Application.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            var categories = await _productService.GetAllCategoriesForCreatingProductServiceAsync();
            ViewBag.categories = new SelectList(categories, "Id", "CategoryTitle");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProduct(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (await _productService.IsProductExistServiceAsync(model.Id))
            {
                ModelState.AddModelError("Title", "این محصول از قبل موجود می باشد.");
                return View(model);
            }

            await _productService.CreateProductServiceAsync(model);
            TempData["ProductCreated"] = true;
            return RedirectToAction("Index");
        }
    }
}
