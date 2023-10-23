using EShop.Domain.Models.Order;
using EShop.Domain.Models.Users;

namespace EShop.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task SaveChangeAsync();
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order> GetOrderAsync(int id);
        Task AddOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(Order order);
        void UpdatePriceOrder(int orderId);


        #region Order Detail
        Task<IEnumerable<OrderDetail>> GetAllOrderDetailsAsync();
        Task AddOrderDetailAsync(OrderDetail orderDetail);
        Task UpdateOrderDetailAsync(OrderDetail orderDetail);
        Task<Order> GetUserOrderInforAsync(int id);
        Task<OrderDetail> GetOrderDetailByIdAsync(int id);

        Task DeleteOrderDetailAsync(OrderDetail orderDetail);

        Task<IEnumerable<OrderDetail>> GetOrderDetailsForOrder(int id);




        #endregion



        #region Admin
        Task<IEnumerable<Order>> GetAllOrdersForAdminAsync();
        Task<IEnumerable<OrderDetail>> GetOrderDetailsForAdminAsync(int orderId);
        User GetOrderUser(int orderId);

        #endregion



        #region UserPanel
        Task<IEnumerable<Order>> GetUserOrderListAsync(int userId);
        Task<IEnumerable<OrderDetail>> GetUserOrderDetailsAsync(int orderId);
        #endregion


    }
}
