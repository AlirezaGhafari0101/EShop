using EShop.Domain.common;
using EShop.Domain.Models.Products;
using EShop.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Models.Comment
{
    public class Comment:BaseEntity
    {
        public string Message { get; set; }
        public bool IsConfirmed { get; set; }

        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int LikeCounts { get; set; } = 0;
        public int DislikeCounts { get; set; }= 0;

        #region Relations
        public User User { get; set; }
        public Product Product { get; set; }
        public List<UserCommentLikeOrDislike>? UserCommentLikes { get; set; }

        #endregion
    }
}
