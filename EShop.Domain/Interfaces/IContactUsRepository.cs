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
        Task SaveChangeAsync();
    }
}
