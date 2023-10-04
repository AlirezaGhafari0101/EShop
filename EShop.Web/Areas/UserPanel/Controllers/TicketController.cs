using EShop.Application.Services.Interfaces;
using EShop.Application.ViewModels.Ticket;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EShop.Web.Areas.UserPanel.Controllers
{
    public class TicketController : BaseController
    {

        private readonly ITicketService _ticketService;
        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [Route("userpanel/tickets")]
        public async Task<IActionResult> Index()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            IEnumerable<TicketVM> ticketsUser = await _ticketService.GetAllUserTicketsByClient(userId);
            return View(ticketsUser);
        }

        #region Create
        [HttpGet("userpanel/create-ticket")]
        public async Task<IActionResult> CreateTicket()
        {

            return View();
        }
        [HttpPost("userpanel/create-ticket")]
        public async Task<IActionResult> CreateTicket(CreateTicketVM createTicket)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));


            if (!ModelState.IsValid)
            {

                return View(createTicket);
            }
            if (createTicket.TicketSection == 0)
            {
                ModelState.AddModelError("TicketSection", "بخش تیکت را مشخص کنید");
                return View(createTicket);
            }
            if (createTicket.TicketPriority == 0)
            {
                ModelState.AddModelError("TicketPriority", "اولویت تیکت را مشخص کنید");
                return View(createTicket);

            }


            await _ticketService.CreateNewTicketByClient(userId, createTicket);
            return RedirectToAction("Index");



        }
        #endregion

        #region Detail
        [HttpGet("userpanel/ticket-detail/{id}")]
        public async Task<IActionResult> TicketDetail(int id)
        {

            ViewBag.TicketMessageList = await _ticketService.GetAllTicketMessagesAsyncService(id);
            return View();
        }

        [HttpPost("userpanel/ticket-detail/{id}")]
        public async Task<IActionResult> TicketDetail(int id,SendTicketMessageVM ticketMessage)
        {
            ViewBag.TicketMessageList = await _ticketService.GetAllTicketMessagesAsyncService(id);

            if (ModelState.IsValid)
            {
                int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                await _ticketService.SendTicketMessage(userId, ticketMessage.Message, id);
                return RedirectToAction("TicketDetail", new {id});
            }
            return View(ticketMessage);
        }
        #endregion
    }
}
