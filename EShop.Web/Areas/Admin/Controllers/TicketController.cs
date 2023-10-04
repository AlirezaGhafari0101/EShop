using EShop.Application.Services.Interfaces;
using EShop.Application.ViewModels.Ticket;

using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EShop.Web.Areas.Admin.Controllers
{
    public class TicketController : BaseController
    {
        private readonly ITicketService _ticketService;
        private readonly IUserService _userService;
        public TicketController(ITicketService ticketService, IUserService userService)
        {
            _ticketService = ticketService;
            _userService = userService;

        }
        [Route("admin/tickets")]
        public async Task<IActionResult> Index()
        {
            IEnumerable<TicketVM> ticketList = await _ticketService.GetAllTicketsByAdminAsyncService();
            return View(ticketList);
        }

        #region Ticket Detail

        [HttpGet("admin/ticket-detail/{id}")]
        public async Task<IActionResult> TicketDetail(int id)
        {

            ViewBag.TicketMessageList = await _ticketService.GetAllTicketMessagesAsyncService(id);
            return View();
        }

        [HttpPost("admin/ticket-detail/{id}")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> TicketDetail(int id, SendTicketMessageVM sendMessage)
        {
            ViewBag.TicketMessageList = await _ticketService.GetAllTicketMessagesAsyncService(id);

            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (ModelState.IsValid)
            {
                await _ticketService.SendTicketMessage(userId, sendMessage.Message, id);
                return RedirectToAction("TicketDetail", new { id });
            }

            return View(sendMessage);
        }
        #endregion



        #region Create Ticket
        [HttpGet("admin/create-ticket")]

        public async Task<IActionResult> CreateNewTicket()
        {
            ViewBag.UserList = await _userService.GetAllUsersAsync();
            return View();

        }

        [HttpPost("admin/create-ticket")]

        public async Task<IActionResult> CreateNewTicket(CreateTicketVM createTicket)
        {
            ViewBag.UserList = await _userService.GetAllUsersAsync();
            int senderId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (!ModelState.IsValid)
            {

                return View(createTicket);
            }
            if (createTicket.WichUser == 0)
            {
                ModelState.AddModelError("WichUser", "انتخاب کردن کاربر الزامی می باشد");
                return View(createTicket);
            }

            await _ticketService.CreateNewTicketByAdminServiceAsync(senderId, createTicket);
            return RedirectToAction("Index");




        }
        #endregion

        #region Delete Ticket
        [HttpGet("admin/delete-ticket/{id}")]

        public async Task<IActionResult> DeleteTicket(int id)
        {

            await _ticketService.DeleteTicket(id);
            return Json(new { status = "success" });

        }
        #endregion

        #region Closed Ticket
        [HttpGet("admin/closed-ticket/{id}")]

        public async Task<IActionResult> ClosedTicket(int id)
        {

            await _ticketService.ClosedTicket(id);
            return Json(new { status = "success" });

        }
        #endregion






    }
}
