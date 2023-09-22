using EShop.Application.Services.Interfaces;
using EShop.Application.ViewModels.Product;
using EShop.Application.ViewModels.Product.ProductGallery;
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
        public async Task<IActionResult> AddProduct(AddProductViewModel model, List<IFormFile> galleryInput)
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

            int productId = await _productService.CreateProductServiceAsync(model);
            var productGalleryModel = new ProductGalleryViewModel {
                ProductImages=galleryInput,
                ProductId=productId
            };

            await _productService.CreateProductGalleryServiceAsync(productGalleryModel);


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
            var productGallery = await _productService.GetProductGalleryByIdServiceAsync(id);
            var editableProduct = new EditProductViewModel
            {
                Title = product.Title,
                Description = product.Description,
                Tag = product.Tag,
                Image = product.Image,
                ImageName = product.ImageName,
                CategoryId = product.CategoryId,
                Count = product.Count,
                ProductGalleryImages = productGallery,
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
            var gallery = new ProductGalleryViewModel
            {
                ProductId = id,
                ProductImages = model.ProductGalleryImagesFile
            };
            await _productService.CreateProductGalleryServiceAsync(gallery);
            TempData["ProductEdited"] = true;
            return RedirectToAction("Index");
        }

        #endregion

        #region Delete ProductGallery

        public async Task<IActionResult> DeleteProductGallery(int id)
        {
            await _productService.DeleteProductGalleryServiceAsync(id);
            return Json(new {status = "success"});
        }

        #endregion

        #endregion

    }
}
