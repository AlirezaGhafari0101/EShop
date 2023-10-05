using EShop.Domain.Models.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Interfaces
{
    public interface ITicketRepository
    {
        Task SaveChangeAsync();
        Task CreateTicketMessageAsync(TicketMessage ticketMessage);
        Task UpdateTicketAsync(Ticket ticket);
        Task<Ticket> GetTicketByTicketIdAsync(int id);
        #region Client Side
        Task<IEnumerable<Ticket>> GetAllUserTicketsByClientAsync(int id);
        Task CreateTicketByClientAsync(Ticket ticket);
        
        Task<IEnumerable<TicketMessage>> GetAllTicketMessagesByClientAsync(int id);
        
        
        #endregion



        #region Admin Side
        Task<IEnumerable<Ticket>> GetAllTicketsByAdminAsync();
     
        

        #endregion
    }
}
