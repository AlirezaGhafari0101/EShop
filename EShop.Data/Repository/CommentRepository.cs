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
    public class CommentRepository : ICommentRepository
    {

        private readonly EshopDBContext _ctx;

        public CommentRepository(EshopDBContext ctx)
        {
            _ctx = ctx;
        }
        public async Task CreateCommentAsync(Comment comment)
        {
            await _ctx.Comments.AddAsync(comment);
        }
        public async Task UpdateCommentAsync(Comment comment)
        {
            _ctx.Comments.Update(comment);
        }

        public async Task<IQueryable<Comment>> GetAllCommentsForProductAsync(int productId, string orderByType = "new")
        {
            IQueryable<Comment> result = _ctx.Comments
                .Where(c => productId == c.ProductId)
                .Include(c => c.UserCommentLikes)
                .Include(c => c.User);

            switch (orderByType)
            {
                case "new":
                   result= result.OrderByDescending(c => c.CreateDate);
                    break;
                case "best":
                   result= result.OrderByDescending(c => c.UserCommentLikes.Count(cl => cl.CommentLikeOrDislike == CommentLikeOrDislike.like));
                    break;
            }

            return result;

        }
        public async Task<List<Comment>> GetAllCommentsAsync()
        {
            return await _ctx.Comments.Include(c => c.User).ToListAsync();
        }

        public async Task<Comment> GetCommentByIdAsync(int commentId)
        {
            return await _ctx.Comments.FindAsync(commentId);
        }
        public async Task SaveChangesAsync()
        {
            await _ctx.SaveChangesAsync();
        }

       
    }
}
