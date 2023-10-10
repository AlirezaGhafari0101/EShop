using EShop.Application.Services.Interfaces;
using EShop.Application.ViewModels.Comment;
using EShop.Domain.Interfaces;
using EShop.Domain.Models.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Services.Implementation
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }
        public async Task CreateCommentServiceAsync(CommentViewModel commentVM)
        {
            var comment = new Comment()
            {
                IsConfirmed= false,
                Message = commentVM.Message,
               ProductId = commentVM.ProductId,
               UserId = commentVM.UserId,
            };
            await _commentRepository.CreateCommentAsync(comment);
        }

        public Task UpdateCommentServiceAsync(CommentViewModel comment)
        {
            throw new NotImplementedException();
        }
    }
}
