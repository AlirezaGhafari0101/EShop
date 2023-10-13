using EShop.Domain.common;
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

        #region Relations
        public User User { get; set; }

        #endregion
    }
}
