using EShop.Domain.common;
using EShop.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Models.Order
{
    public class Order:BaseEntity
    {
        public int OrderSum { get; set; }
        public bool IsFinaly { get; set; }
        public int UserId { get; set; }




        #region Relations
        public User? User { get; set; }
        public IEnumerable<OrderDetail>? OrderDetails { get; set; }
        
        #endregion

    }
}
