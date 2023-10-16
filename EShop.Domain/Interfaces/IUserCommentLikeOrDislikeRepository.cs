using EShop.Domain.Models.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Interfaces
{
    public interface IUserCommentLikeOrDislikeRepository
    {
        Task CreateUserCommentLikeOrDislikeAsync(UserCommentLikeOrDislike model);
        Task DeleteUserCommentLikeOrDislikeAsync(UserCommentLikeOrDislike model);
        Task<UserCommentLikeOrDislike> FindCommentLikeExistAsync(int commentId, int userId);
        Task<UserCommentLikeOrDislike> FindCommentDislikeExistAsync(int commentId, int userId);
        Task<int> GetCommentLikesCountAsync(int commentId);
        Task<int> GetCommentDislikesCountAsync(int commentId);
        Task SaveChangesAsync();
    }
}
