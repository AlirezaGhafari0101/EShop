using EShop.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Areas.UserPanel.Controllers
{
    public class CommentController : BaseController
    {
        #region Fields
        private readonly ICommentService _commentService;
        #endregion

        #region Constructor
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        #endregion

        #region Actions
        [Route("add-comment/{productId}")]
        public IActionResult AddComment(int productId)
        {
            return View();
        }
        #endregion
    }
}
