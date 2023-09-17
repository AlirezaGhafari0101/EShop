using EShop.Application.Convertors;
using EShop.Application.Senders;
using EShop.Application.Services.Interfaces;
using EShop.Application.ViewModels.ContactUs;
using EShop.Domain.Interfaces;
using EShop.Domain.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactUsController : Controller
    {

        private readonly IContactUsService _contactUsService;
        private readonly IViewRenderService _viewRender;

        public ContactUsController(IContactUsService contactUsService,IViewRenderService viewRender)
        {
            _contactUsService = contactUsService;
            _viewRender = viewRender;
        }
        
        public async Task<IActionResult> Index(bool IsSendEmail)
        {
            ViewBag.IsSendEmail = IsSendEmail;
            return View(await _contactUsService.GetAllContactUsServiceAsync());
        }


        [HttpGet]
        public async Task<IActionResult> SendAnswer(int id)
        {
            ViewBag.ContactUsId=id;
            return View(await _contactUsService.GetContaUsInfoForSendAnswer(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendAnswer(SendAnswerContactUsViewModel sendAnswer,int contausId)
        {
            if (!ModelState.IsValid) { return  View(sendAnswer); }

           EmailSendAnswerContactUsViewModel emailInfro= await _contactUsService.UpdateContactUsServiceAsync(sendAnswer, contausId);

            string body = _viewRender.RenderToStringAsync("_EmailSendAnswerContactUs", emailInfro);
            SendEmail.Send(emailInfro.Email, "تماس باما", body);
            bool IsSendEmail = true;

            return RedirectToAction("Index",new{ IsSendEmail});
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
           await _contactUsService.DeleteContactUsServiceAsyc(id);
            return Json(new { status = "success" });
        }

    }
}
