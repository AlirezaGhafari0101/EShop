using EShop.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Areas.Admin.Controllers
{
    public class CommentController : BaseController
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        public async Task<IActionResult> Index()
        {
            var comments  = await _commentService.GetAllCommentsServiceAsync();
            return View(comments);
        }

        [Route("/confirm-comment/{commentId}")]
        public async Task<IActionResult> Confirm(int commentId)
        {
            await _commentService.ConfirmCommentToShowServiceAsync(commentId);
            return Json(new { isSuccess=true });
        }

        [Route("/delete-comment/{commentId}")]
        public async Task<IActionResult> Delete(int commentId)
        {
            await _commentService.DeleteCommentServiceAsync(commentId);
            return Json(new { isSuccess = true });
        }
    }
}
