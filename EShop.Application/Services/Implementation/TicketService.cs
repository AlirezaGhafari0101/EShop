using EShop.Application.Services.Interfaces;
using EShop.Application.ViewModels.Ticket;
using EShop.Domain.Interfaces;
using EShop.Domain.Models.Users.Ticket;

namespace EShop.Application.Services.Implementation
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }


        #region Ticket

        public async Task<IEnumerable<TicketVM>> GetAllUserTicketsByClient(int userId)
        {
            var tickets = await _ticketRepository.GetAllUserTicketsByClientAsync(userId);

            return tickets.Select(t => new TicketVM()
            {
                Answered = t.Answered,
                Pending = t.Pending,
                Closed = t.Closed,
                TicketMessages = t.TicketMessages,
                Title = t.Title,
                CreateDate = t.CreateDate,
                UpdateDate = t.UpdateDate,
                Id = t.Id,
            }).ToList();

        }

        public async Task CreateNewTicketByClient(int userId, CreateTicketVM create)
        {
            Ticket newTicket = new Ticket()
            {
                UserId = create.WichUser,
                Pending = true,
                Answered = false,
                Title = create.Title,
                TicketPriority=create.TicketPriority,
                TicketSection=create.TicketSection,
                UpdateDate=DateTime.Now

            };
            await _ticketRepository.CreateTicketByClientAsync(newTicket);
            await _ticketRepository.SaveChangeAsync();

            TicketMessage newTicketMessage = new TicketMessage()
            {
                Message = create.Message,
                TicketId = newTicket.Id,
                SenderId = create.WichUser,
                CreateDate = DateTime.Now


            };


            await _ticketRepository.CreateTicketMessageAsync(newTicketMessage);
            await _ticketRepository.SaveChangeAsync();

        }

        public async Task<TicketDetailVM> GetAllTicketMessagesAsyncService(int ticketid)
        {
            IEnumerable<TicketMessage> ticketMessages = await _ticketRepository.GetAllTicketMessagesByClientAsync(ticketid);
            Ticket ticket = await _ticketRepository.GetTicketByTicketIdAsync(ticketid);

            return new TicketDetailVM()
            {

                Messages = ticketMessages,
                Closed = ticket.Closed




            };
        }

        public async Task SendTicketMessage(int senderId, string message, int ticketId)
        {

            TicketMessage ticketMessage = new TicketMessage()
            {
                SenderId = senderId,
                Message = message,
                TicketId = ticketId,

            };

            Ticket ticket = await _ticketRepository.GetTicketByTicketIdAsync(ticketId);
            ticket.UpdateDate = DateTime.Now;
            await _ticketRepository.UpdateTicketAsync(ticket);
            await _ticketRepository.CreateTicketMessageAsync(ticketMessage);
            await _ticketRepository.SaveChangeAsync();
        }

        #region Admin side
        public async Task<IEnumerable<TicketVM>> GetAllTicketsByAdminAsyncService()
        {
            IEnumerable<Ticket> tickets = await _ticketRepository.GetAllTicketsByAdminAsync();

            return tickets.Select(t => new TicketVM()
            {
                Title = t.Title,
                Answered = t.Answered,
                Closed = t.Closed,
                CreateDate = t.CreateDate,
                Id = t.Id,
                Pending = t.Pending,
                UpdateDate = DateTime.Now,



            }).ToList();
        }

        public async Task CreateNewTicketByAdminServiceAsync(int senderId, CreateTicketVM create)
        {
            Ticket newTicket = new Ticket()
            {
                UserId = create.WichUser,
                Pending = true,
                Answered = true,
                Closed= false,
                Title = create.Title,
                TicketPriority = create.TicketPriority,
                TicketSection = create.TicketSection,
                UpdateDate = DateTime.Now,
                
                

            };
            await _ticketRepository.CreateTicketByClientAsync(newTicket);
            await _ticketRepository.SaveChangeAsync();

            TicketMessage newTicketMessage = new TicketMessage()
            {
                Message = create.Message,
                TicketId = newTicket.Id,
                SenderId = senderId,
                CreateDate = DateTime.Now


            };


            await _ticketRepository.CreateTicketMessageAsync(newTicketMessage);
            await _ticketRepository.SaveChangeAsync();
        }

        public async Task DeleteTicket(int ticketId)
        {
            IEnumerable <TicketMessage> ticketMessages=await _ticketRepository.GetAllTicketMessagesByClientAsync(ticketId);
            Ticket ticket=await _ticketRepository.GetTicketByTicketIdAsync(ticketId);

            foreach (TicketMessage message in ticketMessages)
            {
                message.IsDelete = true;
            }
            await _ticketRepository.SaveChangeAsync();
            ticket.IsDelete = true;
            await _ticketRepository.SaveChangeAsync();


        }

        public async Task ClosedTicket(int ticketId)
        {
            Ticket ticket = await _ticketRepository.GetTicketByTicketIdAsync(ticketId);
            ticket.Closed = true;
            await _ticketRepository.SaveChangeAsync();
        }
        #endregion


        #endregion
    }
}
