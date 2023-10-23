using EShop.Application.ViewModels.common;
using EShop.Domain.Models.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.ViewModels.Comment
{
    public class AddUserCommentLikeOrDislikeViewModel:BaseViewModel
    {
        public int UserId { get; set; }
        public int CommentId { get; set; }
        public CommentLikeOrDislike CommentLikeOrDislike { get; set; }
    }
}
