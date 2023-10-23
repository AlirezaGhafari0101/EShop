using EShop.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Components
{
    public class ProductCommentsComponent : ViewComponent
    {
        private readonly ICommentService _commentService;

        public ProductCommentsComponent(ICommentService commentService)
        {
            _commentService = commentService;
        }

 
        public async Task<IViewComponentResult> InvokeAsync(int id, string orderByType = "new")
        {
            var comments = await _commentService.GetAllCommentsForProductServiceAsync(id, orderByType);
            return View("/Views/Components/ProductCommentsComponent.cshtml", comments);
        }
    }
}
