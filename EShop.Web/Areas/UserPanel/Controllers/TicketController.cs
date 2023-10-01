using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Areas.UserPanel.Controllers
{
    public class TicketController : BaseController
    {

        [Route("userpanel/tickets")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
