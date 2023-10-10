using EShop.Data.Context;
using EShop.Domain.Interfaces;
using EShop.Domain.Models.Comment;
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

        public async Task SaveChangesAsync()
        {
            await _ctx.SaveChangesAsync();
        }
    }
}
