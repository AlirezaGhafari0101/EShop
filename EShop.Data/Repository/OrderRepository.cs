using EShop.Data.Context;
using EShop.Domain.Interfaces;
using EShop.Domain.Models.Order;
using Microsoft.EntityFrameworkCore;


namespace EShop.Data.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly EshopDBContext _ctx;
        public OrderRepository(EshopDBContext ctx)
        {
            _ctx = ctx;
        }


        public async Task SaveChangeAsync()
        {
            await _ctx.SaveChangesAsync();
        }

        public async Task AddOrderAsync(Order order)
        {
             await _ctx.Orders.AddAsync(order);
        }

        public async Task DeleteOrderAsync(Order order)
        {
            _ctx.Remove(order);
        }


        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _ctx.Orders.ToListAsync();
        }

        public async Task<Order> GetOrderAsync(int id)
        {
            return await _ctx.Orders.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task UpdateOrderAsync(Order order)
        {
            _ctx.Update(order);
        }

        public void UpdatePriceOrder(int orderId)
        {
            var order = _ctx.Orders.Find(orderId);
            order.OrderSum = _ctx.OrderDetails.Where(d => d.OrderId == orderId).Sum(d => d.Price);
            _ctx.Orders.Update(order);
            _ctx.SaveChanges();
        }


        #region Order Detail


        public async Task<IEnumerable<OrderDetail>> GetAllOrderDetailsAsync()
        {
            return await _ctx.OrderDetails.ToListAsync();
        }

        public async Task AddOrderDetailAsync(OrderDetail orderDetail)
        {
            _ctx.OrderDetails.Add(orderDetail);
        }

        public async Task UpdateOrderDetailAsync(OrderDetail orderDetail)
        {
            _ctx.OrderDetails.Update(orderDetail);
        }


        #endregion
    }
}
