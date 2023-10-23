using EShop.Application.Extensions;
using EShop.Application.Services.Interfaces;
using EShop.Application.ViewModels.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace EShop.Web.Components
{
    [Authorize]

    public class UserOptionsComponent : ViewComponent
    {

        private readonly IOrderService _orderService;
        
        public UserOptionsComponent(IOrderService orderService)
        {
            _orderService = orderService;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            int usaerId = User.GetrUserId();

            OrderVM userOrderInfor = await _orderService.ShowUserOrderInforServiceAsync(usaerId);
  


            return View("/Views/Components/UserOptionsComponent.cshtml",userOrderInfor);
        }
    }
}
