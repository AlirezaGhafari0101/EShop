using EShop.Application.ViewModels.Comment;
using EShop.Domain.Models.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Services.Interfaces
{
    public interface ICommentService
    {
        Task CreateCommentServiceAsync(AddCommentViewModel comment);
        Task UpdateCommentServiceAsync(CommentViewModel comment);

        #region Like Or Dislike

        Task<bool> CreateUserCommentLikeServiceAsync(int commentId, int userId);
        Task<bool>  CreateUserCommentDislikeServiceAsync(int commentId, int userId);
        Task DeleteUserCommentLikeServiceAsync(int commentId, int userId);
        Task DeleteUserCommentDislikeServiceAsync(int commentId, int userId);
        Task<bool> FindCommentLikeExistServiceAsync(int commentId, int userId);
        Task<bool> FindCommentDislikeExistServiceAsync(int commentId, int userId);
        Task<List<CommentViewModel>> GetAllCommentsForProductServiceAsync(int productId);
        Task<int> GetCommentLikesCountServiceAsync(int commentId);
        Task<int> GetCommentDislikesCountServiceAsync(int commentId);
        #endregion
    }
}
