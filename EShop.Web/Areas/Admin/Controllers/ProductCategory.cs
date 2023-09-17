using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Areas.Admin.Controllers
{
    public class ProductCategory : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
