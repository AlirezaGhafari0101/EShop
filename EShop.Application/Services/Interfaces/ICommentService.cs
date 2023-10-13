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
    }
}
