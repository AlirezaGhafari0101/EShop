using EShop.Data.Context;
using EShop.Domain.Interfaces;
using EShop.Domain.Models.Comment;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Data.Repository
{
    public class UserCommentLikeOrDislikeRepository : IUserCommentLikeOrDislikeRepository
    {
        private readonly EshopDBContext _ctx;

        public UserCommentLikeOrDislikeRepository(EshopDBContext ctx)
        {
            _ctx = ctx;
        }
        public async Task CreateUserCommentLikeOrDislikeAsync(Domain.Models.Comment.UserCommentLikeOrDislike model)
        {
            await _ctx.UserCommentLikesOrDislikes.AddAsync(model);
        }

        public async Task DeleteUserCommentLikeOrDislikeAsync(Domain.Models.Comment.UserCommentLikeOrDislike model)
        {
            _ctx.UserCommentLikesOrDislikes.Remove(model);
        }

        public async Task<UserCommentLikeOrDislike> FindCommentDislikeExistAsync(int commentId, int userId)
        {
            return await _ctx.UserCommentLikesOrDislikes.FirstOrDefaultAsync(cl => cl.CommentId == commentId && cl.UserId == userId && cl.CommentLikeOrDislike == CommentLikeOrDislike.dislike && cl.IsDelete != true);
        }

        public async Task<UserCommentLikeOrDislike> FindCommentLikeExistAsync(int commentId, int userId)
        {
            return await _ctx.UserCommentLikesOrDislikes.FirstOrDefaultAsync(cl => cl.CommentId == commentId && cl.UserId == userId && cl.CommentLikeOrDislike == CommentLikeOrDislike.like && cl.IsDelete != true);
        }

        public async Task<int> GetCommentDislikesCountAsync(int commentId)
        {
            return await _ctx.UserCommentLikesOrDislikes.CountAsync(c => c.CommentLikeOrDislike == CommentLikeOrDislike.dislike && c.CommentId ==commentId);
        }

        public async Task<int> GetCommentLikesCountAsync(int commentId)
        {
           return await _ctx.UserCommentLikesOrDislikes
                .CountAsync(c => c.CommentLikeOrDislike == CommentLikeOrDislike.like && c.CommentId == commentId);
        }

        public async Task SaveChangesAsync()
        {
          await _ctx.SaveChangesAsync();
        }
    }
}
