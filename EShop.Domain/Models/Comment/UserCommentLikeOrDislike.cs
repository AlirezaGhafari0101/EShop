using EShop.Domain.common;
using EShop.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Models.Comment
{
    public class UserCommentLikeOrDislike: BaseEntity
    {
        public int? UserId { get; set; }
        public int? CommentId { get; set; }
        public CommentLikeOrDislike CommentLikeOrDislike { get; set; }

        #region Relation

        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("CommentId")]
        public Comment Comment { get; set; }

        #endregion
    }
}
