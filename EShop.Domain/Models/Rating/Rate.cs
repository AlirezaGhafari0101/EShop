using EShop.Domain.common;
using EShop.Domain.Models.Products;
using EShop.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Models.Rating
{
    public class Rate:BaseEntity
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public RatingScores RateScore { get; set; }

        #region Relations
        public User User { get; set; }
        public Product Product { get; set; }

        #endregion
    }
}
