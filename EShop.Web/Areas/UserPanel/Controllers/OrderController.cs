using EShop.Application.Extensions;
using EShop.Application.Services.Interfaces;
using EShop.Application.ViewModels.Order;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Areas.UserPanel.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;

        }
        [HttpGet("userpanel/order/order-list")]
        public async Task<IActionResult> Index()
        {
            int userId = User.GetrUserId();
            IEnumerable<OrderVM> orderList = await _orderService.GetUserOrderListServiceAsync(userId);
            return View(orderList);
        }

        [HttpGet("userpanel/order/order-details/{orderId}")]
        public async Task<IActionResult> ShowOrderDetails(int orderId)
        {
            IEnumerable<OrderDetailVM> orderDetails = await _orderService.GetUserOrderDetailsServiceAsync(orderId);
            ViewBag.OrderInfor=await _orderService.GetOrderByIdServiceAsync(orderId);
            return View(orderDetails);
        }
    }
}
