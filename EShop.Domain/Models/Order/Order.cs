using EShop.Domain.common;
using EShop.Domain.Models.Users;
using EShop.Domain.Models.Wallet;


namespace EShop.Domain.Models.Order
{
    public class Order:BaseEntity
    {
        public double OrderSum { get; set; }
        public double? AmountPaid { get; set; }
        public bool IsFinaly { get; set; }
        public int UserId { get; set; }





        #region Relations
        public User? User { get; set; }
        public IEnumerable<OrderDetail>? OrderDetails { get; set; }
        public Wallet.Wallet? Wallet { get; set; }

        #endregion

    }
}
