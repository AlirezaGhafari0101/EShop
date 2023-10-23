using EShop.Application.ViewModels.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.ViewModels.Comment
{
    public class CommentViewModel: BaseViewModel
    {
        public string Message { get; set; }
        public bool IsConfirmed { get; set; }

        public int ProductId { get; set; }
        public int UserId { get; set; }

        public int LikeCounts { get; set; } = 0;
        public int DislikeCounts { get; set; } = 0;

        public string? UserName { get; set; }
        public DateTime CreatedDate { get; set; }

        public List<UserCommentLikeOrDislikeViewModel>? UserCommentLikeOrDislike  { get; set; }
    }
}
