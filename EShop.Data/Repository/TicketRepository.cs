using EShop.Data.Context;
using EShop.Domain.Interfaces;
using EShop.Domain.Models.Users.Ticket;
using Microsoft.EntityFrameworkCore;

namespace EShop.Data.Repository
{
    public class TicketRepository : ITicketRepository
    {
        private readonly EshopDBContext _ctx;

        public TicketRepository(EshopDBContext ctx)
        {
            _ctx = ctx;
        }

        public async Task SaveChangeAsync()
        {
            await _ctx.SaveChangesAsync();
        }

        #region Ticket
        public async Task<IEnumerable<Ticket>> GetAllUserTicketsByClientAsync(int id)
        {
            return await _ctx.Tickets.Where(w => w.UserId == id).Include(w => w.TicketMessages).ToListAsync();
        }

        public async Task CreateTicketByClientAsync(Ticket ticket)
        {
            await _ctx.AddAsync(ticket);
        }
        public async Task CreateTicketMessageAsync(TicketMessage ticketMessage)
        {
            await _ctx.AddAsync(ticketMessage);
        }

        public async Task<IEnumerable<TicketMessage>> GetAllTicketMessagesByClientAsync(int id)
        {
            return await _ctx.TicketMessages.Where(tm => tm.TicketId == id).Include(tm=> tm.User).ToListAsync();
        }

        public async Task<Ticket> GetTicketByTicketIdAsync(int id)
        {
            return await _ctx.Tickets.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task UpdateTicketAsync(Ticket ticket)
        {
            _ctx.Tickets.Update(ticket);
        }

        #region Admin side
        public async Task<IEnumerable<Ticket>> GetAllTicketsByAdminAsync()
        {
            return await _ctx.Tickets.Include(t => t.User).ToListAsync();
        }

      
        #endregion
        #endregion
    }
}
