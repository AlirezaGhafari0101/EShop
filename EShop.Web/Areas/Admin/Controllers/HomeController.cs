

using EShop.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize]
  
    public class HomeController : BaseController
    {
      

        public IActionResult Index()
        {
            return View();
        }

    

   
    }
}
