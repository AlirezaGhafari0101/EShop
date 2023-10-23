using EShop.Application.Services.Interfaces;
using EShop.Application.ViewModels.Order;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Areas.Admin.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet("Admin/order-list")]
        public async Task<IActionResult> Index()
        {
            IEnumerable<OrderVM> orderList = await _orderService.GetAllOrdersForAdminServiceAsync();
            return View(orderList);
        }


        [HttpGet("admin/order/order-details/{id}")]
        public async Task<IActionResult> ShowOrderDetails(int id)
        {

            IEnumerable<OrderDetailVM> orderDetails = await _orderService.GetOrderDetailsForAdminServiceAsync(id);
            ViewBag.OrderUser = _orderService.GetOrderUserService(id);
            return View(orderDetails);
        }

        [HttpGet("admin/order/delete-order/{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            await _orderService.DeleteOrderServiceAsync(id);
            return Json(new { status = "success" });
        }

    }
}
