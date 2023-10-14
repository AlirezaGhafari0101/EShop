using EShop.Application.Services.Interfaces;
using EShop.Application.ViewModels.Comment;
using EShop.Domain.Models.Users;
using EShop.Web.Areas.UserPanel.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EShop.Web.Controllers
{
    [Authorize]
    public class CommentController : BaseController
    {
        #region Fields
        private readonly ICommentService _commentService;
        private readonly IProductService _productService;
        #endregion

        #region Constructor
        public CommentController(ICommentService commentService, IProductService productService)
        {
            _commentService = commentService;
            _productService = productService;
        }
        #endregion

        #region Actions

        [HttpGet("add-comment/{productId}")]
        public async Task<IActionResult> AddComment(int productId)
        {
            ViewBag.product = await _productService.GetProductImageAndTitleByIdServiceAsync(productId);
            return View("~/Views/Comment/AddComment.cshtml");
        }

       
        [HttpPost("add-comment/{productId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComment(int productId, AddCommentViewModel model)
        {
            if (ModelState.IsValid)
            {
            
                await _commentService.CreateCommentServiceAsync(model);
                return Redirect($"/product/{productId}");
            }
            return View("~/Views/Comment/AddComment.cshtml", model);
        }
        #endregion
    }
}
