using EShop.Application.Convertors;
using EShop.Application.Services.Interfaces;
using EShop.Application.ViewModels.Comment;
using EShop.Domain.Interfaces;
using EShop.Domain.Models.Comment;
using EShop.Domain.Models.Users;
using EShop.Web.Areas.UserPanel.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;
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

        #region like and dislike

        [HttpGet("/comment/like/{commentId}")]
        public async Task<IActionResult> Like(int commentId)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
         
            await _commentService.CreateUserCommentLikeServiceAsync(commentId, userId);
            var likeCount = await _commentService.GetCommentLikesCountServiceAsync(commentId);
            var dislikeCount = await _commentService.GetCommentDislikesCountServiceAsync(commentId);
            return Json(new
            {
                isSuccess = true,
                dislikesCount = dislikeCount,
                likesCount = likeCount
            });

        }

        [HttpGet("/comment/dislike/{commentId}")]
        public async Task<IActionResult> Dislike(int commentId)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _commentService.CreateUserCommentDislikeServiceAsync(commentId, userId);
            var likeCount = await _commentService.GetCommentLikesCountServiceAsync(commentId);
            var dislikeCount = await _commentService.GetCommentDislikesCountServiceAsync(commentId);
            return Json(new { 
                isSuccess = true,
                dislikesCount=dislikeCount,
                likesCount=likeCount
            });
        }

        #endregion

        #region Reload and return Partial For showing comment with ajax

        //[HttpGet("comment/reload/{productId}")]
        //public async Task<IActionResult> ReloadCommentPartial(int productId)
        //{
        //    var comments = await _commentService.GetAllCommentsForProductServiceAsync(productId);
        //    return PartialView("_CommentPartial", comments);
        //}

        #endregion
        #endregion
    }
}
