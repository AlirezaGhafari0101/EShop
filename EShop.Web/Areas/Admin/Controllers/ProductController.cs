using EShop.Application.Services.Interfaces;
using EShop.Application.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EShop.Web.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {

        #region Fields
        private IProductService _productService;
        #endregion

        #region Constructor
        public ProductController(IProductService userService)
        {
            _productService = userService;
        }
        #endregion

        #region Actions

        #region Index
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllProductsServiceAsync();
            return View(products);
        }
        #endregion

        #region AddProduct
        public async Task<IActionResult> AddProduct()
        {
            var categories = await _productService.GetAllCategoriesForCreatingProductServiceAsync();
            ViewBag.categories = new SelectList(categories, "Id", "CategoryTitle");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProduct(AddProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (await _productService.IsProductExistServiceAsync(model.Title))
            {
                ModelState.AddModelError("Title", "این محصول از قبل موجود می باشد.");
                return View(model);
            }

            await _productService.CreateProductServiceAsync(model);
            TempData["ProductCreated"] = true;
            return RedirectToAction("Index");
        }
        #endregion

        #region Delete Product

        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProductServiceAsync(id);    
            return Json(new { status = "success" });
        }

        #endregion

        #region Edit Product

        public async Task<IActionResult> EditProduct(int id)
        {
            var product = await _productService.GetProductByIdServiceAsync(id);
            var editableProduct = new EditProductViewModel
            {
                Title = product.Title,
                Description = product.Description,
                Tag = product.Tag,
                Image = product.Image,
                ImageName = product.ImageName,
                CategoryId = product.CategoryId,
                Count=product.Count,
            };
            var categories = await _productService.GetAllCategoriesForCreatingProductServiceAsync();
            ViewBag.categories = new SelectList(categories, "Id", "CategoryTitle");
            return View(editableProduct);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProduct(EditProductViewModel model, int id)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            

            await _productService.UpdateProductServiceAsync(model, id);
            TempData["ProductEdited"] = true;
            return RedirectToAction("Index");
        }

        #endregion

        #endregion

    }
}
