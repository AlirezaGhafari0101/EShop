using EShop.Application.ViewModels.Ticket;
using EShop.Domain.Models.Users.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Services.Interfaces
{
    public interface ITicketService
    {
        Task SendTicketMessage(int senderId, string message, int ticketId);
        #region ClientSide
        Task<IEnumerable<TicketVM>> GetAllUserTicketsByClient(int userId);
        Task CreateNewTicketByClient(int userId, CreateTicketVM create);
        Task<TicketDetailVM> GetAllTicketMessagesAsyncService(int ticketid);

        
        #endregion


        #region Admin Side
        Task<IEnumerable<TicketVM>> GetAllTicketsByAdminAsyncService();
        Task CreateNewTicketByAdminServiceAsync(int senderId, CreateTicketVM create);
        Task DeleteTicket(int ticketId);
        Task ClosedTicket(int ticketId);


        #endregion
    }
}
