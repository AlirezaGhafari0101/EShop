﻿using EShop.Application.Services.Interfaces;
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
        private readonly IUserCommentLikeOrDislikeRepository _userCommentLikeOrDislikeRepository;

        public CommentService(ICommentRepository commentRepository, IUserCommentLikeOrDislikeRepository userCommentLikeOrDislikeRepository)
        {
            _commentRepository = commentRepository;
            _userCommentLikeOrDislikeRepository = userCommentLikeOrDislikeRepository;
        }
        public async Task CreateCommentServiceAsync(AddCommentViewModel commentVM)
        {
            var comment = new Comment()
            {
                IsConfirmed = false,
                Message = commentVM.Message,
                ProductId = commentVM.ProductId,
                UserId = commentVM.UserId,
            };
            await _commentRepository.CreateCommentAsync(comment);
            await _commentRepository.SaveChangesAsync();
        }
        public async Task UpdateCommentServiceAsync(CommentViewModel comment)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> CreateUserCommentLikeServiceAsync(int commentId, int userId)
        {

            if (await _userCommentLikeOrDislikeRepository.FindCommentDislikeExistAsync(commentId, userId) != null)
            {
                await DeleteUserCommentDislikeServiceAsync(commentId, userId);
                return false;
            }
            else if (await _userCommentLikeOrDislikeRepository.FindCommentLikeExistAsync(commentId, userId) != null)
            {
                return false;
            }
            else
            {
                var commentLike = new UserCommentLikeOrDislike()
                {
                    CommentId = commentId,
                    UserId = userId,
                    CommentLikeOrDislike = CommentLikeOrDislike.like,
                };
                await _userCommentLikeOrDislikeRepository.CreateUserCommentLikeOrDislikeAsync(commentLike);
                await _userCommentLikeOrDislikeRepository.SaveChangesAsync();
                return true;
            }

        }
        public async Task<bool> CreateUserCommentDislikeServiceAsync(int commentId, int userId)
        {
            if (await _userCommentLikeOrDislikeRepository.FindCommentLikeExistAsync(commentId, userId) != null)
            {
                await DeleteUserCommentLikeServiceAsync(commentId, userId);
                return false;
            }
            else if (await _userCommentLikeOrDislikeRepository.FindCommentDislikeExistAsync(commentId, userId) != null)
            {
                return false;
            }
            else
            {
                var commentDislike = new UserCommentLikeOrDislike()
                {
                    CommentId = commentId,
                    UserId = userId,
                    CommentLikeOrDislike = CommentLikeOrDislike.dislike,
                };
                await _userCommentLikeOrDislikeRepository.CreateUserCommentLikeOrDislikeAsync(commentDislike);
                await _userCommentLikeOrDislikeRepository.SaveChangesAsync();
                return true;
            }
        }
        public async Task DeleteUserCommentLikeServiceAsync(int commentId, int userId)
        {
            var commentLike = await _userCommentLikeOrDislikeRepository.FindCommentLikeExistAsync(commentId, userId);
            await _userCommentLikeOrDislikeRepository.DeleteUserCommentLikeOrDislikeAsync(commentLike);
            await _userCommentLikeOrDislikeRepository.SaveChangesAsync();
        }
        public async Task DeleteUserCommentDislikeServiceAsync(int commentId, int userId)
        {
            var commentDislike = await _userCommentLikeOrDislikeRepository.FindCommentDislikeExistAsync(commentId, userId);
            await _userCommentLikeOrDislikeRepository.DeleteUserCommentLikeOrDislikeAsync(commentDislike);
            await _userCommentLikeOrDislikeRepository.SaveChangesAsync();
        }
        public async Task<bool> FindCommentLikeExistServiceAsync(int commentId, int userId)
        {
            var usercommentLike = await _userCommentLikeOrDislikeRepository.FindCommentLikeExistAsync(commentId, userId);
            if (usercommentLike == null)
            {
                return false;
            }
            return true;
        }
        public async Task<bool> FindCommentDislikeExistServiceAsync(int commentId, int userId)
        {
            var usercommentDislike = await _userCommentLikeOrDislikeRepository.FindCommentDislikeExistAsync(commentId, userId);
            if (usercommentDislike == null)
            {
                return false;
            }
            return true;
        }
        public async Task<List<CommentViewModel>> GetAllCommentsForProductServiceAsync(int productId)
        {
            var comments = await _commentRepository.GetAllCommentsForProductAsync(productId);
            return comments.Select(c => new CommentViewModel
            {
                Id = c.Id,
                IsConfirmed = c.IsConfirmed,
                DislikeCounts = c.DislikeCounts,
                LikeCounts = c.LikeCounts,
                Message = c.Message,
                UserName=c.User.FirstName+ ' '+ c.User.LastName,
            }).ToList();
        }

        public async Task<int> GetCommentLikesCountServiceAsync(int commentId)
        {
            return await _userCommentLikeOrDislikeRepository.GetCommentLikesCountAsync(commentId);
        }

        public async Task<int> GetCommentDislikesCountServiceAsync(int commentId)
        {
            return await _userCommentLikeOrDislikeRepository.GetCommentDislikesCountAsync(commentId);   
        }
    }
}
