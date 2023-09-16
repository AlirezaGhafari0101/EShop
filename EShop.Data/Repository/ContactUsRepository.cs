using EShop.Data.Context;
using EShop.Domain.Interfaces;
using EShop.Domain.Models.Home;

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

        public async Task SaveChangeAsync()
        {
            await _ctx.SaveChangesAsync();
        }
    }
}
