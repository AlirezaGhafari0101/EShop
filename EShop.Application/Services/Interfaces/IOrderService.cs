using EShop.Application.ViewModels;
using EShop.Application.ViewModels.Order;
using EShop.Application.ViewModels.Wallet;
using EShop.Domain.Models.Order;
using EShop.Domain.Models.Users;

namespace EShop.Application.Services.Interfaces
{
    public interface IOrderService
    {
        Task<int> AddToOrderServiceAsync(int userId, int productId, int colorId);
        Task<OrderVM> ShowUserOrderInforServiceAsync(int userId);
        Task <bool> DeleteOrderServiceAsync(int orderDetailId,int orderId);
        Task<bool> DeleteOrderCountServiceAsync(int orderDetailId, int orderId);


        #region Payment
        Task<WalletVM> BillPaymentWithWalletServiceAsync(int userId, double amount, string description, int orderId,bool isPay, int paymentMethod);
        Task IsFinallyOrder(int orderId,double amountPaid);
        #endregion




        #region Admin
        Task<IEnumerable<OrderVM>> GetAllOrdersForAdminServiceAsync();
        Task<IEnumerable<OrderDetailVM>> GetOrderDetailsForAdminServiceAsync(int orderId);
        UserViewModel GetOrderUserService(int orderId);
        Task DeleteOrderServiceAsync(int orderId);
        #endregion


        #region UserPanel
        Task<IEnumerable<OrderVM>> GetUserOrderListServiceAsync(int userId);
        Task<IEnumerable<OrderDetailVM>> GetUserOrderDetailsServiceAsync(int orderId);
        Task<OrderVM> GetOrderByIdServiceAsync(int orderId);

        #endregion


    }
}
