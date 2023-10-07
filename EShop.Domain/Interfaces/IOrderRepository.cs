using EShop.Domain.Models.Order;

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

        #endregion
    }
}
