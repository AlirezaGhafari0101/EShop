using EShop.Data.Context;
using EShop.Domain.Interfaces;
using EShop.Domain.Models.Home;
using Microsoft.EntityFrameworkCore;

namespace EShop.Data.Repository
{
    public class ContactUsRepository : IContactUsRepository
    {
        private readonly EshopDBContext _ctx;

        public ContactUsRepository(EshopDBContext ctx)
        {
            _ctx = ctx;
        }
        public async Task CreateQuestionAsync(ContactUs contactUs)
        {
            await _ctx.ContactUs.AddAsync(contactUs);
        }

        public async Task DeleteContactUsAsync(ContactUs contactUs)
        {
                _ctx.ContactUs.Remove(contactUs);
        }

        public async Task<IEnumerable<ContactUs>> GetAllContactUsAsync()
        {
            return await _ctx.ContactUs.ToListAsync();
        }

        public async Task<ContactUs> GetContactUsByIdAsync(int id)
        {
            return await _ctx.ContactUs.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task SaveChangeAsync()
        {
            await _ctx.SaveChangesAsync();
        }

        public async Task UpdateContactUsAsync(ContactUs contactUs)
        {
           _ctx.Update(contactUs);
        }
    }
}
