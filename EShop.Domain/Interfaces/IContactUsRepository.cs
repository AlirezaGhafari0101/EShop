using EShop.Domain.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Interfaces
{
    public interface IContactUsRepository
    {
        Task CreateQuestionAsync(ContactUs contactUs);


        #region Admin
        Task<IEnumerable<ContactUs>> GetAllContactUsAsync();
        Task DeleteContactUsAsync(ContactUs contactUs);
        Task<ContactUs> GetContactUsByIdAsync(int id);
        Task UpdateContactUsAsync(ContactUs contactUs);
      
        #endregion
        Task SaveChangeAsync();
    }
}
