using EShop.Domain.common;
using EShop.Domain.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Models.Users
{
    public class UserFavourite: BaseEntity
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }


        #region Relation

        public User User { get; set; }
        public Product Product { get; set; }

        #endregion
    }
}
