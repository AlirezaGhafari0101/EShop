using EShop.Data.Context;
using EShop.Domain.Interfaces;
using EShop.Domain.Models.Order;
using EShop.Domain.Models.Users;
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
            return await _ctx.Orders.Include(o=> o.Wallet).ToListAsync();
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

        public async Task<Order> GetUserOrderInforAsync(int id)
        {
            return await _ctx.Orders.Where(o=> o.UserId== id&&!o.IsFinaly&&o.Wallet==null)
                .Include(o=> o.OrderDetails)
                .ThenInclude(o=> o.Product)
                .ThenInclude(p=> p.Discount)
                .Include(o => o.OrderDetails)
                .ThenInclude(o => o.Color)
                .Include(o=> o.User)
                .FirstOrDefaultAsync();
        }

  

        public async Task DeleteOrderDetailAsync(OrderDetail orderDetail)
        {
            _ctx.OrderDetails.Remove(orderDetail);
        }

        public async Task<OrderDetail> GetOrderDetailByIdAsync(int id)
        {
            return await _ctx.OrderDetails.Where(od=> od.Id==id).Include(od=> od.Color)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<OrderDetail>> GetOrderDetailsForOrder(int id)
        {
            return await _ctx.OrderDetails.Where(od => od.OrderId == id).ToListAsync();
        }
        #endregion

        #region Admin

        public async Task<IEnumerable<Order>> GetAllOrdersForAdminAsync()
        {   
            return await _ctx.Orders.Include(o=> o.User).ToListAsync();
        }
        public async Task<IEnumerable<OrderDetail>> GetOrderDetailsForAdminAsync(int orderId)
        {

            return await _ctx.OrderDetails
                .Where(od => od.OrderId == orderId)
                .Include(od => od.Product)
                .Include(od => od.Color)
                .ToListAsync();

        }

        public  User GetOrderUser(int orderId)
        {
            return  _ctx.Orders.Include(o=> o.User).FirstOrDefault(o=> o.Id==orderId).User;
        }




        #endregion

        #region UserPanel
        public async Task<IEnumerable<Order>> GetUserOrderListAsync(int userId)
        {
            return await _ctx.Orders.Where(o=> o.UserId==userId).Include(o=> o.Wallet).ToListAsync();
        }

        public async Task<IEnumerable<OrderDetail>> GetUserOrderDetailsAsync(int orderId)
        {
            return await _ctx.OrderDetails
                .Include(od=> od.Product)
                .ThenInclude(p=> p.Discount)
                .Include(od=> od.Color)
                .Where(od=> od.OrderId==orderId)
                .ToListAsync();
        }
        #endregion


    }
}
