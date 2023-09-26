using EShop.Application.Services.Interfaces;
using EShop.Application.ViewModels.Product.Color;
using Microsoft.AspNetCore.Mvc;


namespace EShop.Web.Areas.Admin.Controllers
{
    
    public class ProductColorController : BaseController
    {
        private readonly IProductService _productService;
        public ProductColorController(IProductService productService)
        {
            _productService = productService;   
        }
   
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.ProductId = id;
            return View(await _productService.GetAllProductColorsServiceAsync(id));
        }

        [HttpGet("[Area]/[Controller]/test")]
        public async Task<IActionResult> test()
        {
            return RedirectToAction(nameof(Index), new {id=2});

        }

        [HttpGet("admin/add-color/{id?}")]
        public async Task<IActionResult> Add(int id)
        {
            
            return View(new AddProductColorViewModel() { ProductId=id});
        }

        [HttpPost("admin/add-color/{id?}")]
        public async Task<IActionResult> Add(AddProductColorViewModel colorViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(colorViewModel);
            };

            if (colorViewModel.Price == 0)
            {
                ModelState.AddModelError("Price", "قیمت را وارد کنید");
                return View(colorViewModel);
            }

            await _productService.AddProductColorServiceAsync(colorViewModel);

            
            return RedirectToAction("Index", new { id=colorViewModel.ProductId });
        }

        [HttpGet("admin/update-color/{id}/{productId}")]
        public async Task<IActionResult> Update(int id,int productId)
        {

            return View(await _productService.GetProductColorServiceAsync(id));
        }

        [HttpPost("admin/update-color/{id}/{productId}")]
        public async Task<IActionResult> Update(UpdateProductColorViewModel updateColorViewModel,int productId )
        {
            if (!ModelState.IsValid) View(updateColorViewModel);

            await _productService.UpdateProductColorServiceAsync(updateColorViewModel);


            return RedirectToAction(nameof(Index), new {id= productId });
        }

        [HttpGet("admin/delete-color/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteProductColorServiceAsync(id);
            return Json(new { status = "success" });
       
        }


    }
}
