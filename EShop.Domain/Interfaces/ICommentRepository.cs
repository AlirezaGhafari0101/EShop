using EShop.Domain.Models.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Interfaces
{
    public interface ICommentRepository
    {
        Task CreateCommentAsync(Comment comment);
        Task UpdateCommentAsync(Comment comment);
        Task<IQueryable<Comment>> GetAllCommentsForProductAsync(int productId, string orderByType = "new");
        Task<List<Comment>> GetAllCommentsAsync();
        Task<Comment> GetCommentByIdAsync(int commentId);
        Task SaveChangesAsync();
    }
}
