using EShop.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Components
{
    public class ProductCategoriesComponent: ViewComponent
    {
        private readonly IProductService _productService;

        public ProductCategoriesComponent(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _productService.GetAllCategoriesClientSideServiceAsync();
            return View("/Views/Components/ProductCategoriesComponent.cshtml", categories);
        }
    }
}
