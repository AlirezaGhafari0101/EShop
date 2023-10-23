using EShop.Application.ViewModels.common;
using EShop.Domain.Models.Order;
using EShop.Domain.Models.Users;


namespace EShop.Application.ViewModels.Order
{
    public class OrderVM:BaseViewModel
    {

        public double? OrderSum { get; set; }
        public double? AmountPaid { get; set; }
        public bool IsFinaly { get; set; }
        public int? UserId { get; set; }




        #region Relations
        public Domain.Models.Users.User? User { get; set; }
        public IEnumerable<OrderDetail>? OrderDetails { get; set; }
        public Wallet.WalletVM? Wallet { get; set; }

        #endregion
    }
}
